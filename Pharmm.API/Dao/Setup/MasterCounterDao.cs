using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class MasterCounterDao
    {
        public SQLConn db;

        public MasterCounterDao(SQLConn db)
        {
            this.db = db;
        }


        public async Task<string> GenerateKode(master_counter_insert data)
        {
            try
            {
                return await this.db.QuerySPtoSingle<string>("master_counter_getby_kode_counter", new
                {
                    _kode_counter = data.kode_counter,
                    _counter_max_length = data.counter_max_length,
                    _use_alphabet = data.use_alphabet,
                    _use_dash = data.use_dash,
                    _use_date = data.use_date
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<string> AddUpdateMasterCounter(master_counter_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<string>("counter_generator",
                    new
                    {
                        _kode_counter = data.kode_counter,
                        _counter_max_length = data.counter_max_length,
                        _use_alphabet = data.use_alphabet,
                        _use_dash = data.use_dash,
                        _use_date = data.use_date,
                        _description = data.description
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
