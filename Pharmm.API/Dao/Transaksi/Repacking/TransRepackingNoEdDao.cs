using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Transaksi
{
    public class TransRepackingNoEdDao
    {
        public SQLConn db;

        public TransRepackingNoEdDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<List<tr_repacking_no_ed>> GetAllTrRepackingByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_repacking_no_ed>("tr_repacking_no_ed_GetByDynamicFilters",
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

        public async Task<tr_repacking_no_ed> GetTrRepackingById(long repacking_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_repacking_no_ed>("tr_repacking_GetById", new
                {
                    _repacking_id = repacking_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_repacking_no_ed> GetTrRepackingByIdWithLock(long repacking_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_repacking_no_ed>("tr_repacking_GetById_lock", new
                {
                    _repacking_id = repacking_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrRepacking(tr_repacking_no_ed_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_repacking_no_ed_insert",
                    new
                    {
                        _nomor_repacking = data.nomor_repacking,
                        _tanggal_repacking = data.tanggal_repacking,
                        _id_stockroom = data.id_stockroom,
                        _id_item = data.id_item,
                        _qty = data.qty,
                        _hpp_satuan = data.hpp_satuan,
                        _total_nominal = data.total_nominal,
                        _jumlah_item = data.jumlah_item,
                        _total_transaksi = data.total_transaksi,
                        _keterangan_repacking = data.keterangan_repacking,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateTrRepackingValidated(tr_repacking_no_ed_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_repacking_update_to_validated",
                    new
                    {
                        _repacking_id = data.repacking_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateTrRepackingCanceled(tr_repacking_no_ed_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_repacking_update_to_canceled",
                    new
                    {
                        _repacking_id = data.repacking_id,
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


        public async Task<List<tr_repacking_no_ed_detail>> GetTrRepackingDetailByHeaderId(long repacking_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_repacking_no_ed_detail>("tr_repacking_detail_Getby_headerid", new
                {
                    _repacking_id = repacking_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_repacking_no_ed_detail>> GetTrRepackingDetailByHeaderIdWithLock(long repacking_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_repacking_no_ed_detail>("tr_repacking_detail_Getby_headerid_lock", new
                {
                    _repacking_id = repacking_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrRepackingDetail(tr_repacking_no_ed_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_repacking_detail_no_ed_insert",
                    new
                    {
                        _repacking_id = data.repacking_id,
                        _no_urut = data.no_urut,
                        _id_item_child = data.id_item_child,
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
