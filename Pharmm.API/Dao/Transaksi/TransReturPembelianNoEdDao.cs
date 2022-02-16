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
    public class TransReturPembelianNoEdDao
    {
        public SQLConn db;

        public TransReturPembelianNoEdDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<tr_retur_pembelian_no_ed_cek_status> GetStatusTransaksi(long _retur_pembelian_id)
        {
            try
            {

                return await this.db.QuerySPtoSingle<tr_retur_pembelian_no_ed_cek_status>("tr_retur_pembelian_cek_status",
                    new
                    {
                        _retur_pembelian_id    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_retur_pembelian_no_ed>> GetAllTrReturPembelianByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed>("tr_retur_pembelian_no_ed_GetByDynamicFilters",
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

        public async Task<tr_retur_pembelian_no_ed> GetTrReturPembelianById(long retur_pembelian_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_retur_pembelian_no_ed>("tr_retur_pembelian_GetById", new
                {
                    _retur_pembelian_id = retur_pembelian_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_retur_pembelian_no_ed> GetTrReturPembelianByIdWithLock(long retur_pembelian_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_retur_pembelian_no_ed>("tr_retur_pembelian_GetById_lock", new
                {
                    _retur_pembelian_id = retur_pembelian_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrReturPembelian(tr_retur_pembelian_no_ed_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_retur_pembelian_no_ed_insert",
                    new
                    {
                        _nomor_retur_pembelian = data.nomor_retur_pembelian,
                        _tanggal_retur_pembelian = data.tanggal_retur_pembelian,
                        _tanggal_jatuh_tempo_pelunasan_retur = data.tanggal_jatuh_tempo_pelunasan_retur,
                        _id_stockroom = data.id_stockroom,
                        _id_mekanisme_retur = data.id_mekanisme_retur,
                        _id_supplier = data.id_supplier,
                        _penerimaan_id = data.penerimaan_id,
                        _keterangan = data.keterangan,
                        _jumlah_item_retur = data.jumlah_item_retur,
                        _total_transaksi_retur = data.total_transaksi_retur,
                        _jumlah_item_ditukar_dengan = data.jumlah_item_ditukar_dengan,
                        _total_transaksi_ditukar_dengan = data.total_transaksi_ditukar_dengan,
                        _total_transaksi_potong_tagihan = data.total_transaksi_potong_tagihan,
                        _nota_pajak = data.nota_pajak,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToValidated(tr_retur_pembelian_no_ed_update_status_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_retur_pembelian_update_to_validated",
                    new
                    {
                        _retur_pembelian_id = data.retur_pembelian_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateToCanceled(tr_retur_pembelian_no_ed_update_status_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_retur_pembelian_Update_to_canceled",
                    new
                    {
                        _retur_pembelian_id = data.retur_pembelian_id,
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
        public async Task<List<tr_retur_pembelian_no_ed_detail>> GetAllTrReturPembelianDetailByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed_detail>("tr_retur_pembelian_detail_GetByDynamicFilters",
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

        public async Task<List<tr_retur_pembelian_no_ed_detail>> GetTrReturPembelianDetailByReturPembelianId(long retur_pembelian_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed_detail>("tr_retur_pembelian_detail_get_by_retur_pembelian_id", new
                {
                    _retur_pembelian_id = retur_pembelian_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_retur_pembelian_no_ed_detail>> GetTrReturPembelianDetailByReturPembelianIdWithLock(long retur_pembelian_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed_detail>("tr_retur_pembelian_detail_GetBy_headerid_lock", new
                {
                    _retur_pembelian_id = retur_pembelian_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrReturPembelianDetail(tr_retur_pembelian_no_ed_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_retur_pembelian_detail_no_ed_insert",
                    new
                    {
                        _retur_pembelian_id = data.retur_pembelian_id,
                        _penerimaan_id = data.penerimaan_id,
                        _penerimaan_detail_id = data.penerimaan_detail_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar = data.qty_satuan_besar,
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _isi = data.isi,
                        _qty_retur = data.qty_retur,
                        _harga_satuan_retur = data.harga_satuan_retur,
                        _sub_total = data.sub_total
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Detail Penukaran

        public async Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetAllTrReturPembelianDetailPenukaranByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed_detail_penukaran>("tr_retur_pembelian_detail_penukaran_GetByDynamicFilters",
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

        public async Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetTrReturPembelianDetailPenukaranByReturPembelianId(long retur_pembelian_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pembelian_no_ed_detail_penukaran>("tr_retur_pembelian_detail_penukaran_get_by_retur_pembelian_id", new
                {
                    _retur_pembelian_id = retur_pembelian_id
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
