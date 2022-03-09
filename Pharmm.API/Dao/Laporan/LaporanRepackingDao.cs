using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanRepackingDao
    {
        public SQLConn db;

        public LaporanRepackingDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<laporan_repacking_per_faktur>> GetLaporanRepackingPerFaktur(param_laporan_repacking_per_faktur param)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_repacking_per_faktur>(
                    "laporan_repacking_per_faktur", new
                    {
                        _nomor_repacking = param.nomor_repacking
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
