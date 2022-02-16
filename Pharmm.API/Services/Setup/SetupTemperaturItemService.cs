using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupTemperaturItemService
    {

        Task<List<mm_setup_temperatur_item>> GetAllMmSetupTemperaturItem();
        Task<List<mm_setup_temperatur_item>> GetMmSetupTemperaturItemById(Int16 id_temperatur_item);

        Task<short>AddMmSetupTemperaturItem(mm_setup_temperatur_item_insert data);
        Task<short>UpdateMmSetupTemperaturItem(mm_setup_temperatur_item data);
        Task<short>DeleteMmSetupTemperaturItem(Int16 id_temperatur_item);

    }

    public class SetupTemperaturItemService : ISetupTemperaturItemService
    {
        private readonly SQLConn _db;
        private readonly SetupTemperaturItemDao _dao;

        public SetupTemperaturItemService(SQLConn db, SetupTemperaturItemDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_temperatur_item>> GetAllMmSetupTemperaturItem()
        {
            try
            {
                return await this._dao.GetAllMmSetupTemperaturItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_temperatur_item>> GetMmSetupTemperaturItemById(Int16 id_temperatur_item)
        {
            try
            {
                return await this._dao.GetMmSetupTemperaturItemById(id_temperatur_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupTemperaturItem(mm_setup_temperatur_item_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupTemperaturItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupTemperaturItem(mm_setup_temperatur_item data)
        {
            try
            {
                return await this._dao.UpdateMmSetupTemperaturItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>DeleteMmSetupTemperaturItem(Int16 id_temperatur_item)
        {
            try
            {
                return await this._dao.DeleteMmSetupTemperaturItem(id_temperatur_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
