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
    public interface ISetupAuditRakStorageService
    {

        Task<List<mm_setup_audit_rak_storage>> GetAllMmSetupAuditRakStorageByParams(List<ParameterSearchModel> param);
        Task<mm_setup_audit_rak_storage> GetMmSetupAuditRakStorageById(int id_rak_storage);
        Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerRakByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id);
        Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerGrupByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id);
        Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerItemByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id);
        Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStorageAllItemByIdStockroom(
            short _id_stockroom
            );


        Task<short> AddMmSetupAuditRakStorage(mm_setup_audit_rak_storage_insert data);
        Task<short> UpdateMmSetupAuditRakStorage(mm_setup_audit_rak_storage_update data);
        Task<short> DeleteMmSetupAuditRakStorage(int id_rak_storage);

    }

    public class SetupAuditRakStorageService : ISetupAuditRakStorageService
    {
        private readonly SQLConn _db;
        private readonly SetupAuditRakStorageDao _dao;

        public SetupAuditRakStorageService(SQLConn db, SetupAuditRakStorageDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerRakByIdStockroomAndSettingStokOpnameId(short _id_stockroom, long _setting_stok_opname_id)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditRakStoragePerRakByIdStockroomAndSettingStokOpnameId(_id_stockroom, _setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerGrupByIdStockroomAndSettingStokOpnameId(short _id_stockroom, long _setting_stok_opname_id)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditRakStoragePerGrupByIdStockroomAndSettingStokOpnameId(_id_stockroom, _setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerItemByIdStockroomAndSettingStokOpnameId(short _id_stockroom, long _setting_stok_opname_id)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditRakStoragePerItemByIdStockroomAndSettingStokOpnameId(_id_stockroom, _setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStorageAllItemByIdStockroom(short _id_stockroom)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditRakStorageAllItemByIdStockroom(_id_stockroom);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<mm_setup_audit_rak_storage>> GetAllMmSetupAuditRakStorageByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupAuditRakStorageByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<mm_setup_audit_rak_storage> GetMmSetupAuditRakStorageById(int id_rak_storage)
        {
            try
            {
                return await this._dao.GetMmSetupAuditRakStorageById(id_rak_storage);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupAuditRakStorage(mm_setup_audit_rak_storage_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupAuditRakStorage(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateMmSetupAuditRakStorage(mm_setup_audit_rak_storage_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupAuditRakStorage(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteMmSetupAuditRakStorage(int id_rak_storage)
        {
            try
            {
                return await this._dao.DeleteMmSetupAuditRakStorage(id_rak_storage);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
