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
    public class SetupGrupItemDao
    {
        public SQLConn db;

        public SetupGrupItemDao(SQLConn db)
        {
            this.db = db;
        }



        public async Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_grup_item>("mm_setup_grup_item_GetByDynamicFilters",
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

        public async Task<List<mm_setup_grup_item>> GetAllMmSetupGrupItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_grup_item>("mm_setup_grup_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupGrupItem(mm_setup_grup_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_grup_item_Insert",
                    new
                    {
                        _id_tipe_item = data.id_tipe_item,
                        _kode_grup_item = data.kode_grup_item,
                        _grup_item = data.grup_item,
                        _last_no = data.last_no,
                        _id_coa_persediaan = data.id_coa_persediaan,
                        _id_coa_pendapatan = data.id_coa_pendapatan,
                        _id_coa_biaya = data.id_coa_biaya,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupGrupItem(mm_setup_grup_item_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_grup_item_Update",
                    new
                    {
                        _id_grup_item = data.id_grup_item,
                        _id_tipe_item = data.id_tipe_item,
                        _kode_grup_item = data.kode_grup_item,
                        _grup_item = data.grup_item,
                        _last_no = data.last_no,
                        _id_coa_persediaan = data.id_coa_persediaan,
                        _id_coa_pendapatan = data.id_coa_pendapatan,
                        _id_coa_biaya = data.id_coa_biaya
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_grup_item_update_to_active",
                    new
                    {
                        _id_grup_item = data.id_grup_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToDeActiveMmSetupGrupItem(mm_setup_grup_item_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_grup_item_update_to_deactive",
                    new
                    {
                        _id_grup_item = data.id_grup_item,
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
