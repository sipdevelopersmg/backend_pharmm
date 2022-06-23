using Pharmm.API.Dao.Laporan;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Laporan
{
    public interface ILaporanPenerimaanBarangService
    {
        Task<List<laporan_penerimaan_per_vendor>> GetLaporanPenerimaanBarangPerVendor(param_laporan_penerimaan_per_vendor param);

    }

    public class LaporanPenerimaanBarangService : ILaporanPenerimaanBarangService
    {
        public readonly LaporanPenerimaanBarangDao _dao;

        public LaporanPenerimaanBarangService(LaporanPenerimaanBarangDao dao)
        {
            this._dao = dao;
        }

        public async Task<List<laporan_penerimaan_per_vendor>> GetLaporanPenerimaanBarangPerVendor(param_laporan_penerimaan_per_vendor param)
        {
            try
            {
                return await this._dao.GetLaporanPenerimaanBarangPerVendor(param);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
