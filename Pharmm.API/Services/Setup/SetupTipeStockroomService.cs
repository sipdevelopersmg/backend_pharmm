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
    public interface ISetupTipeStockroomService
    {

        Task<List<mm_setup_tipe_stockroom>> GetAllMmSetupTipeStockroom();

        Task<short> AddMmSetupTipeStockroom(mm_setup_tipe_stockroom_insert data);
        Task<short> UpdateMmSetupTipeStockroom(mm_setup_tipe_stockroom data);
        Task<short> DeleteMmSetupTipeStockroom(Int16 id_tipe_stockroom);

    }

    public class SetupTipeStockroomService : ISetupTipeStockroomService
    {
        private readonly SQLConn _db;
        private readonly SetupTipeStockroomDao _dao;

        public SetupTipeStockroomService(SQLConn db, SetupTipeStockroomDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public async Task<List<mm_setup_tipe_stockroom>> GetAllMmSetupTipeStockroom()
        {
            try
            {
                return await this._dao.GetAllMmSetupTipeStockroom();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupTipeStockroom(mm_setup_tipe_stockroom_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupTipeStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateMmSetupTipeStockroom(mm_setup_tipe_stockroom data)
        {
            try
            {
                return await this._dao.UpdateMmSetupTipeStockroom(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteMmSetupTipeStockroom(Int16 id_tipe_stockroom)
        {
            try
            {
                return await this._dao.DeleteMmSetupTipeStockroom(id_tipe_stockroom);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
