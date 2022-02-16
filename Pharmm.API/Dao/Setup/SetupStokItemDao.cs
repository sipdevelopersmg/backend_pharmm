using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupStokItemDao
    {
        public SQLConn db;

        public SetupStokItemDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        //dari data master barang dan gudang
        public async Task<mm_setup_stok_item_data_barang_and_stockroom> GetDataBarangAndStockroomFromMasterWithLock(
            int? _id_item,
            short _id_stockroom)
        {
            try
            {
                var barang = await this.db.QuerySPtoSingle<mm_setup_stok_item_data_barang_and_stockroom>(
                    "mm_setup_item_GetById_lock",
                    new
                    {
                        _id_item
                    });

                var gudang = await this.db.QuerySPtoSingle<mm_setup_stok_item_data_barang_and_stockroom>(
                    "mm_setup_stockroom_GetById_lock",
                    new
                    {
                        _id_stockroom
                    });

                var barangGudang = new mm_setup_stok_item_data_barang_and_stockroom
                {
                    id_item = barang?.id_item,
                    barcode = barang?.barcode,
                    kode_item = barang?.kode_item,
                    nama_item = barang?.nama_item,
                    id_stockroom = gudang?.id_stockroom,
                    nama_stockroom = gudang?.nama_stockroom
                };

                return barangGudang;
            }
            catch (Exception)
            {
                throw;
            }
        }

        //dari stok item
        public async Task<mm_setup_stok_item_data_barang_and_stockroom> GetDataBarangAndStockroomWithLock(
            int? _id_item,
            short _id_stockroom)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_stok_item_data_barang_and_stockroom>(
                    "mm_setup_stok_item_getby_item_stockroom_lock",
                    new
                    {
                        _id_item,
                        _id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //get stok item yang masih ada sisa stoknya
        public async Task<List<mm_setup_stok_item_lookup>> GetAllMmSetupStokItemByIdStockroomAndParams(short _id_stockroom,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_lookup>(
                    "mm_setup_stok_item_getby_idstockroom_params", new
                    {
                        _id_stockroom,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_stok_item_lookup> GetAllMmSetupStokItemByIdItemAndIdStockroom(
            int _id_item,
            short _id_stockroom)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_stok_item_lookup>("mm_setup_stok_item_getby_item_stockroom",
                    new
                    {
                        _id_item,
                        _id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        //untuk get sisa stok
        public async Task<mm_setup_stok_item_get_sisa_stok_lock> GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
            int _id_item,
            short _id_stockroom)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_stok_item_get_sisa_stok_lock>("mm_setup_stok_item_getby_item_stockroom_lock",
                    new
                    {
                        _id_item,
                        _id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_stok_item_lookup>> GetAllMmSetupStokItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stok_item_lookup>("mm_setup_stok_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupStokItem(mm_setup_stok_item data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_stok_item_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _qty_on_hand = data.qty_on_hand,
                        _qty_stok_kritis = data.qty_stok_kritis
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdatePenambahanStok(mm_setup_stok_item_update_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_update_penambahan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdatePenguranganStok(mm_setup_stok_item_update_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_update_pengurangan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Detail

        public async Task<List<mm_setup_stok_item_detail_ed>> GetAllMmSetupStokItemDetailEd()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_ed>("mm_setup_stok_item_detail_ed_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> AddMmSetupStokItemDetailEd(mm_setup_stok_item_detail_ed data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_stok_item_detail_ed_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _expired_date = data.expired_date,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Detail Batch

        public async Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(short _id_stockroom,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch_lookup>(
                    "mm_setup_stok_item_detail_batch_getby_idstockroom_params", new
                    {
                        _id_stockroom,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_stok_item_detail_batch_lookup_satuan>> GetAllMmSetupStokItemDetailBatchWithSatuanByIdStockroomAndParams(short _id_stockroom,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch_lookup_satuan>(
                    "mm_setup_stok_item_detail_batch_getby_idstockroom_satuan_params", new
                    {
                        _id_stockroom,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(
            short _id_stockroom,
            int _id_item)
        {

            try
            {

                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch_lookup>(
                    "mm_setup_stok_item_detail_batch_getby_stockroom_and_item", new
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

        public async Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItemAndParams(
            short _id_stockroom,
            int _id_item,
            List<ParameterSearchModel> param)
        {

            string _filters = String.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch_lookup>(
                    "mm_setup_stok_item_detail_batch_getby_stockroom_and_item", new
                    {
                        _id_stockroom,
                        _id_item,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_stok_item_detail_batch_lookup> GetAllMmSetupStokItemDetailBatchByIdStockroomAndBarcodeBatchNumberAndParams(
            short _id_stockroom,
            string _barcode_batch_number,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoSingle<mm_setup_stok_item_detail_batch_lookup>(
                    "mm_setup_stok_item_detail_batch_getby_stockroom_barcode_params",
                    new
                    {
                        _id_stockroom,
                        _barcode_batch_number,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_stok_item_detail_batch_lookup> GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumber(
            int _id_item,
            short _id_stockroom,
            string _batch_number)
        {
            try
            {

                return await this.db.QuerySPtoSingle<mm_setup_stok_item_detail_batch_lookup>(
                    "mm_setup_stok_item_detail_batch_getby_item_stockroom_batch",
                    new
                    {
                        _id_item,
                        _id_stockroom,
                        _batch_number  // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        //untuk get sisa stok item detail batch
        public async Task<mm_setup_stok_item_detail_batch_get_sisa_stok_lock> GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumberWithLock(
            int _id_item,
            short _id_stockroom,
            string _batch_number)
        {
            try
            {

                return await this.db.QuerySPtoSingle<mm_setup_stok_item_detail_batch_get_sisa_stok_lock>(
                    "mm_setup_stok_item_detail_batch_getby_item_stockroom_batch_lock",
                    new
                    {
                        _id_item,
                        _id_stockroom,
                        _batch_number  // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatchByIdItem(int _id_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch>("mm_setup_stok_item_detail_batch_getby_id_item", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatch()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch>("mm_setup_stok_item_detail_batch_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupStokItemDetailBatch(mm_setup_stok_item_detail_batch_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_stok_item_detail_batch_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand,
                        _expired_date = data.expired_date,
                        _barcode_batch_number = data.barcode_batch_number,
                        _harga_satuan_netto = data.harga_satuan_netto
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<short> UpdatePenambahanStokDetailBatch(mm_setup_stok_item_detail_update_penambahan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_detail_batch_update_penambahan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand,
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdatePenambahanStokWithHargaDetailBatch(mm_setup_stok_item_detail_update_penambahan_stok_with_harga data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_detail_batch_update_penambahan_stok_harga",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand,
                        _harga_satuan_netto = data.harga_satuan_netto
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdatePenguranganStokDetailBatch(mm_setup_stok_item_detail_update_pengurangan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_detail_batch_update_pengurangan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Urai

        //detail item urai
        public async Task<List<mm_setup_stok_item_lookup_urai>> GetAllStokItemUraiByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param)
        {

            string _filters = String.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_lookup_urai>(
                    "mm_setup_stok_item_urai_getby_iditem_params", new
                    {
                        _id_item,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Item Assembly

        //detail item assembly
        public async Task<List<mm_setup_stok_item_detail_batch_lookup_assembly>> GetAllStokItemAssemblyDetailBatchByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param)
        {

            string _filters = String.Empty;

            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch_lookup_assembly>(
                    "mm_setup_stok_item_detail_batch_assembly_getby_iditem_params", new
                    {
                        _id_item,
                        _filters
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
