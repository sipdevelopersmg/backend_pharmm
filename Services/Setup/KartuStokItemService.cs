using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;

namespace Pharmm.API.Services.Setup
{
    public interface IKartuStokItemService
    {

        Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByParams(List<ParameterSearchModel> param);
        Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByIdItemAndParams(int id_item, List<ParameterSearchModel> param);
        Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItem();

        Task<short> AddPenambahanMmKartuStokItem(mm_kartu_stok_item_insert_penambahan_stok data);
        Task<short> AddPenguranganMmKartuStokItem(mm_kartu_stok_item_insert_pengurangan_stok data);


        #region detail batch
        Task<List<mm_kartu_stok_item_detail_batch>> GetMmKartuStokItemDetailBatchByHeaderId(long _id_kartu_stok_item);
        Task<List<mm_kartu_stok_item_detail_batch>> GetMmKartuStokItemDetailBatchByBatchNumber(string _batch_number);

        #endregion

    }

    public class KartuStokItemService : IKartuStokItemService
    {
        private readonly SQLConn _db;
        private readonly KartuStokItemDao _dao;

        public KartuStokItemService(SQLConn db, KartuStokItemDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmKartuStokItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItemByIdItemAndParams(int id_item, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmKartuStokItemByIdItemAndParams(id_item, param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_kartu_stok_item>> GetAllMmKartuStokItem()
        {
            try
            {
                return await this._dao.GetAllMmKartuStokItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddPenambahanMmKartuStokItem(mm_kartu_stok_item_insert_penambahan_stok data)
        {
            try
            {
                return await this._dao.AddPenambahanMmKartuStokItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddPenguranganMmKartuStokItem(mm_kartu_stok_item_insert_pengurangan_stok data)
        {
            try
            {
                return await this._dao.AddPenguranganMmKartuStokItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        #region detail batch
        
        public async Task<List<mm_kartu_stok_item_detail_batch>> GetMmKartuStokItemDetailBatchByHeaderId(long _id_kartu_stok_item)
        {
            try
            {
                return await this._dao.GetMmKartuStokItemDetailBatchByHeaderId(_id_kartu_stok_item);
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
                return await this._dao.GetMmKartuStokItemDetailBatchByBatchNumber(_batch_number);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion

    }
}
