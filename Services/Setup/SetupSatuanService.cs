using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupSatuanService
    {

        Task<List<mm_setup_satuan>> GetAllMmSetupSatuan();
        Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParamsWithLimit(ParameterSearchWithLimit param);

        Task<short>AddMmSetupSatuan(mm_setup_satuan_insert data);
        Task<short>UpdateMmSetupSatuan(mm_setup_satuan_update data);
        Task<short>UpdateToActiveMmSetupSatuan(mm_setup_satuan_update_status_to_active data);
        Task<short>UpdateToDeActiveMmSetupSatuan(mm_setup_satuan_update_status_to_deactive data);

    }

    public class SetupSatuanService : ISetupSatuanService
    {
        private readonly SQLConn _db;
        private readonly SetupSatuanDao _dao;

        public SetupSatuanService(SQLConn db, SetupSatuanDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuan()
        {
            try
            {
                return await this._dao.GetAllMmSetupSatuan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupSatuanByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParamsWithLimit(ParameterSearchWithLimit param)
        {
            try
            {
                return await this._dao.GetAllMmSetupSatuanByParamsWithLimit(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupSatuan(mm_setup_satuan_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupSatuan(mm_setup_satuan_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToActiveMmSetupSatuan(mm_setup_satuan_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSatuan(mm_setup_satuan_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
