using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupMekanismeReturService
    {
        Task<List<mm_setup_mekanisme_retur>> GetAllMmSetupMekanismeRetur();

    }

    public class SetupMekanismeReturService : ISetupMekanismeReturService
    {
        private readonly SQLConn _db;
        private readonly SetupMekanismeReturDao _dao;

        public SetupMekanismeReturService(SQLConn db, SetupMekanismeReturDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_mekanisme_retur>> GetAllMmSetupMekanismeRetur()
        {
            try
            {
                return await this._dao.GetAllMmSetupMekanismeRetur();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
