using DapperPostgreSQL;
using Microsoft.AspNetCore.Http;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupCoaDao
    {
        public SQLConn db;
        private IHttpContextAccessor _context;

        public SetupCoaDao(SQLConn db,IHttpContextAccessor context)
        {
            this.db = db;
            this._context = context;
        }

        public async Task<List<akun_setup_coa>> GetAllAkunSetupCoaByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<akun_setup_coa>("akun_setup_coa_GetByDynamicFilters",
                    new
                    {
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<akun_setup_coa>> GetAkunSetupCoaById(int id_coa)
        {
            try
            {
                return await this.db.QuerySPtoList<akun_setup_coa>("akun_setup_coa_GetById", new
                {
                    _id_coa = id_coa
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<akun_setup_coa>> GetAllAkunSetupCoa()
        {
            try
            {
                return await this.db.QuerySPtoList<akun_setup_coa>("akun_setup_coa_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddAkunSetupCoa(akun_setup_coa_insert data)
        {
            data.user_created = (short)(_context.HttpContext.Items["userId"]);

            try
            {
                return await this.db.executeScalarSp<Int16>("akun_setup_coa_Insert",
                    new
                    {
                        _id_coa_parent = data.id_coa_parent,
                        _id_grup_coa = data.id_grup_coa,
                        _kode_coa = data.kode_coa,
                        _deskripsi = data.deskripsi,
                        _saldo = data.saldo,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateAkunSetupCoa(akun_setup_coa_update data)
        {

            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_coa_Update",
                    new
                    {
                        _id_coa = data.id_coa,
                        _id_coa_parent = data.id_coa_parent,
                        _id_grup_coa = data.id_grup_coa,
                        _kode_coa = data.kode_coa,
                        _deskripsi = data.deskripsi,
                        _saldo = data.saldo
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveAkunSetupCoa(akun_setup_coa_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_coa_update_to_active",
                    new
                    {
                        _id_coa = data.id_coa
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveAkunSetupCoa(akun_setup_coa_update_status_to_deactive data)
        {
            try
            {
                data.user_deactived = (short)this._context.HttpContext.Items["userId"];

                return await this.db.executeScalarSp<short>("akun_setup_coa_update_to_deactive",
                    new
                    {
                        _id_coa = data.id_coa,
                        _user_deactived = data.user_deactived
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
