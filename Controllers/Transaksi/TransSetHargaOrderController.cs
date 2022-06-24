using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Services.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;
using Pharmm.API.Helper;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransSetHargaOrderController : ControllerBase
    {

        private IMasterCounterService _kodeService;
        private ISetupItemService _itemservice;
        private ITransSetHargaOrderService _service;
        private readonly ILogger<TransSetHargaOrderController> _logger;

        public TransSetHargaOrderController(ISetupItemService itemservice, ITransSetHargaOrderService service,
        ILogger<TransSetHargaOrderController> logger,
        IMasterCounterService kodeService )
        {
            this._itemservice = itemservice;
            this._kodeService = kodeService;
            this._service = service;
            this._logger = logger;
        }

        [HttpPost("{_id_supplier}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item yang belum ter setting harga order by id supplier and param (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_setup_item = msi (id_grup_item, id_pabrik, id_supplier, kode_item, barcode, nama_item, " +
            "id_temperatur_item, <br> " +
            "mm_setup_supplier mss (kode_supplier,nama_supplier,alamat_supplier), <br> " +
            "mm_setup_grup_item msgi (kode_grup_item,grup_item), <br> " +
            "mm_setup_pabrik msp (kode_pabrik,nama_pabrik,alamat_pabrik), <br>" +
            "mm_setup_satuan mss (kode_satuan,nama_satuan), <br>" +
            "mm_setup_temperatur_item msti (temperatur_item)")]
        public async Task<IActionResult> GetBarangForLookupInputHargaOrderByIdSupplierAndParams(short _id_supplier, [FromBody] get_barang_input_harga_order_by_id_supplier param)
        {

            try
            {   
                var notin = "";
                if(param.notin!=""){
                    notin = "msi.id_item NOT IN ("+param.notin+") AND";
                }
                
                var result = await this._itemservice.GetAllMmSetupItemBelumSettingHargaOrderByIdSupplierAndParams(_id_supplier, param.filters, notin);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_item>(),
                         _responseResult: true,
                         _message: "data tidak ditemukan"
                         ));
                }

                return Ok(ResponseHelper.GetResponse(
                    _data: result,
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix set_harga_order = sho, <br> " +
            "mm_setup_supplier = mss (kode_supplier,nama_supplier,alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllSetHargaOrderByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<set_harga_order>(),
                         _responseResult: true,
                    _message: "data tidak ditemukan"
                         ));
                }

                return Ok(ResponseHelper.GetResponse(
                    _data: result,
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllSetHargaOrder();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<set_harga_order>(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpGet("{set_harga_order_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<set_harga_order>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order by id")]
        public async Task<IActionResult> GetById(long set_harga_order_id)
        {

            try
            {
                var result = await this._service.GetSetHargaOrderById(set_harga_order_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result[0],
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new object(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk mendapatkan nomor harga order ")]
        public async Task<IActionResult> GetNoHargaOrder()
        {

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

                var result = await this._kodeService.GenerateKode(dataCounter);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }   

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data set_harga_order")]
        public async Task<IActionResult> Insert([FromBody] set_harga_order_insert param)
        {
            try
            {
                param.user_inputed = (short) HttpContext.Items["userId"];

                (bool result,string message) = await this._service.AddSetHargaOrder(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: 0,
                    _message: message
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        #region Detail
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order_detail by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix set_harga_order_detail = shod, <br>" +
            "mm_setup_item msi (kode_item,barcode,nama_item)")]
        public async Task<IActionResult> GetAllDetailByParams([FromBody] List<ParameterSearchModel> param)
        {
            try
            {
                var result = await this._service.GetAllSetHargaOrderDetailByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<set_harga_order_detail>(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order_detail")]
        public async Task<IActionResult> GetAllDetail()
        {
            try
            {
                var result = await this._service.GetAllSetHargaOrderDetail();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<set_harga_order_detail>(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpGet("{set_harga_order_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data set_harga_order_detail by id")]
        public async Task<IActionResult> GetDetailByHargaOrderId(long set_harga_order_id)
        {
            try
            {
                var result = await this._service.GetSetHargaOrderDetailByHargaOrderId(set_harga_order_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<set_harga_order_detail>(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpPost("{_id_supplier}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<set_harga_order_detail_berlaku>>))]
        [SwaggerOperation(summary: "untuk mendapatkan daftar barang yang sudah disetting harga order by id supplier dan dynamic filter",
            description: "prefix barang = b (kode_item,nama_item,id_grup_item,id_pabrik)")]
        public async Task<IActionResult> GetDetailBerlakuByIdSupplierAndParams(short _id_supplier, [FromBody] List<ParameterSearchModel> param)
        {
            try
            {
                var result = await this._service.GetAllSetHargaOrderBerlakuDetailByIdSupplierParams(_id_supplier,param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<set_harga_order_detail>(),
                    _message: "data tidak ditemukan",
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }
        #endregion
    }
}
