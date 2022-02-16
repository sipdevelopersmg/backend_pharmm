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
    public class SetupSupplierDao
    {
        public SQLConn db;

        public SetupSupplierDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_supplier>> GetAllMmSetupSupplierByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_supplier>("mm_setup_supplier_GetByDynamicFilters",
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

        public async Task<List<mm_setup_supplier>> GetMmSetupSupplierById(Int16 id_supplier)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_supplier>("mm_setup_supplier_GetById", new
                {
                    _id_supplier = id_supplier
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_supplier>> GetAllMmSetupSupplier()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_supplier>("mm_setup_supplier_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupSupplier(mm_setup_supplier_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_supplier_Insert",
                    new
                    {
                        _id_tipe_supplier = data.id_tipe_supplier,
                        _kode_supplier = data.kode_supplier,
                        _nama_supplier = data.nama_supplier,
                        _alamat_supplier = data.alamat_supplier,
                        _kode_wilayah = data.kode_wilayah,
                        _negara = data.negara,
                        _telepon = data.telepon,
                        _fax = data.fax,
                        _kode_pos = data.kode_pos,
                        _email = data.email,
                        _contact_person = data.contact_person,
                        _npwp = data.npwp,
                        _default_hari_tempo_bayar = data.default_hari_tempo_bayar,
                        _default_hari_pengiriman = data.default_hari_pengiriman,
                        _default_prosentase_diskon = data.default_prosentase_diskon,
                        _default_prosentase_tax = data.default_prosentase_tax,
                        _is_tax = data.is_tax,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupSupplier(mm_setup_supplier_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_Update",
                    new
                    {
                        _id_supplier = data.id_supplier,
                        _id_tipe_supplier = data.id_tipe_supplier,
                        _kode_supplier = data.kode_supplier,
                        _nama_supplier = data.nama_supplier,
                        _alamat_supplier = data.alamat_supplier,
                        _kode_wilayah = data.kode_wilayah,
                        _negara = data.negara,
                        _telepon = data.telepon,
                        _fax = data.fax,
                        _kode_pos = data.kode_pos,
                        _email = data.email,
                        _contact_person = data.contact_person,
                        _npwp = data.npwp,
                        _default_hari_tempo_bayar = data.default_hari_tempo_bayar,
                        _default_hari_pengiriman = data.default_hari_pengiriman,
                        _default_prosentase_diskon = data.default_prosentase_diskon,
                        _default_prosentase_tax = data.default_prosentase_tax,
                        _is_tax = data.is_tax
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupSupplier(mm_setup_supplier_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_update_to_active",
                    new
                    {
                        _id_supplier = data.id_supplier
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupSupplier(mm_setup_supplier_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_supplier_update_to_deactive",
                    new
                    {
                        _id_supplier = data.id_supplier,
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
