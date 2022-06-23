using DapperPostgreSQL;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Setup
{
    public class SetupPerencanaanKategoriDao
    {
        public SQLConn db;

        public SetupPerencanaanKategoriDao(SQLConn db)
        {
            this.db = db;
        }

        public async Task<List<mm_setup_perencanaan_kategori>> GetAllMmSetupPerencanaanKategori()
        {
            try
            {
                return await this.db.QuerySPtoList<mm_setup_perencanaan_kategori>("mm_setup_perencanaan_kategori_GetAll");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>AddMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<Int16>("mm_setup_perencanaan_kategori_Insert",
                    new
                    {
                        _kategori = data.kategori
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>UpdateMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_perencanaan_kategori_Update",
                    new
                    {
                        _id_kategori = data.id_kategori,
                        _kategori = data.kategori
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short>DeleteMmSetupPerencanaanKategori(Int16 id_kategori)
        {
            try
            {
                return await this.db.executeScalarSp<short>("mm_setup_perencanaan_kategori_Delete",
                    new
                    {
                        _id_kategori = id_kategori // int not null
                            });
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
