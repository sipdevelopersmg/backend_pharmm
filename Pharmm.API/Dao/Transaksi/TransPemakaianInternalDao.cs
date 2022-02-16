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
    public class TransPemakaianInternalDao
    {
        public SQLConn db;

        public TransPemakaianInternalDao(SQLConn db)
        {
            this.db = db;
        }

        #region Header

        public async Task<List<tr_pemakaian_internal>> GetAllOpenTrPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemakaian_internal>("tr_pemakaian_internal_get_open_dynamicfilters",
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

        public async Task<List<tr_pemakaian_internal>> GetAllTrPemakaianInternalByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemakaian_internal>("tr_pemakaian_internal_GetByDynamicFilters",
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

        public async Task<tr_pemakaian_internal> GetTrPemakaianInternalById(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemakaian_internal>("tr_pemakaian_internal_GetById", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<tr_pemakaian_internal> GetTrPemakaianInternalByIdWithLock(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemakaian_internal>("tr_pemakaian_internal_GetById_lock", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemakaianInternal(tr_pemakaian_internal_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemakaian_internal_Insert",
                    new
                    {
                        _nomor_pemakaian_internal = data.nomor_pemakaian_internal,
                        _tanggal_pemakaian_internal = data.tanggal_pemakaian_internal,
                        _id_stockroom = data.id_stockroom,
                        _keterangan_pemakaian_internal = data.keterangan_pemakaian_internal,
                        _jumlah_item = data.jumlah_item,
                        _user_inputed = data.user_inputed
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<short> UpdateTrPemakaianInternalValidated(tr_pemakaian_internal_update_to_validated data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemakaian_internal_update_to_validated",
                    new
                    {
                        _pemakaian_internal_id = data.pemakaian_internal_id,
                        _pic_pemberi = data.pic_pemberi,
                        _pic_penerima = data.pic_penerima,
                        _time_serah_terima = data.time_serah_terima,
                        _total_transaksi = data.total_transaksi,
                        _user_validated = data.user_validated
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<short> UpdateTrPemakaianInternalCanceled(tr_pemakaian_internal_update_to_canceled data)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemakaian_internal_update_to_canceled",
                    new
                    {
                        _pemakaian_internal_id = data.pemakaian_internal_id,
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


        public async Task<List<tr_pemakaian_internal_detail_item>> GetAllTrPemakaianInternalDetailItemByParams(List<ParameterSearchModel> param)
        {
            var _filters = string.Empty;
            try
            {
                _filters = ParameterQuery.GetQueryFiltersByParams(param);

                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_item>("tr_pemakaian_internal_detail_item_GetByDynamicFilters",
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

        public async Task<List<tr_pemakaian_internal_detail_item>> GetTrPemakaianInternalDetailItemByPemakaianInternalId(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_item>("tr_pemakaian_internal_detail_item_get_by_pemakaian_internal_id", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<tr_pemakaian_internal_detail_item>> GetTrPemakaianInternalDetailItemByHeaderIdWithLock(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_item>("tr_pemakaian_internal_detail_item_Getby_headerid_lock", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemakaianInternalDetailItem(tr_pemakaian_internal_detail_item_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemakaian_internal_detail_item_insert",
                    new
                    {
                        _pemakaian_internal_id = data.pemakaian_internal_id,
                        _no_urut = data.no_urut,
                        _id_item = data.id_item,
                        _qty_satuan_besar_pemakaian_internal = data.qty_satuan_besar_pemakaian_internal,
                        _kode_satuan_besar_pemakaian_internal = data.kode_satuan_besar_pemakaian_internal,
                        _isi_pemakaian_internal = data.isi_pemakaian_internal,
                        _qty_pemakaian_internal = data.qty_pemakaian_internal,
                        _keterangan_pemakaian_internal = data.keterangan_pemakaian_internal
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> ValidasiDetailItem(tr_pemakaian_internal_detail_item_validate data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemakaian_internal_detail_item_validate",
                    new
                    {
                        _pemakaian_internal_detail_item_id = data.pemakaian_internal_detail_item_id,
                        _nominal_pemakaian_internal = data.nominal_pemakaian_internal
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }


        #endregion

        #region Detail Batch

        public async Task<List<tr_pemakaian_internal_detail_item_batch>> GetTrPemakaianInternalDetailItemBatchByDetailItemId(long pemakaian_internal_detail_item_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_item_batch>("tr_pemakaian_internal_detail_item_batch_get_by_detailitemid", new
                {
                    _pemakaian_internal_detail_item_id = pemakaian_internal_detail_item_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<tr_pemakaian_internal_detail_item_batch>> GetTrPemakaianInternalDetailItemBatchByPemakaianInternalId(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_item_batch>("tr_pemakaian_internal_detail_item_batch_get_by_headerid", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemakaianInternalDetailItemBatch(tr_pemakaian_internal_detail_item_batch_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemakaian_internal_detail_item_batch_Insert",
                    new
                    {
                        _pemakaian_internal_detail_item_id = data.pemakaian_internal_detail_item_id,
                        _pemakaian_internal_id = data.pemakaian_internal_id,
                        _batch_number = data.batch_number,
                        _expired_date = data.expired_date,
                        _qty_pemakaian_internal = data.qty_pemakaian_internal,
                        _hpp_satuan = data.hpp_satuan,
                        _sub_total = data.sub_total
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion

        #region Detail Upload


        public async Task<List<tr_pemakaian_internal_detail_upload>> GetTrPemakaianInternalDetailUploadByPemakaianInternalId(long pemakaian_internal_id)
        {
            try
            {
                return await this.db.QuerySPtoList<tr_pemakaian_internal_detail_upload>("tr_pemakaian_internal_detail_upload_get_by_headerid", new
                {
                    _pemakaian_internal_id = pemakaian_internal_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<tr_pemakaian_internal_detail_upload> GetTrPemakaianInternalDetailUploadById(long pemakaian_internal_detail_upload_id)
        {
            try
            {
                return await this.db.QuerySPtoSingle<tr_pemakaian_internal_detail_upload>("tr_pemakaian_internal_detail_upload_GetById", new
                {
                    _pemakaian_internal_detail_upload_id = pemakaian_internal_detail_upload_id
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<long> AddTrPemakaianInternalDetailUpload(tr_pemakaian_internal_detail_upload_insert data)
        {
            try
            {
                return await this.db.executeScalarSp<long>("tr_pemakaian_internal_detail_upload_Insert",
                    new
                    {
                        _pemakaian_internal_id = data.pemakaian_internal_id,
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

        public async Task<short> DeleteTrPemakaianInternalDetailUpload(long pemakaian_internal_detail_upload_id)
        {
            try
            {
                return await this.db.executeScalarSp<short>("tr_pemakaian_internal_detail_upload_Delete",
                    new
                    {
                        _pemakaian_internal_detail_upload_id = pemakaian_internal_detail_upload_id // int not null
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
