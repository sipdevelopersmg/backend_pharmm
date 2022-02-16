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
    public interface ITransReturPembelianNoEdService
    {
        #region Header

        Task<List<tr_retur_pembelian_no_ed>> GetAllTrReturPembelianByParams(List<ParameterSearchModel> param);
        Task<tr_retur_pembelian_no_ed> GetTrReturPembelianById(long retur_pembelian_id);
        Task<long> AddTrReturPembelian(tr_retur_pembelian_no_ed_insert data);

        Task<(bool, string)> UpdateToValidated(tr_retur_pembelian_no_ed_update_status_to_validated data);
        Task<(bool, string)> UpdateToCanceled(tr_retur_pembelian_no_ed_update_status_to_canceled data);

        #endregion


        #region Detail

        Task<List<tr_retur_pembelian_no_ed_detail>> GetAllTrReturPembelianDetailByParams(List<ParameterSearchModel> param);
        Task<List<tr_retur_pembelian_no_ed_detail>> GetTrReturPembelianDetailByReturPembelianId(long retur_pembelian_id);

        #endregion

        #region Detail Penukaran

        Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetAllTrReturPembelianDetailPenukaranByParams(List<ParameterSearchModel> param);
        Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetTrReturPembelianDetailPenukaranByReturPembelianId(long retur_pembelian_id);

        #endregion
    }

    public class TransReturPembelianNoEdService : ITransReturPembelianNoEdService
    {
        private readonly SQLConn _db;
        private readonly TransReturPembelianNoEdDao _dao;
        private readonly TransPenerimaanNoEdDao _penerimaanDao;
        private readonly MasterCounterDao _kodeDao;
        private readonly SetupStokItemDao _stokItemDao;
        private readonly SetupItemDao _itemDao;
        private readonly KartuStokItemDao _kartuStokDao;
        private readonly TransPiutangSupplierDao _piutangDao;

        public TransReturPembelianNoEdService(SQLConn db, TransReturPembelianNoEdDao dao,
            MasterCounterDao kodeDao,
            TransPenerimaanNoEdDao penerimaanDao,
            SetupStokItemDao stokItemDao,
            SetupItemDao itemDao,
            KartuStokItemDao kartuStokDao,
            TransPiutangSupplierDao piutangDao)
        {
            this._db = db;
            this._kodeDao = kodeDao;
            this._dao = dao;
            this._penerimaanDao = penerimaanDao;
            this._stokItemDao = stokItemDao;
            this._itemDao = itemDao;
            this._kartuStokDao = kartuStokDao;
            this._piutangDao = piutangDao;
        }

        #region Lookup

        #endregion

        #region Header

        public async Task<List<tr_retur_pembelian_no_ed>> GetAllTrReturPembelianByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrReturPembelianByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<tr_retur_pembelian_no_ed> GetTrReturPembelianById(long retur_pembelian_id)
        {
            try
            {
                return await this._dao.GetTrReturPembelianById(retur_pembelian_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<long> AddTrReturPembelian(tr_retur_pembelian_no_ed_insert data)
        {
            this._db.beginTransaction();

            try
            {

                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeReturPembelian,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                //generate nomor retur pembelian
                var noReturPembelian = this._kodeDao.GenerateKode(dataCounter).Result;

                long returPembelianId = 0;
                long returPembelianDetailId = 0;

                if (string.IsNullOrEmpty(noReturPembelian))
                {
                    throw new Exception("Gagal mendapatkan nomor retur pembelian");
                }
                else
                {
                    data.nomor_retur_pembelian = noReturPembelian;
                }


                if (data.details.Count > 0)
                {
                    data.total_transaksi_retur = 0;
                    data.jumlah_item_retur = 0;

                    foreach (var item in data.details)
                    {

                        item.qty_retur = item.isi * item.qty_satuan_besar;
                        item.sub_total = item.qty_retur * item.harga_satuan_retur;

                        data.jumlah_item_retur += item.qty_satuan_besar;
                        data.total_transaksi_retur += item.sub_total;
                    }
                }

                returPembelianId = await this._dao.AddTrReturPembelian(data);

                if (returPembelianId > 0)
                {

                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.retur_pembelian_id = returPembelianId;
                            returPembelianDetailId = await this._dao.AddTrReturPembelianDetail(detail);

                            if (returPembelianDetailId <= 0)
                            {
                                throw new Exception("Gagal input retur pembelian detail dengan no urut " + detail.no_urut.ToString());
                            }
                            else
                            {
                                var paramUpdateQtyRetur = new tr_penerimaan_detail_item_update_qty_retur
                                {
                                    penerimaan_detail_item_id = detail.penerimaan_detail_id,
                                    qty_diretur = detail.qty_retur
                                };

                                //var updateQtyRetur = await this._penerimaanDao.UpdateQtyRetur(paramUpdateQtyRetur);

                                //if (updateQtyRetur <= 0)
                                //{
                                //    throw new Exception("Gagal merubah qty retur pembelian di tabel pembelian");
                                //}
                            }
                        }
                    }


                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {
                        throw new Exception("Gagal update nomor retur pembelian");
                    }
                }

                this._db.commitTrans();
                return returPembelianId;
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }

        public async Task<(bool, string)> UpdateToValidated(tr_retur_pembelian_no_ed_update_status_to_validated data)
        {
            this._db.beginTransaction();
            try
            {
                var updateStokHeader = 0;
                var insertKartuStok = 0;
                var validate = (short)0;

                short id_stockroom = 0;

                //get retur_pembelian data
                var returPembelianData = await this._dao.GetTrReturPembelianByIdWithLock(data.retur_pembelian_id);

                if (returPembelianData is not null)
                {
                    #region Cek Status Transaksi

                    if (returPembelianData.user_validated is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah divalidasi");
                    }
                    else if (returPembelianData.user_canceled is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah dibatalkan");
                    }
                    else if (returPembelianData.user_lunas_tukar_barang is not null)
                    {

                        this._db.rollBackTrans();
                        return (false, "Transaksi ini sudah lunas tukar barang");
                    }
                    #endregion

                    //validasi retur_pembelian
                    validate = await this._dao.UpdateToValidated(data);

                    if (validate > 0)
                    {
                        id_stockroom = returPembelianData.id_stockroom;

                        //get retur_pembelian data detail
                        var returPembelianDataDetail = await this._dao.GetTrReturPembelianDetailByReturPembelianIdWithLock(data.retur_pembelian_id);

                        if (returPembelianDataDetail.Count > 0)
                        {
                            //jika dari gudang farmasi
                            if (id_stockroom > 0)
                            {
                                //rekap retur_pembelian untuk di summary ke header
                                var rekapForHeader = returPembelianDataDetail.GroupBy(q => q.id_item).Select(res =>
                                new
                                {
                                    retur_pembelian_id = res.FirstOrDefault().retur_pembelian_id,
                                    id_item = res.FirstOrDefault().id_item,
                                    id_stockroom = id_stockroom,
                                    qty_on_hand = res.Sum(s => s.qty_retur),
                                    nominal = res.Sum(n => n.sub_total),
                                    details = returPembelianDataDetail.Where(x => x.id_item == res.FirstOrDefault().id_item).ToList()
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

                                        //get data barang dan gudang
                                        var dataBarangAndGudang = await this._stokItemDao.GetDataBarangAndStockroomFromMasterWithLock(item.id_item, id_stockroom);

                                        #region Insert Kartu Stok Header

                                        //parameter untuk mengurangi kartu stok
                                        var kartuStokParam = new mm_kartu_stok_item_insert_pengurangan_stok
                                        {
                                            id_detail_transaksi = null,
                                            id_header_transaksi = data.retur_pembelian_id,
                                            id_item = item.id_item,
                                            id_stockroom = item.id_stockroom, //gudang farmasi
                                            keterangan = "KELUAR RETUR PEMBELIAN BARANG TANPA ED",
                                            stok_awal = cekStokAkhir?.stok_akhir,
                                            nominal_awal = cekStokAkhir?.nominal_akhir,
                                            stok_keluar = item.qty_on_hand,
                                            nominal_keluar = item.nominal,
                                            nomor_ref_transaksi = returPembelianData.nomor_retur_pembelian,
                                            user_inputed = data.user_validated
                                        };

                                        insertKartuStok = await this._kartuStokDao.AddPenguranganMmKartuStokItem(kartuStokParam);

                                        if (insertKartuStok <= 0)
                                        {
                                            this._db.rollBackTrans();
                                            return (false, "Gagal menambah kartu stok header");

                                            throw new Exception("Gagal menambah kartu stok header");
                                        }

                                        #endregion


                                        //get stok header
                                        var cekStokItem = await this._stokItemDao.GetAllMmSetupStokItemByIdItemAndIdStockroomWithLock(
                                            item.id_item,
                                            id_stockroom
                                            );

                                        if (cekStokItem is null)
                                        {

                                            this._db.rollBackTrans();
                                            return (false, $"Tidak ada stok di gudang {dataBarangAndGudang?.nama_stockroom}");
                                        }
                                        else
                                        {

                                            if ((cekStokItem.qty_on_hand - item.qty_on_hand) < 0)
                                            {

                                                this._db.rollBackTrans();
                                                return (false,  $"Sisa stok untuk item {dataBarangAndGudang?.nama_item} tidak mencukupi ");
                                            }

                                            #region Update Stok Item Header

                                            //parameter untuk update stok header
                                            var stokItemParam = new mm_setup_stok_item_update_stok
                                            {
                                                id_item = item.id_item,
                                                id_stockroom = id_stockroom,
                                                qty_on_hand = item.qty_on_hand
                                            };

                                            updateStokHeader = await this._stokItemDao.UpdatePenguranganStok(stokItemParam);

                                            if (updateStokHeader <= 0)
                                            {
                                                this._db.rollBackTrans();
                                                return (false, "Gagal mengurangi stok header");

                                                throw new Exception("Gagal mengurangi stok header");
                                            }

                                            #endregion
                                        }

                                    }
                                }
                            }


                            //jika potong tagihan
                            if (returPembelianData.id_mekanisme_retur == 1)
                            {
                                //parameter input piutang supplier
                                var paramPiutangSupplier = new akun_tr_piutang_supplier_insert
                                {
                                    id_jenis_piutang_supplier = 1,
                                    tanggal_piutang_supplier = returPembelianData.tanggal_retur_pembelian,
                                    tanggal_jatuh_tempo_pembayaran = returPembelianData.tanggal_jatuh_tempo_pelunasan_retur,
                                    jumlah_piutang = returPembelianData.total_transaksi_retur,
                                    belum_dibayar = returPembelianData.total_transaksi_retur,
                                    user_inputed = data.user_validated,
                                };

                                //piutang supplier
                                var insertpiutangSupplier = await this._piutangDao.AddAkunTrPiutangSupplier(paramPiutangSupplier);

                                if (insertpiutangSupplier <= 0)
                                {
                                    this._db.rollBackTrans();
                                    return (false, "Gagal menambahkan piutang supplier");

                                    throw new Exception("Gagal menambahkan piutang supplier");
                                }
                            }

                        }
                        else
                        {
                            this._db.rollBackTrans();
                            return (false, "Retur pembelian ini sudah tidak tersedia");

                            throw new Exception("Retur pembelian ini sudah tidak tersedia");
                        }

                    }
                    else
                    {
                        this._db.rollBackTrans();
                        return (false, "Gagal validasi retur pembelian");
                    }
                }
                else
                {
                    this._db.rollBackTrans();
                    return (false, "Data transaksi retur_pembelian tidak ditemukan");

                    throw new Exception("Data transaksi retur_pembelian tidak ditemukan");
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

        public async Task<(bool, string)> UpdateToCanceled(tr_retur_pembelian_no_ed_update_status_to_canceled data)
        {

            this._db.beginTransaction();
            try
            {
                var dataRetur = await this._dao.GetTrReturPembelianByIdWithLock(data.retur_pembelian_id);

                if(dataRetur.user_validated is not null)
                {

                    this._db.rollBackTrans();
                    return (false, "Transaksi ini sudah divalidasi");
                }else if(dataRetur.user_canceled is not null)
                {

                    this._db.rollBackTrans();
                    return (false, "Transaksi ini sudah dibatalkan");
                }

                var canceled = await this._dao.UpdateToCanceled(data);


                this._db.commitTrans();
                return (true, "SUCCESS");
            }
            catch (Exception)
            {

                this._db.rollBackTrans();
                throw;
            }
        }

        #endregion

        #region Detail
        public async Task<List<tr_retur_pembelian_no_ed_detail>> GetAllTrReturPembelianDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrReturPembelianDetailByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        public async Task<List<tr_retur_pembelian_no_ed_detail>> GetTrReturPembelianDetailByReturPembelianId(long retur_pembelian_id)
        {
            try
            {
                return await this._dao.GetTrReturPembelianDetailByReturPembelianId(retur_pembelian_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }

        #endregion

        #region Detail Penukaran

        public async Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetAllTrReturPembelianDetailPenukaranByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllTrReturPembelianDetailPenukaranByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }


        public async Task<List<tr_retur_pembelian_no_ed_detail_penukaran>> GetTrReturPembelianDetailPenukaranByReturPembelianId(long retur_pembelian_id)
        {
            try
            {
                return await this._dao.GetTrReturPembelianDetailPenukaranByReturPembelianId(retur_pembelian_id);
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
