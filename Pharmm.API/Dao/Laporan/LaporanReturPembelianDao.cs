using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanReturPembelianDao
    {
        public SQLConn db;

        public LaporanReturPembelianDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<laporan_retur_pembelian_by_periode>> GetLaporanReturPembelianPerPeriodeAndGudang(param_laporan_retur_pembelian_by_periode data)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_retur_pembelian_by_periode>(
                    "laporan_retur_pembelian_by_periode", new
                    {
                        _bulan = data.bulan,
                        _tahun = data.tahun,
                        _id_stockroom = data.id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
