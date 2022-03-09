using DapperPostgreSQL;
using Pharmm.API.Models.Laporan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Laporan
{
    public class LaporanPenerimaanBarangDao
    {
        public SQLConn db;

        public LaporanPenerimaanBarangDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<laporan_penerimaan_per_vendor>> GetLaporanPenerimaanBarangPerVendor(param_laporan_penerimaan_per_vendor param)
        {
            try
            {
                return await this.db.QuerySPtoList<laporan_penerimaan_per_vendor>(
                    "laporan_penerimaan_per_vendor", new
                    {
                        _start_date = param.start_date,
                        _end_date = param.end_date,
                        _id_supplier = param.id_supplier
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
