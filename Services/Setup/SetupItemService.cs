using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupItemService
    {

        Task<List<mm_setup_item_with_stok>> GetAllMmSetupItemByIdStockroomAndParams(short id_stockroom, List<ParameterSearchModel> param);
        Task<List<mm_setup_item>> GetAllMmSetupItemByParams(List<ParameterSearchModel> param);
        Task<List<mm_setup_item>> GetAllMmSetupItemBelumSettingHargaOrderByIdSupplierAndParams(Int16 _id_supplier, List<ParameterSearchModel> param, string notin);
        Task<List<mm_setup_item>> GetAllMmSetupItem();
        Task<mm_setup_item> GetMmSetupItemById(int id_item);

        Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRak(int _id_rak_storage);
        Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRak();
        Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRakParams(int _id_rak_storage, List<ParameterSearchModel> param);
        Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRakParams(List<ParameterSearchModel> param);

        //Task<short> AddMmSetupItem(mm_setup_item_insert data, bool isObat);
        Task<(bool,string)> AddMmSetupItem(mm_setup_item_insert data);
        Task<short> UpdateMmSetupItem(mm_setup_item_update data);
        Task<short> UpdateToActiveMmSetupItem(mm_setup_item_update_status_to_active data);
        Task<short> UpdateToDeActiveMmSetupItem(mm_setup_item_update_status_to_deactive data);
        Task<short> UpdateRak(mm_setup_item_update_rak_storage data);
        Task<short> HapusRak(int _id_item);

        #region Item Urai

        Task<List<mm_setup_item_urai>> GetMmSetupItemUraiByHeaderId(int _id_item);
        Task<(bool,int, string)> AddMmSetupItemUrai(mm_setup_item_urai_insert data);
        Task<short> DeleteMmSetupItemUrai(mm_setup_item_urai_delete param);

        #endregion

        #region Item Assembly

        Task<List<mm_setup_item_assembly>> GetMmSetupItemAssemblyByHeaderId(int _id_item);
        Task<(bool, int, string)> AddMmSetupItemAssembly(mm_setup_item_assembly_insert data);
        Task<short> DeleteMmSetupItemAssembly(mm_setup_item_assembly_delete param);

        #endregion
    }

    public class SetupItemService : ISetupItemService
    {
        private readonly SQLConn _db;
        private readonly SetupItemDao _dao;
        private readonly SetupObatDao _obatDao;

        public SetupItemService(SQLConn db, SetupItemDao dao,
            SetupObatDao obatDao)
        {
            this._db = db;
            this._dao = dao;
            this._obatDao = obatDao;
        }


        public async Task<List<mm_setup_item>> GetAllMmSetupItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_item_with_stok>> GetAllMmSetupItemByIdStockroomAndParams(short id_stockroom, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllMmSetupItemByIdStockroomAndParams(id_stockroom, param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_item>> GetAllMmSetupItemBelumSettingHargaOrderByIdSupplierAndParams(short _id_supplier, List<ParameterSearchModel> param, string notin)
        {
            try
            {
                return await this._dao.GetAllMmSetupItemBelumSettingHargaOrderByIdSupplierAndParams(_id_supplier, param, notin);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_item>> GetAllMmSetupItem()
        {
            try
            {
                return await this._dao.GetAllMmSetupItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRak()
        {
            try
            {
                return await this._dao.GetMmSetupItemBelumRak();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRak(int _id_rak_storage)
        {
            try
            {
                return await this._dao.GetMmSetupItemByIdRak(_id_rak_storage);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemBelumRakParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetMmSetupItemBelumRakParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<mm_setup_item_with_rak>> GetMmSetupItemByIdRakParams(int _id_rak_storage,List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetMmSetupItemByIdRakParams(_id_rak_storage,param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<mm_setup_item> GetMmSetupItemById(int id_item)
        {
            try
            {
                return await this._dao.GetMmSetupItemById(id_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool,string)> AddMmSetupItem(mm_setup_item_insert data)
        {
            this._db.beginTransaction();

            try
            {
                var barangId = await this._dao.AddMmSetupItem(data);

                if (barangId > 0)
                {
                    if (data.is_obat && data.obat is not null)
                    {
                        data.obat.id_item = barangId;
                        var inputObat = await this._obatDao.AddPharSetupObatFromSetupItem(data.obat);

                        if (inputObat > 0)
                        {
                            if (data.obat.details.Count > 0)
                            {
                                foreach (var detail in data.obat.details)
                                {
                                    var detailId = (short)0;
                                    detail.user_created = data.user_created;
                                    detail.id_item = inputObat;
                                    var cekHargaExistingAktif = await this._obatDao.GetPharSetupObatDetailAktifByIdItemAndTglBerlakuWithLock((int)detail.id_item,
                                     detail.tgl_berlaku);


                                    if (cekHargaExistingAktif is not null)
                                    {

                                        var paramUpdate = new phar_setup_obat_detail_update
                                        {
                                            id_obat_detail = (long)cekHargaExistingAktif.id_obat_detail,
                                            harga_jual_apotek = (decimal)detail.harga_jual_apotek,
                                            harga_netto_apotek = detail.harga_netto_apotek,
                                            prosentase_ppn = detail.prosentase_ppn,
                                            prosentase_profit = detail.prosentase_profit,
                                            user_edited = detail.user_created
                                        };

                                        var updateDetail = await this._obatDao.UpdatePharSetupObatDetail(paramUpdate);

                                        if (updateDetail <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, $"Gagal merubah data");
                                        }
                                    }
                                    else
                                    {

                                        detailId = await this._obatDao.AddPharSetupObatDetailFromBarang(detail);

                                        if (detailId <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, $"Gagal menambahkan harga jual");
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            this._db.rollBackTrans();
                            return (false, "Gagal insert ke tabel obat");
                        }
                    }
                }
                else if (barangId == -1)
                {
                    //ketika barang dengan nama itu sudah ada
                    return (true, "exists");
                };

                this._db.commitTrans();

                return (true,"SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
            }
        }

        //public async Task<short> AddMmSetupItem(mm_setup_item_insert data)
        //{
        //    try
        //    {
        //        return await this._dao.AddMmSetupItem(data);
        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //        //TODO : log error
        //    }
        //}

        public async Task<short> UpdateMmSetupItem(mm_setup_item_update data)
        {
            try
            {
                return await this._dao.UpdateMmSetupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateToActiveMmSetupItem(mm_setup_item_update_status_to_active data)
        {
            try
            {
                return await this._dao.UpdateToActiveMmSetupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateToDeActiveMmSetupItem(mm_setup_item_update_status_to_deactive data)
        {
            try
            {
                return await this._dao.UpdateToDeActiveMmSetupItem(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdateRak(mm_setup_item_update_rak_storage data)
        {
            try
            {
                return await this._dao.UpdateRak(data);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<short> HapusRak(int _id_item)
        {
            try
            {
                return await this._dao.HapusRak(_id_item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Item Urai

        public async Task<List<mm_setup_item_urai>> GetMmSetupItemUraiByHeaderId(int _id_item)
        {
            try
            {
                return await this._dao.GetMmSetupItemUraiByHeaderId(_id_item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, int, string)> AddMmSetupItemUrai(mm_setup_item_urai_insert data)
        {
            try
            {
                var hasil = await this._dao.AddMmSetupItemUrai(data);

                if(hasil <= 0)
                {
                    return (false, hasil , $"Gagal input barang urai");
                }

                return (true, hasil, "SUCCESS");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<short> DeleteMmSetupItemUrai(mm_setup_item_urai_delete param)
        {
            try
            {
                return await this._dao.DeleteMmSetupItemUrai(param);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region Item Assembly

        public async Task<List<mm_setup_item_assembly>> GetMmSetupItemAssemblyByHeaderId(int _id_item)
        {
            try
            {
                return await this._dao.GetMmSetupItemAssemblyByHeaderId(_id_item);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<(bool, int, string)> AddMmSetupItemAssembly(mm_setup_item_assembly_insert data)
        {
            try
            {
                var hasil = await this._dao.AddMmSetupItemAssembly(data);

                if (hasil <= 0)
                {
                    return (false, hasil, $"Gagal input barang assembly");
                }

                return (true, hasil, "SUCCESS");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<short> DeleteMmSetupItemAssembly(mm_setup_item_assembly_delete param)
        {
            try
            {
                return await this._dao.DeleteMmSetupItemAssembly(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


    }
}
