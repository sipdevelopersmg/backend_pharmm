using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupStokItemDetailBatchDao
    {
        public SQLConn db;

        public SetupStokItemDetailBatchDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_stok_item_detail_batch>> GetAllMmSetupStokItemDetailBatch()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_stok_item_detail_batch>("mm_setup_stok_item_detail_batch_GetAll");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<short>AddMmSetupStokItemDetailBatch(mm_setup_stok_item_detail_batch data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_stok_item_detail_batch_Insert",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<short>UpdatePenambahanStokSetupStokItemDetailBatch(mm_setup_stok_item_detail_update_penambahan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_detail_batch_Update_penambahan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<short>UpdatePenguranganStokSetupStokItemDetailBatch(mm_setup_stok_item_detail_update_pengurangan_stok data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_stok_item_detail_batch_Update_pengurangan_stok",
                    new
                    {
                        _id_item = data.id_item,
                        _id_stockroom = data.id_stockroom,
                        _batch_number = data.batch_number,
                        _qty_on_hand = data.qty_on_hand
                    });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
