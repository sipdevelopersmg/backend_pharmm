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
    public interface ITransPenerimaanService
    {
        #region header

        Task<List<tr_penerimaan>> GetAllTrPenerimaanByParams(List<ParameterSearchModel> param);
        Task<List<tr_penerimaan>> GetAllTrPenerimaan();
        Task<tr_penerimaan> GetTrPenerimaanById(long penerimaan_id);

        Task<(bool, string)> AddTrPenerimaan(tr_penerimaan_insert data);

        Task<(bool, string)> UpdateToValidated(tr_penerimaan_update_status_to_validated data);
        Task<short> UpdateToCanceled(tr_penerimaan_update_status_to_canceled data);

        #endregion

        #region detail item

        Task<List<tr_penerimaan_detail_item>> GetTrPenerimaanDetailItemByPenerimaanId(long penerimaan_id);
        Task<List<tr_penerimaan_detail_item>> GetAllTrPenerimaanDetailItem();

        #endregion

        #region detail upload

        Task<List<tr_penerimaan_detail_upload>> GetTrPenerimaanDetailUploadByPenerimaanId(long penerimaan_id);
        Task<List<tr_penerimaan_detail_upload>> GetAllTrPenerimaanDetailUpload();
        Task<long> AddTrPenerimaanDetailUpload(tr_penerimaan_detail_upload_insert data);
        Task<short> DeleteTrPenerimaanDetailUpload(long penerimaan_detail_upload_id);

        #endregion
    }

    public class TransPenerimaanService : ITransPenerimaanService
    {
        private readonly SQLConn _db;
        private readonly TransPenerimaanDao _dao;
        private readonly TransPemesananDao _pemesananDao;
        private readonly TransKontrakSpjbDao _kontrakDao;
        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly SetupItemDao _itemDao;
        private readonly KartuStokItemDao _kartuStokDao;
        private readonly TransHutangSupplierDao _hutangDao;

        public TransPenerimaanService(
            SQLConn db,
            TransPenerimaanDao dao,
            TransPemesananDao pemesananDao,
            MasterCounterDao kodeDao,
            SetupStokItemDao stokItemDao,
            SetupItemDao itemDao,
            KartuStokItemDao kartuStokDao,
            TransKontrakSpjbDao kontrakDao,
            TransHutangSupplierDao hutangDao
            )
        {
            this._db = db;
            this._dao = dao;
            this._kodeDao = kodeDao;
            this._pemesananDao = pemesananDao;
            this._stokItemDao = stokItemDao;
            this._itemDao = itemDao;
            this._kontrakDao = kontrakDao;
            this._kartuStokDao = kartuStokDao;
            this._hutangDao = hutangDao;
        }

        #region header

        public async Task<List<tr_penerimaan>> GetAllTrPenerimaanByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrPenerimaanByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_penerimaan>> GetAllTrPenerimaan()
        {
            try
            {
                return await this._dao.GetAllTrPenerimaan();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<tr_penerimaan> GetTrPenerimaanById(long penerimaan_id)
        {
            try
            {
                return await this._dao.GetTrPenerimaanById(penerimaan_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        #region FUNCTION VALIDASI

        //penjagaan qty pesanan 
        public async Task<(bool, string)> ValidateQtyTerimaPemesanan(long pemesanan_detail_id, decimal qty_terima)
        {
            try
            {
                if (pemesanan_detail_id != 0)
                {
                    //sisa qty terima
                    var cekSisaQtyPesan = await this._pemesananDao.GetSisaQtyTerima(pemesanan_detail_id);

                    if (qty_terima > cekSisaQtyPesan.sisa_qty_terima)
                    {
                        this._db.rollBackTrans();
                        return (false, $"Qty terima tidak boleh lebih besar dari sisa qty terima (sisa qty terima untuk {cekSisaQtyPesan.nama_item} adalah {cekSisaQtyPesan.sisa_qty_terima.ToString()})");

                        throw new Exception($"Qty terima tidak boleh lebih besar dari sisa qty terima (sisa qty terima untuk {cekSisaQtyPesan.nama_item} adalah {cekSisaQtyPesan.sisa_qty_terima.ToString()})");
                    }
                }

                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                throw;
            }
        }

        //penjagaan qty pesanan dan qty terima di tabel kontrak
        public async Task<(bool, string)> ValidateQtyTerimaDiKontrak(long kontrak_detail_item_id, decimal qty_terima)
        {
            try
            {
                if (kontrak_detail_item_id != 0)
                {
                    //sisa qty terima
                    var cekSisaQty = await this._kontrakDao.GetSisaQtyPesanDanTerima(kontrak_detail_item_id);

                    if (qty_terima > cekSisaQty.sisa_qty_terima)
                    {
                        this._db.rollBackTrans();
                        return (false, $"Qty terima tidak boleh lebih besar dari sisa qty terima (sisa qty terima untuk {cekSisaQty.nama_item} adalah {cekSisaQty.sisa_qty_terima.ToString()} )");

                        throw new Exception($"Qty terima tidak boleh lebih besar dari sisa qty terima (sisa qty terima untuk {cekSisaQty.nama_item} adalah {cekSisaQty.sisa_qty_terima.ToString()} )");
                    }

                }

                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

        public async Task<(bool, string)> AddTrPenerimaan(tr_penerimaan_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePenerimaan,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor penerimaan
                var noPenerimaan = this._kodeDao.GenerateKode(dataCounter).Result;

                long penerimaanId = 0;
                long penerimaanDetailId = 0;

                if (string.IsNullOrEmpty(noPenerimaan))
                {
                    this._db.rollBackTrans();
                    return (false, "Gagal mendapatkan nomor penerimaan");

                    throw new Exception("Gagal mendapatkan nomor penerimaan");
                }
                else
                {
                    data.nomor_penerimaan = noPenerimaan;
                }

                if (data.details.Count > 0)
                {
                    data.sub_total_1 = 0;
                    data.sub_total_2 = 0;
                    data.total_transaksi = 0;
                    data.jumlah_item = 0;

                    foreach (var item in data.details)
                    {

                        item.qty_terima = item.isi * item.qty_satuan_besar;
                        item.sub_total = item.qty_terima * item.harga_satuan;

                        data.sub_total_1 += item.sub_total;
                        data.sub_total_2 += item.sub_total;
                        data.jumlah_item += item.qty_satuan_besar;
                        data.total_transaksi += item.sub_total;
                    }
                }

                penerimaanId = await this._dao.AddTrPenerimaan(data);

                if (penerimaanId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            if (detail.pemesanan_detail_id is not null)
                            {
                                (bool resultValPesan, string messageValPesan) = await ValidateQtyTerimaPemesanan(
                                    (long)detail.pemesanan_detail_id,
                                    (decimal)detail.qty_terima);

                                if (resultValPesan == false)
                                {
                                    return (resultValPesan, messageValPesan);
                                }

                                var pemesananDataDetail = await this._pemesananDao.GetTrPemesananDetailByIdLock((long)detail.pemesanan_detail_id);


                                if (pemesananDataDetail.kontrak_id is not null && pemesananDataDetail.kontrak_detail_item_id is not null)
                                {
                                    (bool resultValKontrak, string messageValKontrak) = await ValidateQtyTerimaDiKontrak(
                                            (long)pemesananDataDetail.kontrak_detail_item_id,
                                            (decimal)detail.qty_terima);


                                    if (resultValKontrak == false)
                                    {
                                        return (resultValKontrak, messageValKontrak);
                                    }

                                }
                            }

                            detail.penerimaan_id = penerimaanId;
                            penerimaanDetailId = await this._dao.AddTrPenerimaanDetailItem(detail);

                            if (penerimaanDetailId <= 0)
                            {
                                this._db.rollBackTrans();
                                return (false, "Gagal input penerimaan detail dengan no urut " + detail.no_urut.ToString());

                                throw new Exception("Gagal input penerimaan detail dengan no urut " + detail.no_urut.ToString());
                            }
                        }

                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        this._db.rollBackTrans();
                        return (false, "Gagal update nomor penerimaan");

                        throw new Exception("Gagal update nomor penerimaan");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Gagal menyimpan penerimaan");
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

        public async Task<(bool, string)> UpdateToValidated(tr_penerimaan_update_status_to_validated data)
        {
            this._db.beginTransaction();
            try
            {

                var insertKartuStok = 0;
                var validate = (short)0;
                short id_stockroom = 0;


                //get penerimaan data
                var penerimaanData = await this._dao.GetTrPenerimaanByIdWithLock(data.penerimaan_id);

                if (penerimaanData is not null)
                {
                    #region Cek Status Transaksi

                    if (penerimaanData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah divalidasi");
                    }
                    else if (penerimaanData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah dibatalkan");
                    }

                    #endregion

                    //validasi penerimaan
                    validate = await this._dao.UpdateToValidated(data);

                    if (validate > 0)
                    {
                        id_stockroom = penerimaanData.id_stockroom;

                        //get penerimaan data detail
                        var penerimaanDataDetail = await this._dao.GetTrPenerimaanDetailItemByPenerimaanIdWithLock(data.penerimaan_id);

                        if (penerimaanDataDetail.Count > 0)
                        {
                            //jika dari gudang farmasi
                            if (id_stockroom > 0)
                            {
                                //rekap penerimaan untuk di summary ke header
                                var rekapForHeader = penerimaanDataDetail.GroupBy(q => q.id_item).Select(res =>
                                new
                                {
                                    penerimaan_id = res.FirstOrDefault().penerimaan_id,
                                    id_item = res.FirstOrDefault().id_item,
                                    id_stockroom = id_stockroom,
                                    qty_on_hand = res.Sum(s => s.qty_terima),
                                    nominal = res.Sum(n => n.sub_total),
                                    details = penerimaanDataDetail.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
                                }).ToList();

                                if (rekapForHeader.Count > 0)
                                {
                                    foreach (var item in rekapForHeader)
                                    {

                                        //get stok akhir kartu stok
                                        var cekStokAkhir = await this._kartuStokDao.GetStokAkhirByIdStockroomAndIdItemWithLock(
                                            item.id_stockroom,
                                            item.id_item
                                            );

                                        #region Insert Kartu Stok Header

                                        //parameter untuk menambah kartu stok
                                        var kartuStokParam = new mm_kartu_stok_item_insert_penambahan_stok
                                        {
                                            id_detail_transaksi = null,
                                            id_header_transaksi = data.penerimaan_id,
                                            id_item = item.id_item,
                                            id_stockroom = item.id_stockroom, //gudang farmasi
                                            keterangan = "TERIMA PEMBELIAN BARANG",
                                            stok_awal = cekStokAkhir?.stok_akhir,
                                            stok_masuk = item.qty_on_hand,
                                            nominal_masuk = item.nominal,
                                            nominal_awal = cekStokAkhir?.nominal_akhir,
                                            nomor_ref_transaksi = penerimaanData.nomor_penerimaan,
                                            user_inputed = data.user_validated
                                        };

                                        insertKartuStok = await this._kartuStokDao.AddPenambahanMmKartuStokItem(kartuStokParam);

                                        if (insertKartuStok <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, "Gagal menambah kartu stok header");

                                            throw new Exception("Gagal menambah kartu stok header");
                                        }
                                        else
                                        {
                                            if (item.details.Count > 0)
                                            {
                                                foreach (var detailTerima in item.details)
                                                {

                                                    //get stok akhir kartu stok detail
                                                    var cekStokAkhirDetail = await this._kartuStokDao.GetStokAkhirDetailBatchWithLock(
                                                        detailTerima.batch_number,
                                                        detailTerima.expired_date,
                                                        id_stockroom,
                                                        detailTerima.id_item
                                                        );

                                                    #region Input Kartu Stok Detail Batch

                                                    //jika input kartu stok
                                                    if (insertKartuStok > 0)
                                                    {
                                                        //parameter untuk insert kartu stok detail
                                                        var kartuStokDetailParam = new mm_kartu_stok_item_detail_batch_insert_penambahan_stok
                                                        {
                                                            batch_number = detailTerima.batch_number,
                                                            expired_date = detailTerima.expired_date,
                                                            id_detail_transaksi = detailTerima.penerimaan_detail_item_id,
                                                            id_header_transaksi = detailTerima.penerimaan_id,
                                                            id_kartu_stok_item = insertKartuStok,
                                                            nominal_awal = cekStokAkhirDetail?.nominal_akhir,
                                                            stok_awal = cekStokAkhirDetail?.stok_akhir,
                                                            nominal_masuk = detailTerima.sub_total,
                                                            stok_masuk = detailTerima.qty_terima
                                                        };

                                                        //untuk insert kartu stok detail batch
                                                        var insertKartuStokDetail = await this._kartuStokDao.AddPenambahanMmKartuStokItemDetailBatch(kartuStokDetailParam);

                                                        if (insertKartuStokDetail <= 0)
                                                        {
                                                            this._db.rollBackTrans();
                                                            return (false, "Gagal menambah ke kartu stok detail batch");

                                                            throw new Exception("Gagal menambah ke kartu stok detail batch");
                                                        }
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }

                                        #endregion


                                        #region setup stok item

                                        //get stok header
                                        var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                            item.id_item,
                                            id_stockroom
                                            );

                                        if (cekStokItem is null)
                                        {
                                            #region Insert Stok Item Header

                                            //parameter untuk insert stok header
                                            var stokItemParam = new mm_setup_stok_item
                                            {
                                                id_item = item.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = item.qty_on_hand,
                                                qty_stok_kritis = 0
                                            };

                                            var insertStokHeader = await this._stokItemDao.AddMmSetupStokItem(stokItemParam);

                                            if (insertStokHeader <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, "Gagal menambah stok header");

                                                throw new Exception("Gagal menambah stok header");
                                            }

                                            #endregion
                                        }
                                        else
                                        {

                                            #region Update Stok Item Header

                                            //parameter untuk update stok header
                                            var stokItemParam = new mm_setup_stok_item_update_stok
                                            {
                                                id_item = item.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = item.qty_on_hand
                                            };

                                            var updateStokHeader = await this._stokItemDao.UpdatePenambahanStok(stokItemParam);

                                            if (updateStokHeader <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, "Gagal menambah stok header");

                                                throw new Exception("Gagal menambah stok header");
                                            }

                                            #endregion
                                        }


                                        foreach (var detail in item.details)
                                        {

                                            //get stok detail
                                            var cekStokItemDetail = await this._stokItemDao.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBatchNumberWithLock(
                                                detail.id_item,
                                                id_stockroom,
                                                detail.batch_number
                                                );

                                            if (cekStokItemDetail is null)
                                            {
                                                #region Insert Stok Detail Batch

                                                //parameter untuk insert penambahan stok detail
                                                var insertStokItemDetailParam = new mm_setup_stok_item_detail_batch_insert
                                                {
                                                    id_item = detail.id_item,
                                                    id_stockroom = id_stockroom,
                                                    qty_on_hand = detail.qty_terima,
                                                    batch_number = detail.batch_number,
                                                    expired_date = detail.expired_date,
                                                    barcode_batch_number = string.Concat(detail.barcode, detail.batch_number),
                                                    harga_satuan_netto = detail.harga_satuan_netto
                                                };

                                                var insertStokDetail = await this._stokItemDao.AddMmSetupStokItemDetailBatch(insertStokItemDetailParam);

                                                if (insertStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, "Gagal insert stok detail batch");
                                                }

                                                #endregion
                                            }
                                            else
                                            {
                                                #region Update Stok Detail

                                                //parameter untuk update stok detail
                                                var stokItemDetailParam = new mm_setup_stok_item_detail_update_penambahan_stok_with_harga
                                                {
                                                    id_item = detail.id_item,
                                                    id_stockroom = id_stockroom,
                                                    qty_on_hand = detail.qty_terima,
                                                    batch_number = detail.batch_number,
                                                    expired_date = detail.expired_date,
                                                    barcode_batch_number = string.Concat(detail.barcode, detail.batch_number),
                                                    harga_satuan_netto = detail.harga_satuan_netto
                                                };

                                                var updateStokDetail = await this._stokItemDao.UpdatePenambahanStokWithHargaDetailBatch(stokItemDetailParam);

                                                if (updateStokDetail <= 0)
                                                {
                                                    this._db.rollBackTrans();
                                                    return (false, "Gagal menambah stok detail");
                                                }

                                                #endregion
                                            }

                                            #region Update Harga Perolehan

                                            //update harga beli terakhir
                                            var updateHargaPerolehan = await this._itemDao.UpdateHargaPerolehan(
                                                new mm_setup_item_update_harga_perolehan
                                                {
                                                    harga_beli_netto = detail.harga_satuan_netto,
                                                    id_item = detail.id_item,
                                                    qty_terima = detail.qty_terima
                                                });

                                            if (updateHargaPerolehan <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, "Gagal merubah harga perolehan");
                                            }

                                            #endregion

                                            //update qty terima untuk pemesanan dan kontrak
                                            if (detail.pemesanan_id is not null && detail.pemesanan_id is not null)
                                            {

                                                //data pemesanan detail
                                                var pemesananData = await this._pemesananDao.GetTrPemesananDetailByIdLock((long)detail.pemesanan_detail_id);

                                                if (pemesananData is not null)
                                                {

                                                    #region Update Qty Penerimaan di Pemesanan 

                                                    (bool resultValTerima, string messageValTerima) = await ValidateQtyTerimaPemesanan(
                                                        (long)detail.pemesanan_detail_id,
                                                        (decimal)detail.qty_terima);

                                                    if (resultValTerima == false)
                                                    {
                                                        return (resultValTerima, messageValTerima);
                                                    }

                                                    //untuk update penerimaan di pemesanan header
                                                    var updatePemesananHeader = await this._pemesananDao.UpdatePenambahanTerimaHeader(
                                                        new tr_pemesanan_update_penambahan_terima
                                                        {
                                                            pemesanan_id = (long)detail.pemesanan_id,
                                                            jumlah_item_terima = detail.qty_terima,
                                                            total_transaksi_terima = detail.sub_total
                                                        });

                                                    if (updatePemesananHeader <= 0)
                                                    {
                                                        if (updatePemesananHeader == -2)
                                                        {
                                                            this._db.rollBackTrans();
                                                            return (false, "Data pemesanan tidak ditemukan");

                                                            throw new Exception("Data pemesanan tidak ditemukan");
                                                        }


                                                        this._db.rollBackTrans();
                                                        return (false, "Gagal update penambahan pesanan di tabel pemesanan");

                                                        throw new Exception("Gagal update penambahan pesanan di tabel pemesanan");
                                                    }


                                                    //untuk update penerimaan di pemesanan header
                                                    var updatePemesananDetail = await this._pemesananDao.UpdatePenambahanTerimaDetail(
                                                        new tr_pemesanan_detail_update_penambahan_terima
                                                        {
                                                            pemesanan_id = (long)detail.pemesanan_id,
                                                            pemesanan_detail_id = (long)detail.pemesanan_detail_id,
                                                            qty_terima = detail.qty_terima,
                                                            sub_total_terima = detail.sub_total
                                                        });

                                                    if (updatePemesananDetail <= 0)
                                                    {
                                                        this._db.rollBackTrans();
                                                        return (false, "Gagal update penambahan pesanan di tabel pemesanan detail");

                                                        throw new Exception("Gagal update penambahan pesanan di tabel pemesanan detail");
                                                    }

                                                    #endregion

                                                    #region Update Qty Terima di Kontrak

                                                    if (pemesananData.kontrak_id is not null && pemesananData.kontrak_detail_item_id is not null)
                                                    {
                                                        (bool resultValKontrak, string messageValKontrak) = await ValidateQtyTerimaDiKontrak(
                                                             (long)pemesananData.kontrak_detail_item_id,
                                                             (decimal)detail.qty_terima);

                                                        if (resultValKontrak == false)
                                                        {
                                                            return (resultValKontrak, messageValKontrak);
                                                        }

                                                        //untuk update penambahan 
                                                        var paramUpdateKontrak = new tr_kontrak_spjb_update_penambahan_terima
                                                        {
                                                            kontrak_id = (long)pemesananData.kontrak_id,
                                                            jumlah_item_terima = detail.qty_terima,
                                                            total_transaksi_terima = detail.sub_total
                                                        };

                                                        //untuk update penambahan qty kontrak
                                                        var updateQtyTerimaKontrak = await this._kontrakDao.UpdatePenambahanTerimaHeader(paramUpdateKontrak);

                                                        if (updateQtyTerimaKontrak > 0)
                                                        {
                                                            //untuk update penambahan detail
                                                            var paramUpdateKontrakDetail = new tr_kontrak_spjb_detail_update_penambahan_terima
                                                            {
                                                                kontrak_detail_item_id = (long)pemesananData.kontrak_detail_item_id,
                                                                kontrak_id = (long)pemesananData.kontrak_id,
                                                                qty_terima = detail.qty_terima,
                                                                sub_total_terima = detail.sub_total
                                                            };

                                                            //untuk update penambahan qty detail kontrak
                                                            var updateQtyTerimaKontrakDetail = await this._kontrakDao.UpdatePenambahanTerimaDetail(paramUpdateKontrakDetail);


                                                            if (updateQtyTerimaKontrakDetail <= 0)
                                                            {
                                                                this._db.rollBackTrans();
                                                                return (false, "Gagal update qty terima di tabel kontrak detail");

                                                                throw new Exception("Gagal update qty terima di tabel kontrak detail");
                                                            }
                                                        }
                                                        else
                                                        {
                                                            this._db.rollBackTrans();
                                                            return (false, "Gagal update qty terima di tabel kontrak");

                                                            throw new Exception("Gagal update qty terima di tabel kontrak");
                                                        }
                                                    }

                                                    #endregion
                                                }
                                            }
                                        }

                                        #endregion
                                    }
                                }
                            }

                            //jika jenis penerimaan account payable == true => input hutang supplier
                            if (penerimaanData.is_account_payable)
                            {
                                //parameter input hutang supplier
                                var paramHutangSupplier = new akun_tr_hutang_supplier_insert
                                {
                                    tanggal_hutang_supplier = penerimaanData.tanggal_penerimaan,
                                    tanggal_jatuh_tempo_pembayaran = penerimaanData.tanggal_jatuh_tempo_bayar,
                                    jumlah_hutang = penerimaanData.total_tagihan,
                                    belum_dibayar = penerimaanData.total_tagihan,
                                    user_inputed = data.user_validated,
                                };

                                //hutang supplier   
                                var insertHutangSupplier = await this._hutangDao.AddAkunTrHutangSupplier(paramHutangSupplier);

                                if (insertHutangSupplier <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, "Gagal menambahkan hutang supplier");
                                }
                            }
                        }
                        else
                        {
                            this._db.rollBackTrans();
                            return (false, "penerimaan ini sudah tidak tersedia");
                        }

                    }
                    else
                    {
                        this._db.rollBackTrans();
                        return (false, "Gagal validasi penerimaan");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Data transaksi penerimaan tidak ditemukan");

                    throw new Exception("Data transaksi penerimaan tidak ditemukan");
                }


                this._db.commitTrans();
                return (true, "SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
            }
        }


        public async Task<short> UpdateToCanceled(tr_penerimaan_update_status_to_canceled data)
        {
            try
            {
                return await this._dao.UpdateToCanceled(data);
            }
            catch (Exception)
            {

                throw;
            }
        }


        #endregion


        #region detail item

        public async Task<List<tr_penerimaan_detail_item>> GetTrPenerimaanDetailItemByPenerimaanId(long penerimaan_id)
        {
            try
            {
                return await this._dao.GetTrPenerimaanDetailItemByPenerimaanId(penerimaan_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_penerimaan_detail_item>> GetAllTrPenerimaanDetailItem()
        {
            try
            {
                return await this._dao.GetAllTrPenerimaanDetailItem();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region detail upload

        public async Task<List<tr_penerimaan_detail_upload>> GetTrPenerimaanDetailUploadByPenerimaanId(long penerimaan_id)
        {
            try
            {
                return await this._dao.GetTrPenerimaanDetailUploadByPenerimaanId(penerimaan_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_penerimaan_detail_upload>> GetAllTrPenerimaanDetailUpload()
        {
            try
            {
                return await this._dao.GetAllTrPenerimaanDetailUpload();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrPenerimaanDetailUpload(tr_penerimaan_detail_upload_insert data)
        {
            try
            {
                return await this._dao.AddTrPenerimaanDetailUpload(data);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<short> DeleteTrPenerimaanDetailUpload(long penerimaan_detail_upload_id)
        {
            try
            {
                short deleteBerkas = (short)0;

                var dataBerkas = await this._dao.GetTrPenerimaanDetailUploadById(penerimaan_detail_upload_id);

                if (dataBerkas.Count > 0)
                {
                    deleteBerkas = await this._dao.DeleteTrPenerimaanDetailUpload(penerimaan_detail_upload_id);

                    if (deleteBerkas > 0)
                    {
                        //hapus gambar dari bucket minio
                        await UploadHelper.RemoveObjectFromBucketByPath(dataBerkas[0].path_dokumen);
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
