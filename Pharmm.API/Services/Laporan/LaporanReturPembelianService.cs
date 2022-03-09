using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanReturPembelianService
    {
        Task<List<laporan_retur_pembelian_by_periode>> GetLaporanReturPembelianPerPeriodeAndGudang(param_laporan_retur_pembelian_by_periode param);

    }

    public class LaporanReturPembelianService : ILaporanReturPembelianService
    {
        public readonly LaporanReturPembelianDao _dao;

        public LaporanReturPembelianService(LaporanReturPembelianDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_retur_pembelian_by_periode>> GetLaporanReturPembelianPerPeriodeAndGudang(param_laporan_retur_pembelian_by_periode param)
        {
            try
            {
                return await this._dao.GetLaporanReturPembelianPerPeriodeAndGudang(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


    }
}
