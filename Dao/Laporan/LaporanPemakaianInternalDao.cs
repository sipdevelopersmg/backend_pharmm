using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanPemakaianInternalDao
    {
        public SQLConn db;

        public LaporanPemakaianInternalDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<laporan_pengeluaran_issue_per_faktur>> GetLaporanPengeluaranIssuePerFaktur(param_pengeluaran_issue_per_faktur param)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_pengeluaran_issue_per_faktur>(
                    "laporan_pengeluaran_issue_per_faktur", new
                    {
                        _nomor_pemakaian_internal = param.nomor_pemakaian_internal
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
