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
    public interface ISetupCoaService
    {

        Task<List<akun_setup_coa>> GetAllAkunSetupCoaByParams(List<ParameterSearchModel> param);
        Task<List<akun_setup_coa>> GetAllAkunSetupCoa();
        Task<List<akun_setup_coa>> GetAkunSetupCoaById(int id_coa);

        Task<short>AddAkunSetupCoa(akun_setup_coa_insert data);
        Task<short>UpdateAkunSetupCoa(akun_setup_coa_update data);
        Task<short>UpdateToActiveAkunSetupCoa(akun_setup_coa_update_status_to_active data);
        Task<short>UpdateToDeActiveAkunSetupCoa(akun_setup_coa_update_status_to_deactive data);

    }

    public class SetupCoaService : ISetupCoaService
    {
        private readonly SQLConn _db;
        private readonly SetupCoaDao _dao;

        public SetupCoaService(SQLConn db, SetupCoaDao dao)
        {
            this._db = db;
            this._dao = dao;
        }



        public async Task<List<akun_setup_coa>> GetAllAkunSetupCoaByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllAkunSetupCoaByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<akun_setup_coa>> GetAllAkunSetupCoa()
        {
            try
            {
                return await this._dao.GetAllAkunSetupCoa();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<akun_setup_coa>> GetAkunSetupCoaById(int id_coa)
        {
            try
            {
                return await this._dao.GetAkunSetupCoaById(id_coa);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddAkunSetupCoa(akun_setup_coa_insert data)
        {
            try
            {
                return await this._dao.AddAkunSetupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateAkunSetupCoa(akun_setup_coa_update data)
        {
            try
            {
                return await this._dao.UpdateAkunSetupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveAkunSetupCoa(akun_setup_coa_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveAkunSetupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveAkunSetupCoa(akun_setup_coa_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveAkunSetupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
