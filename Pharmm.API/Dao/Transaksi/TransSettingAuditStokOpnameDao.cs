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
    public class TransSettingAuditStokOpnameDao
    {
        public SQLConn db;

        public TransSettingAuditStokOpnameDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header


        public async Task<tr_setting_audit_stok_opname_header> GetTrSettingAuditStokOpnameHeaderById(long setting_stok_opname_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_setting_audit_stok_opname_header>("tr_setting_audit_stok_opname_header_GetById", new
                {
                    _setting_stok_opname_id = setting_stok_opname_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_setting_audit_stok_opname_header_recursive> GetTrSettingAuditStokOpnameHeaderByIdRecursive(long setting_stok_opname_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_setting_audit_stok_opname_header_recursive>("tr_setting_audit_stok_opname_header_GetById_recursive",
                    new
                    {
                        _setting_stok_opname_id = setting_stok_opname_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_setting_audit_stok_opname_header_recursive>> GetAllTrSettingAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_setting_audit_stok_opname_header_recursive>("tr_setting_audit_stok_opname_header_GetByDynamicFilters",
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

        public async Task<long> AddTrSettingAuditStokOpnameHeaderItem(tr_setting_audit_stok_opname_header_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_header_Insert",
                    new
                    {
                        _no_setting_stok_opname = data.no_setting_stok_opname,
                        _tanggal_stok_opname = data.tanggal_stok_opname,
                        _id_jenis_setting_stok_opname = data.id_jenis_setting_stok_opname,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrSettingAuditStokOpnameHeaderSemuaItem(tr_setting_audit_stok_opname_header_semua_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_header_Insert",
                    new
                    {
                        _no_setting_stok_opname = data.no_setting_stok_opname,
                        _tanggal_stok_opname = data.tanggal_stok_opname,
                        _id_jenis_setting_stok_opname = data.id_jenis_setting_stok_opname,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrSettingAuditStokOpnameHeaderGrup(tr_setting_audit_stok_opname_header_grup_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_header_Insert",
                    new
                    {
                        _no_setting_stok_opname = data.no_setting_stok_opname,
                        _tanggal_stok_opname = data.tanggal_stok_opname,
                        _id_jenis_setting_stok_opname = data.id_jenis_setting_stok_opname,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<long> AddTrSettingAuditStokOpnameHeaderRakStorage(tr_setting_audit_stok_opname_header_rak_storage_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_header_Insert",
                    new
                    {
                        _no_setting_stok_opname = data.no_setting_stok_opname,
                        _tanggal_stok_opname = data.tanggal_stok_opname,
                        _id_jenis_setting_stok_opname = data.id_jenis_setting_stok_opname,
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


        #region Detail Stockroom

        public async Task<List<tr_setting_audit_stok_opname_detail_stockroom>> GetAllTrSettingAuditStokOpnameDetailStockroomBySettingStokOpnameId(
            long _setting_stok_opname_id
            )
        {
            try
            {
                return await this.db.QuerySPtoList<tr_setting_audit_stok_opname_detail_stockroom>("tr_audit_stok_opname_detail_stockroom_getby_settingid",
                    new
                    {
                        _setting_stok_opname_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrSettingAuditStokOpnameDetailStockroom(tr_setting_audit_stok_opname_detail_stockroom_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_detail_stockroom_Insert",
                    new
                    {
                        _setting_stok_opname_id = data.setting_stok_opname_id,
                        _id_stockroom = data.id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrSettingAuditStokOpnameDetailRakStorage(tr_setting_audit_stok_opname_detail_rak_storage_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_detail_rak_storage_Insert",
                    new
                    {
                        _setting_stok_opname_id = data.setting_stok_opname_id,
                        _id_rak_storage = data.id_rak_storage
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrSettingAuditStokOpnameDetailItem(tr_setting_audit_stok_opname_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_detail_item_Insert",
                    new
                    {
                        _setting_stok_opname_id = data.setting_stok_opname_id,
                        _id_item = data.id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrSettingAuditStokOpnameDetailGrup(tr_setting_audit_stok_opname_detail_grup_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_setting_audit_stok_opname_detail_grup_Insert",
                    new
                    {
                        _setting_stok_opname_id = data.setting_stok_opname_id,
                        _id_grup_item = data.id_grup_item
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
