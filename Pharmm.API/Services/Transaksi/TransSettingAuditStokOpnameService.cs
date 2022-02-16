using DapperPostgreSQL;
using Pharmm.API.Dao.Setup;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Transaksi
{
    public interface ITransSettingAuditStokOpnameService
    {
        #region Header

        Task<tr_setting_audit_stok_opname_header_recursive> GetTrSettingAuditStokOpnameHeaderByIdRecursive(long setting_stok_opname_id);
        Task<tr_setting_audit_stok_opname_header> GetTrSettingAuditStokOpnameHeaderById(long setting_stok_opname_id);
        Task<List<tr_setting_audit_stok_opname_header_recursive>> GetAllTrSettingAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param);

        Task<(bool, long, string)> AddTrSettingAuditStokOpnameItem(tr_setting_audit_stok_opname_header_item_insert data);
        Task<(bool, long, string)> AddTrSettingAuditStokOpnameGrup(tr_setting_audit_stok_opname_header_grup_insert data);
        Task<(bool, long, string)> AddTrSettingAuditStokOpnameRakStorage(tr_setting_audit_stok_opname_header_rak_storage_insert data);
        Task<(bool, long, string)> AddTrSettingAuditStokOpnameSemuaItem(tr_setting_audit_stok_opname_header_semua_item_insert data);

        #endregion

        #region Detail

        Task<List<tr_setting_audit_stok_opname_detail_stockroom>> GetAllTrSettingAuditStokOpnameDetailStockroomBySettingStokOpnameId(
            long _setting_stok_opname_id
            );

        #endregion

    }

    public class TransSettingAuditStokOpnameService : ITransSettingAuditStokOpnameService
    {
        private readonly SQLConn _db;
        private readonly TransSettingAuditStokOpnameDao _dao;
        private readonly MasterCounterDao _kodeDao;

        public TransSettingAuditStokOpnameService(
            MasterCounterDao kodeDao,
            SQLConn db, TransSettingAuditStokOpnameDao dao)
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
        }

        #region Header

        public async Task<tr_setting_audit_stok_opname_header_recursive> GetTrSettingAuditStokOpnameHeaderByIdRecursive(long setting_stok_opname_id)
        {
            try
            {
                return await this._dao.GetTrSettingAuditStokOpnameHeaderByIdRecursive(setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<tr_setting_audit_stok_opname_header> GetTrSettingAuditStokOpnameHeaderById(long setting_stok_opname_id)
        {
            try
            {
                return await this._dao.GetTrSettingAuditStokOpnameHeaderById(setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_setting_audit_stok_opname_header_recursive>> GetAllTrSettingAuditStokOpnameHeaderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrSettingAuditStokOpnameHeaderByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, long, string)> AddTrSettingAuditStokOpnameGrup(tr_setting_audit_stok_opname_header_grup_insert data)
        {

            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixSettingStokOpname,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor setting 
                var noSetting = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noSetting))
                {
                    data.no_setting_stok_opname = noSetting;
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor setting stok opname");
                }

                var headerId = await this._dao.AddTrSettingAuditStokOpnameHeaderGrup(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailGrup(detail);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail grup");
                            }
                        }
                    }

                    if (data.detailGudangs.Count > 0)
                    {
                        foreach (var gudang in data.detailGudangs)
                        {
                            gudang.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailStockroom(gudang);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail gudang");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor setting stok opname");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal input setting stok opname grup");
                }


                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> AddTrSettingAuditStokOpnameItem(tr_setting_audit_stok_opname_header_item_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixSettingStokOpname,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor setting 
                var noSetting = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noSetting))
                {
                    data.no_setting_stok_opname = noSetting;
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor setting stok opname");
                }

                var headerId = await this._dao.AddTrSettingAuditStokOpnameHeaderItem(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailItem(detail);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail barang");
                            }
                        }
                    }


                    if (data.detailGudangs.Count > 0)
                    {
                        foreach (var gudang in data.detailGudangs)
                        {
                            gudang.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailStockroom(gudang);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail gudang");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor setting stok opname");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal input setting stok opname barang");
                }


                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> AddTrSettingAuditStokOpnameRakStorage(tr_setting_audit_stok_opname_header_rak_storage_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixSettingStokOpname,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor setting 
                var noSetting = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noSetting))
                {
                    data.no_setting_stok_opname = noSetting;
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor setting stok opname");
                }

                var headerId = await this._dao.AddTrSettingAuditStokOpnameHeaderRakStorage(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailRakStorage(detail);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail rak storage");
                            }
                        }
                    }


                    if (data.detailGudangs.Count > 0)
                    {
                        foreach (var gudang in data.detailGudangs)
                        {
                            gudang.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailStockroom(gudang);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail gudang");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor kontrak");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal input setting stok opname rak storage");
                }


                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        public async Task<(bool, long, string)> AddTrSettingAuditStokOpnameSemuaItem(tr_setting_audit_stok_opname_header_semua_item_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixSettingStokOpname,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor setting 
                var noSetting = this._kodeDao.GenerateKode(dataCounter).Result;

                if (!string.IsNullOrEmpty(noSetting))
                {
                    data.no_setting_stok_opname = noSetting;
                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal generate nomor setting stok opname");
                }

                var headerId = await this._dao.AddTrSettingAuditStokOpnameHeaderSemuaItem(data);

                if (headerId > 0)
                {
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.setting_stok_opname_id = headerId;

                            var detailId = await this._dao.AddTrSettingAuditStokOpnameDetailStockroom(detail);

                            if (detailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, 0, $"Gagal input detail gudang");
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, 0, $"Gagal update nomor kontrak");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, 0, $"Gagal input setting stok opname gudang");
                }


                this._db.commitTrans();
                return (true, headerId, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        #endregion

        #region Detail

        public async Task<List<tr_setting_audit_stok_opname_detail_stockroom>> GetAllTrSettingAuditStokOpnameDetailStockroomBySettingStokOpnameId(
            long _setting_stok_opname_id
            )
        {
            try
            {
                return await this._dao.GetAllTrSettingAuditStokOpnameDetailStockroomBySettingStokOpnameId(_setting_stok_opname_id);
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion

    }
}
