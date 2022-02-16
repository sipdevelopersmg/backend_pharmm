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
    public class TransPemesananDao
    {
        public SQLConn db;

        public TransPemesananDao(SQLConn db)
        {
            this.db = db;
        }


        #region Update

        #region Update

        public async Task<long> UpdatePenambahanTerimaHeader(tr_pemesanan_update_penambahan_terima data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemesanan_update_penambahan_penerimaan",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
                        _jumlah_item_terima = data.jumlah_item_terima,
                        _total_transaksi_terima = data.total_transaksi_terima
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdatePenambahanTerimaDetail(tr_pemesanan_detail_update_penambahan_terima data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemesanan_detail_update_penambahan_penerimaan",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
                        _pemesanan_detail_id = data.pemesanan_detail_id,
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

        #endregion

        #region Lookup

        public async Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplier(short _id_supplier)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemesanan_lookup_barang>(
                    "tr_pemesanan_get_lookup_barang_by_id_supplier", new
                    {
                        _id_supplier
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemesanan_lookup_barang>> GetLookupBarangBelumPoActiveByIdSupplierAndParams(short _id_supplier,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemesanan_lookup_barang>(
                    "tr_pemesanan_get_lookup_barang_by_id_supplier_dynamicfilters", new
                    {
                        _id_supplier,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Header

        public async Task<tr_pemesanan_get_jumlah_total_pesan_terima> GetJumlahTotalPesanTerima(long _pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan_get_jumlah_total_pesan_terima>("tr_pemesanan_get_jumlah_total_pesan_terima", new
                {
                    _pemesanan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //untuk penerimaan
        public async Task<List<tr_pemesanan>> GetAllTrPemesananLookupForPenerimaan(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemesanan>("tr_pemesanan_get_lookup_for_penerimaan_dynamicfilters",
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

        public async Task<List<tr_pemesanan>> GetAllTrPemesananByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemesanan>("tr_pemesanan_GetByDynamicFilters",
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

        public async Task<tr_pemesanan> GetTrPemesananById(long pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan>("tr_pemesanan_GetById", new
                {
                    _pemesanan_id = pemesanan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<tr_pemesanan> GetTrPemesananByIdWithLock(long pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan>("tr_pemesanan_GetById_lock", new
                {
                    _pemesanan_id = pemesanan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemesanan>> GetAllTrPemesanan()
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemesanan>("tr_pemesanan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemesanan(tr_pemesanan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemesanan_Insert",
                    new
                    {
                        _nomor_pemesanan = data.nomor_pemesanan,
                        _tanggal_pemesanan = data.tanggal_pemesanan,
                        _tanggal_expired_pemesanan = data.tanggal_expired_pemesanan,
                        _id_stockroom = data.id_stockroom,
                        _id_supplier = data.id_supplier,
                        _keterangan = data.keterangan,
                        _jumlah_item_pesan = data.jumlah_item_pesan,
                        _sub_total_1 = data.sub_total_1,
                        _total_disc = data.total_disc,
                        _sub_total_2 = data.sub_total_2,
                        _total_tax = data.total_tax,
                        _total_transaksi_pesan = data.total_transaksi_pesan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToValidatedTrPemesanan(tr_pemesanan_update_status_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemesanan_Update_to_validated",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToClosedTrPemesanan(tr_pemesanan_update_status_to_closed data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemesanan_Update_to_closed",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
                        _user_closed = data.user_closed,
                        _reason_closed = data.reason_closed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToCanceledTrPemesanan(tr_pemesanan_update_status_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemesanan_Update_to_canceled",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
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


        #region Detail


        public async Task<tr_pemesanan_get_sisa_qty> GetSisaQtyTerima(long _pemesanan_detail_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan_get_sisa_qty>("tr_pemesanan_get_sisa_qty_by_pemesanan_detail_id", new
                {
                    _pemesanan_detail_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByPemesananIdAndParams(long pemesanan_id, List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_by_pemesananid_dynamicfilters",
                    new
                    {
                        _pemesanan_id = pemesanan_id,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemesanan_detail>> GetAllTrPemesananDetailByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_GetByDynamicFilters",
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

        public async Task<List<tr_pemesanan_detail>> GetTrPemesananDetailByPemesananId(long pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_get_by_pemesanan_id", new
                {
                    _pemesanan_id = pemesanan_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<tr_pemesanan_detail>> GetTrPemesananDetailByPemesananIdWithLock(long _pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_Getby_headerid_lock", new
                {
                    _pemesanan_id 
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_pemesanan_detail> GetTrPemesananDetailById(long _pemesanan_detail_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan_detail>("tr_pemesanan_detail_getbyid", new
                {
                    _pemesanan_detail_id 
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<tr_pemesanan_detail_lock> GetTrPemesananDetailByIdLock(long _pemesanan_detail_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemesanan_detail_lock>("tr_pemesanan_detail_Getbyid_lock", new
                {
                    _pemesanan_detail_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //untuk penerimaan
        public async Task<List<tr_pemesanan_detail>> GetTrPemesananDetailLookupForPenerimaanByPemesananId(long pemesanan_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_get_available_by_pemesanan_id", new
                {
                    _pemesanan_id = pemesanan_id
                });
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
                return await this.db.QuerySPtoList<tr_pemesanan_detail>("tr_pemesanan_detail_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemesananDetail(tr_pemesanan_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemesanan_detail_Insert",
                    new
                    {
                        _pemesanan_id = data.pemesanan_id,
                        _kontrak_id = data.kontrak_id,
                        _kontrak_detail_item_id = data.kontrak_detail_item_id,
                        _set_harga_order_id = data.set_harga_order_id,
                        _set_harga_order_detail_id = data.set_harga_order_detail_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar = data.qty_satuan_besar,
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _harga_satuan_besar = data.harga_satuan_besar,
                        _isi = data.isi,
                        _qty_pesan = data.qty_pesan,
                        _harga_satuan = data.harga_satuan,
                        _disc_prosentase_1 = data.disc_prosentase_1,
                        _disc_nominal_1 = data.disc_nominal_1,
                        _disc_prosentase_2 = data.disc_prosentase_2,
                        _disc_nominal_2 = data.disc_nominal_2,
                        _harga_satuan_brutto = data.harga_satuan_brutto,
                        _tax_prosentase = data.tax_prosentase,
                        _tax_nominal = data.tax_nominal,
                        _harga_satuan_netto = data.harga_satuan_netto,
                        _sub_total_pesan = data.sub_total_pesan
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
