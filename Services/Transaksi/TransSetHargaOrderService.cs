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
    public interface ITransSetHargaOrderService
    {
        #region Header

        Task<List<set_harga_order>> GetAllSetHargaOrderByParams(List<ParameterSearchModel> param);
        Task<List<set_harga_order>> GetAllSetHargaOrder();
        Task<List<set_harga_order>> GetSetHargaOrderById(long set_harga_order_id);

        Task<(bool,string)> AddSetHargaOrder(set_harga_order_insert data);

        #endregion

        #region Detail

        Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetailByParams(List<ParameterSearchModel> param);
        Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetail();
        Task<List<set_harga_order_detail>> GetSetHargaOrderDetailByHargaOrderId(long set_harga_order_detail_id);
        Task<List<set_harga_order_detail_berlaku>> GetAllSetHargaOrderBerlakuDetailByIdSupplierParams(Int16 _id_supplier, List<ParameterSearchModel> param);
        #endregion
    }

    public class TransSetHargaOrderService : ITransSetHargaOrderService
    {
        private readonly SQLConn _db;
        private readonly TransSetHargaOrderDao _dao;
        private readonly MasterCounterDao _kodeDao;

        public TransSetHargaOrderService(SQLConn db, TransSetHargaOrderDao dao,
            MasterCounterDao kodeDao)
        {
            this._db = db;
            this._kodeDao = kodeDao;
            this._dao = dao;
        }

        #region Header
        public async Task<List<set_harga_order>> GetAllSetHargaOrderByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllSetHargaOrderByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<set_harga_order>> GetAllSetHargaOrder()
        {
            try
            {
                return await this._dao.GetAllSetHargaOrder();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<set_harga_order>> GetSetHargaOrderById(long set_harga_order_id)
        {
            try
            {
                return await this._dao.GetSetHargaOrderById(set_harga_order_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<(bool,string)> AddSetHargaOrder(set_harga_order_insert data)
        {
            this._db.beginTransaction();
            try
            {


                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeSetHargaOrder,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };


                //generate nomor harga order
                var noHargaOrder = this._kodeDao.GenerateKode(dataCounter).Result; 

                short hargaOrderDetailId = 0;

                if (string.IsNullOrEmpty(noHargaOrder))
                {

                    this._db.rollBackTrans();
                    return (false, "Gagal mendapatkan nomor harga order");

                    throw new Exception("Gagal mendapatkan nomor harga order");
                }
                else
                {
                    data.nomor_harga_order = noHargaOrder;
                }


                //input header
                short hargaOrderId = await this._dao.AddSetHargaOrder(data);

                //cek jika input header berhasil
                if (hargaOrderId > 0)
                {
                    //cek jika jumlah detail > 0
                    if (data.details.Count > 0)
                    {
                        foreach (var detail in data.details)
                        {
                            detail.set_harga_order_id = hargaOrderId;
                            
                            //input detail
                            hargaOrderDetailId = await this._dao.AddSetHargaOrderDetail(detail);


                            //cek jika gagal input detail, maka rollback
                            if(hargaOrderDetailId <= 0)
                            {

                                this._db.rollBackTrans();
                                return (false, "Gagal input harga order detail, dengan nomor urut " + detail.no_urut.ToString());

                                throw new Exception("Gagal input harga order detail, dengan nomor urut " + detail.no_urut.ToString());
                            }
                        }
                    }

                    //jika berhasil simpan transaksi, maka update counter
                    var updateKodeCounter = await this._kodeDao.AddUpdateMasterCounter(dataCounter);

                    if (string.IsNullOrEmpty(updateKodeCounter))
                    {

                        this._db.rollBackTrans();
                        return (false, "Gagal update nomor harga order");

                        throw new Exception("Gagal update nomor harga order");
                    }   
                }

                this._db.commitTrans();
                return (true,"SUCCESS");
            }
            catch (Exception)
            {
                this._db.rollBackTrans();
                throw;
                //TODO : log error
            }
        }
        #endregion

        #region Detail
        public async Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetailByParams(List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllSetHargaOrderDetailByParams(param);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<set_harga_order_detail>> GetAllSetHargaOrderDetail()
        {
            try
            {
                return await this._dao.GetAllSetHargaOrderDetail();
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<set_harga_order_detail>> GetSetHargaOrderDetailByHargaOrderId(long set_harga_order_id)
        {
            try
            {
                return await this._dao.GetSetHargaOrderDetailByHargaOrderId(set_harga_order_id);
            }
            catch (Exception)
            {
                throw;
                //TODO : log error
            }
        }
        public async Task<List<set_harga_order_detail_berlaku>> GetAllSetHargaOrderBerlakuDetailByIdSupplierParams(short _id_supplier, List<ParameterSearchModel> param)
        {
            try
            {
                return await this._dao.GetAllSetHargaOrderBerlakuDetailByIdSupplierParams(_id_supplier, param);
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
