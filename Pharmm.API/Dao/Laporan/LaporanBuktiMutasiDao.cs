using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanBuktiMutasiDao
    {
        public SQLConn db;

        public LaporanBuktiMutasiDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<laporan_bukti_mutasi_per_faktur>> GetLaporanBuktiMutasiByFaktur(param_bukti_mutasi param)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_bukti_mutasi_per_faktur>(
                    "laporan_bukti_mutasi_per_faktur", new
                    {
                        _nomor_mutasi = param.nomor_mutasi
                    });
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
                return await this.db.QuerySPtoList<laporan_permintaan_mutasi_per_faktur>(
                    "laporan_permintaan_mutasi_per_faktur", new
                    {
                        _nomor_permintaan_mutasi = param.nomor_permintaan_mutasi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
