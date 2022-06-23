using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;

namespace Pharmm.API.Services.Transaksi
{
    public interface ITransPemusnahanStokService
    {
        #region Header

        Task<tr_pemusnahan_stok> GetTrPemusnahanStokById(long pemusnahan_stok_id);
        Task<List<tr_pemusnahan_stok>> GetAllTrPemusnahanStokByParams(List<ParameterSearchModel> param);

        Task<(bool, long, string)> AddTrPemusnahanStok(tr_pemusnahan_stok_insert data);
        Task<(bool, long, string)> UpdateToValidated(tr_pemusnahan_stok_update_to_validated data);
        Task<(bool, long, string)> UpdateToCanceled(tr_pemusnahan_stok_update_to_canceled data);

        #endregion


        #region Detail

        Task<List<tr_pemusnahan_stok_detail>> GetAllTrPemusnahanStokDetailByParams(List<ParameterSearchModel> param);
        Task<List<tr_pemusnahan_stok_detail>> GetTrPemusnahanStokDetailByHeaderId(long _pemusnahan_stok_id);

        #endregion
    }

    public class TransPemusnahanStokService : ITransPemusnahanStokService
    {

        private readonly SQLConn _db;
        private readonly TransPemusnahanStokDao _dao;

        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly SetupItemDao _itemDao;
        private readonly KartuStokItemDao _kartuStokDao;

        public TransPemusnahanStokService(
            SQLConn db,
            TransPemusnahanStokDao dao,
            SetupStokItemDao stokItemDao,
            MasterCounterDao kodeDao,
            SetupItemDao itemDao,
            KartuStokItemDao kartuStokDao

            )
        {
            this._db = db;
            this._dao = dao;

            this._stokItemDao = stokItemDao;
            this._itemDao = itemDao;
            this._kartuStokDao = kartuStokDao;
            this._kodeDao = kodeDao;
        }

        #region Header

        public async Task<tr_pemusnahan_stok> GetTrPemusnahanStokById(long pemusnahan_stok_id)
        {
            try
            {
                return await this._dao.GetTrPemusnahanStokById(pemusnahan_stok_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_pemusnahan_stok>> GetAllTrPemusnahanStokByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemusnahanStokByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<(bool, long, string)> AddTrPemusnahanStok(tr_pemusnahan_stok_insert data)
        {

            this._db.beginTransaction();
            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePemusnahanStok,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor kontrak
                var noRef = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noRef))
                {
                    data.nomor_pemusnahan_stok = noRef;
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor pemusnahan_stok");
                }

                var headerId = await this._dao.AddTrPemusnahanStok(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.pemusnahan_stok_id = headerId;
                            var detailId = await this._dao.AddTrPemusnahanStokDetail(detail);

                            if (detailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal menambahkan detail pemusnahan stok dengan nomor urut {detail.no_urut}");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor pemusnahan stok");
                    }
                }

                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool,long,string)> UpdateToValidated(tr_pemusnahan_stok_update_to_validated data)
        {

            this._db.beginTransaction();
            
            try
            {
                var id_stockroom = (short)0;

                var pemusnahanData = await this._dao.GetTrPemusnahanStokByIdWithLock(data.pemusnahan_stok_id);

                if (pemusnahanData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data pemusnahan stok tidak ditemukan");
                }
                else
                {
                    id_stockroom = pemusnahanData.id_stockroom;

                    if (pemusnahanData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (pemusnahanData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan ");
                    }
                }

                var validasi = await this._dao.UpdateToValidated(data);


                if(validasi > 0)
                {
                    var pemusnahanDetail = await this._dao.GetTrPemusnahanStokDetailByHeaderIdWithLock(data.pemusnahan_stok_id);

                    //rekap pemusnahan stok untuk di summary ke header
                    var rekapForHeader = pemusnahanDetail.GroupBy(q => q.id_item).Select(res =>
                    new
                    {
                        pemusnahan_stok_id = res.FirstOrDefault().pemusnahan_stok_id,
                        id_item = res.FirstOrDefault().id_item,
                        id_stockroom = id_stockroom,
                        qty_on_hand = res.Sum(s => s.qty),
                        nominal = res.Sum(n => n.sub_total),
                        details = pemusnahanDetail.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
                    }).ToList();

                    if (rekapForHeader.Count > 0)
                    {
                        foreach (var item in rekapForHeader)
                        {
                            //get data barang dan gudang
                            var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomWithLock(item.id_item, id_stockroom);

                            //get stok akhir kartu stok
                            var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                item.id_stockroom,
                                item.id_item
                                );

                            #region Stok Item Header

                            //get stok header
                            var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                item.id_item,
                                id_stockroom
                                );

                            if (cekStokItem is null)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Barang tidak tersedia untuk {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang?.nama_stockroom}");   
                            }
                            else
                            {
                                if ((cekStokItem.qty_on_hand - item.qty_on_hand) < 0)
                                {

                                    this._db.rollBackTrans();
                                    return (false, 0, $"Sisa stok untuk item {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang?.nama_stockroom} tidak mencukupi ");
                                }


                                #region Update Stok Item Header

                                //parameter untuk update stok header
                                var stokItemParam = new mm_setup_stok_item_update_stok
                                {
                                    id_item = item.id_item,
                                    id_stockroom = id_stockroom,
                                    qty_on_hand = item.qty_on_hand
                                };

                                var updateStokHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemParam);

                                if (updateStokHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, $"Gagal mengurangi stok header untuk barang {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang?.nama_stockroom}");

                                }

                                #endregion
                            }

                            #endregion

                            #region Insert Kartu Stok Header

                            //parameter untuk mengurangi kartu stok
                            var kartuStokParam = new mm_kartu_stok_item_insert_pengurangan_stok
                            {
                                id_detail_transaksi = null,
                                id_header_transaksi = data.pemusnahan_stok_id,
                                id_item = item.id_item,
                                id_stockroom = item.id_stockroom, //gudang farmasi
                                keterangan = "KELUAR PEMUSNAHAN STOK",
                                stok_awal = cekStokAkhir?.stok_akhir,
                                stok_keluar = item.qty_on_hand,
                                nominal_keluar = item.nominal,
                                nominal_awal = cekStokAkhir?.nominal_akhir,
                                nomor_ref_transaksi = pemusnahanData.nomor_pemusnahan_stok,
                                user_inputed = data.user_validated
                            };

                            var insertKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokParam);

                            if (insertKartuStok <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false,0, $"Gagal mengurangi kartu stok header untuk barang {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang?.nama_stockroom}");
                            }
                            else
                            {
                                if (item.details.Count > 0)
                                {
                                    foreach (var detailPemusnahan in item.details)
                                    {
                                        //get data barang dan gudang
                                        var dataBarangAndGudangDetail = await this._stokItemDao.GetDataBarangAndStockroomWithLock(detailPemusnahan.id_item, id_stockroom);


                                        #region Stok Item Detail

                                        //get stok detail
                                        var cekStokItemDetail = await this._stokItemDao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumberWithLock(
                                            detailPemusnahan.id_item,
                                            id_stockroom,
                                            detailPemusnahan.batch_number
                                            );

                                        if (cekStokItemDetail is null)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, 0, $"Barang {dataBarangAndGudangDetail?.nama_item} tidak ditemukan di gudang {dataBarangAndGudangDetail?.nama_stockroom}");
                                        }
                                        else
                                        {
                                            if ((cekStokItemDetail.qty_on_hand - detailPemusnahan.qty) < 0)
                                            {

                                                this._db.rollBackTrans();
                                                return (false,0, $"Sisa stok untuk item {dataBarangAndGudangDetail?.nama_item} di gudang {dataBarangAndGudangDetail?.nama_stockroom} tidak mencukupi ");
                                            }

                                            #region Update Stok Detail

                                            //parameter untuk update stok detail
                                            var stokItemDetailParam = new mm_setup_stok_item_detail_update_pengurangan_stok
                                            {
                                                id_item = detailPemusnahan.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = detailPemusnahan.qty,
                                                batch_number = detailPemusnahan.batch_number,
                                                expired_date = detailPemusnahan.expired_date,
                                            };

                                            var updateStokDetail = await this._stokItemDao.UpdatePenguranganStokDetailBatch(stokItemDetailParam);

                                            if (updateStokDetail <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, 0, $"Gagal mengurangi stok detail untuk barang {dataBarangAndGudangDetail?.nama_item} di gudnag {dataBarangAndGudangDetail?.nama_stockroom}");
                                            }

                                            #endregion
                                        }

                                        #endregion

                                        //get stok akhir kartu stok detail
                                        var cekStokAkhirDetail = await this._kartuStokDao.GetStokAkhirDetailBatchWithLock(
                                            detailPemusnahan.batch_number,
                                            detailPemusnahan.expired_date,
                                            id_stockroom,
                                            detailPemusnahan.id_item
                                            );

                                        #region Input Kartu Stok Detail Batch

                                        //jika input kartu stok
                                        if (insertKartuStok > 0)
                                        {
                                            //parameter untuk insert kartu stok detail
                                            var kartuStokDetailParam = new mm_kartu_stok_item_detail_batch_insert_pengurangan_stok
                                            {
                                                batch_number = detailPemusnahan.batch_number,
                                                expired_date = detailPemusnahan.expired_date,
                                                id_detail_transaksi = detailPemusnahan.pemusnahan_stok_detail_id,
                                                id_header_transaksi = detailPemusnahan.pemusnahan_stok_id,
                                                id_kartu_stok_item = insertKartuStok,
                                                nominal_awal = cekStokAkhirDetail?.nominal_akhir,
                                                stok_awal = cekStokAkhirDetail?.stok_akhir,
                                                nominal_keluar = detailPemusnahan.sub_total,
                                                stok_keluar = detailPemusnahan.qty
                                            };

                                            //untuk insert kartu stok detail batch
                                            var insertKartuStokDetail = await this._kartuStokDao.AddPenguranganMmKartuStokItemDetailBatch(kartuStokDetailParam);

                                            if (insertKartuStokDetail <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false,0, $"Gagal mengurangi ke kartu stok detail batch untuk barang {dataBarangAndGudangDetail?.nama_item} di gudang {dataBarangAndGudangDetail?.nama_stockroom}");

                                            }
                                        }

                                        #endregion
                                    }
                                }
                            }

                            #endregion

                        }
                    }
                }

                this._db.commitTrans();
                return (true, validasi, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> UpdateToCanceled(tr_pemusnahan_stok_update_to_canceled data)
        {

            this._db.beginTransaction();

            try
            {
                var pemusnahanData = await this._dao.GetTrPemusnahanStokByIdWithLock(data.pemusnahan_stok_id);

                if (pemusnahanData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data pemusnahan stok tidak ditemukan");
                }
                else
                {
                    if (pemusnahanData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (pemusnahanData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan ");
                    }
                }

                var batal = await this._dao.UpdateToCanceled(data);

                this._db.commitTrans();
                return (true, batal, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail

        public async Task<List<tr_pemusnahan_stok_detail>> GetAllTrPemusnahanStokDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemusnahanStokDetailByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemusnahan_stok_detail>> GetTrPemusnahanStokDetailByHeaderId(long pemusnahan_stok_id)
        {
            try
            {
                return await this._dao.GetTrPemusnahanStokDetailByHeaderId(pemusnahan_stok_id);
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
