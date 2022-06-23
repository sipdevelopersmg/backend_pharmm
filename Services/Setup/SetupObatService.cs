using DapperPostgreSQL;
using Microsoft.AspNetCore.Http;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Models.Setup;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public interface ISetupObatService
    {

        Task<List<phar_setup_obat>> GetAllPharSetupObatByParams(List<ParameterSearchModel> param);
        Task<List<phar_setup_obat>> GetAllPharSetupObat();

        Task<short> AddPharSetupObat(phar_setup_obat_insert data);
        Task<short> UpdatePharSetupObat(phar_setup_obat_update data);
        Task<short> DeletePharSetupObat(int id_item);

        #region Setup Obat Detail

        Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByParams(List<ParameterSearchModel> param);
        Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByIdAndParams(int id_item, List<ParameterSearchModel> param);

        Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetail();
        Task<(bool, long, string)> AddPharSetupObatDetail(phar_setup_obat_detail_insert data);
        //Task<short> AddPharSetupObatDetailMultiple(List<phar_setup_obat_detail_insert> data);
        Task<short> UpdateStatusToDeactivePharSetupObatDetail(phar_setup_obat_detail_update_status data);

        #endregion

    }

    public class SetupObatService : ISetupObatService
    {
        private IHttpContextAccessor _context;
        private readonly SQLConn _db;
        private readonly SetupObatDao _dao;

        public SetupObatService(SQLConn db, SetupObatDao dao,
            IHttpContextAccessor context)
        {
            this._db = db;
            this._dao = dao;
            this._context = context;

        }

        public async Task<List<phar_setup_obat>> GetAllPharSetupObatByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllPharSetupObatByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<phar_setup_obat>> GetAllPharSetupObat()
        {
            try
            {
                return await this._dao.GetAllPharSetupObat();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> AddPharSetupObat(phar_setup_obat_insert data)
        {
            try
            {
                var headerId = await this._dao.AddPharSetupObat(data);

                return headerId;
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> UpdatePharSetupObat(phar_setup_obat_update data)
        {
            try
            {
                return await this._dao.UpdatePharSetupObat(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeletePharSetupObat(int id_item)
        {
            try
            {
                return await this._dao.DeletePharSetupObat(id_item);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #region Setup Obat Detail       

        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllPharSetupObatDetailByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetailByIdAndParams(
            int id_item,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllPharSetupObatDetailByIdAndParams(id_item, param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<phar_setup_obat_detail>> GetAllPharSetupObatDetail()
        {
            try
            {
                return await this._dao.GetAllPharSetupObatDetail();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> AddPharSetupObatDetail(phar_setup_obat_detail_insert data)
        {
            this._db.beginTransaction();
            try
            {
                var headerId = (short)0;
                var cekHargaExistingAktif = await this._dao.GetPharSetupObatDetailAktifByIdItemAndTglBerlakuWithLock((int)data.id_item,
                 data.tgl_berlaku);

                if (cekHargaExistingAktif is not null)
                {
                    ////parameter untuk mengupdate harga obat menjadi non aktif
                    //var ubahHarga = new phar_setup_obat_detail_update_status
                    //{
                    //    id_obat_detail = cekHargaAktif.id_obat_detail,
                    //    tgl_berakhir = data.tgl_berlaku,
                    //    user_edited = data.user_created
                    //};

                    //var stopHargaAktif = await this._dao.UpdateStatusToDeactivePharSetupObatDetail(ubahHarga);

                    //if(stopHargaAktif <= 0)
                    //{
                    //    throw new Exception("Gagal menonaktifkan harga sebelumnya");
                    //}

                    var paramUpdate = new phar_setup_obat_detail_update
                    {
                        id_obat_detail = (long)cekHargaExistingAktif.id_obat_detail,
                        harga_jual_apotek = (decimal)data.harga_jual_apotek,
                        harga_netto_apotek = data.harga_netto_apotek,
                        prosentase_ppn = data.prosentase_ppn,
                        prosentase_profit = data.prosentase_profit,
                        user_edited = data.user_created
                    };

                    var updateDetail = await this._dao.UpdatePharSetupObatDetail(paramUpdate);

                    if (updateDetail <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal merubah data");
                    }
                }
                else
                {

                    headerId = await this._dao.AddPharSetupObatDetail(data);

                    if (headerId <= 0)
                    {
                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal menambahkan harga jual");
                    }
                }

                ////rumus harga jual
                //data.harga_jual_apotek = data.harga_netto_apotek 
                //    + (data.prosentase_profit/100 * data.harga_netto_apotek) 
                //    + (data.prosentase_ppn/100 * (data.harga_netto_apotek + (data.prosentase_profit/100 * data.harga_netto_apotek)));


                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        //public async Task<short> AddPharSetupObatDetailMultiple(List<phar_setup_obat_detail_insert> data)
        //{
        //    this._db.beginTransaction();
        //    try
        //    {
        //        if (data.Count > 0)
        //        {
        //            foreach (var item in data)
        //            {

        //                var cekHargaAktif = await this._dao.GetPharSetupObatDetailAktifByIdItemWithLock((int)item.id_item);

        //                if (cekHargaAktif is not null)
        //                {
        //                    //parameter untuk mengupdate harga obat menjadi non aktif
        //                    var ubahHarga = new phar_setup_obat_detail_update_status
        //                    {
        //                        id_obat_detail = cekHargaAktif.id_obat_detail,
        //                        tgl_berakhir = item.tgl_berlaku,
        //                        user_edited = item.user_created
        //                    };

        //                    var stopHargaAktif = await this._dao.UpdateStatusToDeactivePharSetupObatDetail(ubahHarga);

        //                    if (stopHargaAktif <= 0)
        //                    {
        //                        throw new Exception("Gagal menonaktifkan harga sebelumnya");
        //                    }
        //                }

        //                //rumus harga jual
        //                item.harga_jual_apotek = item.harga_netto_apotek
        //                    + (item.prosentase_profit / 100 * item.harga_netto_apotek)
        //                    + (item.prosentase_ppn / 100 * (item.harga_netto_apotek + (item.prosentase_profit / 100 * item.harga_netto_apotek)));

        //                var headerId = await this._dao.AddPharSetupObatDetail(item);

        //                if (headerId <= 0)
        //                {
        //                    if (cekHargaAktif is not null)
        //                    {
        //                        throw new Exception("Gagal merubah harga sebelumnya");
        //                    }
        //                }

        //            }
        //        }

        //        this._db.commitTrans();
        //        return headerId;
        //    }
        //    catch (Exception)
        //    {
        //        this._db.rollBackTrans();
        //        throw;
        //        //TODO : log error
        //    }
        //}

        public async Task<short> UpdateStatusToDeactivePharSetupObatDetail(phar_setup_obat_detail_update_status data)
        {
            try
            {
                return await this._dao.UpdateStatusToDeactivePharSetupObatDetail(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion
    }
}
