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
    public class TransAuditStokOpnameTanpaSettingDao
    {
        public SQLConn db;

        public TransAuditStokOpnameTanpaSettingDao(SQLConn db)
        {
            this.db = db;
        }


        #region Lookup


        public async Task<List<tr_audit_stok_opname_no_setting_lookup_barang_header>> GetLookupBarangByIdStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_param data
            )
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(data.param);

                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_lookup_barang_header>(
                    "tr_audit_stok_opname_get_item_by_idgudang_waktu_capture_params", new
                    {
                        _id_stockroom = data.id_stockroom,
                        _waktu_capture = data.waktu_capture,
                        _filters
                    });
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
                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_lookup_barang_batch>(
                    "tr_audit_stok_opname_get_batch_by_iditem_idgudang_waktu_capture", new
                    {
                        _id_item = param.id_item,
                        _id_stockroom = param.id_stockroom,
                        _waktu_capture = param.waktu_capture
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_header>("tr_audit_stok_opname_no_setting_header_GetByDynamicFilters",
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
        

        //untuk get audit stok opname yang masih open
        public async Task<List<tr_audit_stok_opname_no_setting_header>> GetAllTrAuditStokOpnameHeaderOpenByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_header>("tr_audit_stok_opname_no_setting_header_open_GetByDynamicFilters",
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

        #endregion

        #region Header

        public async Task<tr_audit_stok_opname_header_recursive> GetTrAuditStokOpnameHeaderById(long audit_stok_opname_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_audit_stok_opname_header_recursive>("tr_audit_stok_opname_header_GetById", new
                {
                    _audit_stok_opname_id = audit_stok_opname_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_audit_stok_opname_no_setting_header_recursive> GetTrAuditStokOpnameHeaderByIdWithLock(long audit_stok_opname_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_audit_stok_opname_no_setting_header_recursive>("tr_audit_stok_opname_no_setting_header_getbyid_lock", new
                {
                    _audit_stok_opname_id = audit_stok_opname_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrAuditStokOpnameHeader(tr_audit_stok_opname_no_setting_header_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_no_setting_header_insert",
                    new
                    {
                        _nomor_audit_stok_opname = data.nomor_audit_stok_opname,
                        _id_stockroom = data.id_stockroom,
                        _id_grup_item = data.id_grup_item,
                        _waktu_capture_stok = data.waktu_capture_stok,
                        _jumlah_item_fisik = data.jumlah_item_fisik,
                        _total_nominal_fisik = data.total_nominal_fisik,
                        _jumlah_item_sistem_capture_stok = data.jumlah_item_sistem_capture_stok,
                        _total_nominal_sistem_capture_stok = data.total_nominal_sistem_capture_stok,
                        _keterangan_entry = data.keterangan_entry,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrAuditStokOpnameDetail(tr_audit_stok_opname_no_setting_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_detail_Insert",
                    new
                    {
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_fisik = data.qty_fisik,
                        _qty_sistem_capture_stok = data.qty_sistem_capture_stok,
                        _keterangan = data.keterangan,
                        _hpp_average = data.hpp_average,
                        _harga_jual = data.harga_jual,
                        _sub_total_fisik = data.sub_total_fisik,
                        _sub_total_sistem_capture_stok = data.sub_total_sistem_capture_stok
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrAuditStokOpnameDetailBatch(tr_audit_stok_opname_no_setting_detail_batch_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_detail_batch_Insert",
                    new
                    {
                        _audit_stok_opname_detail_id = data.audit_stok_opname_detail_id,
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _qty_fisik = data.qty_fisik,
                        _qty_sistem_capture_stok = data.qty_sistem_capture_stok,
                        _keterangan = data.keterangan,
                        _hpp_average = data.hpp_average,
                        _harga_jual = data.harga_jual,
                        _sub_total_fisik = data.sub_total_fisik,
                        _sub_total_sistem_capture_stok = data.sub_total_sistem_capture_stok
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> UpdateFinalisasi(tr_audit_stok_opname_no_setting_header_update_after_proses data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_header_update_after_proses",
                    new
                    {
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _keterangan_proses = data.keterangan_proses,
                        _user_proses = data.user_proses
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdateAdjustment(tr_audit_stok_opname_no_setting_header_update_after_adjust data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_header_update_after_adjust",
                    new
                    {
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _waktu_capture_stok_adj = data.waktu_capture_stok_adj,
                        _jumlah_item_fisik_adj = data.jumlah_item_fisik_adj,
                        _total_nominal_fisik_adj = data.total_nominal_fisik_adj,
                        _jumlah_item_sistem_capture_stok_adj = data.jumlah_item_sistem_capture_stok_adj,
                        _total_nominal_sistem_capture_stok_adj = data.total_nominal_sistem_capture_stok_adj,
                        _keterangan_adj = data.keterangan_adj,
                        _user_adj = data.user_adj
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }



        #endregion


        #region Detail

        public async Task<List<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailByHeaderIdAndWaktuCapture(
            tr_audit_stok_opname_no_setting_detail_getby_headerid_and_waktu param)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>("tr_audit_stok_opname_detail_getby_headerid_and_waktu", new
                {
                    _audit_stok_opname_id = param.audit_stok_opname_id,
                    _waktu_capture = param.waktu_capture
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdateDetailAdjustment(tr_audit_stok_opname_no_setting_detail_update_after_adjust data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_detail_update_after_adjust",
                    new
                    {
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _audit_stok_opname_detail_id = data.audit_stok_opname_detail_id,
                        _qty_fisik_adj = data.qty_fisik_adj,
                        _qty_sistem_capture_stok_adj = data.qty_sistem_capture_stok_adj,
                        _sub_total_fisik_adj = data.sub_total_fisik_adj,
                        _sub_total_sistem_capture_stok_adj = data.sub_total_sistem_capture_stok_adj
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Detail Batch

        public async Task<List<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>> GetTrAuditStokOpnameDetailBatchByDetailIdAndWaktuCapture(
            tr_audit_stok_opname_no_setting_detail_batch_getby_detailid_and_waktu param)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>("tr_audit_stok_opname_detail_batch_getby_detailid_and_waktu", new
                {
                    _audit_stok_opname_detail_id = param.audit_stok_opname_detail_id,
                    _waktu_capture = param.waktu_capture
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> UpdateDetailBatchAdjustment(tr_audit_stok_opname_no_setting_detail_batch_update_after_adjust data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_audit_stok_opname_detail_batch_update_after_adjust",
                    new
                    {
                        _audit_stok_opname_id = data.audit_stok_opname_id,
                        _audit_stok_opname_detail_id = data.audit_stok_opname_detail_id,
                        _audit_stok_opname_detail_batch_id = data.audit_stok_opname_detail_batch_id,
                        _qty_fisik_adj = data.qty_fisik_adj,
                        _qty_sistem_capture_stok_adj = data.qty_sistem_capture_stok_adj,
                        _sub_total_fisik_adj = data.sub_total_fisik_adj,
                        _sub_total_sistem_capture_stok_adj = data.sub_total_sistem_capture_stok_adj
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
