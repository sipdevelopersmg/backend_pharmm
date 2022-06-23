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
    public interface ISetupGrupCoaService
    {
        Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoaByParams(List<ParameterSearchModel> param);
        Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoa();
        Task<List<akun_setup_grup_coa>> GetAkunSetupGrupCoaById(Int16 id_grup_coa);

        Task<short>AddAkunSetupGrupCoa(akun_setup_grup_coa_insert data);
        Task<short>UpdateAkunSetupGrupCoa(akun_setup_grup_coa data);
        Task<short> UpdateToActiveAkunSetupGrupCoa(short _id_grup_coa);
        Task<short> UpdateToDeActiveAkunSetupGrupCoa(short _id_grup_coa);
        Task<short>DeleteAkunSetupGrupCoa(Int16 id_grup_coa);

    }

    public class SetupGrupCoaService : ISetupGrupCoaService
    {
        private readonly SQLConn _db;
        private readonly SetupGrupCoaDao _dao;

        public SetupGrupCoaService(SQLConn db, SetupGrupCoaDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoaByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllAkunSetupGrupCoaByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoa()
        {
            try
            {
                return await this._dao.GetAllAkunSetupGrupCoa();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<akun_setup_grup_coa>> GetAkunSetupGrupCoaById(Int16 id_grup_coa)
        {
            try
            {
                return await this._dao.GetAkunSetupGrupCoaById(id_grup_coa);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddAkunSetupGrupCoa(akun_setup_grup_coa_insert data)
        {
            try
            {
                return await this._dao.AddAkunSetupGrupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateAkunSetupGrupCoa(akun_setup_grup_coa data)
        {
            try
            {
                return await this._dao.UpdateAkunSetupGrupCoa(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateToDeActiveAkunSetupGrupCoa(short _id_grup_coa)
        {
            try
            {
                return await this._dao.UpdateToDeActiveAkunSetupGrupCoa(_id_grup_coa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<short> UpdateToActiveAkunSetupGrupCoa(short _id_grup_coa)
        {
            try
            {
                return await this._dao.UpdateToActiveAkunSetupGrupCoa(_id_grup_coa);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<short>DeleteAkunSetupGrupCoa(Int16 id_grup_coa)
        {
            try
            {
                return await this._dao.DeleteAkunSetupGrupCoa(id_grup_coa);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
