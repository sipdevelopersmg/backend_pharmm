using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupItemSatuanService
    {
        Task<List<mm_setup_item_satuan>> GetMmSetupItemSatuanByIdItem(int id_item);
        Task<List<mm_setup_item_satuan>> GetAllMmSetupItemSatuan();

        Task<short> AddMmSetupItemSatuan(mm_setup_item_satuan_insert data);
        Task<(bool, long, string)> AddMmSetupItemSatuanMultiple(List<mm_setup_item_satuan_insert> data);

        Task<(bool, long, string)> UpdateMmSetupItemSatuanMultiple(List<mm_setup_item_satuan_insert> data);
        Task<short> UpdateMmSetupItemSatuan(mm_setup_item_satuan_insert data);
        Task<short> DeleteMmSetupItemSatuan(mm_setup_item_satuan_delete data);

    }

    public class SetupItemSatuanService : ISetupItemSatuanService
    {
        private readonly SQLConn _db;
        private readonly SetupItemSatuanDao _dao;
        private readonly SetupItemDao _itemDao;
        private readonly SetupSatuanDao _satuanDao;


        public SetupItemSatuanService(SQLConn db, SetupItemSatuanDao dao,
            SetupItemDao itemDao,
            SetupSatuanDao satuanDao)
        {
            this._db = db;
            this._dao = dao;
            this._itemDao = itemDao;
            this._satuanDao = satuanDao;
        }

        public async Task<List<mm_setup_item_satuan>> GetMmSetupItemSatuanByIdItem(int id_item)
        {
            try
            {

                return await this._dao.GetMmSetupItemSatuanByIdItem(id_item);
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
                return await this._dao.GetAllMmSetupItemSatuan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddMmSetupItemSatuan(mm_setup_item_satuan_insert data)
        {
            try
            {
                return await this._dao.AddMmSetupItemSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> AddMmSetupItemSatuanMultiple(List<mm_setup_item_satuan_insert> data)
        {

            this._db.beginTransaction();
            try
            {
                if (data.Count > 0)
                {
                    foreach (var item in data)
                    {
                        var result = await this._dao.AddMmSetupItemSatuan(item);

                        if (result == 0)
                        {

                            this._db.rollBackTrans();
                            return (false, result, $"Gagal input item satuan");

                        }
                        else if (result == -1)
                        {

                            this._db.rollBackTrans();

                            //get data barang by id
                            var barang = await this._itemDao.GetMmSetupItemByIdWithLock((int)item.id_item);
                            var satuan = await this._satuanDao.GetAllMmSetupSatuanByIdWithLock(item.kode_satuan);

                            return (false, 0, $"Item ini sudah tersedia {barang?.nama_item}, satuan {satuan.nama_satuan}");
                        }
                    }
                }


                this._db.commitTrans();
                return (true, 1, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> UpdateMmSetupItemSatuanMultiple(List<mm_setup_item_satuan_insert> data)
        {

            this._db.beginTransaction();
            try
            {
                if (data.Count > 0)
                {
                    //menghapus satuan lama, dan input satuan baru
                    var delete = await this._dao.DeleteMmSetupItemByIdItem(data.First().id_item);

                    if (delete > 0)
                    {
                        foreach (var item in data)
                        {
                            //menghapus satuan lama, dan input satuan baru
                            var result = await this._dao.AddMmSetupItemSatuan(item);

                            if (result == 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal merubah item satuan");
                            }
                            else if (result == -1)
                            {

                                this._db.rollBackTrans();

                                //get data barang by id
                                var barang = await this._itemDao.GetMmSetupItemByIdWithLock((int)item.id_item);
                                var satuan = await this._satuanDao.GetAllMmSetupSatuanByIdWithLock(item.kode_satuan);

                                return (false, 0, $"Item ini sudah tersedia {barang?.nama_item}, satuan {satuan.nama_satuan}");
                            }

                        }
                    }
                    else
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal menghapus satuan sebelumnya");
                    }
                }


                this._db.commitTrans();
                return (true, 1, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateMmSetupItemSatuan(mm_setup_item_satuan_insert data)
        {
            try
            {
                return await this._dao.UpdateMmSetupItemSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteMmSetupItemSatuan(mm_setup_item_satuan_delete data)
        {
            try
            {
                return await this._dao.DeleteMmSetupItemSatuan(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
    }
}
