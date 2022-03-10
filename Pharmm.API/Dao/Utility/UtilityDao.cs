using DapperPostgreSQL;
using Pharmm.API.Models.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Utility
{
    public class UtilityDao
    {
        private readonly SQLConn db;

        public UtilityDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<field_constraint>> GetFieldConstraint(string _constraint_name)
        {
            try
            {
                return await this.db.QuerySPtoList<field_constraint>("fn_get_constraint_fields",new
                {
                    _constraint_name
                });
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
