using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanAssemblyDao
    {
        public SQLConn db;

        public LaporanAssemblyDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<laporan_assembly_per_faktur>> GetLaporanAssemblyPerFaktur(param_laporan_assembly_per_faktur param)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_assembly_per_faktur>(
                    "laporan_assembly_per_faktur", new
                    {
                        _nomor_assembly = param.nomor_assembly
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
