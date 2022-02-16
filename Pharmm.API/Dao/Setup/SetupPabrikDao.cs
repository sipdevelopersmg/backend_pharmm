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
    public class SetupPabrikDao
    {
        public SQLConn db;

        public SetupPabrikDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_pabrik>> GetAllMmSetupPabrikByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_pabrik>("mm_setup_pabrik_GetByDynamicFilters",
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

        public async Task<List<mm_setup_pabrik>> GetMmSetupPabrikById(Int16 id_pabrik)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_pabrik>("mm_setup_pabrik_GetById", new
                {
                    _id_pabrik = id_pabrik
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_pabrik>> GetAllMmSetupPabrik()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_pabrik>("mm_setup_pabrik_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupPabrik(mm_setup_pabrik_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_pabrik_Insert",
                    new
                    {
                        _kode_pabrik = data.kode_pabrik,
                        _nama_pabrik = data.nama_pabrik,
                        _alamat_pabrik = data.alamat_pabrik,
                        _kode_wilayah = data.kode_wilayah,
                        _negara = data.negara,
                        _telepon = data.telepon,
                        _fax = data.fax,
                        _kode_pos = data.kode_pos,
                        _email = data.email,
                        _contact_person = data.contact_person,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupPabrik(mm_setup_pabrik_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_pabrik_Update",
                    new
                    {
                        _id_pabrik = data.id_pabrik,
                        _kode_pabrik = data.kode_pabrik,
                        _nama_pabrik = data.nama_pabrik,
                        _alamat_pabrik = data.alamat_pabrik,
                        _kode_wilayah = data.kode_wilayah,
                        _negara = data.negara,
                        _telepon = data.telepon,
                        _fax = data.fax,
                        _kode_pos = data.kode_pos,
                        _email = data.email,
                        _contact_person = data.contact_person
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_pabrik_update_to_active",
                    new
                    {
                        _id_pabrik = data.id_pabrik
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupPabrik(mm_setup_pabrik_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_pabrik_update_to_deactive",
                    new
                    {
                        _id_pabrik = data.id_pabrik,
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
