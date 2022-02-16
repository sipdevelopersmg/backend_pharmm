using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface IMasterCounterService
    {

        Task<string> GenerateKode(master_counter_insert data);
        Task<string> AddUpdateMasterCounter(master_counter_insert data);
    }

    public class MasterCounterService : IMasterCounterService
    {
        private readonly SQLConn _db;
        private readonly MasterCounterDao _dao;

        public MasterCounterService(SQLConn db, MasterCounterDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<string> GenerateKode(master_counter_insert data)
        {
            try
            {
                return await this._dao.GenerateKode(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<string> AddUpdateMasterCounter(master_counter_insert data)
        {
            try
            {
                return await this._dao.AddUpdateMasterCounter(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }




}
