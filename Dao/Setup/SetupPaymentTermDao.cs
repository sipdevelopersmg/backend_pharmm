using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupPaymentTermDao
    {
        public SQLConn db;

        public SetupPaymentTermDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_payment_term>> GetAllMmSetupPaymentTerm()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_payment_term>("mm_setup_payment_term_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
