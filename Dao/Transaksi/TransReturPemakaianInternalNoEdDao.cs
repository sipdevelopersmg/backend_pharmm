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
    public class TransReturPemakaianInternalNoEdDao
    {
        public SQLConn db;

        public TransReturPemakaianInternalNoEdDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<List<tr_retur_pemakaian_internal_no_ed>> GetAllTrReturPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_retur_pemakaian_internal_no_ed>("tr_retur_pemakaian_internal_no_ed_GetByDynamicFilters",
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


        public async Task<tr_retur_pemakaian_internal_no_ed> GetTrReturPemakaianInternalById(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_retur_pemakaian_internal_no_ed>("tr_retur_pemakaian_internal_GetById", new
                {
                    _retur_pemakaian_internal_id = retur_pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_retur_pemakaian_internal_no_ed> GetTrReturPemakaianInternalByIdWithLock(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_retur_pemakaian_internal_no_ed>("tr_retur_pemakaian_internal_GetById_lock", new
                {
                    _retur_pemakaian_internal_id = retur_pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrReturPemakaianInternal(tr_retur_pemakaian_internal_no_ed_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_retur_pemakaian_internal_no_ed_Insert",
                    new
                    {
                        _nomor_retur_pemakaian_internal = data.nomor_retur_pemakaian_internal,
                        _tanggal_retur_pemakaian_internal = data.tanggal_retur_pemakaian_internal,
                        _id_stockroom = data.id_stockroom,
                        _keterangan_retur_pemakaian_internal = data.keterangan_retur_pemakaian_internal,
                        _pic_pemberi = data.pic_pemberi,
                        _pic_penerima = data.pic_penerima,
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


        public async Task<short> UpdateTrReturPemakaianInternalValidated(tr_retur_pemakaian_internal_no_ed_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_retur_pemakaian_internal_update_to_validated",
                    new
                    {
                        _retur_pemakaian_internal_id = data.retur_pemakaian_internal_id,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateTrReturPemakaianInternalCanceled(tr_retur_pemakaian_internal_no_ed_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_retur_pemakaian_internal_update_to_canceled",
                    new
                    {
                        _retur_pemakaian_internal_id = data.retur_pemakaian_internal_id,
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

        public async Task<List<tr_retur_pemakaian_internal_no_ed_detail_item>> GetAllTrReturPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_retur_pemakaian_internal_no_ed_detail_item>("tr_retur_pemakaian_internal_detail_item_GetByDynamicFilters",
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


        public async Task<List<tr_retur_pemakaian_internal_no_ed_detail_item>> GetTrReturPemakaianInternalDetailItemByHeaderId(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pemakaian_internal_no_ed_detail_item>("tr_retur_pemakaian_internal_detail_item_get_by_headerid", new
                {
                    _retur_pemakaian_internal_id = retur_pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }



        public async Task<List<tr_retur_pemakaian_internal_no_ed_detail_item>> GetTrReturPemakaianInternalDetailItemByHeaderIdWithLock(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pemakaian_internal_no_ed_detail_item>("tr_retur_pemakaian_internal_detail_item_Getby_headerid_lock", new
                {
                    _retur_pemakaian_internal_id = retur_pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrReturPemakaianInternalDetailItem(tr_retur_pemakaian_internal_no_ed_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_retur_pemakaian_internal_detail_item_no_ed_Insert",
                    new
                    {
                        _retur_pemakaian_internal_id = data.retur_pemakaian_internal_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar_retur_pemakaian_internal = data.qty_satuan_besar_retur_pemakaian_internal,
                        _kode_satuan_besar_retur_pemakaian_internal = data.kode_satuan_besar_retur_pemakaian_internal,
                        _isi_retur_pemakaian_internal = data.isi_retur_pemakaian_internal,
                        _qty_retur_pemakaian_internal = data.qty_retur_pemakaian_internal,
                        _hpp_satuan = data.hpp_satuan,
                        _nominal_retur_pemakaian_internal = data.nominal_retur_pemakaian_internal,
                        _keterangan_retur_pemakaian_internal = data.keterangan_retur_pemakaian_internal
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region Detail Upload



        public async Task<tr_retur_pemakaian_internal_no_ed_detail_upload> GetTrReturPemakaianInternalDetailUploadById(long retur_pemakaian_internal_detail_upload_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_retur_pemakaian_internal_no_ed_detail_upload>("tr_retur_pemakaian_internal_detail_upload_getbyid", new
                {
                    _retur_pemakaian_internal_detail_upload_id = retur_pemakaian_internal_detail_upload_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_retur_pemakaian_internal_no_ed_detail_upload>> GetTrReturPemakaianInternalDetailUploadHeaderId(long retur_pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_retur_pemakaian_internal_no_ed_detail_upload>("tr_retur_pemakaian_internal_detail_upload_get_by_headerid",
                    new
                    {
                        _retur_pemakaian_internal_id = retur_pemakaian_internal_id
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<long> AddTrReturPemakaianInternalDetailUpload(tr_retur_pemakaian_internal_no_ed_detail_upload_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_retur_pemakaian_internal_detail_upload_Insert",
                    new
                    {
                        _retur_pemakaian_internal_id = data.retur_pemakaian_internal_id,
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

        public async Task<short> DeleteTrReturPemakaianInternalDetailUpload(long retur_pemakaian_internal_detail_upload_id)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_retur_pemakaian_internal_detail_upload_Delete",
                    new
                    {
                        _retur_pemakaian_internal_detail_upload_id = retur_pemakaian_internal_detail_upload_id // int not null
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
