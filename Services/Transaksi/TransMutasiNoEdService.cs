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
    public interface ITransMutasiNoEdService
    {
        #region Header

        Task<List<tr_mutasi_no_ed>> GetMutasiPermintaanByParams(List<ParameterSearchModel> param);
        Task<List<tr_mutasi_no_ed>> GetMutasiPermintaanByIdStockroomPemberiAndParams(
            short _id_stockroom,
            List<ParameterSearchModel> param);

        Task<List<tr_mutasi_no_ed>> GetAllTrMutasiByParams(List<ParameterSearchModel> param);
        Task<tr_mutasi_no_ed> GetTrMutasiById(long mutasi_id);

        Task<(bool, string)> ApproveMutasi(tr_mutasi_no_ed_approve data);
        Task<(bool, string)> AddTrMutasi(tr_mutasi_no_ed_insert data);
        Task<long> AddTrMutasiPermintaan(tr_mutasi_no_ed_insert_permintaan data);
        Task<short> UpdateTrMutasiValidated(tr_mutasi_no_ed_update_to_validated data);
        Task<(bool, string)> UpdateTrMutasiCanceled(tr_mutasi_no_ed_update_to_canceled data);

        #endregion

        #region Detail Item

        Task<List<tr_mutasi_no_ed_detail_item>> GetAllTrMutasiDetailItemByParams(List<ParameterSearchModel> param);
        Task<List<tr_mutasi_no_ed_detail_item>> GetTrMutasiDetailItemByMutasiId(long mutasi_id);
        Task<List<tr_mutasi_no_ed_detail_item_with_hpp>> GetTrMutasiDetailItemWithHppByMutasiId(long mutasi_id);

        #endregion

        #region Detail Upload

        Task<List<tr_mutasi_no_ed_detail_upload>> GetTrMutasiDetailUploadByMutasiId(long mutasi_id);
        Task<long> AddTrMutasiDetailUpload(tr_mutasi_no_ed_detail_upload_insert data);
        Task<short> DeleteTrMutasiDetailUpload(long mutasi_detail_upload_id);

        #endregion
    }

    public class TransMutasiNoEdService : ITransMutasiNoEdService
    {
        private readonly SQLConn _db;
        private readonly TransMutasiNoEdDao _dao;
        private readonly SetupItemDao _itemDao;
        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly KartuStokItemDao _kartuStokDao;

        public TransMutasiNoEdService(SQLConn db, TransMutasiNoEdDao dao,
            MasterCounterDao kodeDao,
            SetupStokItemDao stokItemDao,
            KartuStokItemDao kartuStokDao,
            SetupItemDao itemDao)
        {
            this._db = db;
            this._kodeDao = kodeDao;
            this._itemDao = itemDao;
            this._kartuStokDao = kartuStokDao;
            this._stokItemDao = stokItemDao;
            this._dao = dao;
        }

        #region Header

        public async Task<List<tr_mutasi_no_ed>> GetMutasiPermintaanByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetMutasiPermintaanByParams(param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed>> GetMutasiPermintaanByIdStockroomPemberiAndParams(
            short _id_stockroom,
            List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetMutasiPermintaanByIdStockroomPemberiAndParams(_id_stockroom, param);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed>> GetAllTrMutasiByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrMutasiByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<tr_mutasi_no_ed> GetTrMutasiById(long mutasi_id)
        {
            try
            {
                return await this._dao.GetTrMutasiById(mutasi_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, string)> AddTrMutasi(tr_mutasi_no_ed_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeMutasi,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                long mutasiId = 0;
                long mutasiDetailId = 0;

                //generate nomor mutasi
                var noMutasi = this._kodeDao.GenerateKode(dataCounter).Result;

                if (string.IsNullOrEmpty(noMutasi))
                {

                    this._db.rollBackTrans();
                    return (false, "Gagal mendapatkan nomor mutasi");

                }
                else
                {
                    data.nomor_mutasi = noMutasi;
                }

                mutasiId = await this._dao.AddTrMutasi(data);

                if (mutasiId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.mutasi_id = mutasiId;
                            mutasiDetailId = await this._dao.AddTrMutasiDetailItem(detail);

                            if (mutasiDetailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, "Gagal input mutasi detail dengan no urut " + detail.no_urut.ToString());

                            }
                        }


                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, "Gagal update nomor mutasi");

                    }

                }
                else
                {

                    this._db.rollBackTrans();
                    return (false, "Gagal menyimpan permintaan mutasi");
                }

                this._db.commitTrans();
                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }


        public async Task<(bool, string)> ApproveMutasi(tr_mutasi_no_ed_approve data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeMutasi,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                long mutasiId = 0;
                long mutasiDetailId = 0;

                short insertPenambahanKartuStok = 0;
                short insertPenguranganKartuStok = 0;

                short updateStokPenambahanHeader = 0;
                short updateStokPenguranganHeader = 0;


                //generate nomor mutasi
                var noMutasi = this._kodeDao.GenerateKode(dataCounter).Result;

                if (string.IsNullOrEmpty(noMutasi))
                {

                    this._db.rollBackTrans();
                    return (false, "Gagal mendapatkan nomor mutasi");

                }
                else
                {
                    data.nomor_mutasi = noMutasi;
                }


                //get data permintaan mutasi
                var dataMutasiPermintaan = await this._dao.GetTrMutasiByIdWithLock(data.mutasi_id);

                if (dataMutasiPermintaan.user_validated is not null)
                {

                    this._db.rollBackTrans();
                    return (false, "Mutasi ini sudah disetujui");
                }
                else if (dataMutasiPermintaan.user_canceled is not null)
                {
                    this._db.rollBackTrans();
                    return (false, "Mutasi ini sudah dibatalkan");

                }

                if (data.jumlah_item > dataMutasiPermintaan.jumlah_item)
                {

                    this._db.rollBackTrans();
                    return (false, "Jumlah item persetujuan tidak boleh lebih dari jumlah item permintaan ");
                }

                mutasiId = await this._dao.ApproveMutasi(data);

                if (mutasiId > 0)
                {

                    if (data.details.Count > 0)
                    {

                        var rekapForHeader = data.details.GroupBy(q => q.id_item).Select(res =>
                                    new
                                    {
                                        mutasi_id = res.FirstOrDefault().mutasi_id,
                                        id_item = res.FirstOrDefault().id_item,
                                        id_stockroom_penerima = dataMutasiPermintaan.id_stockroom_penerima,
                                        id_stockroom_pemberi = dataMutasiPermintaan.id_stockroom_pemberi,
                                        qty_on_hand = res.Sum(s => s.qty_mutasi),
                                        nominal = res.Sum(n => n.nominal_mutasi),
                                        details = data.details.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
                                    }).ToList();


                        if (rekapForHeader.Count > 0)
                        {
                            foreach (var item in rekapForHeader)
                            {

                                //get stok akhir kartu stok
                                var cekStokAkhirPemberi = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                    item.id_stockroom_pemberi,
                                    item.id_item
                                    );


                                //get stok akhir kartu stok
                                var cekStokAkhirPenerima = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                    item.id_stockroom_penerima,
                                    item.id_item
                                    );

                                //get data barang dan gudang
                                var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomFromMasterWithLock(item.id_item, dataMutasiPermintaan.id_stockroom_pemberi);

                                #region Insert Kartu Stok Header

                                //parameter untuk menambah kartu stok
                                var kartuStokPlusParam = new mm_kartu_stok_item_insert_penambahan_stok
                                {
                                    id_detail_transaksi = null,
                                    id_header_transaksi = data.mutasi_id,
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom_penerima, //gudang farmasi
                                    keterangan = "MASUK MUTASI BARANG TANPA ED",
                                    stok_awal = cekStokAkhirPenerima?.stok_akhir,
                                    stok_masuk = item.qty_on_hand,
                                    nominal_awal = cekStokAkhirPenerima?.nominal_akhir,
                                    nominal_masuk = item.nominal,
                                    nomor_ref_transaksi = noMutasi,
                                    user_inputed = data.user_validated
                                };

                                //parameter untuk mengurangi kartu stok
                                var kartuStokMinusParam = new mm_kartu_stok_item_insert_pengurangan_stok
                                {
                                    id_detail_transaksi = null,
                                    id_header_transaksi = data.mutasi_id,
                                    id_item = item.id_item,
                                    id_stockroom = item.id_stockroom_pemberi, //gudang farmasi
                                    keterangan = "KELUAR MUTASI BARANG TANPA ED",
                                    stok_awal = cekStokAkhirPemberi?.stok_akhir,
                                    stok_keluar = item.qty_on_hand,
                                    nominal_awal = cekStokAkhirPemberi?.nominal_akhir,
                                    nominal_keluar = item.nominal,
                                    nomor_ref_transaksi = noMutasi,
                                    user_inputed = data.user_validated
                                };

                                #region Header

                                //mengurangi kartu stok
                                insertPenguranganKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokMinusParam);

                                if (insertPenguranganKartuStok <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, "Gagal mengurangi kartu stok header");
                                }
                                else
                                {

                                    //menambah kartu stok
                                    insertPenambahanKartuStok = await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokPlusParam);

                                    if (insertPenambahanKartuStok <= 0)
                                    {
                                        this._db.rollBackTrans();
                                        return (false, "Gagal menambah kartu stok header");
                                    }
                                    else
                                    {

                                        //get stok header
                                        var cekStokItemPemberiHeader = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                            item.id_item,
                                            dataMutasiPermintaan.id_stockroom_pemberi
                                            );


                                        if (cekStokItemPemberiHeader is null)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, "Tidak ada stok item di gudang tujuan permintaan");
                                        }
                                        else
                                        {
                                            if ((cekStokItemPemberiHeader.qty_on_hand - item.qty_on_hand) < 0)
                                            {

                                                this._db.rollBackTrans();
                                                return (false, $"Sisa stok untuk item {dataBarangAndGudang?.nama_item} tidak mencukupi ");
                                            }
                                        }

                                        #region Update Pengurangan Stok Item Header

                                        //parameter untuk update pengurangan stok header
                                        var stokItemPenguranganParam = new mm_setup_stok_item_update_stok
                                        {
                                            id_item = item.id_item,
                                            id_stockroom = dataMutasiPermintaan.id_stockroom_pemberi,
                                            qty_on_hand = item.qty_on_hand
                                        };

                                        updateStokPenguranganHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemPenguranganParam);

                                        if (updateStokPenguranganHeader <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, "Gagal mengurangi stok header");
                                        }
                                        else
                                        {

                                            //get stok header
                                            var cekStokItemPenerimaHeader = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                                item.id_item,
                                                dataMutasiPermintaan.id_stockroom_penerima
                                                );


                                            if (cekStokItemPenerimaHeader is null)
                                            {

                                                #region Insert Stok Item Header

                                                //parameter untuk insert stok header
                                                var stokItemParam = new mm_setup_stok_item
                                                {
                                                    id_item = item.id_item,
                                                    id_stockroom = dataMutasiPermintaan.id_stockroom_penerima,
                                                    qty_on_hand = item.qty_on_hand,
                                                    qty_stok_kritis = 0
                                                };

                                                var insertStokHeader = await this._stokItemDao.AddMmSetupStokItem(stokItemParam);

                                                if (insertStokHeader <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, "Gagal menambah stok header");

                                                }

                                                #endregion
                                            }
                                            else
                                            {

                                                #region Update Penambahan Stok Item Header

                                                //parameter untuk update penambahan stok header
                                                var stokItemPenambahanParam = new mm_setup_stok_item_update_stok
                                                {
                                                    id_item = item.id_item,
                                                    id_stockroom = dataMutasiPermintaan.id_stockroom_penerima,
                                                    qty_on_hand = item.qty_on_hand
                                                };

                                                updateStokPenambahanHeader = await this._stokItemDao.UpdatePenambahanStok(stokItemPenambahanParam);

                                                if (updateStokPenambahanHeader <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, "Gagal menambah stok header");

                                                }

                                                #endregion
                                            }
                                        }

                                        #endregion

                                    }

                                }

                                #endregion


                                #region Detail

                                if (item.details.Count > 0)
                                {
                                    //looping mtuasi detail
                                    foreach (var detail in item.details)
                                    {
                                        var dataPermintaanDetail = await this._dao.GetTrMutasiPermintaanDetailItemByIdWithLock(detail.mutasi_detail_item_id);

                                        if (dataPermintaanDetail is not null)
                                        {
                                            if (detail.qty_mutasi > dataPermintaanDetail.qty_permintaan)
                                            {

                                                this._db.rollBackTrans();
                                                return (false, "Jumlah qty yang disetujui tidak boleh lebih dari jumlah qty yang diminta");
                                            }
                                        }
                                        else
                                        {

                                            this._db.rollBackTrans();
                                            return (false, $"Data detail permintaan dengan nomor urut {detail.no_urut} tidak ditemukan ");
                                        }

                                        detail.mutasi_id = mutasiId;


                                        #region Approve Mutasi Detail

                                        mutasiDetailId = await this._dao.ApproveMutasiDetailItem(detail);

                                        if (mutasiDetailId <= 0)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, "Gagal approve mutasi detail dengan no urut " + detail.no_urut.ToString());

                                        }

                                        #endregion

                                    }
                                }
                                else
                                {

                                    this._db.rollBackTrans();
                                    return (false, $"Detail mutasi tidak boleh kosong ");
                                }

                                #endregion

                                #endregion

                            }
                        }

                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, "Gagal update nomor mutasi");

                    }

                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Gagal menyetujui permintaan mutasi");
                }

                this._db.commitTrans();
                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }


        public async Task<long> AddTrMutasiPermintaan(tr_mutasi_no_ed_insert_permintaan data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeMutasiPermintaan,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor mutasi permintaan
                var noMutasiPermintaan = this._kodeDao.GenerateKode(dataCounter).Result;

                long mutasiId = 0;
                long mutasiDetailId = 0;

                if (string.IsNullOrEmpty(noMutasiPermintaan))
                {
                    throw new Exception("Gagal mendapatkan nomor mutasi permintaan");
                }
                else
                {
                    data.nomor_permintaan_mutasi = noMutasiPermintaan;
                }



                if (data.details.Count > 0)
                {

                    foreach (var item in data.details)
                    {

                        //get data barang by id
                        //var barang = await this._itemDao.GetMmSetupItemById((int)item.id_item);
                        //item.qty_permintaan = item.isi_permintaan * item.qty_satuan_besar_permintaan;

                    }
                }

                mutasiId = await this._dao.AddTrMutasiPermintaan(data);

                if (mutasiId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.mutasi_id = mutasiId;
                            mutasiDetailId = await this._dao.AddTrMutasiDetailItemPermintaan(detail);

                            if (mutasiDetailId <= 0)
                            {
                                throw new Exception("Gagal input mutasi detail berdasarkan permintaan dengan no urut " + detail.no_urut.ToString());
                            }
                            else
                            {
                                //if (detail.detailBatchs is not null)
                                //{
                                //    if (detail.detailBatchs.Count > 0)
                                //    {
                                //        foreach (var batch in detail.detailBatchs)
                                //        {
                                //            batch.mutasi_detail_item_id = mutasiDetailId;
                                //            batch.mutasi_id = mutasiId;
                                //            mutasiDetailBatchId = await this._dao.AddTrMutasiDetailItemBatch(batch);

                                //            if (mutasiDetailBatchId <= 0)
                                //            {
                                //                throw new Exception("Gagal input mutasi detail batch dengan batch number " + batch.batch_number.ToString());
                                //            }
                                //        }
                                //    }
                                //}
                            }
                        }


                    }


                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        throw new Exception("Gagal update nomor mutasi permintaan");
                    }
                }

                this._db.commitTrans();
                return mutasiId;
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }


        public async Task<short> UpdateTrMutasiValidated(tr_mutasi_no_ed_update_to_validated data)
        {
            try
            {
                return await this._dao.UpdateTrMutasiValidated(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, string)> UpdateTrMutasiCanceled(tr_mutasi_no_ed_update_to_canceled data)
        {
            try
            {
                var canceled = await this._dao.UpdateTrMutasiCanceled(data);

                switch (canceled)
                {
                    case 0: return (false, "Gagal membatalkan mutasi");
                    case -1: return (false, "Transaksi ini sudah disetujui");
                    case -2: return (false, "Transaksi ini sudah dibatalkan");
                    default: return (true, "SUCCESS");
                }

            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        #endregion

        #region Detail Item

        public async Task<List<tr_mutasi_no_ed_detail_item>> GetAllTrMutasiDetailItemByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrMutasiDetailItemByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item>> GetTrMutasiDetailItemByMutasiId(long mutasi_id)
        {
            try
            {
                return await this._dao.GetTrMutasiDetailItemByMutasiId(mutasi_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item_with_hpp>> GetTrMutasiDetailItemWithHppByMutasiId(long mutasi_id)
        {
            try
            {
                return await this._dao.GetTrMutasiDetailItemWithHppByMutasiId(mutasi_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail Upload

        public async Task<List<tr_mutasi_no_ed_detail_upload>> GetTrMutasiDetailUploadByMutasiId(long mutasi_id)
        {
            try
            {
                return await this._dao.GetTrMutasiDetailUploadByMutasiId(mutasi_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrMutasiDetailUpload(tr_mutasi_no_ed_detail_upload_insert data)
        {
            try
            {
                return await this._dao.AddTrMutasiDetailUpload(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteTrMutasiDetailUpload(long mutasi_detail_upload_id)
        {
            try
            {
                short deleteBerkas = (short)0;

                var dataBerkas = await this._dao.GetTrMutasiDetailUploadById(mutasi_detail_upload_id);

                if (dataBerkas is not null)
                {
                    deleteBerkas = await this._dao.DeleteTrMutasiDetailUpload(mutasi_detail_upload_id);

                    if (deleteBerkas > 0)
                    {
                        //hapus gambar dari bucket minio
                        await UploadHelper.RemoveObjectFromBucketByPath(dataBerkas.path_dokumen);
                    }
                }

                return deleteBerkas;
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
