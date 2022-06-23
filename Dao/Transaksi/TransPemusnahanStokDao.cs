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
    public class TransPemusnahanStokDao
    {
        public SQLConn db;

        public TransPemusnahanStokDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<List<tr_pemusnahan_stok>> GetAllTrPemusnahanStokByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemusnahan_stok>("tr_pemusnahan_stok_GetByDynamicFilters",
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

        public async Task<tr_pemusnahan_stok> GetTrPemusnahanStokByIdWithLock(long pemusnahan_stok_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemusnahan_stok>("tr_pemusnahan_stok_GetById_lock", new
                {
                    _pemusnahan_stok_id = pemusnahan_stok_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_pemusnahan_stok> GetTrPemusnahanStokById(long pemusnahan_stok_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemusnahan_stok>("tr_pemusnahan_stok_GetById", new
                {
                    _pemusnahan_stok_id = pemusnahan_stok_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemusnahanStok(tr_pemusnahan_stok_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemusnahan_stok_Insert",
                    new
                    {
                        _nomor_pemusnahan_stok = data.nomor_pemusnahan_stok,
                        _tanggal_pemusnahan_stok = data.tanggal_pemusnahan_stok,
                        _id_stockroom = data.id_stockroom,
                        _jumlah_item = data.jumlah_item,
                        _total_transaksi = data.total_transaksi,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToValidated(tr_pemusnahan_stok_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemusnahan_stok_update_to_validated",
                    new
                    {
                        _pemusnahan_stok_id = data.pemusnahan_stok_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToCanceled(tr_pemusnahan_stok_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemusnahan_stok_update_to_canceled",
                    new
                    {
                        _pemusnahan_stok_id = data.pemusnahan_stok_id,
                        _user_canceled = data.user_canceled,
                        _reason_canceled = data.reason_canceled
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Detail

        public async Task<List<tr_pemusnahan_stok_detail>> GetAllTrPemusnahanStokDetailByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemusnahan_stok_detail>("tr_pemusnahan_stok_detail_GetByDynamicFilters",
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

        public async Task<List<tr_pemusnahan_stok_detail>> GetTrPemusnahanStokDetailByHeaderId(long _pemusnahan_stok_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemusnahan_stok_detail>("tr_pemusnahan_stok_detail_getby_headerid", new
                {
                    _pemusnahan_stok_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemusnahan_stok_detail>> GetTrPemusnahanStokDetailByHeaderIdWithLock(long _pemusnahan_stok_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemusnahan_stok_detail>("tr_pemusnahan_stok_detail_getby_headerid_lock", new
                {
                    _pemusnahan_stok_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemusnahanStokDetail(tr_pemusnahan_stok_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemusnahan_stok_detail_Insert",
                    new
                    {
                        _pemusnahan_stok_id = data.pemusnahan_stok_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _qty = data.qty,
                        _hpp_satuan = data.hpp_satuan,
                        _sub_total = data.sub_total
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
