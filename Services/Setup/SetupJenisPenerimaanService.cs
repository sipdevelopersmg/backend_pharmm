using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupJenisPenerimaanService
    {

        Task<List<mm_setup_jenis_penerimaan>> GetAllMmSetupJenisPenerimaan();

    }

    public class SetupJenisPenerimaanService : ISetupJenisPenerimaanService
    {
        private readonly SQLConn _db;
        private readonly SetupJenisPenerimaanDao _dao;

        public SetupJenisPenerimaanService(SQLConn db, SetupJenisPenerimaanDao dao)
        {
            this._db = db;
            this._dao = dao;
        }



        public async Task<List<mm_setup_jenis_penerimaan>> GetAllMmSetupJenisPenerimaan()
        {
            try
            {
                return await this._dao.GetAllMmSetupJenisPenerimaan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
