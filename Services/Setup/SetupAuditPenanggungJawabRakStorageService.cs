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
    public interface ISetupAuditPenanggungJawabRakStorageService
    {

        Task<List<mm_setup_audit_penanggung_jawab_rak_storage>> GetAllMmSetupAuditPenanggungJawabRakStorageByParams(List<ParameterSearchModel> param);
        Task<mm_setup_audit_penanggung_jawab_rak_storage> GetMmSetupAuditPenanggungJawabRakStorageById(int id_penanggung_jawab_rak_storage);

        Task<short> AddMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_insert data);
        Task<short> UpdateMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_update data);
         Task<short> DeleteMmSetupAuditPenanggungJawabRakStorage(int id_penanggung_jawab_rak_storage);

    }

    public class SetupAuditPenanggungJawabRakStorageService : ISetupAuditPenanggungJawabRakStorageService
    {
        private readonly SQLConn _db;
        private readonly SetupAuditPenanggungJawabRakStorageDao _dao;

        public SetupAuditPenanggungJawabRakStorageService(SQLConn db, SetupAuditPenanggungJawabRakStorageDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_audit_penanggung_jawab_rak_storage>> GetAllMmSetupAuditPenanggungJawabRakStorageByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditPenanggungJawabRakStorageByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<mm_setup_audit_penanggung_jawab_rak_storage> GetMmSetupAuditPenanggungJawabRakStorageById(int id_penanggung_jawab_rak_storage)
        {
            try
            {
                return await this._dao.GetMmSetupAuditPenanggungJawabRakStorageById(id_penanggung_jawab_rak_storage);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupAuditPenanggungJawabRakStorage(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupAuditPenanggungJawabRakStorage(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteMmSetupAuditPenanggungJawabRakStorage(int id_penanggung_jawab_rak_storage)
        {
            try
            {
                return await this._dao.DeleteMmSetupAuditPenanggungJawabRakStorage(id_penanggung_jawab_rak_storage);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
