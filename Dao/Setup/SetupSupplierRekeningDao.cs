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
    public class SetupSupplierRekeningDao
    {
        public SQLConn db;

        public SetupSupplierRekeningDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekeningByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_supplier_rekening>("mm_setup_supplier_rekening_GetByDynamicFilters",
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

        public async Task<List<mm_setup_supplier_rekening>> GetMmSetupSupplierRekeningById(Int16 id_supplier_rekening)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_supplier_rekening>("mm_setup_supplier_rekening_GetById", new
                {
                    _id_supplier_rekening = id_supplier_rekening
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_supplier_rekening>> GetAllMmSetupSupplierRekening()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_supplier_rekening>("mm_setup_supplier_rekening_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupSupplierRekening(mm_setup_supplier_rekening_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_supplier_rekening_Insert",
                    new
                    {
                        _id_supplier = data.id_supplier,
                        _bank = data.bank,
                        _nomor_rekening = data.nomor_rekening,
                        _nama_rekening = data.nama_rekening,
                        _mata_uang = data.mata_uang,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupSupplierRekening(mm_setup_supplier_rekening_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_rekening_Update",
                    new
                    {
                        _id_supplier_rekening = data.id_supplier_rekening,
                        _id_supplier = data.id_supplier,
                        _bank = data.bank,
                        _nomor_rekening = data.nomor_rekening,
                        _nama_rekening = data.nama_rekening,
                        _mata_uang = data.mata_uang
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_rekening_update_to_active",
                    new
                    {
                        _id_supplier_rekening = data.id_supplier_rekening
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSupplierRekening(mm_setup_supplier_rekening_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_rekening_update_to_deactive",
                    new
                    {
                        _id_supplier_rekening = data.id_supplier_rekening
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
