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

namespace Pharmm.API.Services.Transaksi
{
    public interface ITransAuditStokOpnameTanpaSettingService
    {
        Task<tr_audit_stok_opname_header_recursive> GetTrAuditStokOpnameHeaderById(long audit_stok_opname_id);

        Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param);
        Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderOpenByParams(List<ParameterSearchModel> param);

        Task<List<tr_audit_stok_opname_no_setting_lookup_barang_header>> GetLookupBarangByIdStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_param data
            );

        Task<List<tr_audit_stok_opname_no_setting_lookup_barang_batch>> GetLookupBatchByIdItemAndIdStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_batch_param param
            );

        Task<(bool, long, string)> AddTrAuditStokOpnameHeader(tr_audit_stok_opname_no_setting_header_insert data);
        Task<(bool, long, string)> Adjustment(tr_audit_stok_opname_no_setting_header_update_after_adjust data);
        Task<(bool, long, string)> Finalisasi(tr_audit_stok_opname_no_setting_header_update_after_proses data);


        #region Detail

        Task<List<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailByHeaderIdAndWaktuCapture(
            tr_audit_stok_opname_no_setting_detail_getby_headerid_and_waktu param);

        #endregion


        #region Detail Batch

        Task<List<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailBatchByDetailIdAndWaktuCapture(
            tr_audit_stok_opname_no_setting_detail_batch_getby_detailid_and_waktu param);

        #endregion

    }

    public class TransAuditStokOpnameTanpaSettingService : ITransAuditStokOpnameTanpaSettingService
    {
        private readonly SQLConn _db;
        private readonly TransAuditStokOpnameTanpaSettingDao _dao;
        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly KartuStokItemDao _kartuStokDao;

        public TransAuditStokOpnameTanpaSettingService(
            SetupStokItemDao stokItemDao,
            KartuStokItemDao kartuStokDao,
            MasterCounterDao kodeDao,
            SQLConn db, TransAuditStokOpnameTanpaSettingDao dao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;

            this._kartuStokDao = kartuStokDao;
            this._stokItemDao = stokItemDao;
        }

        public async Task<tr_audit_stok_opname_header_recursive> GetTrAuditStokOpnameHeaderById(long audit_stok_opname_id)
        {
            try
            {
                return await this._dao.GetTrAuditStokOpnameHeaderById(audit_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_audit_stok_opname_no_setting_lookup_barang_header>> GetLookupBarangByIdStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_param data
            )
        {
            try
            {
                return await this._dao.GetLookupBarangByIdStockroomAndWaktuCapture(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_audit_stok_opname_no_setting_lookup_barang_batch>> GetLookupBatchByIdItemAndIdStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_batch_param param
            )
        {
            try
            {
                return await this._dao.GetLookupBatchByIdItemAndIdStockroomAndWaktuCapture(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


        public async Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrAuditStokOpnameHeaderByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderOpenByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrAuditStokOpnameHeaderOpenByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, long, string)> AddTrAuditStokOpnameHeader(tr_audit_stok_opname_no_setting_header_insert data)
        {

            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixStokOpname,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor audit stok opname
                var noStokOpname = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noStokOpname))
                {
                    data.nomor_audit_stok_opname = noStokOpname;
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor stok opname");
                }

                var headerId = await this._dao.AddTrAuditStokOpnameHeader(data);

                if (headerId > 0)
                {
                    foreach (var detail in data.details)
                    {
                        detail.audit_stok_opname_id = headerId;
                        //detail.qty_proses_selisih = detail.qty_fisik - detail.qty_sistem_capture_stok;
                        //detail.sub_total_proses_selisih = detail.sub_total_fisik - detail.sub_total_sistem_capture_stok;

                        //get data barang dan gudang
                        var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomWithLock(detail.id_item, data.id_stockroom);

                        var detailId = await this._dao.AddTrAuditStokOpnameDetail(detail);

                        if (detailId <= 0)
                        {
                            this._db.rollBackTrans();
                            return (false, 0, $"Gagal input audit stok opname detail untuk barang {dataBarangAndGudang?.nama_stockroom} di gudang {dataBarangAndGudang?.nama_stockroom}");
                        }
                        else
                        {

                            if (detail.detailBatchs.Count > 0)
                            {
                                foreach (var batch in detail.detailBatchs)
                                {
                                    batch.audit_stok_opname_detail_id = detailId;
                                    batch.audit_stok_opname_id = headerId;

                                    //batch.qty_proses_selisih = batch.qty_fisik - batch.qty_sistem_capture_stok;
                                    //batch.sub_total_proses_selisih = batch.sub_total_fisik - batch.sub_total_sistem_capture_stok;

                                    var detailBatchId = await this._dao.AddTrAuditStokOpnameDetailBatch(batch);

                                    if (detailBatchId <= 0)
                                    {
                                        this._db.rollBackTrans();
                                        return (false, 0, $"Gagal input audit stok opname detail batch untuk barang {dataBarangAndGudang?.nama_stockroom} dengan nomor batch {batch.batch_number} di gudang {dataBarangAndGudang?.nama_stockroom}");
                                    }

                                }
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor stok opname");
                    }

                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal audit stok opname");
                }

                this._db.commitTrans();
                return (true, headerId, "SUCCES");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> Adjustment(tr_audit_stok_opname_no_setting_header_update_after_adjust data)
        {

            this._db.beginTransaction();
            try
            {
                var stokOpnameData = await this._dao.GetTrAuditStokOpnameHeaderByIdWithLock(data.audit_stok_opname_id);

                if (stokOpnameData == null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data stok opname tidak ditemukan");
                }
                else
                {
                    if (stokOpnameData.user_proses is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dilakukan finalisasi");
                    }
                }

                var adjust = await this._dao.UpdateAdjustment(data);

                if (adjust > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            var adjustDetail = await this._dao.UpdateDetailAdjustment(detail);

                            if (adjustDetail <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal update adjustment detail");
                            }
                            else
                            {
                                if (detail.detailBatchs.Count > 0)
                                {
                                    foreach (var batch in detail.detailBatchs)
                                    {
                                        var batchDetail = await this._dao.UpdateDetailBatchAdjustment(batch);

                                        if (batchDetail <= 0)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, 0, $"Gagal update adjustment detail batch");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal update adjustment");
                }

                this._db.commitTrans();
                return (true, adjust, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> Finalisasi(tr_audit_stok_opname_no_setting_header_update_after_proses data)
        {

            this._db.beginTransaction();
            try
            {
                short id_stockroom = 0;

                var stokOpnameData = await this._dao.GetTrAuditStokOpnameHeaderByIdWithLock(data.audit_stok_opname_id);

                if (stokOpnameData == null)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Data stok opname tidak ditemukan");
                }
                else
                {
                    id_stockroom = stokOpnameData.id_stockroom;
                    if (stokOpnameData.user_proses is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Data ini sudah dilakukan finalisasi");
                    }
                }

                var final = await this._dao.UpdateFinalisasi(data);

                if (final > 0)
                {
                    if (stokOpnameData.details.Count > 0)
                    {
                        var stokOpnameDetail = stokOpnameData.details.Where(q => q.qty_proses_selisih != 0).ToList();

                        foreach (var detail in stokOpnameDetail)
                        {
                            #region Manajemen Stok

                            //get stok akhir kartu stok
                            var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                id_stockroom,
                                detail.id_item
                                );

                            #region Insert Kartu Stok Header

                            //parameter untuk menambah kartu stok
                            var kartuStokPenambahanParam = new mm_kartu_stok_item_insert_penambahan_stok
                            {
                                id_detail_transaksi = null,
                                id_header_transaksi = data.audit_stok_opname_id,
                                id_item = detail.id_item,
                                id_stockroom = id_stockroom, //gudang farmasi
                                keterangan = "ADJUST HASIL AUDIT STOK",
                                stok_awal = cekStokAkhir?.stok_akhir,
                                stok_masuk = detail.qty_proses_selisih,
                                nominal_masuk = detail.sub_total_proses_selisih,
                                nominal_awal = cekStokAkhir?.nominal_akhir,
                                nomor_ref_transaksi = stokOpnameData.nomor_audit_stok_opname,
                                user_inputed = data.user_proses
                            };

                            var kartuStokPenguranganParam = new mm_kartu_stok_item_insert_pengurangan_stok
                            {
                                id_detail_transaksi = null,
                                id_header_transaksi = data.audit_stok_opname_id,
                                id_item = detail.id_item,
                                id_stockroom = id_stockroom, //gudang farmasi
                                keterangan = "ADJUST HASIL AUDIT STOK",
                                stok_awal = cekStokAkhir?.stok_akhir,
                                stok_keluar = detail.qty_proses_selisih * -1,
                                nominal_keluar = detail.sub_total_proses_selisih * -1,
                                nominal_awal = cekStokAkhir?.nominal_akhir,
                                nomor_ref_transaksi = stokOpnameData.nomor_audit_stok_opname,
                                user_inputed = data.user_proses
                            };

                            var insertKartuStok = detail.qty_proses_selisih > 0 ?
                                await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokPenambahanParam) :
                                await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokPenguranganParam);

                            if (insertKartuStok <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal { (detail.qty_proses_selisih > 0 ? "menambah" : "mengurangi") } kartu stok header");
                            }
                            else
                            {

                                #region setup stok item

                                //get data barang dan gudang
                                var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomWithLock(detail.id_item, id_stockroom);

                                //get stok header
                                var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                    detail.id_item,
                                    id_stockroom
                                    );

                                if (cekStokItem is null)
                                {

                                    this._db.rollBackTrans();
                                    return (false, 0, $"Barang {dataBarangAndGudang?.nama_item} tidak ditemukan di gudang {dataBarangAndGudang?.nama_stockroom}");
                                }
                                else
                                {

                                    #region Update Stok Item Header

                                    //parameter untuk update stok header
                                    var stokItemPenambahanaram = new mm_setup_stok_item_update_stok
                                    {
                                        id_item = detail.id_item,
                                        id_stockroom = id_stockroom,
                                        qty_on_hand = (decimal)detail.qty_proses_selisih
                                    };

                                    var stokItemPenguranganParam = new mm_setup_stok_item_update_stok
                                    {
                                        id_item = detail.id_item,
                                        id_stockroom = id_stockroom,
                                        qty_on_hand = (decimal)detail.qty_proses_selisih * -1
                                    };

                                    if (detail.qty_proses_selisih > 0)
                                    {
                                        var updateStokHeader = await this._stokItemDao.UpdatePenambahanStok(stokItemPenambahanaram);

                                        if (updateStokHeader <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, 0, "Gagal menambah stok header");

                                        }
                                    }
                                    else
                                    {
                                        if (cekStokItem?.qty_on_hand - (detail.qty_proses_selisih * -1) < 0)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, 0, $"Sisa stok untuk barang {dataBarangAndGudang?.nama_item} di gudang {dataBarangAndGudang.nama_stockroom} tidak mencukupi ");
                                        }


                                        var updateStokHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemPenguranganParam);

                                        if (updateStokHeader <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, 0, "Gagal mengurangi stok header");

                                        }
                                    }

                                    #endregion
                                }


                                #endregion


                                if (detail.batchs.Count > 0)
                                {
                                    var stokOpnameDetailBatch = detail.batchs.Where(q => q.qty_proses_selisih != 0).ToList();

                                    foreach (var batch in stokOpnameDetailBatch)
                                    {

                                        //get stok detail
                                        //get stok akhir kartu stok detail
                                        var cekStokAkhirDetail = await this._kartuStokDao.GetStokAkhirDetailBatchWithLock(
                                            batch.batch_number,
                                            batch.expired_date,
                                            id_stockroom,
                                            detail.id_item
                                            );

                                        #region Kartu Stok Detail Batch

                                        //jika input kartu stok
                                        if (insertKartuStok > 0)
                                        {

                                            //parameter untuk insert kartu stok detail
                                            var kartuStokDetailPenambahanParam = new mm_kartu_stok_item_detail_batch_insert_penambahan_stok
                                            {
                                                batch_number = batch.batch_number,
                                                expired_date = batch.expired_date,
                                                id_detail_transaksi = batch.audit_stok_opname_detail_id,
                                                id_header_transaksi = batch.audit_stok_opname_id,
                                                id_kartu_stok_item = insertKartuStok,
                                                nominal_awal = cekStokAkhirDetail?.nominal_akhir,
                                                stok_awal = cekStokAkhirDetail?.stok_akhir,
                                                nominal_masuk = batch.sub_total_proses_selisih,
                                                stok_masuk = batch.qty_proses_selisih
                                            };

                                            var kartuStokDetailPenguranganParam = new mm_kartu_stok_item_detail_batch_insert_pengurangan_stok
                                            {
                                                batch_number = batch.batch_number,
                                                expired_date = batch.expired_date,
                                                id_detail_transaksi = batch.audit_stok_opname_detail_batch_id,
                                                id_header_transaksi = batch.audit_stok_opname_id,
                                                id_kartu_stok_item = insertKartuStok,
                                                nominal_awal = cekStokAkhirDetail?.nominal_akhir,
                                                stok_awal = cekStokAkhirDetail?.stok_akhir,
                                                nominal_keluar = batch.sub_total_proses_selisih * -1,
                                                stok_keluar = batch.qty_proses_selisih * -1
                                            };

                                            if (batch.qty_proses_selisih > 0)
                                            {
                                                //untuk insert kartu stok detail batch
                                                var insertKartuStokDetail = await this._kartuStokDao.AddPenambahanMmKartuStokItemDetailBatch(kartuStokDetailPenambahanParam);

                                                if (insertKartuStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, 0, "Gagal menambah kartu stok detail batch");

                                                }
                                            }
                                            else
                                            {

                                                //untuk insert kartu stok detail batch
                                                var insertKartuStokDetail = await this._kartuStokDao.AddPenguranganMmKartuStokItemDetailBatch(kartuStokDetailPenguranganParam);

                                                if (insertKartuStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, 0, "Gagal mengurangi kartu stok detail batch");

                                                }

                                            }

                                        }

                                        #endregion

                                        #region Stok Item Detail Batch

                                        var cekStokItemDetail = await this._stokItemDao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumberWithLock(
                                            detail.id_item,
                                            id_stockroom,
                                            batch.batch_number
                                            );

                                        if (cekStokItemDetail is null)
                                        {
                                            if (batch.qty_proses_selisih > 0)
                                            {
                                                var insertNewStokParam = new mm_setup_stok_item_detail_batch_insert
                                                {
                                                    id_item = detail.id_item,
                                                    id_stockroom = id_stockroom,
                                                    qty_on_hand = batch.qty_proses_selisih,
                                                    batch_number = batch.batch_number,
                                                    expired_date = batch.expired_date,
                                                    barcode_batch_number = String.Concat(detail.barcode, batch.batch_number),
                                                    harga_satuan_netto = detail.hpp_average,
                                                };

                                                var insertStok = await this._stokItemDao.AddMmSetupStokItemDetailBatch(insertNewStokParam);

                                                if (insertStok <= 0)
                                                {

                                                    this._db.rollBackTrans();
                                                    return (false, 0, $"Gagal menambahkan stok baru untuk barang {dataBarangAndGudang?.nama_item} ke gudang {dataBarangAndGudang.nama_stockroom}");
                                                }
                                            }

                                            this._db.rollBackTrans();
                                            return (false, 0, $"Barang {dataBarangAndGudang?.nama_item} dengan batch {batch.batch_number} tidak ditemukan di gudang {dataBarangAndGudang?.nama_stockroom}");
                                        }
                                        else
                                        {
                                            #region Update Stok Detail Batch

                                            //parameter untuk insert penambahan stok detail
                                            var insertStokDetailPenambahanParam = new mm_setup_stok_item_detail_update_penambahan_stok
                                            {
                                                id_item = detail.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = batch.qty_proses_selisih,
                                                batch_number = batch.batch_number,
                                                expired_date = batch.expired_date,
                                                barcode_batch_number = String.Concat(detail.barcode, batch.batch_number)
                                            };

                                            var insertStokDetailPenguranganParam = new mm_setup_stok_item_detail_update_pengurangan_stok
                                            {
                                                id_item = detail.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = batch.qty_proses_selisih * -1,
                                                batch_number = batch.batch_number,
                                                expired_date = batch.expired_date
                                            };

                                            if (batch.qty_proses_selisih > 0)
                                            {
                                                var insertStokDetail = await this._stokItemDao.UpdatePenambahanStokDetailBatch(insertStokDetailPenambahanParam);

                                                if (insertStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, 0, "Gagal menambah stok detail batch");
                                                }
                                            }
                                            else
                                            {

                                                var insertStokDetail = await this._stokItemDao.UpdatePenguranganStokDetailBatch(insertStokDetailPenguranganParam);

                                                if (insertStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, 0, "Gagal mengurangi stok detail batch");
                                                }
                                            }

                                            #endregion
                                        }

                                        #endregion
                                    }
                                }
                            }

                            #endregion



                            #endregion

                        }
                    }
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal update finalisasi");
                }

                this._db.commitTrans();
                return (true, final, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }




        #region Detail


        public async Task<List<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailByHeaderIdAndWaktuCapture(tr_audit_stok_opname_no_setting_detail_getby_headerid_and_waktu param)
        {
            try
            {
                return await this._dao.GetTrAuditStokOpnameDetailByHeaderIdAndWaktuCapture(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Detail Batch



        public async Task<List<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailBatchByDetailIdAndWaktuCapture(tr_audit_stok_opname_no_setting_detail_batch_getby_detailid_and_waktu param)
        {
            try
            {
                return await this._dao.GetTrAuditStokOpnameDetailBatchByDetailIdAndWaktuCapture(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion
    }
}
