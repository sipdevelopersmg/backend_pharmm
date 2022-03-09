using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanPemakaianInternalService
    {
        Task<List<laporan_pengeluaran_issue_per_faktur>> GetLaporanPengeluaranIssuePerFaktur(param_pengeluaran_issue_per_faktur param);

    }

    public class LaporanPemakaianInternalService : ILaporanPemakaianInternalService
    {
        public readonly LaporanPemakaianInternalDao _dao;

        public LaporanPemakaianInternalService(LaporanPemakaianInternalDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_pengeluaran_issue_per_faktur>> GetLaporanPengeluaranIssuePerFaktur(param_pengeluaran_issue_per_faktur param)
        {
            try
            {
                return await this._dao.GetLaporanPengeluaranIssuePerFaktur(param);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
