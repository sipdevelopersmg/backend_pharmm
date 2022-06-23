using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Transaksi
{
    public interface ITransPemesananService
    {

        #region Lookup

        Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplier(short _id_supplier);
        Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplierAndParams(short _id_supplier,
            List<ParameterSearchModel> param);

        //untuk lookup penerimaan
        Task<List<tr_pemesanan>> GetAllTrPemesananLookupForPenerimaan(List<ParameterSearchModel> param);
        Task<List<tr_pemesanan_detail>> GetTrPemesananDetailLookupForPenerimaanByPemesananId(long pemesanan_id);

        #endregion


        #region Header

        Task<List<tr_pemesanan>> GetAllTrPemesananByParams(List<ParameterSearchModel> param);
        Task<List<tr_pemesanan>> GetAllTrPemesanan();
        Task<tr_pemesanan> GetTrPemesananById(long pemesanan_id);

        Task<(bool, string)> AddTrPemesanan(tr_pemesanan_insert data);

        Task<(bool, string)> UpdateToValidatedTrPemesanan(tr_pemesanan_update_status_to_validated data);
        Task<short> UpdateToClosedTrPemesanan(tr_pemesanan_update_status_to_closed data);
        Task<short> UpdateToCanceledTrPemesanan(tr_pemesanan_update_status_to_canceled data);

        #endregion

        #region Detail

        Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByPemesananIdAndParams(long pemesanan_id, List<ParameterSearchModel> param);
        Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByParams(List<ParameterSearchModel> param);
        Task<List<tr_pemesanan_detail>> GetTrPemesananDetailByPemesananId(long pemesanan_id);
        Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetail();

        #endregion

    }

    public class TransPemesananService : ITransPemesananService
    {
        private readonly SQLConn _db;
        private readonly TransPemesananDao _dao;
        private readonly MasterCounterDao _kodeDao;
        private readonly TransKontrakSpjbDao _kontrakDao;

        public TransPemesananService(SQLConn db, TransPemesananDao dao,
            TransKontrakSpjbDao kontrakDao,
            MasterCounterDao kodeDao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
            this._kontrakDao = kontrakDao;
        }

        #region Lookup

        public async Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplier(short _id_supplier)
        {
            try
            {
                return await this._dao.GetLookupBarangBelumPoActiveByIdSupplier(_id_supplier);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplierAndParams(short _id_supplier,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetLookupBarangBelumPoActiveByIdSupplierAndParams(_id_supplier, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemesanan>> GetAllTrPemesananLookupForPenerimaan(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemesananLookupForPenerimaan(param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Header

        public async Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByPemesananIdAndParams(long pemesanan_id, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemesananDetailByPemesananIdAndParams(pemesanan_id, param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemesanan>> GetAllTrPemesananByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemesananByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemesanan>> GetAllTrPemesanan()
        {
            try
            {
                return await this._dao.GetAllTrPemesanan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<tr_pemesanan> GetTrPemesananById(long pemesanan_id)
        {
            try
            {
                return await this._dao.GetTrPemesananById(pemesanan_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        //penjagaan qty pesanan 
        public async Task<(bool, string)> ValidateQtyPesanan(long kontrak_detail_item_id, decimal qty_pesan)
        {

            try
            {
                if (kontrak_detail_item_id != 0)
                {
                    //sisa qty pesan dan terima
                    var cekSisaQtyPesan = await this._kontrakDao.GetSisaQtyPesanDanTerima(kontrak_detail_item_id);

                    if (qty_pesan > cekSisaQtyPesan.sisa_qty_pesan)
                    {
                        this._db.rollBackTrans();
                        return (false, $"Qty pesan tidak boleh lebih besar dari sisa qty pesan (Sisa qty pesan untuk {cekSisaQtyPesan.nama_item} adalah {cekSisaQtyPesan.sisa_qty_pesan.ToString()})");
                    }
                }
                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task<(bool,string)> AddTrPemesanan(tr_pemesanan_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePemesanan,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor pemesanan
                var noPemesanan = this._kodeDao.GenerateKode(dataCounter).Result;


                long pemesananId = 0;
                long pemesananDetailId = 0;

                if (string.IsNullOrEmpty(noPemesanan))
                {
                    this._db.rollBackTrans();
                    return(false,"Gagal mendapatkan nomor pemesanan");

                }
                else
                {
                    data.nomor_pemesanan = noPemesanan;
                }

                if (data.details.Count > 0)
                {
                    data.sub_total_1 = 0;
                    data.sub_total_2 = 0;
                    data.total_transaksi_pesan = 0;
                    data.jumlah_item_pesan = 0;

                    foreach (var item in data.details)
                    {
                        item.qty_pesan = item.qty_satuan_besar * item.isi;
                        item.sub_total_pesan = item.qty_pesan * item.harga_satuan;


                        data.sub_total_1 += item.sub_total_pesan;
                        data.sub_total_2 += item.sub_total_pesan;
                        data.total_transaksi_pesan += item.sub_total_pesan;
                        data.jumlah_item_pesan += item.qty_satuan_besar;
                    }
                }

                pemesananId = await this._dao.AddTrPemesanan(data);

                if (pemesananId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            if (detail.kontrak_detail_item_id is not null)
                            {
                                (bool result,string message) = await ValidateQtyPesanan((long)detail.kontrak_detail_item_id, (decimal)detail.qty_pesan);

                                if (result == false)
                                {
                                    return (result, message);
                                }
                            }

                            detail.pemesanan_id = pemesananId;
                            pemesananDetailId = await this._dao.AddTrPemesananDetail(detail);

                            if (pemesananDetailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return(false,"Gagal input pemesanan detail dengan no urut " + detail.no_urut.ToString());

                                throw new Exception("Gagal input pemesanan detail dengan no urut " + detail.no_urut.ToString());
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        this._db.rollBackTrans();
                        return (false,"Gagal update nomor pemesanan");

                        throw new Exception("Gagal update nomor pemesanan");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Gagal menyimpan pemesanan barang");
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


        public async Task<(bool,string)> UpdateToValidatedTrPemesanan(tr_pemesanan_update_status_to_validated data)
        {
            this._db.beginTransaction();
            try
            {
                var pemesananHeaderData = await this._dao.GetTrPemesananByIdWithLock(data.pemesanan_id);

                if(pemesananHeaderData is not null)
                {

                    #region Cek Status Transaksi

                    if (pemesananHeaderData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah divalidasi");
                    }
                    else if (pemesananHeaderData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah dibatalkan");
                    }
                    #endregion
                }

                var validate = await this._dao.UpdateToValidatedTrPemesanan(data);

                if (validate > 0)
                {
                    //get pemesanan data
                    var pemesananData = await this._dao.GetTrPemesananDetailByPemesananIdWithLock(data.pemesanan_id);

                    if (pemesananData.Count > 0)
                    {
                        foreach (var item in pemesananData)
                        {
                            if (item.kontrak_detail_item_id is not null)
                            {
                                (bool result,string message) = await ValidateQtyPesanan((long)item.kontrak_detail_item_id, (decimal)item.qty_pesan);

                                if (result == false)
                                {
                                    return (result, message);
                                }

                                //untuk update pemesanan di kontrak header
                                var updateKontrakHeader = await this._kontrakDao.UpdatePenambahanPesanHeader(
                                    new tr_kontrak_spjb_update_penambahan_pesan
                                    {
                                        kontrak_id = (long)item.kontrak_id,
                                        jumlah_item_pesan = item.qty_pesan,
                                        total_transaksi_pesan = item.sub_total_pesan
                                    });

                                if (updateKontrakHeader <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false,"Gagal update penambahan pesanan di tabel kontrak");

                                    throw new Exception("Gagal update penambahan pesanan di tabel kontrak");
                                }

                                //untuk update pemesanan di kontrak header
                                var updateKontrakDetail = await this._kontrakDao.UpdatePenambahanPesanDetail(
                                    new tr_kontrak_spjb_detail_update_penambahan_pesan
                                    {
                                        kontrak_id = (long)item.kontrak_id,
                                        kontrak_detail_item_id = (long)item.kontrak_detail_item_id,
                                        qty_pesan = item.qty_pesan,
                                        sub_total_pesan = item.sub_total_pesan
                                    });

                                if (updateKontrakDetail <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false,"Gagal update penambahan pesanan di tabel kontrak detail");

                                    throw new Exception("Gagal update penambahan pesanan di tabel kontrak detail");
                                }
                            }
                        }
                    }
                    else
                    {
                        this._db.rollBackTrans();
                        return (false,"Pemesanan ini sudah tidak tersedia");

                        throw new Exception("Pemesanan ini sudah tidak tersedia");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Gagal validasi pemesanan barang");
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

        public async Task<short> UpdateToClosedTrPemesanan(tr_pemesanan_update_status_to_closed data)
        {
            try
            {
                return await this._dao.UpdateToClosedTrPemesanan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateToCanceledTrPemesanan(tr_pemesanan_update_status_to_canceled data)
        {
            try
            {
                return await this._dao.UpdateToCanceledTrPemesanan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail


        public async Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPemesananDetailByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemesanan_detail>> GetTrPemesananDetailByPemesananId(long pemesanan_id)
        {
            try
            {
                return await this._dao.GetTrPemesananDetailByPemesananId(pemesanan_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_pemesanan_detail>> GetTrPemesananDetailLookupForPenerimaanByPemesananId(long pemesanan_id)
        {
            try
            {
                return await this._dao.GetTrPemesananDetailLookupForPenerimaanByPemesananId(pemesanan_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetail()
        {
            try
            {
                return await this._dao.GetAllTrPemesananDetail();
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
