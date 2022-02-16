using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupTipeItemDao
    {
        public SQLConn db;

        public SetupTipeItemDao(SQLConn db)
        {
            this.db = db;
        }
        public async Task<List<mm_setup_tipe_item>> GetMmSetupTipeItemById(Int16 id_tipe_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_tipe_item>("mm_setup_tipe_item_GetById", new
                {
                    _id_tipe_item = id_tipe_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_tipe_item>> GetAllMmSetupTipeItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_tipe_item>("mm_setup_tipe_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupTipeItem(mm_setup_tipe_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_tipe_item_Insert",
                    new
                    {
                        _kode_tipe_item = data.kode_tipe_item,
                        _tipe_item = data.tipe_item,
                        _user_created = data.user_created
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupTipeItem(mm_setup_tipe_item_update data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_item_Update",
                    new
                    {
                        _id_tipe_item = data.id_tipe_item,
                        _kode_tipe_item = data.kode_tipe_item,
                        _tipe_item = data.tipe_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_active data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_item_update_to_active",
                    new
                    {
                        _id_tipe_item = data.id_tipe_item
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateToDeActiveMmSetupTipeItem(mm_setup_tipe_item_update_status_to_deactive data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_item_update_to_deactive",
                    new
                    {
                        _id_tipe_item = data.id_tipe_item,
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
