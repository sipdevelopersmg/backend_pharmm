using DapperPostgreSQL;
using Pharmm.API.Dao;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupTipeSupplierService
    {

        Task<List<mm_setup_tipe_supplier>> GetAllMmSetupTipeSupplier();

        Task<short>AddMmSetupTipeSupplier(mm_setup_tipe_supplier_insert data);
        Task<short>UpdateMmSetupTipeSupplier(mm_setup_tipe_supplier_update data);
        Task<short>UpdateToActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_deactive data);

    }

    public class SetupTipeSupplierService : ISetupTipeSupplierService
    {
        private readonly SQLConn _db;
        private readonly SetupTipeSupplierDao _dao;

        public SetupTipeSupplierService(SQLConn db, SetupTipeSupplierDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_tipe_supplier>> GetAllMmSetupTipeSupplier()
        {
            try
            {
                return await this._dao.GetAllMmSetupTipeSupplier();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupTipeSupplier(mm_setup_tipe_supplier_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupTipeSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupTipeSupplier(mm_setup_tipe_supplier_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupTipeSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupTipeSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupTipeSupplier(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
