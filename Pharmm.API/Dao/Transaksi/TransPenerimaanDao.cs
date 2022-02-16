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
    public class TransPenerimaanDao
    {
        public SQLConn db;

        public TransPenerimaanDao(SQLConn db)
        {
            this.db = db;
        }

        #region header

        public async Task<tr_penerimaan_get_sisa_qty_retur> GetSisaQtyRetur(long _penerimaan_detail_item_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_penerimaan_get_sisa_qty_retur>("tr_penerimaan_get_sisa_qty_retur_by_penerimaan_detail_id", new
                {
                    _penerimaan_detail_item_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_penerimaan>> GetAllTrPenerimaanByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_penerimaan>("tr_penerimaan_GetByDynamicFilters",
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

        public async Task<tr_penerimaan> GetTrPenerimaanById(long penerimaan_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_penerimaan>("tr_penerimaan_GetById", new
                {
                    _penerimaan_id = penerimaan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<tr_penerimaan> GetTrPenerimaanByIdWithLock(long penerimaan_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_penerimaan>("tr_penerimaan_getbyid_lock", new
                {
                    _penerimaan_id = penerimaan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<tr_penerimaan>> GetAllTrPenerimaan()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan>("tr_penerimaan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPenerimaan(tr_penerimaan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_penerimaan_Insert",
                    new
                    {
                        _nomor_penerimaan = data.nomor_penerimaan,
                        _tanggal_penerimaan = data.tanggal_penerimaan,
                        _kode_jenis_penerimaan = data.kode_jenis_penerimaan,
                        _id_stockroom = data.id_stockroom,
                        _id_supplier = data.id_supplier,
                        _pemesanan_id = data.pemesanan_id,
                        _nomor_surat_jalan_supplier = data.nomor_surat_jalan_supplier,
                        _tanggal_surat_jalan_supplier = data.tanggal_surat_jalan_supplier,
                        _id_shipping_method = data.id_shipping_method,
                        _id_payment_term = data.id_payment_term,
                        _tanggal_jatuh_tempo_bayar = data.tanggal_jatuh_tempo_bayar,
                        _keterangan = data.keterangan,
                        _jumlah_item = data.jumlah_item,
                        _sub_total_1 = data.sub_total_1,
                        _total_disc = data.total_disc,
                        _sub_total_2 = data.sub_total_2,
                        _total_tax = data.total_tax,
                        _total_transaksi = data.total_transaksi,
                        _biaya_kirim = data.biaya_kirim,
                        _biaya_asuransi = data.biaya_asuransi,
                        _biaya_lain = data.biaya_lain,
                        _potongan_nominal = data.potongan_nominal,
                        _potongan_prosentase = data.potongan_prosentase,
                        _total_uang_muka = data.total_uang_muka,
                        _total_tagihan = data.total_tagihan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateQtyRetur(tr_penerimaan_detail_item_update_qty_retur data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_penerimaan_detail_item_update_qty_retur",
                    new
                    {
                        _penerimaan_detail_item_id = data.penerimaan_detail_item_id,
                        _qty_diretur = data.qty_diretur
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateToValidated(tr_penerimaan_update_status_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_penerimaan_Update_to_validated",
                    new
                    {
                        _penerimaan_id = data.penerimaan_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateToCanceled(tr_penerimaan_update_status_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_penerimaan_Update_to_canceled",
                    new
                    {
                        _penerimaan_id = data.penerimaan_id,
                        _user_canceled = data.user_canceled,
                        _reason_canceled = data.reason_canceled
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region detail item


        public async Task<List<tr_penerimaan_detail_item>> GetTrPenerimaanDetailItemByPenerimaanId(long penerimaan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_item>("tr_penerimaan_detail_item_get_by_penerimaan_id", new
                {
                    _penerimaan_id = penerimaan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_penerimaan_detail_item>> GetTrPenerimaanDetailItemByPenerimaanIdWithLock(long penerimaan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_item>("tr_penerimaan_detail_item_get_by_penerimaanid_lock", new
                {
                    _penerimaan_id = penerimaan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_penerimaan_detail_item>> GetAllTrPenerimaanDetailItem()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_item>("tr_penerimaan_detail_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPenerimaanDetailItem(tr_penerimaan_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_penerimaan_detail_item_Insert",
                    new
                    {
                        _penerimaan_id = data.penerimaan_id,
                        _pemesanan_id = data.pemesanan_id,
                        _pemesanan_detail_id = data.pemesanan_detail_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _qty_satuan_besar = data.qty_satuan_besar,
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _harga_satuan_besar = data.harga_satuan_besar,
                        _isi = data.isi,
                        _qty_terima = data.qty_terima,
                        _harga_satuan = data.harga_satuan,
                        _disc_prosentase_1 = data.disc_prosentase_1,
                        _disc_nominal_1 = data.disc_nominal_1,
                        _disc_prosentase_2 = data.disc_prosentase_2,
                        _disc_nominal_2 = data.disc_nominal_2,
                        _harga_satuan_brutto = data.harga_satuan_brutto,
                        _tax_prosentase = data.tax_prosentase,
                        _tax_nominal = data.tax_nominal,
                        _harga_satuan_netto = data.harga_satuan_netto,
                        _sub_total = data.sub_total
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region detail upload
        public async Task<List<tr_penerimaan_detail_upload>> GetTrPenerimaanDetailUploadByPenerimaanId(long penerimaan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_upload>("tr_penerimaan_detail_upload_get_by_penerimaan_id", new
                {
                    _penerimaan_id = penerimaan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_penerimaan_detail_upload>> GetTrPenerimaanDetailUploadById(long penerimaan_detail_upload_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_upload>("tr_penerimaan_detail_upload_GetById", new
                {
                    _penerimaan_detail_upload_id = penerimaan_detail_upload_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_penerimaan_detail_upload>> GetAllTrPenerimaanDetailUpload()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_penerimaan_detail_upload>("tr_penerimaan_detail_upload_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPenerimaanDetailUpload(tr_penerimaan_detail_upload_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_penerimaan_detail_upload_Insert",
                    new
                    {
                        _penerimaan_id = data.penerimaan_id,
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

        public async Task<short> DeleteTrPenerimaanDetailUpload(long penerimaan_detail_upload_id)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_penerimaan_detail_upload_Delete",
                    new
                    {
                        _penerimaan_detail_upload_id = penerimaan_detail_upload_id // int not null
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
