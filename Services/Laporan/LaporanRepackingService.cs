using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanRepackingService
    {
        Task<List<laporan_repacking_per_faktur>> GetLaporanRepackingPerFaktur(param_laporan_repacking_per_faktur param);

    }

    public class LaporanRepackingService : ILaporanRepackingService
    {
        public readonly LaporanRepackingDao _dao;

        public LaporanRepackingService(LaporanRepackingDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_repacking_per_faktur>> GetLaporanRepackingPerFaktur(param_laporan_repacking_per_faktur param)
        {
            try
            {
                return await this._dao.GetLaporanRepackingPerFaktur(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
