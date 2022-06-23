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
    public class TransAssemblyNoEdDao
    {
        public SQLConn db;

        public TransAssemblyNoEdDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<List<tr_assembly_no_ed>> GetAllTrAssemblyByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_assembly_no_ed>("tr_assembly_no_ed_GetByDynamicFilters",
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

        public async Task<tr_assembly_no_ed> GetTrAssemblyById(long assembly_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_assembly_no_ed>("tr_assembly_GetById", new
                {
                    _assembly_id = assembly_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_assembly_no_ed> GetTrAssemblyByIdWithLock(long assembly_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_assembly_no_ed>("tr_assembly_GetById_lock", new
                {
                    _assembly_id = assembly_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrAssembly(tr_assembly_no_ed_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_assembly_no_ed_insert",
                    new
                    {
                        _nomor_assembly = data.nomor_assembly,
                        _tanggal_assembly = data.tanggal_assembly,
                        _id_stockroom = data.id_stockroom,
                        _id_item = data.id_item,
                        _qty = data.qty,
                        _hpp_satuan = data.hpp_satuan,
                        _total_nominal = data.total_nominal,
                        _jumlah_item = data.jumlah_item,
                        _total_transaksi = data.total_transaksi,
                        _keterangan_assembly = data.keterangan_assembly,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateTrAssemblyValidated(tr_assembly_no_ed_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_assembly_update_to_validated",
                    new
                    {
                        _assembly_id = data.assembly_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateTrAssemblyCanceled(tr_assembly_no_ed_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_assembly_update_to_canceled",
                    new
                    {
                        _assembly_id = data.assembly_id,
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


        public async Task<List<tr_assembly_no_ed_detail>> GetTrAssemblyDetailByHeaderId(long assembly_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_assembly_no_ed_detail>("tr_assembly_detail_Getby_headerid", new
                {
                    _assembly_id = assembly_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_assembly_no_ed_detail>> GetTrAssemblyDetailByHeaderIdWithLock(long assembly_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_assembly_no_ed_detail>("tr_assembly_detail_Getby_headerid_lock", new
                {
                    _assembly_id = assembly_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrAssemblyDetail(tr_assembly_no_ed_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_assembly_detail_no_ed_insert",
                    new
                    {
                        _assembly_id = data.assembly_id,
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
