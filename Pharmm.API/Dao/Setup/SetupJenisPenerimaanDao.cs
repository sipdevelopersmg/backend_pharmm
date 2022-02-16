using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupJenisPenerimaanDao
    {
        public SQLConn db;

        public SetupJenisPenerimaanDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_jenis_penerimaan>> GetAllMmSetupJenisPenerimaan()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_jenis_penerimaan>("mm_setup_jenis_penerimaan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
