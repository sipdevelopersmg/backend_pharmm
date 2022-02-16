using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupTemperaturItemDao
    {
        public SQLConn db;

        public SetupTemperaturItemDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_temperatur_item>> GetMmSetupTemperaturItemById(Int16 id_temperatur_item)
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_temperatur_item>("mm_setup_temperatur_item_GetById", new
                {
                    _id_temperatur_item = id_temperatur_item
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<mm_setup_temperatur_item>> GetAllMmSetupTemperaturItem()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_temperatur_item>("mm_setup_temperatur_item_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupTemperaturItem(mm_setup_temperatur_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_temperatur_item_Insert",
                    new
                    {
                        _temperatur_item = data.temperatur_item,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupTemperaturItem(mm_setup_temperatur_item data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_temperatur_item_Update",
                    new
                    {
                        _id_temperatur_item = data.id_temperatur_item,
                        _temperatur_item = data.temperatur_item,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>DeleteMmSetupTemperaturItem(Int16 id_temperatur_item)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_temperatur_item_Delete",
                    new
                    {
                        _id_temperatur_item = id_temperatur_item // int not null
                            });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
