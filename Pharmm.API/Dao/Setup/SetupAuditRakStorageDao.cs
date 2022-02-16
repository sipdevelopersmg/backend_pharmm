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
    public class SetupAuditRakStorageDao
    {
        public SQLConn db;

        public SetupAuditRakStorageDao(SQLConn db)
        {
            this.db = db;
        }


        #region Audit Rak

        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerRakByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id)
        {
            try
            {

                return await this.db.QuerySPtoList<mm_setup_audit_rak_storage_for_stok_opname>("mm_setup_audit_rak_storage_per_rak_getby_idstockroom_settingid",
                    new
                    {
                        _id_stockroom,
                        _setting_stok_opname_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerGrupByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id)
        {
            try
            {

                return await this.db.QuerySPtoList<mm_setup_audit_rak_storage_for_stok_opname>(
                    "mm_setup_audit_rak_storage_per_grup_getby_idstockroom_settingid",
                    new
                    {
                        _id_stockroom,
                        _setting_stok_opname_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStoragePerItemByIdStockroomAndSettingStokOpnameId(
            short _id_stockroom,
            long _setting_stok_opname_id)
        {
            try
            {

                return await this.db.QuerySPtoList<mm_setup_audit_rak_storage_for_stok_opname>(
                    "mm_setup_audit_rak_storage_per_item_getby_idstockroom_settingid",
                    new
                    {
                        _id_stockroom,
                        _setting_stok_opname_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_audit_rak_storage_for_stok_opname>> GetAllMmSetupAuditRakStorageAllItemByIdStockroom(
            short _id_stockroom
            )
        {
            try
            {

                return await this.db.QuerySPtoList<mm_setup_audit_rak_storage_for_stok_opname>(
                    "mm_setup_audit_rak_storage_all_item_getby_idstockroom",
                    new
                    {
                        _id_stockroom
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_audit_rak_storage>> GetAllMmSetupAuditRakStorageByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_audit_rak_storage>("mm_setup_audit_rak_storage_GetByDynamicFilters",
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

        public async Task<mm_setup_audit_rak_storage> GetMmSetupAuditRakStorageById(int id_rak_storage)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_audit_rak_storage>("mm_setup_audit_rak_storage_GetById", new
                {
                    _id_rak_storage = id_rak_storage
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_audit_rak_storage> GetMmSetupAuditRakStorageByIdWithLock(int id_rak_storage)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_audit_rak_storage>("mm_setup_audit_rak_storage_GetById_lock", new
                {
                    _id_rak_storage = id_rak_storage
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupAuditRakStorage(mm_setup_audit_rak_storage_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_audit_rak_storage_Insert",
                    new
                    {
                        _kode_rak_storage = data.kode_rak_storage,
                        _nama_rak_storage = data.nama_rak_storage,
                        _id_stockroom = data.id_stockroom,
                        _id_penanggung_jawab_rak_storage = data.id_penanggung_jawab_rak_storage,
                        _keterangan = data.keterangan,
                        _user_inputed = data.user_inputed,
                        _user_set_penanggung_jawab_rak_storage = data.user_set_penanggung_jawab_rak_storage
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupAuditRakStorage(mm_setup_audit_rak_storage_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_audit_rak_storage_Update",
                    new
                    {
                        _id_rak_storage = data.id_rak_storage,
                        _kode_rak_storage = data.kode_rak_storage,
                        _nama_rak_storage = data.nama_rak_storage,
                        _id_stockroom = data.id_stockroom,
                        _id_penanggung_jawab_rak_storage = data.id_penanggung_jawab_rak_storage,
                        _keterangan = data.keterangan,
                        _user_set_penanggung_jawab_rak_storage = data.user_set_penanggung_jawab_rak_storage
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> DeleteMmSetupAuditRakStorage(int id_rak_storage)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_audit_rak_storage_Delete",
                    new
                    {
                        _id_rak_storage = id_rak_storage // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



    }
}
