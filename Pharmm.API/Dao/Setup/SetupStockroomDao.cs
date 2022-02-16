using DapperPostgreSQL;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupStockroomDao
    {
        public SQLConn db;

        public SetupStockroomDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_stockroom>> GetAllMmSetupStockroomByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_stockroom>("mm_setup_stockroom_GetByDynamicFilters",
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

        public async Task<List<mm_setup_stockroom>> GetMmSetupStockroomById(Int16 id_stockroom)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stockroom>("mm_setup_stockroom_GetById", new
                {
                    _id_stockroom = id_stockroom
                });
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
                return await this.db.QuerySPtoList<mm_setup_stockroom>("mm_setup_stockroom_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupStockroom(mm_setup_stockroom_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_stockroom_Insert",
                    new
                    {
                        _kode_stockroom = data.kode_stockroom,
                        _nama_stockroom = data.nama_stockroom,
                        _id_tipe_stockroom = data.id_tipe_stockroom,
                        _store_type = data.store_type,
                        _gl_no = data.gl_no,
                        _gl_dept_name = data.gl_dept_name,
                        _id_stockroom_parent = data.id_stockroom_parent,
                        _is_show_persediaan = data.is_show_persediaan,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupStockroom(mm_setup_stockroom_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stockroom_Update",
                    new
                    {
                        _id_stockroom = data.id_stockroom,
                        _kode_stockroom = data.kode_stockroom,
                        _nama_stockroom = data.nama_stockroom,
                        _id_tipe_stockroom = data.id_tipe_stockroom,
                        _store_type = data.store_type,
                        _gl_no = data.gl_no,
                        _gl_dept_name = data.gl_dept_name,
                        _id_stockroom_parent = data.id_stockroom_parent,
                        _is_show_persediaan = data.is_show_persediaan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stockroom_update_to_active",
                    new
                    {
                        _id_stockroom = data.id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupStockroom(mm_setup_stockroom_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stockroom_update_to_deactive",
                    new
                    {
                        _id_stockroom = data.id_stockroom,
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
