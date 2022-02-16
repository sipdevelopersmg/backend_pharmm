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
    public interface ISetupSupplierRekeningService
    {

        Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekeningByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekening();
        Task<List<mm_setup_supplier_rekening>> GetMmSetupSupplierRekeningById(Int16 id_supplier_rekening);

        Task<short>AddMmSetupSupplierRekening(mm_setup_supplier_rekening_insert data);
        Task<short>UpdateMmSetupSupplierRekening(mm_setup_supplier_rekening_update data);
        Task<short>UpdateToActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_deactive data);

    }

    public class SetupSupplierRekeningService : ISetupSupplierRekeningService
    {
        private readonly SQLConn _db;
        private readonly SetupSupplierRekeningDao _dao;

        public SetupSupplierRekeningService(SQLConn db, SetupSupplierRekeningDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekeningByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupSupplierRekeningByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekening()
        {
            try
            {
                return await this._dao.GetAllMmSetupSupplierRekening();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_supplier_rekening>> GetMmSetupSupplierRekeningById(Int16 id_supplier_rekening)
        {
            try
            {
                return await this._dao.GetMmSetupSupplierRekeningById(id_supplier_rekening);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupSupplierRekening(mm_setup_supplier_rekening_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupSupplierRekening(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupSupplierRekening(mm_setup_supplier_rekening_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupSupplierRekening(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupSupplierRekening(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupSupplierRekening(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
