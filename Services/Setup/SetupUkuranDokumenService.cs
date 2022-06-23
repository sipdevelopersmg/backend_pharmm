using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupUkuranDokumenService
    {

        Task<List<set_ukuran_dokumen>> GetAllSetUkuranDokumen();

    }

    public class SetupUkuranDokumenService : ISetupUkuranDokumenService
    {
        private readonly SQLConn _db;
        private readonly SetupUkuranDokumenDao _dao;

        public SetupUkuranDokumenService(SQLConn db, SetupUkuranDokumenDao dao)
        {
            this._db = db;
            this._dao = dao;
        }

        public Task<List<set_ukuran_dokumen>> GetAllSetUkuranDokumen()
        {
            try
            {
                return this._dao.GetAllSetUkuranDokumen();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
