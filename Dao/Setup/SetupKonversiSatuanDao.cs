using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using QueryModel.Utility;

namespace Pharmm.API.Dao.Setup
{
    public class SetupKonversiSatuanDao
    {
        public SQLConn db;

        public SetupKonversiSatuanDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuanByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<mm_setup_konversi_satuan>("mm_setup_konversi_satuan_GetByDynamicFilters",
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

        public async Task<List<mm_setup_konversi_satuan>> GetAllMmSetupKonversiSatuan()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_konversi_satuan>("mm_setup_konversi_satuan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupKonversiSatuan(mm_setup_konversi_satuan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_konversi_satuan_Insert",
                    new
                    {
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _kode_satuan_kecil = data.kode_satuan_kecil,
                        _faktor_konversi = data.faktor_konversi,
                        _faktor_dekonversi = data.faktor_dekonversi,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupKonversiSatuan(mm_setup_konversi_satuan_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_konversi_satuan_Update",
                    new
                    {
                        _id_konversi_satuan = data.id_konversi_satuan,
                        _kode_satuan_besar = data.kode_satuan_besar,
                        _kode_satuan_kecil = data.kode_satuan_kecil,
                        _faktor_konversi = data.faktor_konversi,
                        _faktor_dekonversi = data.faktor_dekonversi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>DeleteMmSetupKonversiSatuan(int id_konversi_satuan)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_konversi_satuan_Delete",
                    new
                    {
                        _id_konversi_satuan = id_konversi_satuan // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
