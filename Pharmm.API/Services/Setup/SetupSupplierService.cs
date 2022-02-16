using DapperPostgreSQL;
using Pharmm.API.Dao;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupSupplierService
    {

        Task<List<mm_setup_supplier>> GetAllMmSetupSupplierByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_supplier>> GetAllMmSetupSupplier();
        Task<List<mm_setup_supplier>> GetMmSetupSupplierById(Int16 id_supplier);

        Task<short>AddMmSetupSupplier(mm_setup_supplier_insert data);
        Task<short>UpdateMmSetupSupplier(mm_setup_supplier_update data);
        Task<short>UpdateToActiveMmSetupSupplier(mm_setup_supplier_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupSupplier(mm_setup_supplier_update_status_to_deactive data);

    }

    public class SetupSupplierService : ISetupSupplierService
    {
        private readonly SQLConn _db;
        private readonly SetupSupplierDao _dao;

        public SetupSupplierService(SQLConn db, SetupSupplierDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_supplier>> GetAllMmSetupSupplierByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupSupplierByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_setup_supplier>> GetAllMmSetupSupplier()
        {
            try
            {
                return await this._dao.GetAllMmSetupSupplier();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_supplier>> GetMmSetupSupplierById(Int16 id_supplier)
        {
            try
            {
                return await this._dao.GetMmSetupSupplierById(id_supplier);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupSupplier(mm_setup_supplier_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupSupplier(mm_setup_supplier_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupSupplier(mm_setup_supplier_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSupplier(mm_setup_supplier_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
