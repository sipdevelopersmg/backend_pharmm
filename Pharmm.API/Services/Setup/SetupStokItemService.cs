using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupStokItemService
    {
        #region Header

        Task<List<mm_setup_stok_item_lookup>> GetAllMmSetupStokItemByIdStockroomAndParams(short _id_stockroom, List<ParameterSearchModel> param);
         Task<List<mm_setup_stok_item_lookup>> GetAllMmSetupStokItem();
        Task<short> AddMmSetupStokItem(mm_setup_stok_item data);

        #endregion


        #region Detail

        Task<List<mm_setup_stok_item_detail_ed>> GetAllMmSetupStokItemDetailEd();

        Task<short> AddMmSetupStokItemDetailEd(mm_setup_stok_item_detail_ed data);

        #endregion

        #region Detail Batch

        Task<List<mm_setup_stok_item_detail_batch_lookup_satuan>> GetAllMmSetupStokItemDetailBatchWithSatuanByIdStockroomAndParams(short _id_stockroom, List<ParameterSearchModel> param);
        Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(short _id_stockroom, List<ParameterSearchModel> param);
        Task<mm_setup_stok_item_detail_batch_lookup> GetAllMmSetupStokItemDetailBatchByIdStockroomAndBarcodeBatchNumberAndParams(
            short _id_stockroom,
            string _barcode_batch_number,
            List<ParameterSearchModel> param);

        Task<mm_setup_stok_item_detail_batch_lookup> GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumber(
            int _id_item,
            short _id_stockroom,
            string _batch_number);

        Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(
            short _id_stockroom,
            int _id_item);

        Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItemAndParams(
            short _id_stockroom,
            int _id_item,
            List<ParameterSearchModel> param);

        Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatchByIdItem(int _id_item);
        Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatch();

        Task<short> AddMmSetupStokItemDetailBatch(mm_setup_stok_item_detail_batch_insert data);
        Task<short> UpdatePenambahanStokDetailBatch(mm_setup_stok_item_detail_update_penambahan_stok data);
        Task<short> UpdatePenguranganStokDetailBatch(mm_setup_stok_item_detail_update_pengurangan_stok data);

        #endregion

        #region Item Urai

        Task<List<mm_setup_stok_item_lookup_urai>> GetAllStokItemUraiByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param);

        Task<List<mm_setup_stok_item_detail_batch_lookup_assembly>> GetAllStokItemAssemblyDetailBatchByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param);

        #endregion
    }

    public class SetupStokItemService : ISetupStokItemService
    {
        private readonly SQLConn _db;
        private readonly SetupStokItemDao _dao;

        public SetupStokItemService(SQLConn db, SetupStokItemDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        #region Header

        public async Task<List<mm_setup_stok_item_lookup>> GetAllMmSetupStokItemByIdStockroomAndParams(short _id_stockroom, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemByIdStockroomAndParams(_id_stockroom, param);
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
                return await this._dao.GetAllMmSetupStokItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupStokItem(mm_setup_stok_item data)
        {
            try
            {
                return await this._dao.AddMmSetupStokItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail

        public async Task<List<mm_setup_stok_item_detail_ed>> GetAllMmSetupStokItemDetailEd()
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailEd();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupStokItemDetailEd(mm_setup_stok_item_detail_ed data)
        {
            try
            {
                return await this._dao.AddMmSetupStokItemDetailEd(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail Batch

        public async Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(
            short _id_stockroom,
            int _id_item)
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(_id_stockroom,_id_item);
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
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItemAndParams(_id_stockroom, _id_item,param);
            }
            catch (Exception )
            {

                throw;
            }
        }

        public async Task<List<mm_setup_stok_item_detail_batch_lookup_satuan>> GetAllMmSetupStokItemDetailBatchWithSatuanByIdStockroomAndParams(short _id_stockroom,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchWithSatuanByIdStockroomAndParams(_id_stockroom, param);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<mm_setup_stok_item_detail_batch_lookup>> GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(short _id_stockroom, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(_id_stockroom, param);
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
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumber(_id_item, _id_stockroom, _batch_number);

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

            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBarcodeBatchNumberAndParams(_id_stockroom, _barcode_batch_number, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatchByIdItem(int id_item)
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatchByIdItem(id_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatch()
        {
            try
            {
                return await this._dao.GetAllMmSetupStokItemDetailBatch();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupStokItemDetailBatch(mm_setup_stok_item_detail_batch_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupStokItemDetailBatch(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdatePenambahanStokDetailBatch(mm_setup_stok_item_detail_update_penambahan_stok data)
        {
            try
            {
                return await this._dao.UpdatePenambahanStokDetailBatch(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdatePenguranganStokDetailBatch(mm_setup_stok_item_detail_update_pengurangan_stok data)
        {
            try
            {
                return await this._dao.UpdatePenguranganStokDetailBatch(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Item Urai
        
        public async Task<List<mm_setup_stok_item_lookup_urai>> GetAllStokItemUraiByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllStokItemUraiByHeaderIdAndParams(_id_item, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region Item Assembly

        public async Task<List<mm_setup_stok_item_detail_batch_lookup_assembly>> GetAllStokItemAssemblyDetailBatchByHeaderIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllStokItemAssemblyDetailBatchByHeaderIdAndParams(_id_item, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion
    }
}
