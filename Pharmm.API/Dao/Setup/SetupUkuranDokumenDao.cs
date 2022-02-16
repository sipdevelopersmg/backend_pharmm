using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupUkuranDokumenDao
    {
        public SQLConn db;

        public SetupUkuranDokumenDao(SQLConn db)
        {
            this.db = db;
        }
        public Task<List<set_ukuran_dokumen>> GetAllSetUkuranDokumen()
        {
            try
            {
                return this.db.QuerySPtoList<set_ukuran_dokumen>("set_ukuran_dokumen_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
