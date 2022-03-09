using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanAssemblyService
    {
        Task<List<laporan_assembly_per_faktur>> GetLaporanAssemblyPerFaktur(param_laporan_assembly_per_faktur param);

    }

    public class LaporanAssemblyService : ILaporanAssemblyService
    {
        public readonly LaporanAssemblyDao _dao;

        public LaporanAssemblyService(LaporanAssemblyDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_assembly_per_faktur>> GetLaporanAssemblyPerFaktur(param_laporan_assembly_per_faktur param)
        {
            try
            {
                return await this._dao.GetLaporanAssemblyPerFaktur(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
