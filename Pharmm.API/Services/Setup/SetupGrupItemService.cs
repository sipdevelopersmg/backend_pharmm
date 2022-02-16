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
    public interface ISetupGrupItemService
    {

        Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItemByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItem();

        Task<short>AddMmSetupGrupItem(mm_setup_grup_item_insert data);
        Task<short>UpdateMmSetupGrupItem(mm_setup_grup_item_update data);
        Task<short>UpdateToActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_deactive data);

    }

    public class SetupGrupItemService : ISetupGrupItemService
    {
        private readonly SQLConn _db;
        private readonly SetupGrupItemDao _dao;

        public SetupGrupItemService(SQLConn db, SetupGrupItemDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupGrupItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItem()
        {
            try
            {
                return await this._dao.GetAllMmSetupGrupItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupGrupItem(mm_setup_grup_item_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupGrupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupGrupItem(mm_setup_grup_item_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupGrupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupGrupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupGrupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
