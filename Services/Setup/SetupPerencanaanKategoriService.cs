using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupPerencanaanKategoriService
    {

        Task<List<mm_setup_perencanaan_kategori>> GetAllMmSetupPerencanaanKategori();

        Task<short>AddMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori_insert data);
        Task<short>UpdateMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori data);
        Task<short>DeleteMmSetupPerencanaanKategori(Int16 id_kategori);

    }

    public class SetupPerencanaanKategoriService : ISetupPerencanaanKategoriService
    {
        private readonly SQLConn _db;
        private readonly SetupPerencanaanKategoriDao _dao;

        public SetupPerencanaanKategoriService(SQLConn db, SetupPerencanaanKategoriDao dao)
        {
            this._db = db;
            this._dao = dao;
        }


        public async Task<List<mm_setup_perencanaan_kategori>> GetAllMmSetupPerencanaanKategori()
        {
            try
            {
                return await this._dao.GetAllMmSetupPerencanaanKategori();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>AddMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupPerencanaanKategori(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>UpdateMmSetupPerencanaanKategori(mm_setup_perencanaan_kategori data)
        {
            try
            {
                return await this._dao.UpdateMmSetupPerencanaanKategori(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short>DeleteMmSetupPerencanaanKategori(Int16 id_kategori)
        {
            try
            {
                return await this._dao.DeleteMmSetupPerencanaanKategori(id_kategori);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
