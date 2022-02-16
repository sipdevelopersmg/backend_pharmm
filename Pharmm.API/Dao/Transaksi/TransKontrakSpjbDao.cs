using DapperPostgreSQL;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Transaksi
{
    public class TransKontrakSpjbDao
    {
        public SQLConn db;

        public TransKontrakSpjbDao(SQLConn db)
        {
            this.db = db;
        }


        #region Update

        public async Task<long> UpdatePenambahanPesanHeader(tr_kontrak_spjb_update_penambahan_pesan data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_update_penambahan_pemesanan",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _jumlah_item_pesan = data.jumlah_item_pesan,
                        _total_transaksi_pesan = data.total_transaksi_pesan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdatePenambahanPesanDetail(tr_kontrak_spjb_detail_update_penambahan_pesan data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_detail_update_penambahan_pemesanan",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _kontrak_detail_item_id = data.kontrak_detail_item_id,
                        _qty_pesan = data.qty_pesan,
                        _sub_total_pesan = data.sub_total_pesan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdatePenambahanTerimaHeader(tr_kontrak_spjb_update_penambahan_terima data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_update_penambahan_penerimaan",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _jumlah_item_terima = data.jumlah_item_terima,
                        _total_transaksi_terima = data.total_transaksi_terima
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdatePenambahanTerimaDetail(tr_kontrak_spjb_detail_update_penambahan_terima data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_detail_update_penambahan_penerimaan",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _kontrak_detail_item_id = data.kontrak_detail_item_id,
                        _qty_terima = data.qty_terima,
                        _sub_total_terima = data.sub_total_terima
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Header

        public async Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjbByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_kontrak_spjb>("tr_kontrak_spjb_GetByDynamicFilters",
                    new
                    {
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_kontrak_spjb>> GetTrKontrakSpjbById(long kontrak_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb>("tr_kontrak_spjb_GetById", new
                {
                    _kontrak_id = kontrak_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_kontrak_spjb>> GetAllTrKontrakSpjb()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb>("tr_kontrak_spjb_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrKontrakSpjb(tr_kontrak_spjb_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_Insert",
                    new
                    {
                        _id_supplier = data.id_supplier,
                        _nomor_kontrak_spjb = data.nomor_kontrak_spjb,
                        _nomor_kontrak = data.nomor_kontrak,
                        _judul_kontrak = data.judul_kontrak,
                        _tanggal_ttd_kontrak = data.tanggal_ttd_kontrak,
                        _tanggal_berlaku_kontrak = data.tanggal_berlaku_kontrak,
                        _tanggal_berakhir_kontrak = data.tanggal_berakhir_kontrak,
                        _jumlah_item_kontrak = data.jumlah_item_kontrak,
                        //_jumlah_item_pesan = data.jumlah_item_pesan,
                        //_jumlah_item_terima = data.jumlah_item_terima,
                        _total_transaksi_kontrak = data.total_transaksi_kontrak,
                        //_total_transaksi_pesan = data.total_transaksi_pesan,
                        //_total_transaksi_terima = data.total_transaksi_terima,
                        _tahun_anggaran = data.tahun_anggaran,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Detail

        public async Task<List<tr_kontrak_spjb_detail_item>> GetTrKontrakSpjbDetailItemByKontrakId(long kontrak_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb_detail_item>("tr_kontrak_spjb_detail_item_Get_By_kontrak_id", new
                {
                    _kontrak_id = kontrak_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_kontrak_spjb_get_sisa_qty> GetSisaQtyPesanDanTerima(long _kontrak_detail_item_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_kontrak_spjb_get_sisa_qty>("tr_kontrak_spjb_get_sisa_qty_by_kontrak_detail_id", new
                {
                    _kontrak_detail_item_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrKontrakSpjbDetailItem(tr_kontrak_spjb_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_detail_item_Insert",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _tanggal_maksimal_expired_date = data.tanggal_maksimal_expired_date,
                        _qty_kontrak_satuan_besar = data.qty_kontrak_satuan_besar,
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _isi = data.isi,
                        _qty_kontrak = data.qty_kontrak,
                        //_qty_pesan = data.qty_pesan,
                        //_qty_terima = data.qty_terima,
                        _harga_satuan = data.harga_satuan,
                        _sub_total_kontrak = data.sub_total_kontrak,
                        //_sub_total_pesan = data.sub_total_pesan,
                        //_sub_total_terima = data.sub_total_terima
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Detail Upload

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakId(long kontrak_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb_detail_upload>("tr_kontrak_spjb_detail_upload_Get_by_kontrak_id", new
                {
                    _kontrak_id = kontrak_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(long kontrak_detail_upload_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb_detail_upload>("tr_kontrak_spjb_detail_upload_Get_By_kontrak_detail_upload_id", new
                {
                    _kontrak_detail_upload_id = kontrak_detail_upload_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_kontrak_spjb_detail_upload>> GetAllTrKontrakSpjbDetailUpload()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_kontrak_spjb_detail_upload>("tr_kontrak_spjb_detail_upload_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrKontrakSpjbDetailUpload(tr_kontrak_spjb_detail_upload_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_kontrak_spjb_detail_upload_Insert",
                    new
                    {
                        _kontrak_id = data.kontrak_id,
                        _jenis_dokumen = data.jenis_dokumen,
                        _url_dokumen = data.url_dokumen,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteTrKontrakSpjbDetailUpload(long? kontrak_detail_upload_id)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_kontrak_spjb_detail_upload_Delete",
                    new
                    {
                        _kontrak_detail_upload_id = kontrak_detail_upload_id // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

    }
}
