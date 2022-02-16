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
    public interface ITransReturPemakaianInternalService
    {
        #region Header

        Task<List<tr_retur_pemakaian_internal>> GetAllTrReturPemakaianInternalByParams(List<ParameterSearchModel> param);
        Task<tr_retur_pemakaian_internal> GetTrReturPemakaianInternalById(long retur_pemakaian_internal_id);

        Task<long> AddTrReturPemakaianInternal(tr_retur_pemakaian_internal_insert data);

        Task<(bool, long, string)> UpdateToValidated(tr_retur_pemakaian_internal_update_to_validated data);
        Task<(bool, long, string)> UpdateToCanceled(tr_retur_pemakaian_internal_update_to_canceled data);

        #endregion

        #region Detail Item

        Task<List<tr_retur_pemakaian_internal_detail_item>> GetAllTrReturPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param);
        Task<List<tr_retur_pemakaian_internal_detail_item>> GetTrReturPemakaianInternalDetailItemByHeaderId(long retur_pemakaian_internal_item_id);

        #endregion

        #region Detail Upload

        Task<List<tr_retur_pemakaian_internal_detail_upload>> GetTrReturPemakaianInternalDetailUploadByHeaderId(long retur_pemakaian_internal_id);
        Task<long> AddTrReturPemakaianInternalDetailUpload(tr_retur_pemakaian_internal_detail_upload_insert data);
        Task<short> DeleteTrReturPemakaianInternalDetailUpload(long retur_pemakaian_internal_detail_upload_id);

        #endregion


    }

    public class TransReturPemakaianInternalService : ITransReturPemakaianInternalService
    {
        private readonly SQLConn _db;
        private readonly TransReturPemakaianInternalDao _dao;
        private readonly TransPemakaianInternalDao _internalDao;
        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly KartuStokItemDao _kartuStokDao;


        public TransReturPemakaianInternalService(SQLConn db, TransReturPemakaianInternalDao dao,
            MasterCounterDao kodeDao,
            SetupStokItemDao stokItemDao,
            KartuStokItemDao kartuStokDao,
            TransPemakaianInternalDao internalDao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
            this._internalDao = internalDao;

            this._stokItemDao = stokItemDao;
            this._kartuStokDao = kartuStokDao;
        }

        #region Header

        public async Task<List<tr_retur_pemakaian_internal>> GetAllTrReturPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrReturPemakaianInternalByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<tr_retur_pemakaian_internal> GetTrReturPemakaianInternalById(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this._dao.GetTrReturPemakaianInternalById(retur_pemakaian_internal_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrReturPemakaianInternal(tr_retur_pemakaian_internal_insert data)
        {

            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeReturPemakaianInternal,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor retur pemakaian Internal
                var noReturPemakaianInternal = this._kodeDao.GenerateKode(dataCounter).Result;

                long returPemakaianInternalId = 0;
                long returPemakaianInternalDetailId = 0;

                if (string.IsNullOrEmpty(noReturPemakaianInternal))
                {
                    throw new Exception("Gagal mendapatkan nomor retur pemakaian internal");
                }
                else
                {
                    data.nomor_retur_pemakaian_internal = noReturPemakaianInternal;
                }


                //if (data.details.Count > 0)
                //{
                //    data.total_transaksi = 0;
                //    data.jumlah_item = 0;

                //    foreach (var item in data.details)
                //    {
                ////        get data barang by id
                //        var barang = await this._obatDao.GetPharSetupObatDetailAktifByIdItem((int)item.id_item);
                //        item.qty_retur_pemakaian_internal = item.isi_retur_pemakaian_internal * item.qty_satuan_besar_retur_pemakaian_internal;

                //        data.jumlah_item += item.qty_satuan_besar_retur_pemakaian_internal;
                //        data.total_transaksi += item.qty_retur_pemakaian_internal * item.hpp_satuan; //barang.hpp_average;
                //    }
                //}

                returPemakaianInternalId = await this._dao.AddTrReturPemakaianInternal(data);

                if (returPemakaianInternalId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.retur_pemakaian_internal_id = returPemakaianInternalId;
                            returPemakaianInternalDetailId = await this._dao.AddTrReturPemakaianInternalDetailItem(detail);

                            if (returPemakaianInternalDetailId <= 0)
                            {
                                throw new Exception("Gagal input pemakaian_internal detail dengan no urut " + detail.no_urut.ToString());
                            }

                        }


                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        throw new Exception("Gagal update nomor retur pemakaian internal");
                    }

                }

                this._db.commitTrans();
                return returPemakaianInternalId;
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }


        public async Task<(bool, long, string)> UpdateToValidated(tr_retur_pemakaian_internal_update_to_validated data)
        {
            this._db.beginTransaction();

            try
            {

                var insertKartuStok = 0;
                var validate = (short)0;
                short id_stockroom = 0;


                //get retur pemakaian internal data
                var returIssueData = await this._dao.GetTrReturPemakaianInternalByIdWithLock(data.retur_pemakaian_internal_id);

                if (returIssueData is not null)
                {
                    #region Cek Status Transaksi

                    if (returIssueData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah divalidasi");
                    }
                    else if (returIssueData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah dibatalkan");
                    }


                    #endregion

                    //validasi retur pemakaian internal
                    validate = await this._dao.UpdateTrReturPemakaianInternalValidated(data);

                    if (validate > 0)
                    {
                        id_stockroom = returIssueData.id_stockroom;

                        //get retur pemakaian internal data detail
                        var returIssueDataDetail = await this._dao.GetTrReturPemakaianInternalDetailItemByHeaderIdWithLock(data.retur_pemakaian_internal_id);

                        if (returIssueDataDetail.Count > 0)
                        {
                            //rekap retur pemakaian internal untuk di summary ke header
                            var rekapForHeader = returIssueDataDetail.GroupBy(q => q.id_item).Select(res =>
                            new
                            {
                                retur_pemakaian_internal_id = res.FirstOrDefault().retur_pemakaian_internal_id,
                                id_item = res.FirstOrDefault().id_item,
                                id_stockroom = id_stockroom,
                                qty_on_hand = res.Sum(s => s.qty_retur_pemakaian_internal),
                                nominal = res.Sum(n => n.nominal_retur_pemakaian_internal), //hpp_average * qty_retur
                                details = returIssueDataDetail.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
                            }).ToList();

                            if (rekapForHeader.Count > 0)
                            {
                                foreach (var item in rekapForHeader)
                                {

                                    //get stok akhir kartu stok
                                    var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                        item.id_stockroom,
                                        item.id_item
                                        );

                                    #region Insert Kartu Stok Header

                                    //parameter untuk menambah kartu stok
                                    var kartuStokParam = new mm_kartu_stok_item_insert_penambahan_stok
                                    {
                                        id_detail_transaksi = null,
                                        id_header_transaksi = data.retur_pemakaian_internal_id,
                                        id_item = item.id_item,
                                        id_stockroom = item.id_stockroom, //gudang farmasi
                                        keterangan = "MASUK RETUR PEMAKAIAN INTERNAL",
                                        stok_awal = cekStokAkhir?.stok_akhir,
                                        stok_masuk = item.qty_on_hand,
                                        nominal_masuk = item.nominal,
                                        nominal_awal = cekStokAkhir?.nominal_akhir,
                                        nomor_ref_transaksi = returIssueData.nomor_retur_pemakaian_internal,
                                        user_inputed = data.user_validated
                                    };

                                    insertKartuStok = await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokParam);

                                    if (insertKartuStok <= 0)
                                    {
                                        this._db.rollBackTrans();
                                        return (false, 0, "Gagal menambah kartu stok header");

                                        throw new Exception("Gagal menambah kartu stok header");
                                    }
                                    else
                                    {


                                        //get stok header
                                        var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                            item.id_item,
                                            id_stockroom
                                            );

                                        if (cekStokItem is null)
                                        {
                                            #region Insert Stok Item Header

                                            //parameter untuk insert stok header
                                            var stokItemParam = new mm_setup_stok_item
                                            {
                                                id_item = item.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = item.qty_on_hand,
                                                qty_stok_kritis = 0
                                            };

                                            var insertStokHeader = await this._stokItemDao.AddMmSetupStokItem(stokItemParam);

                                            if (insertStokHeader <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, 0, "Gagal menambah stok header");

                                                throw new Exception("Gagal menambah stok header");
                                            }

                                            #endregion
                                        }
                                        else
                                        {

                                            #region Update Stok Item Header

                                            //parameter untuk update stok header
                                            var stokItemParam = new mm_setup_stok_item_update_stok
                                            {
                                                id_item = item.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = item.qty_on_hand
                                            };

                                            var updateStokHeader = await this._stokItemDao.UpdatePenambahanStok(stokItemParam);

                                            if (updateStokHeader <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, 0, "Gagal menambah stok header");

                                                throw new Exception("Gagal menambah stok header");
                                            }

                                            #endregion
                                        }

                                        if (item.details.Count > 0)
                                        {
                                            foreach (var detailRetur in item.details)
                                            {

                                                //get stok akhir kartu stok detail
                                                var cekStokAkhirDetail = await this._kartuStokDao.GetStokAkhirDetailBatchWithLock(
                                                    detailRetur.batch_number,
                                                    detailRetur.expired_date,
                                                    id_stockroom,
                                                    detailRetur.id_item
                                                    );

                                                #region Input Kartu Stok Detail Batch

                                                //jika input kartu stok
                                                if (insertKartuStok > 0)
                                                {
                                                    //parameter untuk insert kartu stok detail
                                                    var kartuStokDetailParam = new mm_kartu_stok_item_detail_batch_insert_penambahan_stok
                                                    {
                                                        batch_number = detailRetur.batch_number,
                                                        expired_date = detailRetur.expired_date,
                                                        id_detail_transaksi = detailRetur.retur_pemakaian_internal_detail_item_id,
                                                        id_header_transaksi = detailRetur.retur_pemakaian_internal_id,
                                                        id_kartu_stok_item = insertKartuStok,
                                                        nominal_awal = cekStokAkhirDetail?.nominal_akhir,
                                                        stok_awal = cekStokAkhirDetail?.stok_akhir,
                                                        nominal_masuk = detailRetur.nominal_retur_pemakaian_internal,
                                                        stok_masuk = detailRetur.qty_retur_pemakaian_internal
                                                    };

                                                    //untuk insert kartu stok detail batch
                                                    var insertKartuStokDetail = await this._kartuStokDao.AddPenambahanMmKartuStokItemDetailBatch(kartuStokDetailParam);

                                                    if (insertKartuStokDetail <= 0)
                                                    {
                                                        this._db.rollBackTrans();
                                                        return (false, 0, "Gagal menambah ke kartu stok detail batch");

                                                        throw new Exception("Gagal menambah ke kartu stok detail batch");
                                                    }
                                                    else
                                                    {
                                                          //get stok di gudang 
                                                        var cekStokItemDetail = await this._stokItemDao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumberWithLock(
                                                            detailRetur.id_item,
                                                            id_stockroom,
                                                            detailRetur.batch_number
                                                            );


                                                        if (cekStokItemDetail is null)
                                                        {
                                                            #region Insert Stok Detail Batch

                                                            //parameter untuk insert penambahan stok detail
                                                            var insertStokItemDetailParam = new mm_setup_stok_item_detail_batch_insert
                                                            {
                                                                id_item = detailRetur.id_item,
                                                                id_stockroom = id_stockroom,
                                                                qty_on_hand = detailRetur.qty_retur_pemakaian_internal,
                                                                batch_number = detailRetur.batch_number,
                                                                expired_date = detailRetur.expired_date,
                                                                barcode_batch_number = string.Concat(detailRetur.barcode, detailRetur.batch_number),
                                                                harga_satuan_netto = detailRetur.hpp_satuan
                                                            };

                                                            var insertStokDetail = await this._stokItemDao.AddMmSetupStokItemDetailBatch(insertStokItemDetailParam);

                                                            if (insertStokDetail <= 0)
                                                            {
                                                                this._db.rollBackTrans();
                                                                return (false, 0, "Gagal insert stok detail batch");

                                                            }

                                                            #endregion

                                                            //this._db.rollBackTrans();
                                                            //return (false, 0, $"Stok tidak tersedia untuk item {detailRetur.id_item.ToString()} dan gudang {detailRetur.id_stockroom.ToString()}");
                                                        }
                                                        else
                                                        {

                                                            #region Update Stok Detail

                                                            //parameter untuk update stok detail
                                                            var stokItemDetailParam = new mm_setup_stok_item_detail_update_penambahan_stok
                                                            {
                                                                id_item = detailRetur.id_item,
                                                                id_stockroom = id_stockroom,
                                                                qty_on_hand = detailRetur.qty_retur_pemakaian_internal,
                                                                batch_number = detailRetur.batch_number,
                                                                expired_date = detailRetur.expired_date,
                                                                barcode_batch_number = string.Concat(detailRetur.barcode, detailRetur.batch_number)
                                                            };

                                                            //update stok item detail batch
                                                            var updateStokDetail = await this._stokItemDao.UpdatePenambahanStokDetailBatch(stokItemDetailParam);

                                                            if (updateStokDetail <= 0)
                                                            {
                                                                this._db.rollBackTrans();
                                                                return (false, 0, "Gagal menambah stok detail");

                                                            }
                                                        }

                                                        #endregion

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
                        else
                        {
                            this._db.rollBackTrans();
                            return (false, 0, "retur pemakaian internal ini sudah tidak tersedia");

                            throw new Exception("retur pemakaian internal ini sudah tidak tersedia");
                        }

                    }
                    else
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Gagal validasi retur pemakaian internal");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, "Data transaksi retur pemakaian internal tidak ditemukan");

                    throw new Exception("Data transaksi retur pemakaian internal tidak ditemukan");
                }


                this._db.commitTrans();
                return (true, validate, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> UpdateToCanceled(tr_retur_pemakaian_internal_update_to_canceled data)
        {

            this._db.beginTransaction();
            try
            {
                //get retur pemakaian internal data
                var returIssueData = await this._dao.GetTrReturPemakaianInternalByIdWithLock(data.retur_pemakaian_internal_id);

                if (returIssueData is not null)
                {
                    #region Cek Status Transaksi

                    if (returIssueData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah divalidasi");
                    }
                    else if (returIssueData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah dibatalkan");
                    }


                    #endregion
                }

                var cancel = await this._dao.UpdateTrReturPemakaianInternalCanceled(data);


                this._db.commitTrans();
                return (true, cancel, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }



        #endregion

        #region Detail Item

        public async Task<List<tr_retur_pemakaian_internal_detail_item>> GetAllTrReturPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrReturPemakaianInternalDetailItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_retur_pemakaian_internal_detail_item>> GetTrReturPemakaianInternalDetailItemByHeaderId(long retur_pemakaian_internal_item_id)
        {
            try
            {
                return await this._dao.GetTrReturPemakaianInternalDetailItemByHeaderId(retur_pemakaian_internal_item_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail Upload

        public async Task<List<tr_retur_pemakaian_internal_detail_upload>> GetTrReturPemakaianInternalDetailUploadByHeaderId(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this._dao.GetTrReturPemakaianInternalDetailUploadHeaderId(retur_pemakaian_internal_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrReturPemakaianInternalDetailUpload(tr_retur_pemakaian_internal_detail_upload_insert data)
        {
            try
            {
                return await this._dao.AddTrReturPemakaianInternalDetailUpload(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteTrReturPemakaianInternalDetailUpload(long retur_pemakaian_internal_detail_upload_id)
        {
            try
            {
                short deleteBerkas = (short)0;

                var dataBerkas = await this._dao.GetTrReturPemakaianInternalDetailUploadById(retur_pemakaian_internal_detail_upload_id);

                if (dataBerkas is not null)
                {
                    deleteBerkas = await this._dao.DeleteTrReturPemakaianInternalDetailUpload(retur_pemakaian_internal_detail_upload_id);

                    if (deleteBerkas > 0)
                    {
                        //hapus gambar dari bucket minio
                        await UploadHelper.RemoveObjectFromBucketByPath(dataBerkas.path_dokumen);
                    }
                }

                return deleteBerkas;
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
