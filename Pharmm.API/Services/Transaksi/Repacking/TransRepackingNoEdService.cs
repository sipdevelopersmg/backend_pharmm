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
    public interface ITransRepackingNoEdService
    {
        #region Header

        Task<tr_repacking_no_ed> GetTrRepackingById(long repacking_id);
        Task<List<tr_repacking_no_ed>> GetAllTrRepackingByParams(List<ParameterSearchModel> param);

        Task<(bool, long, string)> AddTrRepacking(tr_repacking_no_ed_insert data);

        Task<(bool, long, string)> UpdateTrRepackingValidated(tr_repacking_no_ed_update_to_validated data);
        Task<(bool, long, string)> UpdateTrRepackingCanceled(tr_repacking_no_ed_update_to_canceled data);

        #endregion

        #region Detail

        Task<List<tr_repacking_no_ed_detail>> GetTrRepackingDetailByHeaderId(long repacking_id);

        #endregion
    }


    public class TransRepackingNoEdService : ITransRepackingNoEdService
    {

        private readonly SQLConn _db;

        private readonly MasterCounterDao _kodeDao;
        private readonly TransRepackingNoEdDao _dao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly SetupItemDao _itemDao;
        private readonly KartuStokItemDao _kartuStokDao;

        public TransRepackingNoEdService(SQLConn db, TransRepackingNoEdDao dao,
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

        public async Task<tr_repacking_no_ed> GetTrRepackingById(long repacking_id)
        {
            try
            {
                return await this._dao.GetTrRepackingById(repacking_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> AddTrRepacking(tr_repacking_no_ed_insert data)
        {

            this._db.beginTransaction();
            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeRepacking,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor kontrak
                var noRepacking = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noRepacking))
                {
                    data.nomor_repacking = noRepacking;
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor repacking");
                }

                var headerId = await this._dao.AddTrRepacking(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.repacking_id = headerId;
                            var detailId = await this._dao.AddTrRepackingDetail(detail);

                            if (detailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal menambahkan repacking detail dengan nomor urut {detail.no_urut}");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        throw new Exception("Gagal update nomor repacking");
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

        public async Task<List<tr_repacking_no_ed>> GetAllTrRepackingByParams(List<ParameterSearchModel> param)
        {
            try
            {

                return await this._dao.GetAllTrRepackingByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, long, string)> UpdateTrRepackingValidated(tr_repacking_no_ed_update_to_validated data)
        {

            this._db.beginTransaction();
            try
            {

                var repackingData = await this._dao.GetTrRepackingByIdWithLock(data.repacking_id);

                if (repackingData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data repacking tidak ditemukan");
                }
                else
                {
                    if (repackingData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (repackingData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan ");
                    }

                }

                var validasi = await this._dao.UpdateTrRepackingValidated(data);

                if (validasi > 0)
                {
                    //get data barang dan gudang
                    var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomWithLock(repackingData.id_item, repackingData.id_stockroom);

                    //get stok akhir kartu stok
                    var cekStokAkhirForPenguranganHeader = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                        repackingData.id_stockroom,
                        repackingData.id_item
                        );

                    #region Update Stok Item Header (Pengurangan stok dari Repacking Item Header)


                    //get stok header
                    var cekStokItemPenguranganHeader = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                        repackingData.id_item,
                        repackingData.id_stockroom
                        );

                    //parameter untuk update stok header
                    var stokItemHeaderParam = new mm_setup_stok_item_update_stok
                    {
                        id_item = repackingData.id_item,
                        id_stockroom = repackingData.id_stockroom,
                        qty_on_hand = repackingData.qty
                    };

                    if ((cekStokItemPenguranganHeader.qty_on_hand - repackingData.qty) < 0)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Sisa stok untuk item {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang?.nama_stockroom} tidak mencukupi ");
                    }

                    var updateStokHeaderRepacking = await this._stokItemDao.UpdatePenguranganStok(stokItemHeaderParam);

                    if (updateStokHeaderRepacking <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Gagal mengurangi stok header");
                    }

                    //parameter untuk mengurangi kartu stok 
                    var kartuStokPenguranganParam = new mm_kartu_stok_item_insert_pengurangan_stok
                    {
                        id_detail_transaksi = null,
                        id_header_transaksi = data.repacking_id,
                        id_item = repackingData.id_item,
                        id_stockroom = repackingData.id_stockroom, //gudang farmasi
                        keterangan = "KELUAR REPACKING BARANG TANPA ED",
                        stok_awal = cekStokAkhirForPenguranganHeader?.stok_akhir,
                        stok_keluar = repackingData.qty,
                        nominal_keluar = repackingData.total_nominal,
                        nominal_awal = cekStokAkhirForPenguranganHeader?.nominal_akhir,
                        nomor_ref_transaksi = repackingData.nomor_repacking,
                        user_inputed = data.user_validated
                    };

                    var insertPenguranganKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokPenguranganParam);

                    if (insertPenguranganKartuStok <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Gagal mengurangi kartu stok header");
                    }

                    #endregion

                    var repackingDetail = await this._dao.GetTrRepackingDetailByHeaderIdWithLock(data.repacking_id);

                    //rekap repacking untuk di summary ke header
                    var rekapForHeader = repackingDetail.GroupBy(q => q.id_item_child).Select(res =>
                    new
                    {
                        repacking_id = res.FirstOrDefault().repacking_id,
                        id_item = res.FirstOrDefault().id_item_child,
                        id_stockroom = repackingData.id_stockroom,
                        qty_on_hand = res.Sum(s => s.qty),
                        nominal = res.Sum(n => n.sub_total),
                        details = repackingDetail.Where(x => x.id_item_child == res.FirstOrDefault().id_item_child).ToList()
                    }).ToList();

                    if (rekapForHeader.Count > 0)
                    {
                        foreach (var item in rekapForHeader)
                        {
                            #region STOK

                            var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                repackingData.id_stockroom,
                                item.id_item
                                );

                            //get stok header
                            var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                item.id_item,
                                item.id_stockroom
                                );

                            //jika stok tidak ada ditabel
                            if (cekStokItem is null)
                            {
                                #region Insert Stok Item Header

                                //parameter untuk insert stok header
                                var stokItemParam = new mm_setup_stok_item
                                {
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom,
                                    qty_on_hand = item.qty_on_hand,
                                    qty_stok_kritis = 0
                                };

                                var insertStokHeader = await this._stokItemDao.AddMmSetupStokItem(stokItemParam);

                                if (insertStokHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, "Gagal menambah stok header");
                                }

                                #endregion
                            }
                            else
                            {
                                #region Update Stok Item Header (Repacking Urai)

                                //parameter untuk update stok header
                                var stokItemParam = new mm_setup_stok_item_update_stok
                                {
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom,
                                    qty_on_hand = item.qty_on_hand
                                };

                                var updateStokHeader = await this._stokItemDao.UpdatePenambahanStok(stokItemParam);

                                if (updateStokHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, "Gagal menambah stok header");
                                }

                                #endregion
                            }

                            //parameter untuk menambah kartu stok
                            var kartuStokMasukParam = new mm_kartu_stok_item_insert_penambahan_stok
                            {
                                id_detail_transaksi = null,
                                id_header_transaksi = data.repacking_id,
                                id_item = item.id_item,
                                id_stockroom = item.id_stockroom, //gudang farmasi
                                keterangan = "MASUK REPACKING BARANG TANPA ED",
                                stok_awal = cekStokAkhir?.stok_akhir,
                                stok_masuk = item.qty_on_hand,
                                nominal_masuk = item.nominal,
                                nominal_awal = cekStokAkhir?.nominal_akhir,
                                nomor_ref_transaksi = repackingData.nomor_repacking,
                                user_inputed = data.user_validated
                            };

                            var insertKartuStok = await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokMasukParam);

                            if (insertKartuStok <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, "Gagal menambah kartu stok header");
                            }

                            #endregion
                        }
                    }
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal validasi repacking");
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

        public async Task<(bool, long, string)> UpdateTrRepackingCanceled(tr_repacking_no_ed_update_to_canceled data)
        {
            this._db.beginTransaction();
            try
            {
                var repackingData = await this._dao.GetTrRepackingByIdWithLock(data.repacking_id);

                if (repackingData is null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data repacking tidak ditemukan");
                }
                else
                {
                    if (repackingData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah divalidasi ");
                    }

                    if (repackingData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dibatalkan");
                    }

                }

                var validasi = await this._dao.UpdateTrRepackingCanceled(data);

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

        public async Task<List<tr_repacking_no_ed_detail>> GetTrRepackingDetailByHeaderId(long repacking_id)
        {
            try
            {
                return await this._dao.GetTrRepackingDetailByHeaderId(repacking_id);
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
