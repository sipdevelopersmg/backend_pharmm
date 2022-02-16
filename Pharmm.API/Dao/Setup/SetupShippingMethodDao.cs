using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupShippingMethodDao
    {
        public SQLConn db;

        public SetupShippingMethodDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_shipping_method>> GetAllMmSetupShippingMethod()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_shipping_method>("mm_setup_shipping_method_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
