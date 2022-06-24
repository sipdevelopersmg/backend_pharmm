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
    public class TransSetHargaOrderDao
    {
        public SQLConn db;

        public TransSetHargaOrderDao(SQLConn db)
        {
            this.db = db;
        }


        #region Header
        public async Task<List<set_harga_order>> GetAllSetHargaOrderByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<set_harga_order>("set_harga_order_GetByDynamicFilters",
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
        public async Task<List<set_harga_order>> GetSetHargaOrderById(long set_harga_order_id)
        {
            try
            {
                return await this.db.QuerySPtoList<set_harga_order>("set_harga_order_GetById", new
                {
                    _set_harga_order_id = set_harga_order_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<set_harga_order>> GetAllSetHargaOrder()
        {
            try
            {
                return await this.db.QuerySPtoList<set_harga_order>("set_harga_order_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<short> AddSetHargaOrder(set_harga_order_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("set_harga_order_Insert",
                    new
                    {
                        _nomor_harga_order = data.nomor_harga_order,
                        _id_supplier = data.id_supplier,
                        _tanggal_berlaku = data.tanggal_berlaku,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Detail
        public async Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetailByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<set_harga_order_detail>("set_harga_order_detail_GetByDynamicFilters",
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
        public async Task<List<set_harga_order_detail>> GetSetHargaOrderDetailByHargaOrderId(long set_harga_order_id)
        {
            try
            {
                return await this.db.QuerySPtoList<set_harga_order_detail>("set_harga_order_detail_get_by_set_harga_order_id", new
                {
                    _set_harga_order_id = set_harga_order_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetail()
        {
            try
            {
                return await this.db.QuerySPtoList<set_harga_order_detail>("set_harga_order_detail_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateBerlakuSetHargaOrderDetail(set_harga_order_detail_update_berlaku data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("set_harga_order_detail_update_berlaku",
                    new
                    {
                        _id_item = data.id_item,
                        _tanggal_berakhir = data.tanggal_berakhir,
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddSetHargaOrderDetail(set_harga_order_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("set_harga_order_detail_Insert",
                    new
                    {
                        _set_harga_order_id = data.set_harga_order_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _harga_order = data.harga_order,
                        _disc_prosentase_1 = data.disc_prosentase_1,
                        _disc_prosentase_2 = data.disc_prosentase_2,
                        _harga_order_netto = data.harga_order_netto,
                        _tanggal_berlaku = data.tanggal_berlaku,
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<set_harga_order_detail_berlaku>> GetAllSetHargaOrderBerlakuDetailByIdSupplierParams(Int16 _id_supplier, List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<set_harga_order_detail_berlaku>("set_harga_order_detail_get_berlaku_by_id_supplier_dynamicfilter",
                    new
                    {
                        _id_supplier,            //   smallint not null
                        _filters                 // not null 
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
