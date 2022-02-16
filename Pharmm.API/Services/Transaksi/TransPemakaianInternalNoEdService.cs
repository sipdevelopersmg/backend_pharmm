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
    public interface ITransPemakaianInternalNoEdService
    {
        #region Header

        Task<List<tr_pemakaian_internal_no_ed>> GetAllOpenTrPemakaianInternalByParams(List<ParameterSearchModel> param);

        Task<List<tr_pemakaian_internal_no_ed>> GetAllTrPemakaianInternalByParams(List<ParameterSearchModel> param);
        Task<tr_pemakaian_internal_no_ed> GetTrPemakaianInternalById(long pemakaian_internal_id);

        Task<(bool, long, string)> AddTrPemakaianInternal(tr_pemakaian_internal_no_ed_insert data);
        Task<(bool, long, string)> UpdateTrPemakaianInternalValidated(tr_pemakaian_internal_no_ed_update_to_validated data);
        Task<(bool, long, string)> UpdateTrPemakaianInternalCanceled(tr_pemakaian_internal_no_ed_update_to_canceled data);

        #endregion

        #region Detail Item

        Task<List<tr_pemakaian_internal_no_ed_detail_item>> GetAllTrPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param);
        Task<List<tr_pemakaian_internal_no_ed_detail_item>> GetTrPemakaianInternalDetailItemByPemakaianInternalId(long pemakaian_internal_id);

        #endregion

        #region Detail Upload

        Task<List<tr_pemakaian_internal_no_ed_detail_upload>> GetTrPemakaianInternalDetailUploadByPemakaianInternalId(long pemakaian_internal_id);
        Task<long> AddTrPemakaianInternalDetailUpload(tr_pemakaian_internal_no_ed_detail_upload_insert data);
        Task<short> DeleteTrPemakaianInternalDetailUpload(long pemakaian_internal_detail_upload_id);

        #endregion
    }

    public class TransPemakaianInternalNoEdService : ITransPemakaianInternalNoEdService
    {
        private readonly SQLConn _db;
        private readonly SetupItemDao _itemDao;
        private readonly TransPemakaianInternalNoEdDao _dao;
        private readonly MasterCounterDao _kodeDao;
        private readonly KartuStokItemDao _kartuStokDao;
        private readonly SetupStokItemDao _stokItemDao;

        public TransPemakaianInternalNoEdService(SQLConn db, TransPemakaianInternalNoEdDao dao,
            MasterCounterDao kodeDao,
            KartuStokItemDao kartuStokDao,
            SetupStokItemDao stokItemDao,
            SetupItemDao itemDao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
            this._itemDao = itemDao;
            this._kartuStokDao = kartuStokDao;
            this._stokItemDao = stokItemDao;
        }

        #region Header

        public async Task<List<tr_pemakaian_internal_no_ed>> GetAllOpenTrPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllOpenTrPemakaianInternalByParams(param);

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_pemakaian_internal_no_ed>> GetAllTrPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemakaianInternalByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<tr_pemakaian_internal_no_ed> GetTrPemakaianInternalById(long pemakaian_internal_id)
        {
            try
            {
                return await this._dao.GetTrPemakaianInternalById(pemakaian_internal_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> AddTrPemakaianInternal(tr_pemakaian_internal_no_ed_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePemakaianInternal,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor pemakaian internal
                var noPemakaianInternal = this._kodeDao.GenerateKode(dataCounter).Result;

                long pemakaianInternalId = 0;
                long pemakaianInternalDetailId = 0;


                if (string.IsNullOrEmpty(noPemakaianInternal))
                {

                    this._db.rollBackTrans();
                    return (false, 0, "Gagal mendapatkan nomor pemakaian internal");
                }
                else
                {
                    data.nomor_pemakaian_internal = noPemakaianInternal;
                }


                //perhitungan dari front end
                //if (data.details.Count > 0)
                //{
                //    //data.jumlah_item = 0;

                //    foreach (var item in data.details)
                //    {
                //        //get data barang by id
                //        var barang = await this._itemDao.GetMmSetupItemById((int)item.id_item);
                //        item.qty_pemakaian_internal = item.isi_pemakaian_internal * item.qty_satuan_besar_pemakaian_internal;

                //        data.jumlah_item += item.qty_satuan_besar_pemakaian_internal;
                //        data.total_transaksi = item.qty_pemakaian_internal * barang.hpp_average;
                //    }
                //}

                pemakaianInternalId = await this._dao.AddTrPemakaianInternal(data);

                if (pemakaianInternalId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.pemakaian_internal_id = pemakaianInternalId;
                            pemakaianInternalDetailId = await this._dao.AddTrPemakaianInternalDetailItem(detail);

                            if (pemakaianInternalDetailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, 0, "Gagal input pemakaian_internal detail dengan no urut " + detail.no_urut.ToString());
                            }
                        }


                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Gagal update nomor pemakaianInternal");
                    }
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, "Gagal input pemakaian internal");
                }

                this._db.commitTrans();
                return (true, pemakaianInternalId, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> UpdateTrPemakaianInternalValidated(tr_pemakaian_internal_no_ed_update_to_validated data)
        {

            this._db.beginTransaction();
            try
            {

                //cek data transaksi
                var issuedData = await this._dao.GetTrPemakaianInternalByIdWithLock(data.pemakaian_internal_id);

                if (issuedData is null)
                {
                    this._db.rollBackTrans();
                    return (false, 0, "Transaksi ini tidak ditemukan ");
                }
                else
                {
                    //cek validasi dan dibatalkan
                    if (issuedData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah divalidasi ");
                    }
                    else if (issuedData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah dibatalkan ");
                    }
                }

                var validateId = await this._dao.UpdateTrPemakaianInternalValidated(data);

                if (validateId > 0)
                {
                    #region Insert Stok

                    //cek detail transaksi
                    var issuedDataDetail = await this._dao.GetTrPemakaianInternalDetailItemByHeaderIdWithLock(data.pemakaian_internal_id);

                    if (issuedDataDetail.Count == 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, "Detail transaksi tidak ditemukan ");
                    }

                    var rekapForHeader = issuedDataDetail.GroupBy(q => q.id_item)
                        .Select(res => new
                        {
                            pemakaian_internal_id = res.FirstOrDefault().pemakaian_internal_id,
                            id_item = res.FirstOrDefault().id_item,
                            id_stockroom = issuedData.id_stockroom,
                            qty_on_hand = res.Sum(q => q.qty_pemakaian_internal),
                            nominal = res.Sum(n => n.nominal_pemakaian_internal),
                            details = data.details.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
                        }).ToList();

                    if (rekapForHeader.Count > 0)
                    {
                        foreach (var item in rekapForHeader)
                        {
                            //get stok akhir kartu stok 
                            var cekKartuStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                item.id_stockroom,
                                item.id_item
                                );

                            //get data barang dan gudang
                            var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomFromMasterWithLock(item.id_item, issuedData.id_stockroom);

                            //jika qty > 0 maka kurangi stok
                            if (item.qty_on_hand > 0)
                            {
                                #region Insert Kartu Stok Header

                                //parameter untuk menambah kartu stok
                                var kartuStokParam = new mm_kartu_stok_item_insert_pengurangan_stok
                                {
                                    id_detail_transaksi = null,
                                    id_header_transaksi = validateId,
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom, //gudang farmasi
                                    keterangan = "KELUAR PEMAKAIAN INTERNAL TANPA ED",
                                    nominal_awal = cekKartuStokAkhir?.nominal_akhir,
                                    stok_awal = cekKartuStokAkhir?.stok_akhir,
                                    stok_keluar = item.qty_on_hand,
                                    nominal_keluar = item.nominal,
                                    nomor_ref_transaksi = issuedData.nomor_pemakaian_internal,
                                    user_inputed = data.user_validated
                                };

                                var insertKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokParam);

                                if (insertKartuStok <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, "Gagal mengurangi kartu stok header");

                                    throw new Exception("Gagal mengurangi kartu stok header");
                                }

                                #endregion

                                #region Update Stok Item Header

                                //get stok di gudang 
                                var cekStokItemHeader = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                    item.id_item,
                                    item.id_stockroom
                                    );


                                if (cekStokItemHeader is null)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, $"Stok tidak tersedia untuk item {dataBarangAndGudang?.nama_item} dan gudang {dataBarangAndGudang?.nama_stockroom}");
                                }
                                else
                                {
                                    if ((cekStokItemHeader.qty_on_hand - item.qty_on_hand) < 0)
                                    {

                                        this._db.rollBackTrans();
                                        return (false, 0, $"Sisa stok untuk item {dataBarangAndGudang?.nama_item} tidak mencukupi ");
                                    }
                                }

                                //parameter untuk update stok header
                                var stokItemParam = new mm_setup_stok_item_update_stok
                                {
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom,
                                    qty_on_hand = item.qty_on_hand
                                };

                                var updateStokHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemParam);

                                if (updateStokHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, 0, "Gagal mengurangi stok header");

                                    throw new Exception("Gagal mengurangi stok header");
                                }


                                #endregion

                                #endregion

                                //input batch dan pengurangan stok batch
                                if (item.details.Count > 0)
                                {
                                    foreach (var detail in item.details)
                                    {
                                        #region Insert detail

                                        var detailId = await this._dao.ValidasiDetailItem(detail);

                                        if (detailId <= 0)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, 0, "Gagal input pemakaian internal detail");
                                        }

                                        #endregion

                                    }
                                }

                            }
                        }
                    }

                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, validateId, "Gagal validasi pemakaian internal");
                }

                this._db.commitTrans();
                return (true, validateId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> UpdateTrPemakaianInternalCanceled(tr_pemakaian_internal_no_ed_update_to_canceled data)
        {

            this._db.beginTransaction();
            try
            {
                var issuedData = await this._dao.GetTrPemakaianInternalByIdWithLock(data.pemakaian_internal_id);

                if (issuedData is null)
                {
                    this._db.rollBackTrans();
                    return (false, 0, "Transaksi ini tidak ditemukan ");
                }
                else
                {
                    //cek validasi dan dibatalkan
                    if (issuedData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah divalidasi ");
                    }
                    else if (issuedData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, 0, "Transaksi ini sudah dibatalkan ");
                    }
                }

                var cancel = await this._dao.UpdateTrPemakaianInternalCanceled(data);

                if (cancel <= 0)
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Transaksi gagal dibatalkan ");
                }

                this._db.commitTrans();
                return (true, cancel, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }
       
        #endregion

        #region Detail Item

        public async Task<List<tr_pemakaian_internal_no_ed_detail_item>> GetAllTrPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemakaianInternalDetailItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemakaian_internal_no_ed_detail_item>> GetTrPemakaianInternalDetailItemByPemakaianInternalId(long pemakaian_internal_id)
        {
            try
            {
                return await this._dao.GetTrPemakaianInternalDetailItemByPemakaianInternalId(pemakaian_internal_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion


        #region Detail Upload

        public async Task<List<tr_pemakaian_internal_no_ed_detail_upload>> GetTrPemakaianInternalDetailUploadByPemakaianInternalId(long pemakaian_internal_id)
        {
            try
            {
                return await this._dao.GetTrPemakaianInternalDetailUploadByPemakaianInternalId(pemakaian_internal_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrPemakaianInternalDetailUpload(tr_pemakaian_internal_no_ed_detail_upload_insert data)
        {
            try
            {
                return await this._dao.AddTrPemakaianInternalDetailUpload(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteTrPemakaianInternalDetailUpload(long pemakaian_internal_detail_upload_id)
        {
            try
            {
                short deleteBerkas = (short)0;

                var dataBerkas = await this._dao.GetTrPemakaianInternalDetailUploadById(pemakaian_internal_detail_upload_id);

                if (dataBerkas is not null)
                {
                    deleteBerkas = await this._dao.DeleteTrPemakaianInternalDetailUpload(pemakaian_internal_detail_upload_id);

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
