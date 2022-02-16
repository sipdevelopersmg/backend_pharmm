using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupItemSatuanDao
    {
        public SQLConn db;

        public SetupItemSatuanDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<List<mm_setup_item_satuan>> GetMmSetupItemSatuanByIdItem(int id_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_satuan>("mm_setup_item_satuan_getby_iditem", new
                {
                    _id_item = id_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<mm_setup_item_satuan>> GetAllMmSetupItemSatuan()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_item_satuan>("mm_setup_item_satuan_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupItemSatuan(mm_setup_item_satuan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_item_satuan_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _kode_satuan = data.kode_satuan,
                        _isi = data.isi,
                        _is_satuan_beli = data.is_satuan_beli
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupItemSatuan(mm_setup_item_satuan_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_satuan_Update",
                    new
                    {
                        _id_item = data.id_item,
                        _kode_satuan = data.kode_satuan,
                        _isi = data.isi,
                        _is_satuan_beli = data.is_satuan_beli
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>DeleteMmSetupItemSatuan(mm_setup_item_satuan_delete data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_satuan_Delete",
                    new
                    {
                        _id_item = data.id_item,
                        _kode_satuan = data.kode_satuan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> DeleteMmSetupItemByIdItem(int _id_item)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_item_satuan_delete_by_item",
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

    }
}
