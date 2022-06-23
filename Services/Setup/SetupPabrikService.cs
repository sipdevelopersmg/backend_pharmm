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
    public interface ISetupPabrikService
    {

        Task<List<mm_setup_pabrik>> GetAllMmSetupPabrikByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_pabrik>> GetAllMmSetupPabrik();
        Task<List<mm_setup_pabrik>> GetMmSetupPabrikById(Int16 id_pabrik);

        Task<short>AddMmSetupPabrik(mm_setup_pabrik_insert data);
        Task<short>UpdateMmSetupPabrik(mm_setup_pabrik_update data);
        Task<short>UpdateToActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_deactive data);

    }

    public class SetupPabrikService : ISetupPabrikService
    {
        private readonly SQLConn _db;
        private readonly SetupPabrikDao _dao;

        public SetupPabrikService(SQLConn db, SetupPabrikDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_pabrik>> GetAllMmSetupPabrikByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupPabrikByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_pabrik>> GetAllMmSetupPabrik()
        {
            try
            {
                return await this._dao.GetAllMmSetupPabrik();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_pabrik>> GetMmSetupPabrikById(Int16 id_pabrik)
        {
            try
            {
                return await this._dao.GetMmSetupPabrikById(id_pabrik);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupPabrik(mm_setup_pabrik_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupPabrik(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupPabrik(mm_setup_pabrik_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupPabrik(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupPabrik(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupPabrik(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
