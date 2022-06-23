using DapperPostgreSQL;
using Pharmm.API.Dao;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupStockroomService
    {
        Task<List<mm_setup_stockroom>> GetAllMmSetupStockroomByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_stockroom>> GetAllMmSetupStockroom();
        Task<List<mm_setup_stockroom>> GetMmSetupStockroomById(Int16 id_stockroom);

        Task<short>AddMmSetupStockroom(mm_setup_stockroom_insert data);
        Task<short>UpdateMmSetupStockroom(mm_setup_stockroom_update data);
        Task<short>UpdateToActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_deactive data);

    }

    public class SetupStockroomService : ISetupStockroomService
    {
        private readonly SQLConn _db;
        private readonly SetupStockroomDao _dao;

        public SetupStockroomService(SQLConn db, SetupStockroomDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_stockroom>> GetAllMmSetupStockroomByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupStockroomByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<mm_setup_stockroom>> GetAllMmSetupStockroom()
        {
            try
            {
                return await this._dao.GetAllMmSetupStockroom();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_stockroom>> GetMmSetupStockroomById(Int16 id_stockroom)
        {
            try
            {
                return await this._dao.GetMmSetupStockroomById(id_stockroom);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupStockroom(mm_setup_stockroom_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupStockroom(mm_setup_stockroom_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
