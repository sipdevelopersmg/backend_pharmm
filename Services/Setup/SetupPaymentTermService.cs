using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupPaymentTermService
    {

        Task<List<mm_setup_payment_term>> GetAllMmSetupPaymentTerm();
    }

    public class SetupPaymentTermService : ISetupPaymentTermService
    {
        private readonly SQLConn _db;
        private readonly SetupPaymentTermDao _dao;

        public SetupPaymentTermService(SQLConn db, SetupPaymentTermDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_payment_term>> GetAllMmSetupPaymentTerm()
        {
            try
            {
                return await this._dao.GetAllMmSetupPaymentTerm();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
