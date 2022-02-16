using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupJenisSettingAuditStokOpnameService
    {

        Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpnameByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpname();

        Task<short> AddMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname_insert data);
        Task<short> UpdateMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname data);
        Task<short> DeleteMmSetupJenisSettingAuditStokOpname(short id_jenis_setting_stok_opname);

    }

    public class SetupJenisSettingAuditStokOpnameService : ISetupJenisSettingAuditStokOpnameService
    {
        private readonly SQLConn _db;
        private readonly SetupJenisSettingAuditStokOpnameDao _dao;

        public SetupJenisSettingAuditStokOpnameService(SQLConn db, SetupJenisSettingAuditStokOpnameDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpnameByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupJenisSettingAuditStokOpnameByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_jenis_setting_audit_stok_opname>> GetAllMmSetupJenisSettingAuditStokOpname()
        {
            try
            {
                return await this._dao.GetAllMmSetupJenisSettingAuditStokOpname();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupJenisSettingAuditStokOpname(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateMmSetupJenisSettingAuditStokOpname(mm_setup_jenis_setting_audit_stok_opname data)
        {
            try
            {
                return await this._dao.UpdateMmSetupJenisSettingAuditStokOpname(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteMmSetupJenisSettingAuditStokOpname(short id_jenis_setting_stok_opname)
        {
            try
            {
                return await this._dao.DeleteMmSetupJenisSettingAuditStokOpname(id_jenis_setting_stok_opname);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
