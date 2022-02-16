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
    public class SetupAuditPenanggungJawabRakStorageDao
    {
        public SQLConn db;

        public SetupAuditPenanggungJawabRakStorageDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_audit_penanggung_jawab_rak_storage>> GetAllMmSetupAuditPenanggungJawabRakStorageByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_audit_penanggung_jawab_rak_storage>("mm_setup_audit_penanggung_jawab_rak_storage_GetByDynamicFilters",
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


        public async Task<mm_setup_audit_penanggung_jawab_rak_storage> GetMmSetupAuditPenanggungJawabRakStorageById(int id_penanggung_jawab_rak_storage)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_audit_penanggung_jawab_rak_storage>("mm_setup_audit_penanggung_jawab_rak_storage_GetById", new
                {
                    _id_penanggung_jawab_rak_storage = id_penanggung_jawab_rak_storage
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> AddMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_audit_penanggung_jawab_rak_storage_Insert",
                    new
                    {
                        _nik_penanggung_jawab_rak_storage = data.nik_penanggung_jawab_rak_storage,
                        _nama_penanggung_jawab_rak_storage = data.nama_penanggung_jawab_rak_storage,
                        _id_supplier = data.id_supplier,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupAuditPenanggungJawabRakStorage(mm_setup_audit_penanggung_jawab_rak_storage_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_audit_penanggung_jawab_rak_storage_Update",
                    new
                    {
                        _id_penanggung_jawab_rak_storage = data.id_penanggung_jawab_rak_storage,
                        _nik_penanggung_jawab_rak_storage = data.nik_penanggung_jawab_rak_storage,
                        _nama_penanggung_jawab_rak_storage = data.nama_penanggung_jawab_rak_storage,
                        _id_supplier = data.id_supplier,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteMmSetupAuditPenanggungJawabRakStorage(int id_penanggung_jawab_rak_storage)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_audit_penanggung_jawab_rak_storage_Delete",
                    new
                    {
                        _id_penanggung_jawab_rak_storage = id_penanggung_jawab_rak_storage // int not null
                        });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
