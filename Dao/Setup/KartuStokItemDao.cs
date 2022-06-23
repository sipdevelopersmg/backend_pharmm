using DapperPostgreSQL;
using Microsoft.AspNetCore.Http;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class KartuStokItemDao
    {
        public SQLConn db;
        private IHttpContextAccessor _context;

        public KartuStokItemDao(SQLConn db, IHttpContextAccessor context)
        {
            this._context = context;
            this.db = db;
        }

        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByIdItemAndParams(int _id_item, List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_kartu_stok_item>("mm_kartu_stok_item_lookup_by_iditem_dynamicfilters",
                    new
                    {
                        _id_item,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_kartu_stok_item>("mm_kartu_stok_item_GetByDynamicFilters",
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


        public async Task<mm_kartu_stok_item_stok_akhr> GetStokAkhirByIdStockroomAndIdItem(
            short _id_stockroom,
            int _id_item
            )
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_kartu_stok_item_stok_akhr>("mm_kartu_stok_item_get_stok_akhir_by_stockroom_and_item",
                    new
                    {
                        _id_stockroom,
                        _id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_kartu_stok_item_stok_akhr> GetStokAkhirByIdStockroomAndIdItemWithLock(
           short _id_stockroom,
           int _id_item
           )
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_kartu_stok_item_stok_akhr>("mm_kartu_stok_item_get_stok_akhir_by_stockroom_and_item_lock",
                    new
                    {
                        _id_stockroom,
                        _id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_kartu_stok_item>("mm_kartu_stok_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPenambahanMmKartuStokItem(mm_kartu_stok_item_insert_penambahan_stok data)
        {
            try
            {
                data.user_inputed = (short)this._context.HttpContext.Items["userId"];

                return await this.db.executeScalarSp<Int16>("mm_kartu_stok_item_insert_penambahan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _nomor_ref_transaksi = data.nomor_ref_transaksi,
                        _id_header_transaksi = data.id_header_transaksi,
                        _id_detail_transaksi = data.id_detail_transaksi,
                        _stok_awal = data.stok_awal,
                        _nominal_awal = data.nominal_awal,
                        _stok_masuk = data.stok_masuk,
                        _nominal_masuk = data.nominal_masuk,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPenguranganMmKartuStokItem(mm_kartu_stok_item_insert_pengurangan_stok data)
        {
            try
            {
                data.user_inputed = (short)this._context.HttpContext.Items["userId"];

                return await this.db.executeScalarSp<Int16>("mm_kartu_stok_item_insert_pengurangan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _nomor_ref_transaksi = data.nomor_ref_transaksi,
                        _id_header_transaksi = data.id_header_transaksi,
                        _id_detail_transaksi = data.id_detail_transaksi,
                        _stok_awal = data.stok_awal,
                        _nominal_awal = data.nominal_awal,
                        _stok_keluar = data.stok_keluar,
                        _nominal_keluar = data.nominal_keluar,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #region Detail Item Batch

        public async Task<mm_kartu_stok_item_stok_akhr> GetStokAkhirDetailBatch(
            string _batch_number,
            DateTime? _expired_date,
            short _id_stockroom,
            int _id_item
            )
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_kartu_stok_item_stok_akhr>("mm_kartu_stok_item_detail_batch_get_stok_akhir",
                    new
                    {
                        _batch_number,
                        _expired_date,
                        _id_stockroom,
                        _id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<mm_kartu_stok_item_stok_akhr> GetStokAkhirDetailBatchWithLock(
            string _batch_number,
            DateTime? _expired_date,
            short _id_stockroom,
            int _id_item
            )
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_kartu_stok_item_stok_akhr>("mm_kartu_stok_item_detail_batch_get_stok_akhir_lock",
                    new
                    {
                        _batch_number,
                        _expired_date,
                        _id_stockroom,
                        _id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_kartu_stok_item_detail_batch>> GetMmKartuStokItemDetailBatchByHeaderId(long _id_kartu_stok_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_kartu_stok_item_detail_batch>("mm_kartu_stok_item_detail_batch_GetBy_headerid", new
                {
                    _id_kartu_stok_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_kartu_stok_item_detail_batch>> GetMmKartuStokItemDetailBatchByBatchNumber(string _batch_number)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_kartu_stok_item_detail_batch>("mm_kartu_stok_item_detail_batch_getby_batch_number", new
                {
                    _batch_number
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> AddPenambahanMmKartuStokItemDetailBatch(mm_kartu_stok_item_detail_batch_insert_penambahan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_kartu_stok_item_detail_batch_insert_penambahan_stok",
                    new
                    {
                        _id_kartu_stok_item = data.id_kartu_stok_item,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _stok_awal = data.stok_awal,
                        _nominal_awal = data.nominal_awal,
                        _stok_masuk = data.stok_masuk,
                        _nominal_masuk = data.nominal_masuk,
                        _id_header_transaksi = data.id_header_transaksi,
                        _id_detail_transaksi = data.id_detail_transaksi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPenguranganMmKartuStokItemDetailBatch(mm_kartu_stok_item_detail_batch_insert_pengurangan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_kartu_stok_item_detail_batch_insert_pengurangan_stok",
                    new
                    {
                        _id_kartu_stok_item = data.id_kartu_stok_item,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _stok_awal = data.stok_awal,
                        _nominal_awal = data.nominal_awal,
                        _stok_keluar = data.stok_keluar,
                        _nominal_keluar = data.nominal_keluar,
                        _id_header_transaksi = data.id_header_transaksi,
                        _id_detail_transaksi = data.id_detail_transaksi
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
