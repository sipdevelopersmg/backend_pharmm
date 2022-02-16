using DapperPostgreSQL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao
{
    public class LocalizationDao
    {
        private readonly SQLConn db;
        
        public LocalizationDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<DateTime> GetDate()
        {
            try
            {
                return await this.db.executeScalarSp<DateTime>("getdate");
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
