using DapperPostgreSQL;
using Pharmm.API.Models.Transaksi;
using QueryModel.Model;
using QueryModel.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Dao.Transaksi
{
    public class TransMutasiNoEdDao
    {
        public SQLConn db;

        public TransMutasiNoEdDao(SQLConn db)
        {
            this.db = db;
        }


        #region Header

        public async Task<List<tr_mutasi_no_ed>> GetMutasiPermintaanByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_mutasi_no_ed>("tr_mutasi_permintaan_no_ed_getbydynamicfilters",
                    new
                    {
                        _filters    // not null
                    });
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
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_mutasi_no_ed>("tr_mutasi_permintaan_no_ed_getby_idstockroom_dynamicfilters",
                    new
                    {
                        _id_stockroom,
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed>> GetAllTrMutasiByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_mutasi_no_ed>("tr_mutasi_no_ed_GetByDynamicFilters",
                    new
                    {
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_mutasi_no_ed> GetTrMutasiById(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_mutasi_no_ed>("tr_mutasi_GetById", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_mutasi_no_ed> GetTrMutasiByIdWithLock(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_mutasi_no_ed>("tr_mutasi_GetById_lock", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrMutasi(tr_mutasi_no_ed_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_Insert",
                    new
                    {
                        _nomor_mutasi = data.nomor_mutasi,
                        _tanggal_mutasi = data.tanggal_mutasi,
                        _id_stockroom_pemberi = data.id_stockroom_pemberi,
                        _id_stockroom_penerima = data.id_stockroom_penerima,
                        _keterangan_mutasi = data.keterangan_mutasi,
                        _pic_pemberi_mutasi = data.pic_pemberi_mutasi,
                        _pic_penerima_mutasi = data.pic_penerima_mutasi,
                        _time_serah_terima = data.time_serah_terima,
                        _jumlah_item = data.jumlah_item,
                        _total_transaksi = data.total_transaksi,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrMutasiPermintaan(tr_mutasi_no_ed_insert_permintaan data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_no_ed_insert_permintaan",
                    new
                    {
                        _id_stockroom_pemberi = data.id_stockroom_pemberi,
                        _id_stockroom_penerima = data.id_stockroom_penerima,
                        _nomor_permintaan_mutasi = data.nomor_permintaan_mutasi,
                        _tanggal_permintaan_mutasi = data.tanggal_permintaan_mutasi,
                        //_tanggal_expired_permintaan_mutasi = data.tanggal_expired_permintaan_mutasi,
                        _keterangan_permintaan_mutasi = data.keterangan_permintaan_mutasi,
                        _jumlah_item = data.jumlah_item,
                        _user_permintaan_mutasi = data.user_permintaan_mutasi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> ApproveMutasi(tr_mutasi_no_ed_approve data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_approve",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _nomor_mutasi = data.nomor_mutasi,
                        _keterangan_mutasi = data.keterangan_mutasi,
                        _pic_pemberi_mutasi = data.pic_pemberi_mutasi,
                        _pic_penerima_mutasi = data.pic_penerima_mutasi,
                        _time_serah_terima = data.time_serah_terima,
                        _jumlah_item = data.jumlah_item,
                        _total_transaksi = data.total_transaksi,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateTrMutasiValidated(tr_mutasi_no_ed_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_mutasi_update_to_validated",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateTrMutasiCanceled(tr_mutasi_no_ed_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_mutasi_update_to_canceled",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _user_canceled = data.user_canceled,
                        _reason_canceled = data.reason_canceled
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion

        #region Detail Item


        public async Task<long> ApproveMutasiDetailItem(tr_mutasi_no_ed_detail_item_approve data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_detail_item_approve",
                    new
                    {
                        _mutasi_detail_item_id = data.mutasi_detail_item_id,
                        _qty_satuan_besar_mutasi = data.qty_satuan_besar_mutasi,
                        _kode_satuan_besar_mutasi = data.kode_satuan_besar_mutasi,
                        _isi_mutasi = data.isi_mutasi,
                        _qty_mutasi = data.qty_mutasi,
                        _nominal_mutasi = data.nominal_mutasi,
                        _keterangan_mutasi = data.keterangan_mutasi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item>> GetAllTrMutasiDetailItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_mutasi_no_ed_detail_item>("tr_mutasi_detail_item_GetByDynamicFilters",
                    new
                    {
                        _filters    // not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_mutasi_no_ed_detail_item> GetTrMutasiPermintaanDetailItemById(long _mutasi_detail_item_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_mutasi_no_ed_detail_item>("tr_mutasi_detail_item_get_by_mutasi_detail_item_id", new
                {
                    _mutasi_detail_item_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_mutasi_no_ed_detail_item> GetTrMutasiPermintaanDetailItemByIdWithLock(long _mutasi_detail_item_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_mutasi_no_ed_detail_item>("tr_mutasi_detail_item_get_by_mutasi_detail_item_id_lock", new
                {
                    _mutasi_detail_item_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item>> GetTrMutasiPermintaanDetailItemByMutasiId(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_mutasi_no_ed_detail_item>("tr_mutasi_detail_item_get_permintaan_by_mutasi_id", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item>> GetTrMutasiDetailItemByMutasiId(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_mutasi_no_ed_detail_item>("tr_mutasi_detail_item_get_by_mutasi_id", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_mutasi_no_ed_detail_item_with_hpp>> GetTrMutasiDetailItemWithHppByMutasiId(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_mutasi_no_ed_detail_item_with_hpp>("tr_mutasi_detail_item_with_hpp_get_by_mutasi_id", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrMutasiDetailItem(tr_mutasi_no_ed_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_detail_item_insert",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar_mutasi = data.qty_satuan_besar_mutasi,
                        _kode_satuan_besar_mutasi = data.kode_satuan_besar_mutasi,
                        _isi_mutasi = data.isi_mutasi,
                        _qty_mutasi = data.qty_mutasi,
                        _nominal_mutasi = data.nominal_mutasi,
                        _keterangan_mutasi = data.keterangan_mutasi
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrMutasiDetailItemPermintaan(tr_mutasi_no_ed_detail_item_insert_permintaan data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_detail_item_Insert_permintaan",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar_permintaan = data.qty_satuan_besar_permintaan,
                        _kode_satuan_besar_permintaan = data.kode_satuan_besar_permintaan,
                        _isi_permintaan = data.isi_permintaan,
                        _qty_permintaan = data.qty_permintaan,
                        _keterangan_permintaan = data.keterangan_permintaan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Detail Upload


        public async Task<List<tr_mutasi_no_ed_detail_upload>> GetTrMutasiDetailUploadByMutasiId(long mutasi_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_mutasi_no_ed_detail_upload>("tr_mutasi_detail_upload_get_by_mutasi_id", new
                {
                    _mutasi_id = mutasi_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_mutasi_no_ed_detail_upload> GetTrMutasiDetailUploadById(long mutasi_detail_upload_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_mutasi_no_ed_detail_upload>("tr_mutasi_detail_upload_GetById", new
                {
                    _mutasi_detail_upload_id = mutasi_detail_upload_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrMutasiDetailUpload(tr_mutasi_no_ed_detail_upload_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_mutasi_detail_upload_Insert",
                    new
                    {
                        _mutasi_id = data.mutasi_id,
                        _jenis_dokumen = data.jenis_dokumen,
                        _url_dokumen = data.url_dokumen,
                        _keterangan = data.keterangan
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> DeleteTrMutasiDetailUpload(long mutasi_detail_upload_id)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_mutasi_detail_upload_Delete",
                    new
                    {
                        _mutasi_detail_upload_id = mutasi_detail_upload_id // int not null
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion



    }
}
