using DapperPostgreSQL;
using ImageMagick;
using Microsoft.AspNetCore.Hosting;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Transaksi
{
    public interface ITransKontrakSpjbService
    {
        #region Header

        Task<List<tr_kontrak_spjb>> GetTrKontrakSpjbById(long kontrak_id);
        Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjbByParams(List<ParameterSearchModel> param);
        Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjb();

        Task<(bool,string)> AddTrKontrakSpjb(tr_kontrak_spjb_insert data);

        #endregion

        #region Detail Item

        Task<List<tr_kontrak_spjb_detail_item>> GetTrKontrakSpjbDetailItemByKontrakId(long kontrak_id);

        #endregion


        #region Detail Upload

        Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakId(long kontrak_id);
        Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(long kontrak_detail_upload_id);
        Task<List<tr_kontrak_spjb_detail_upload>> GetAllTrKontrakSpjbDetailUpload();
        Task<long> AddTrKontrakSpjbDetailUpload(tr_kontrak_spjb_detail_upload_insert datas);
        Task<short> DeleteTrKontrakSpjbDetailUpload(long kontrak_detail_upload_id);

        #endregion

    }

    public class TransKontrakSpjbService : ITransKontrakSpjbService
    {
        private readonly SQLConn _db;
        private readonly TransKontrakSpjbDao _dao;
        private readonly SetupUkuranDokumenDao _dokumenDao;
        private readonly MasterCounterDao _kodeDao;

        public TransKontrakSpjbService(SQLConn db, TransKontrakSpjbDao dao, SetupUkuranDokumenDao dokumenDao,
            MasterCounterDao kodeDao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
            this._dokumenDao = dokumenDao;
        }


        #region Header

        public async Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjbByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrKontrakSpjbByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_kontrak_spjb>> GetTrKontrakSpjbById(long kontrak_id)
        {
            try
            {
                return await this._dao.GetTrKontrakSpjbById(kontrak_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjb()
        {
            try
            {
                return await this._dao.GetAllTrKontrakSpjb();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool,string)> AddTrKontrakSpjb(tr_kontrak_spjb_insert data)
        {
            this._db.beginTransaction();
            try
            {


                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeSPJB,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = "TRANSAKSI KONTRAK SPJB"
                };

                //generate nomor kontrak
                var noKontrak = this._kodeDao.GenerateKode(dataCounter).Result;

                if (string.IsNullOrEmpty(noKontrak))
                {
                    this._db.rollBackTrans();
                    return (false,"Gagal mendapatkan nomor kontrak");

                    throw new Exception("Gagal mendapatkan nomor kontrak");
                }
                else
                {
                    data.nomor_kontrak_spjb = noKontrak;
                }


                if (data.details.Count > 0)
                {
                    data.total_transaksi_kontrak = 0;
                    data.jumlah_item_kontrak = 0;

                    foreach (var item in data.details)
                    {

                        item.qty_kontrak = item.isi * item.qty_kontrak_satuan_besar;
                        item.sub_total_kontrak = item.qty_kontrak * item.harga_satuan;

                        data.jumlah_item_kontrak += item.qty_kontrak_satuan_besar;
                        data.total_transaksi_kontrak += item.sub_total_kontrak;
                    }
                }

                //input header kontrak spjb
                var kontrakId = await this._dao.AddTrKontrakSpjb(data);

                //cek jika sukses
                if (kontrakId > 0)
                {
                    //untuk input detail item kontrak spjb
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.kontrak_id = kontrakId;
                            var kontrakDetailId = await this._dao.AddTrKontrakSpjbDetailItem(detail);

                            //cek jika gagal
                            if (kontrakDetailId <= 0)
                            {
                                kontrakId = kontrakDetailId;


                                this._db.rollBackTrans();
                                return (false, "Gagal input detail kontrak SPJB");

                                throw new Exception("Gagal input detail kontrak SPJB");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, "Gagal update nomor kontrak");

                        throw new Exception("Gagal update nomor kontrak");
                    }
                }
                else
                {
                    kontrakId = 0;

                    this._db.rollBackTrans();
                    return (false, "Gagal input kontrak SPJB");

                    throw new Exception("Gagal input kontrak SPJB");
                }


                this._db.commitTrans();
                return (true,"SUCCESS");
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

        public async Task<List<tr_kontrak_spjb_detail_item>> GetTrKontrakSpjbDetailItemByKontrakId(long kontrak_id)
        {
            try
            {
                return await this._dao.GetTrKontrakSpjbDetailItemByKontrakId(kontrak_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail Upload

        public async Task<long> AddTrKontrakSpjbDetailUpload(tr_kontrak_spjb_detail_upload_insert data)
        {
            try
            {
                long kontrakDetailUploadId = 0;

                kontrakDetailUploadId = await this._dao.AddTrKontrakSpjbDetailUpload(data);

                return kontrakDetailUploadId;

            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteTrKontrakSpjbDetailUpload(long kontrak_detail_upload_id)
        {
            try
            {
                short deleteBerkas = (short)0;

                var dataBerkas = await this._dao.GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(kontrak_detail_upload_id);

                if (dataBerkas.Count > 0)
                {
                    deleteBerkas = await this._dao.DeleteTrKontrakSpjbDetailUpload(kontrak_detail_upload_id);

                    if (deleteBerkas > 0)
                    {
                        //hapus gambar dari bucket minio
                        await UploadHelper.RemoveObjectFromBucketByPath(dataBerkas[0].path_dokumen);
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

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakId(long kontrak_id)
        {
            try
            {
                return await this._dao.GetTrKontrakSpjbDetailUploadByKontrakId(kontrak_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(long kontrak_detail_upload_id)
        {
            try
            {
                return await this._dao.GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(kontrak_detail_upload_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetAllTrKontrakSpjbDetailUpload()
        {
            try
            {
                return await this._dao.GetAllTrKontrakSpjbDetailUpload();
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
