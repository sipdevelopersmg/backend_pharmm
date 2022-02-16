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
    public class SetupTipeStockroomDao
    {
        public SQLConn db;

        public SetupTipeStockroomDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_tipe_stockroom>> GetAllMmSetupTipeStockroom()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_tipe_stockroom>("mm_setup_tipe_stockroom_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> AddMmSetupTipeStockroom(mm_setup_tipe_stockroom_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_tipe_stockroom_Insert",
                    new
                    {
                        _tipe_stockroom = data.tipe_stockroom,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateMmSetupTipeStockroom(mm_setup_tipe_stockroom data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_stockroom_Update",
                    new
                    {
                        _id_tipe_stockroom = data.id_tipe_stockroom,
                        _tipe_stockroom = data.tipe_stockroom,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteMmSetupTipeStockroom(Int16 id_tipe_stockroom)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_tipe_stockroom_Delete",
                    new
                    {
                        _id_tipe_stockroom = id_tipe_stockroom // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
