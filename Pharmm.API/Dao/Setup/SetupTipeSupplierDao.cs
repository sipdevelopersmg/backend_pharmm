using DapperPostgreSQL;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupTipeSupplierDao
    {
        public SQLConn db;

        public SetupTipeSupplierDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_tipe_supplier>> GetAllMmSetupTipeSupplier()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_tipe_supplier>("mm_setup_tipe_supplier_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupTipeSupplier(mm_setup_tipe_supplier_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_tipe_supplier_Insert",
                    new
                    {
                        _tipe_supplier = data.tipe_supplier,
                        _is_ap = data.is_ap
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupTipeSupplier(mm_setup_tipe_supplier_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_supplier_Update",
                    new
                    {
                        _id_tipe_supplier = data.id_tipe_supplier,
                        _tipe_supplier = data.tipe_supplier,
                        _is_ap = data.is_ap
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_supplier_update_to_active",
                    new
                    {
                        _id_tipe_supplier = data.id_tipe_supplier
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupTipeSupplier(mm_setup_tipe_supplier_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_supplier_update_to_deactive",
                    new
                    {
                        _id_tipe_supplier = data.id_tipe_supplier
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>DeleteMmSetupTipeSupplier(Int16 id_tipe_supplier)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_supplier_Delete",
                    new
                    {
                        _id_tipe_supplier = id_tipe_supplier // int not null
                            });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
