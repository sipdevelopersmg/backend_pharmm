using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupShippingMethodService
    {

        Task<List<mm_setup_shipping_method>> GetAllMmSetupShippingMethod();

    }

    public class SetupShippingMethodService : ISetupShippingMethodService
    {
        private readonly SQLConn _db;
        private readonly SetupShippingMethodDao _dao;

        public SetupShippingMethodService(SQLConn db, SetupShippingMethodDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_shipping_method>> GetAllMmSetupShippingMethod()
        {
            try
            {
                return await this._dao.GetAllMmSetupShippingMethod();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

    }
}
