using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupJenisSettingAuditStokOpnameDao
    {
        public SQLConn db;

        public SetupJenisSettingAuditStokOpnameDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpnameByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_jenis_setting_audit_stok_opname>("mm_setup_jenis_setting_audit_stok_opname_GetByDynamicFilters",
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

        public async Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpname()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_jenis_setting_audit_stok_opname>("mm_setup_jenis_setting_audit_stok_opname_Getall");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_jenis_setting_audit_stok_opname_Insert",
                    new
                    {
                        _kode_jenis_setting_stok_opname = data.kode_jenis_setting_stok_opname,
                        _nama_jenis_setting_stok_opname = data.nama_jenis_setting_stok_opname
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_jenis_setting_audit_stok_opname_Update",
                    new
                    {
                        _id_jenis_setting_stok_opname = data.id_jenis_setting_stok_opname,
                        _kode_jenis_setting_stok_opname = data.kode_jenis_setting_stok_opname,
                        _nama_jenis_setting_stok_opname = data.nama_jenis_setting_stok_opname
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteMmSetupJenisSettingAuditStokOpname(short id_jenis_setting_stok_opname)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_jenis_setting_audit_stok_opname_Delete",
                    new
                    {
                        _id_jenis_setting_stok_opname = id_jenis_setting_stok_opname // int not null
                            });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
