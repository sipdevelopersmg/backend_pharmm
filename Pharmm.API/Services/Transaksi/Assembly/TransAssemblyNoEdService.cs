using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services
{
    public interface ITransAssemblyNoEdService
    {
        #region Header

        Task<tr_assembly_no_ed> GetTrAssemblyById(long assembly_id);
        Task<List<tr_assembly_no_ed>> GetAllTrAssemblyByParams(List<ParameterSearchModel> param);

        Task<(bool, long, string)> AddTrAssembly(tr_assembly_no_ed_insert data);

        Task<(bool, long, string)> UpdateTrAssemblyValidated(tr_assembly_no_ed_update_to_validated data);
        Task<(bool, long, string)> UpdateTrAssemblyCanceled(tr_assembly_no_ed_update_to_canceled data);

        #endregion

        #region Detail

        Task<List<tr_assembly_no_ed_detail>> GetTrAssemblyDetailByHeaderId(long assembly_id);

        #endregion
    }


    public class TransAssemblyNoEdService : ITransAssemblyNoEdService
    {

        private readonly SQLConn _db;

        private readonly MasterCounterDao _kodeDao;
        private readonly TransAssemblyNoEdDao _dao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly SetupItemDao _itemDao;
        private readonly KartuStokItemDao _kartuStokDao;

        public TransAssemblyNoEdService(SQLConn db, TransAssemblyNoEdDao dao,
            SetupStokItemDao stokItemDao,
            MasterCounterDao kodeDao,
            SetupItemDao itemDao,
            KartuStokItemDao kartuStokDao)
        {
            this._db = db;
            this._dao = dao;
            this._stokItemDao = stokItemDao;
            this._itemDao = itemDao;
            this._kartuStokDao = kartuStokDao;
            this._kodeDao = kodeDao;
        }

        #region Header

        public async Task<(bool, long, string)> AddTrAssembly(tr_assembly_no_ed_insert data)
        {

            this._db.beginTransaction();
            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeAssembly,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor kontrak
                var noAssembly = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noAssembly))
                {
                    data.nomor_assembly = noAssembly;
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor assembly");
                }

                var headerId = await this._dao.AddTrAssembly(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.assembly_id = headerId;
                            var detailId = await this._dao.AddTrAssemblyDetail(detail);

                            if (detailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal menambahkan assembly detail dengan nomor urut {detail.no_urut}");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        throw new Exception("Gagal update nomor assembly");
                    }
                }

                this._db.commitTrans();
                return (true, headerId, "SUCCESS");

            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<tr_assembly_no_ed> GetTrAssemblyById(long assembly_id)
        {
            try
            {
                return await this._dao.GetTrAssemblyById(assembly_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_assembly_no_ed>> GetAllTrAssemblyByParams(List<ParameterSearchModel> param)
        {
            try
            {

                return await this._dao.GetAllTrAssemblyByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, long, string)> UpdateTrAssemblyValidated(tr_assembly_no_ed_update_to_validated data)
        {

            this._db.beginTransaction();
            try
            {

                var assemblyData = await this._dao.GetTrAssemblyByIdWithLock(data.assembly_id);

                if (assemblyData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data assembly tidak ditemukan");
                }
                else
                {
                    if (assemblyData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (assemblyData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan ");
                    }

                }

                var validasi = await this._dao.UpdateTrAssemblyValidated(data);

                if (validasi > 0)
                {

                    //get data barang dan gudang
                    var dataBarangAndGudangPengurangan = await this._stokItemDao.GetDataBarangAndStockroomWithLock(assemblyData.id_item, assemblyData.id_stockroom);

                    //get stok akhir kartu stok
                    var cekStokAkhirForPenambahanHeader = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                        assemblyData.id_stockroom,
                        assemblyData.id_item
                        );

                    #region Update Stok Item Header (Pengurangan stok dari Assembly Item Header)

                    //parameter untuk update stok header
                    var stokItemHeaderParam = new mm_setup_stok_item_update_stok
                    {
                        id_item = assemblyData.id_item,
                        id_stockroom = assemblyData.id_stockroom,
                        qty_on_hand = assemblyData.qty
                    };

                    var updateStokHeaderAssembly = await this._stokItemDao.UpdatePenambahanStok(stokItemHeaderParam);

                    if (updateStokHeaderAssembly <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Gagal menambah stok header");
                    }

                    //parameter untuk menambah kartu stok dari barang hasil assembly
                    var kartuStokPenambahanParam = new mm_kartu_stok_item_insert_penambahan_stok
                    {
                        id_detail_transaksi = null,
                        id_header_transaksi = data.assembly_id,
                        id_item = assemblyData.id_item,
                        id_stockroom = assemblyData.id_stockroom, //gudang farmasi
                        keterangan = "MASUK ASSEMBLY BARANG TANPA ED",
                        stok_awal = cekStokAkhirForPenambahanHeader?.stok_akhir,
                        stok_masuk = assemblyData.qty,
                        nominal_masuk = assemblyData.total_nominal,
                        nominal_awal = cekStokAkhirForPenambahanHeader?.nominal_akhir,
                        nomor_ref_transaksi = assemblyData.nomor_assembly,
                        user_inputed = data.user_validated
                    };

                    var insertPenambahanKartuStok = await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokPenambahanParam);

                    if (insertPenambahanKartuStok <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Gagal menambah kartu stok header");
                    }

                    #endregion

                    var assemblyDetail = await this._dao.GetTrAssemblyDetailByHeaderIdWithLock(data.assembly_id);

                    //rekap assembly untuk di summary ke header
                    var rekapForHeader = assemblyDetail.GroupBy(q => q.id_item_child).Select(res =>
                    new
                    {
                        assembly_id = res.FirstOrDefault().assembly_id,
                        id_item = res.FirstOrDefault().id_item_child,
                        id_stockroom = assemblyData.id_stockroom,
                        qty_on_hand = res.Sum(s => s.qty),
                        nominal = res.Sum(n => n.sub_total),
                        details = assemblyDetail.Where(x => x.id_item_child == res.FirstOrDefault().id_item_child).ToList()
                    }).ToList();

                    if (rekapForHeader.Count > 0)
                    {
                        foreach (var item in rekapForHeader)
                        {
                            #region STOK

                            var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                assemblyData.id_stockroom,
                                item.id_item
                                );

                            //get stok header
                            var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                item.id_item,
                                item.id_stockroom
                                );

                            //get data barang dan gudang
                            var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomWithLock(item.id_item, item.id_stockroom);

                            //jika stok tidak ada ditabel
                            if (cekStokItem is null)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Barang {dataBarangAndGudang?.nama_item} tidak ada di gudang {dataBarangAndGudang?.nama_stockroom} ");
                            

                            }
                            else
                            {
                                #region Update Stok Item Header (Assembly Urai)

                                //parameter untuk update stok header
                                var stokItemParam = new mm_setup_stok_item_update_stok
                                {
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom,
                                    qty_on_hand = item.qty_on_hand
                                };

                                if ((cekStokItem.qty_on_hand - item.qty_on_hand) < 0)
                                {

                                    this._db.rollBackTrans();
                                    return (false, 0, $"Sisa stok untuk item {dataBarangAndGudangPengurangan?.nama_item} di gudang {dataBarangAndGudangPengurangan?.nama_stockroom} tidak mencukupi ");
                                }

                                var updateStokHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemParam);

                                if (updateStokHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, $"Gagal mengurangi stok {dataBarangAndGudang?.nama_item} dari gudang {dataBarangAndGudang?.nama_stockroom}");
                                }

                                #endregion
                            }

                            //parameter untuk menambah kartu stok
                            var kartuStokMasukParam = new mm_kartu_stok_item_insert_pengurangan_stok
                            {
                                id_detail_transaksi = null,
                                id_header_transaksi = data.assembly_id,
                                id_item = item.id_item,
                                id_stockroom = item.id_stockroom, //gudang farmasi
                                keterangan = "KELUAR ASSEMBLY BARANG TANPA ED",
                                stok_awal = cekStokAkhir?.stok_akhir,
                                stok_keluar = item.qty_on_hand,
                                nominal_keluar = item.nominal,
                                nominal_awal = cekStokAkhir?.nominal_akhir,
                                nomor_ref_transaksi = assemblyData.nomor_assembly,
                                user_inputed = data.user_validated
                            };

                            var insertKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokMasukParam);

                            if (insertKartuStok <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal mengurangi kartu stok {dataBarangAndGudang?.nama_item} dari gudang {dataBarangAndGudang?.nama_stockroom}");
                            }

                            #endregion
                        }
                    }
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal validasi assembly");
                }

                this._db.commitTrans();
                return (true, validasi, $"SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> UpdateTrAssemblyCanceled(tr_assembly_no_ed_update_to_canceled data)
        {
            this._db.beginTransaction();
            try
            {
                var assemblyData = await this._dao.GetTrAssemblyByIdWithLock(data.assembly_id);

                if (assemblyData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data assembly tidak ditemukan");
                }
                else
                {
                    if (assemblyData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (assemblyData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan");
                    }

                }

                var validasi = await this._dao.UpdateTrAssemblyCanceled(data);

                this._db.commitTrans();
                return (true, validasi, $"SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        #endregion

        #region Detail



        public async Task<List<tr_assembly_no_ed_detail>> GetTrAssemblyDetailByHeaderId(long assembly_id)
        {
            try
            {
                return await this._dao.GetTrAssemblyDetailByHeaderId(assembly_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        #endregion
    }
}
