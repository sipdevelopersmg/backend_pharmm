using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupGrupCoaDao
    {
        public SQLConn db;

        public SetupGrupCoaDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoaByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<akun_setup_grup_coa>("akun_setup_grup_coa_GetByDynamicFilters",
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

        public async Task<List<akun_setup_grup_coa>> GetAkunSetupGrupCoaById(Int16 id_grup_coa)
        {
            try
            {
                return await this.db.QuerySPtoList<akun_setup_grup_coa>("akun_setup_grup_coa_GetById", new
                {
                    _id_grup_coa = id_grup_coa
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<akun_setup_grup_coa>> GetAllAkunSetupGrupCoa()
        {
            try
            {
                return await this.db.QuerySPtoList<akun_setup_grup_coa>("akun_setup_grup_coa_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddAkunSetupGrupCoa(akun_setup_grup_coa_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("akun_setup_grup_coa_Insert",
                    new
                    {
                        _grup_coa = data.grup_coa,
                        _deskripsi = data.deskripsi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateAkunSetupGrupCoa(akun_setup_grup_coa data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_grup_coa_Update",
                    new
                    {
                        _id_grup_coa = data.id_grup_coa,
                        _grup_coa = data.grup_coa,
                        _deskripsi = data.deskripsi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToActiveAkunSetupGrupCoa(short _id_grup_coa)
        {
            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_grup_coa_update_to_active",
                    new
                    {
                        _id_grup_coa
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToDeActiveAkunSetupGrupCoa(short _id_grup_coa)
        {
            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_grup_coa_update_to_deactive",
                    new
                    {
                        _id_grup_coa
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteAkunSetupGrupCoa(Int16 id_grup_coa)
        {
            try
            {
                return await this.db.executeScalarSp<short>("akun_setup_grup_coa_Delete",
                    new
                    {
                        _id_grup_coa = id_grup_coa // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
