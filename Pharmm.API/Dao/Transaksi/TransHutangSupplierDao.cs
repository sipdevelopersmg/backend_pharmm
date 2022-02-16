using DapperPostgreSQL;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Transaksi
{
    public class TransHutangSupplierDao
    {
        public SQLConn db;

        public TransHutangSupplierDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<akun_tr_hutang_supplier>> GetAllAkunTrHutangSupplierByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<akun_tr_hutang_supplier>("akun_tr_hutang_supplier_GetByDynamicFilters",
                    new
                    {
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> AddAkunTrHutangSupplier(akun_tr_hutang_supplier_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("akun_tr_hutang_supplier_Insert",
                    new
                    {
                        _tanggal_hutang_supplier = data.tanggal_hutang_supplier,
                        _tanggal_jatuh_tempo_pembayaran = data.tanggal_jatuh_tempo_pembayaran,
                        _jumlah_hutang = data.jumlah_hutang,
                        _sudah_titip_tagihan = data.sudah_titip_tagihan,
                        _belum_titip_tagihan = data.belum_titip_tagihan,
                        _sudah_dibayar = data.sudah_dibayar,
                        _belum_dibayar = data.belum_dibayar,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
