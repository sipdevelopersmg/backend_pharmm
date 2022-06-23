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
    public class SetupItemDao
    {
        public SQLConn db;

        public SetupItemDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<phar_setup_obat_detail> GetObatDetailAktifByIdItem(long _id_item)
        {
            try
            {
                return await this.db.QuerySPtoSingle<phar_setup_obat_detail>("phar_setup_obat_detail_get_active_by_iditem", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<phar_setup_obat_detail_lock> GetObatDetailAktifByIdItemWithLock(long _id_item)
        {
            try
            {
                return await this.db.QuerySPtoSingle<phar_setup_obat_detail_lock>("phar_setup_obat_detail_get_active_by_iditem_lock", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_item>> GetAllMmSetupItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_item>("mm_setup_item_GetByDynamicFilters",
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


        public async Task<List<mm_setup_item_with_stok>> GetAllMmSetupItemByIdStockroomAndParams(short id_stockroom, List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_item_with_stok>("mm_setup_item_by_idstockroom_dynamicfilters",
                    new
                    {
                        _id_stockroom = id_stockroom,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_item>> GetAllMmSetupItemBelumSettingHargaOrderByIdSupplierAndParams(Int16 _id_supplier, List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_item>("mm_setup_item_belum_set_harga_order_by_idsupplier_dynamicfilter",
                    new
                    {
                        _id_supplier, // smallint not null
                        _filters      // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_item> GetMmSetupItemById(int id_item)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_item>("mm_setup_item_GetById", new
                {
                    _id_item = id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRak()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_with_rak>("mm_setup_item_getall_tanpa_rak");
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRak(int _id_rak_storage)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_with_rak>("mm_setup_item_getby_id_rak",
                    new
                    {
                        _id_rak_storage
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRakParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);
                return await this.db.QuerySPtoList<mm_setup_item_with_rak>("mm_setup_item_tanpa_rak_dynamicfilters", new
                {
                    _filters
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRakParams(int _id_rak_storage,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);
                return await this.db.QuerySPtoList<mm_setup_item_with_rak>("mm_setup_item_getby_id_rak_DynamicFilters",
                    new
                    {
                        _id_rak_storage,
                        _filters
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<mm_setup_item> GetMmSetupItemByIdWithLock(int id_item)
        {
            try
            {
                return await this.db.QuerySPtoSingle<mm_setup_item>("mm_setup_item_getbyid_lock", new
                {
                    _id_item = id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_item>> GetAllMmSetupItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item>("mm_setup_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupItem(mm_setup_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_item_Insert",
                    new
                    {
                        _id_grup_item = data.id_grup_item,
                        _id_pabrik = data.id_pabrik,
                        _id_supplier = data.id_supplier,
                        _kode_item = data.kode_item,
                        _barcode = data.barcode,
                        _nama_item = data.nama_item,
                        _kode_satuan = data.kode_satuan,
                        _id_temperatur_item = data.id_temperatur_item,
                        _batas_maksimal_pesan = data.batas_maksimal_pesan,
                        _batas_maksimal_pakai = data.batas_maksimal_pakai,
                        _batas_maksimal_mutasi = data.batas_maksimal_mutasi,
                        _batas_maksimal_jual = data.batas_maksimal_jual,
                        _batas_stok_kritis = data.batas_stok_kritis,
                        _prosentase_stok_kritis = data.prosentase_stok_kritis,
                        _harga_beli_terakhir = data.harga_beli_terakhir,
                        _hpp_average = data.hpp_average,
                        _prosentase_default_profit = data.prosentase_default_profit,
                        _is_ppn = data.is_ppn,
                        _user_created = data.user_created,
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupItem(mm_setup_item_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_Update",
                    new
                    {
                        _id_item = data.id_item,
                        _id_grup_item = data.id_grup_item,
                        _id_pabrik = data.id_pabrik,
                        _id_supplier = data.id_supplier,
                        _kode_item = data.kode_item,
                        _barcode = data.barcode,
                        _nama_item = data.nama_item,
                        //_kode_satuan = data.kode_satuan,
                        _id_temperatur_item = data.id_temperatur_item,
                        _batas_maksimal_pesan = data.batas_maksimal_pesan,
                        _batas_maksimal_pakai = data.batas_maksimal_pakai,
                        _batas_maksimal_mutasi = data.batas_maksimal_mutasi,
                        _batas_maksimal_jual = data.batas_maksimal_jual,
                        _batas_stok_kritis = data.batas_stok_kritis,
                        _prosentase_stok_kritis = data.prosentase_stok_kritis,
                        _harga_beli_terakhir = data.harga_beli_terakhir,
                        _hpp_average = data.hpp_average,
                        _prosentase_default_profit = data.prosentase_default_profit,
                        _is_ppn = data.is_ppn
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToActiveMmSetupItem(mm_setup_item_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_update_to_active",
                    new
                    {
                        _id_item = data.id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateToDeActiveMmSetupItem(mm_setup_item_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_update_to_deactive",
                    new
                    {
                        _id_item = data.id_item,
                        _user_deactived = data.user_deactived
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateHargaPerolehan(mm_setup_item_update_harga_perolehan data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_update_harga_perolehan",
                    new
                    {
                        _id_item = data.id_item,
                        _qty_terima = data.qty_terima,
                        _harga_beli_netto = data.harga_beli_netto
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateRak(mm_setup_item_update_rak_storage data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_update_rak_storage",
                    new
                    {
                        _id_item = data.id_item,
                        _id_rak_storage = data.id_rak_storage,
                        _user_set_rak_storage = data.user_set_rak_storage
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> HapusRak(int _id_item)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_hapus_rak_storage",
                    new
                    {
                        _id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Item Urai

        public async Task<List<mm_setup_item_urai>> GetMmSetupItemUraiByHeaderId(int _id_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_urai>("mm_setup_item_urai_GetBy_headerid", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupItemUrai(mm_setup_item_urai_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_item_urai_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_item_urai = data.id_item_urai,
                        _qty_urai = data.qty_urai
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<short> DeleteMmSetupItemUrai(mm_setup_item_urai_delete param)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_urai_Delete",
                    new
                    {
                        _id_item_urai = param.id_item_urai, // int not null
                        _id_item = param.id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion


        #region Item Assembly

        public async Task<List<mm_setup_item_assembly>> GetMmSetupItemAssemblyByHeaderId(int _id_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_assembly>("mm_setup_item_assembly_GetBy_headerid", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupItemAssembly(mm_setup_item_assembly_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_item_assembly_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_item_assembly = data.id_item_assembly,
                        _qty_assembly = data.qty_assembly
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<short> DeleteMmSetupItemAssembly(mm_setup_item_assembly_delete param)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_assembly_Delete",
                    new
                    {
                        _id_item_assembly = param.id_item_assembly, // int not null
                        _id_item = param.id_item
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
