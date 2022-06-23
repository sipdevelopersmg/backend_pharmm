using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupTipeItemService
    {

        Task<List<mm_setup_tipe_item>> GetAllMmSetupTipeItem();
        Task<List<mm_setup_tipe_item>> GetMmSetupTipeItemById(Int16 id_tipe_item);

        Task<short>AddMmSetupTipeItem(mm_setup_tipe_item_insert data);
        Task<short>UpdateMmSetupTipeItem(mm_setup_tipe_item_update data);
        Task<short>UpdateToActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_deactive data);

    }

    public class SetupTipeItemService : ISetupTipeItemService
    {
        private readonly SQLConn _db;
        private readonly SetupTipeItemDao _dao;

        public SetupTipeItemService(SQLConn db, SetupTipeItemDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_tipe_item>> GetAllMmSetupTipeItem()
        {
            try
            {
                return await this._dao.GetAllMmSetupTipeItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_tipe_item>> GetMmSetupTipeItemById(Int16 id_tipe_item)
        {
            try
            {
                return await this._dao.GetMmSetupTipeItemById(id_tipe_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupTipeItem(mm_setup_tipe_item_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupTipeItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupTipeItem(mm_setup_tipe_item_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupTipeItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupTipeItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupTipeItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
