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
    public class SetupObatDao
    {
        public SQLConn db;

        public SetupObatDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<phar_setup_obat>> GetAllPharSetupObatByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<phar_setup_obat>("phar_setup_obat_GetByDynamicFilters",
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

        public async Task<List<phar_setup_obat>> GetAllPharSetupObat()
        {
            try
            {
                return await this.db.QuerySPtoList<phar_setup_obat>("phar_setup_obat_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPharSetupObat(phar_setup_obat_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("phar_setup_obat_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_grup_obat = data.id_grup_obat,
                        _id_cara_pakai_obat = data.id_cara_pakai_obat,
                        _id_rute_pemberian_obat = data.id_rute_pemberian_obat,
                        _id_restriksi_obat = data.id_restriksi_obat,
                        _id_peresepan_maksimal = data.id_peresepan_maksimal,
                        _kandungan_obat = data.kandungan_obat,
                        _is_fornas = data.is_fornas,
                        _is_narkotika = data.is_narkotika
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPharSetupObatFromSetupItem(phar_setup_obat_insert_from_barang data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("phar_setup_obat_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_grup_obat = data.id_grup_obat,
                        _id_cara_pakai_obat = data.id_cara_pakai_obat,
                        _id_rute_pemberian_obat = data.id_rute_pemberian_obat,
                        _id_restriksi_obat = data.id_restriksi_obat,
                        _id_peresepan_maksimal = data.id_peresepan_maksimal,
                        _kandungan_obat = data.kandungan_obat,
                        _is_fornas = data.is_fornas,
                        _is_narkotika = data.is_narkotika
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdatePharSetupObat(phar_setup_obat_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("phar_setup_obat_Update",
                    new
                    {
                        _id_item = data.id_item,
                        _id_grup_obat = data.id_grup_obat,
                        _id_cara_pakai_obat = data.id_cara_pakai_obat,
                        _id_rute_pemberian_obat = data.id_rute_pemberian_obat,
                        _id_restriksi_obat = data.id_restriksi_obat,
                        _id_peresepan_maksimal = data.id_peresepan_maksimal,
                        _kandungan_obat = data.kandungan_obat,
                        _is_fornas = data.is_fornas,
                        _is_narkotika = data.is_narkotika
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeletePharSetupObat(int id_item)
        {
            try
            {
                return await this.db.executeScalarSp<short>("phar_setup_obat_Delete",
                    new
                    {
                        _id_item = id_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #region Setup Obat Detail



        public async Task<phar_setup_obat_detail> GetPharSetupObatDetailAktifByIdItemWithLock(int _id_item)
        {
            try
            {
                return await this.db.QuerySPtoSingle<phar_setup_obat_detail>("phar_setup_obat_detail_Getby_id_item_lock", new
                {
                    _id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<phar_setup_obat_detail> GetPharSetupObatDetailAktifByIdItem(long _id_item)
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

        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<phar_setup_obat_detail>("phar_setup_obat_detail_GetByDynamicFilters",
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
        
        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByIdAndParams(
            int _id_item,
            List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<phar_setup_obat_detail>("phar_setup_obat_detail_GetBy_iditem_DynamicFilters",
                    new
                    {
                        _id_item,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetail()
        {
            try
            {
                return await this.db.QuerySPtoList<phar_setup_obat_detail>("phar_setup_obat_detail_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPharSetupObatDetail(phar_setup_obat_detail_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("phar_setup_obat_detail_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _harga_netto_apotek = data.harga_netto_apotek,
                        _prosentase_profit = data.prosentase_profit,
                        _prosentase_ppn = data.prosentase_ppn,
                        _harga_jual_apotek = data.harga_jual_apotek,
                        _tgl_berlaku = data.tgl_berlaku,
                        _tgl_berakhir = data.tgl_berakhir,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddPharSetupObatDetailFromSetupItem(phar_setup_obat_detail_insert_from_barang data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("phar_setup_obat_detail_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _harga_netto_apotek = data.harga_netto_apotek,
                        _prosentase_profit = data.prosentase_profit,
                        _prosentase_ppn = data.prosentase_ppn,
                        _harga_jual_apotek = data.harga_jual_apotek,
                        _tgl_berlaku = data.tgl_berlaku,
                        _tgl_berakhir = data.tgl_berakhir,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateStatusToDeactivePharSetupObatDetail(phar_setup_obat_detail_update_status data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("phar_setup_obat_detail_update_to_deactive",
                    new
                    {
                        _id_obat_detail = data.id_obat_detail,
                        _tgl_berakhir = data.tgl_berakhir,
                        _user_edited = data.user_edited
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
