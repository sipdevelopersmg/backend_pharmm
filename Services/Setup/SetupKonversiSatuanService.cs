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
    public interface ISetupKonversiSatuanService
    {

        Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuanByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuan();

        Task<short>AddMmSetupKonversiSatuan(mm_setup_konversi_satuan_insert data);
        Task<short>UpdateMmSetupKonversiSatuan(mm_setup_konversi_satuan_update data);
        Task<short>DeleteMmSetupKonversiSatuan(int id_konversi_satuan);

    }

    public class SetupKonversiSatuanService : ISetupKonversiSatuanService
    {
        private readonly SQLConn _db;
        private readonly SetupKonversiSatuanDao _dao;

        public SetupKonversiSatuanService(SQLConn db, SetupKonversiSatuanDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuanByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupKonversiSatuanByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuan()
        {
            try
            {
                return await this._dao.GetAllMmSetupKonversiSatuan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<short>AddMmSetupKonversiSatuan(mm_setup_konversi_satuan_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupKonversiSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupKonversiSatuan(mm_setup_konversi_satuan_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupKonversiSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>DeleteMmSetupKonversiSatuan(int id_konversi_satuan)
        {
            try
            {
                return await this._dao.DeleteMmSetupKonversiSatuan(id_konversi_satuan);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
