using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanBuktiMutasiService
    {
        Task<List<laporan_bukti_mutasi_per_faktur>> GetLaporanBuktiMutasiByFaktur(param_bukti_mutasi param);
        Task<List<laporan_permintaan_mutasi_per_faktur>> GetLaporanPermintaanMutasiByFaktur(param_permintaan_mutasi param);
    }

    public class LaporanBuktiMutasiService : ILaporanBuktiMutasiService
    {
        public readonly LaporanBuktiMutasiDao _dao;

        public LaporanBuktiMutasiService(LaporanBuktiMutasiDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_bukti_mutasi_per_faktur>> GetLaporanBuktiMutasiByFaktur(param_bukti_mutasi param)
        {
            try
            {
                return await this._dao.GetLaporanBuktiMutasiByFaktur(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<laporan_permintaan_mutasi_per_faktur>> GetLaporanPermintaanMutasiByFaktur(param_permintaan_mutasi param)
        {
            try
            {
                return await this._dao.GetLaporanPermintaanMutasiByFaktur(param);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
