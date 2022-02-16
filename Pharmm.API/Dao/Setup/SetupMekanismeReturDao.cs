using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupMekanismeReturDao
    {
        public SQLConn db;

        public SetupMekanismeReturDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_mekanisme_retur>> GetAllMmSetupMekanismeRetur()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_mekanisme_retur>("mm_setup_mekanisme_retur_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
