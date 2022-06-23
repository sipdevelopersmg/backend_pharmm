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
    public class SetupSatuanDao
    {
        public SQLConn db;

        public SetupSatuanDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_satuan>("mm_setup_satuan_GetByDynamicFilters",
                    new
                    {
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuanByParamsWithLimit(ParameterSearchWithLimit param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param.paramSearch);

                return await this.db.QuerySPtoList<mm_setup_satuan>("mm_setup_satuan_GetByDynamicFiltersWithLimit",
                    new
                    {
                        _filters,    // not null
                        _row_count = param.row_count,
                        _page = param.page
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_satuan> GetAllMmSetupSatuanById(
            string _kode_satuan
            )
        {
            try
            {
                return await this.db.QuerySPtoSingle <mm_setup_satuan>("mm_setup_satuan_GetById", new
                {
                    _kode_satuan
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<mm_setup_satuan> GetAllMmSetupSatuanByIdWithLock(
            string _kode_satuan
            )
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_satuan>("mm_setup_satuan_GetById_lock", new
                {
                    _kode_satuan
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_satuan>> GetAllMmSetupSatuan()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_satuan>("mm_setup_satuan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupSatuan(mm_setup_satuan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_satuan_Insert",
                    new
                    {
                        _kode_satuan = data.kode_satuan,
                        _nama_satuan = data.nama_satuan,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupSatuan(mm_setup_satuan_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_satuan_Update",
                    new
                    {
                        _kode_satuan = data.kode_satuan,
                        _nama_satuan = data.nama_satuan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupSatuan(mm_setup_satuan_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_satuan_update_to_active",
                    new
                    {
                        _kode_satuan = data.kode_satuan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSatuan(mm_setup_satuan_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_satuan_update_to_deactive",
                    new
                    {
                        _kode_satuan = data.kode_satuan,
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
