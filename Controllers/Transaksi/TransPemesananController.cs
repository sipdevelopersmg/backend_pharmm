using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using Utility.OKResponse.Helper;
using Pharmm.API.Services.Setup;
using Pharmm.API.Models.Setup;
using Pharmm.API.Helper;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransPemesananController : ControllerBase
    {
        private IMasterCounterService _kodeService;
        private ITransPemesananService _service;
        private readonly ILogger<TransPemesananController> _logger;

        public TransPemesananController(ITransPemesananService service,
        ILogger<TransPemesananController> logger,
        IMasterCounterService kodeService)
        {
            this._service = service;
            this._logger = logger;
            this._kodeService = kodeService;
        }


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk mendapatkan nomor pemesanan")]
        public async Task<IActionResult> GetNoPemesanan()
        {

            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePemesanan,
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_pemesanan = tp, <br> " +
            "mm_setup_stockroom = mss (kode_stockroom,nama_stockroom) , <br>" +
            "mm_setup_supplier = sup (kode_supplier,nama_supplier,alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrPemesananByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<tr_pemesanan>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllTrPemesanan();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan>(),
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

        [HttpGet("{pemesanan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_pemesanan>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan by id")]
        public async Task<IActionResult> GetById(long pemesanan_id)
        {

            try
            {
                var result = await this._service.GetTrPemesananById(pemesanan_id);

                if (result is not null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
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


        [HttpPost("{id_supplier}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan_lookup_barang>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data barang by id supplier", description: "prefixnya brg ")]
        public async Task<IActionResult> GetLookupBarangBelumPoByIdSupplier([FromRoute]short id_supplier,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetLookupBarangBelumPoActiveByIdSupplierAndParams(id_supplier,param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan_lookup_barang>(),
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
        [SwaggerOperation(summary: "untuk input data tr_pemesanan")]
        public async Task<IActionResult> Insert([FromBody] tr_pemesanan_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];

                (bool result,string message) = await this._service.AddTrPemesanan(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: 1,
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> CekStatusPemesanan(long? pemesanan_id)
        {
            string status = "";

            var paramCekData = new List<ParameterSearchModel>
                {
                    new ParameterSearchModel
                    {
                        columnName = "pemesanan_id",
                        filter = "equal",
                        searchText = pemesanan_id.ToString(),
                        searchText2 = ""
                    }

                };

            //cek apakah ada Data pemesanan sudah closed atau sudah canceled
            var datatoValidate = await this._service.GetAllTrPemesananByParams(paramCekData);

            if (!string.IsNullOrEmpty(datatoValidate.First().user_closed.ToString()))
            {
                status = "closed";
            }
            else if (!string.IsNullOrEmpty(datatoValidate.First().user_canceled.ToString()))
            {

                status = "canceled";
            }
            else if (!string.IsNullOrEmpty(datatoValidate.First().user_validated.ToString()))
            {

                status = "validated";
            }


            //jika status = "" maka belum di apa2kan (closed,validated,atau canceled)

            return status;
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data tr_pemesanan")]
        public async Task<IActionResult> UpdateToValidated([FromBody] tr_pemesanan_update_status_to_validated param)
        {
            try
            {
                bool result = false;
                string message = "";

                param.user_validated = (short) HttpContext.Items["userId"];

                var isAllowed = await this.CekStatusPemesanan(param.pemesanan_id);

                if (string.IsNullOrEmpty(isAllowed))
                {
                    (bool resultService,string messageService) = await this._service.UpdateToValidatedTrPemesanan(param);
                    result = resultService;
                    message = messageService;

                }else if(isAllowed == "validated")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah divalidasi"
                    ));
                }
                else if (isAllowed == "closed")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah diselesaikan"
                    ));
                }
                else if (isAllowed == "canceled")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah dibatalkan"
                    ));
                }

                _logger.LogInformation("update data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data pemesanan berhasil divalidasi"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: message
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data tr_pemesanan")]
        public async Task<IActionResult> UpdateToClosed([FromBody] tr_pemesanan_update_status_to_closed param)
        {
            try
            {
                var result = 0;

                param.user_closed = (short)HttpContext.Items["userId"];

                var isAllowed = await this.CekStatusPemesanan(param.pemesanan_id);

                if (string.IsNullOrEmpty(isAllowed))
                {
                    result = await this._service.UpdateToClosedTrPemesanan(param);
                }
                else if (isAllowed == "validated")
                {
                    result = await this._service.UpdateToClosedTrPemesanan(param);
                }
                else if (isAllowed == "closed")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah diselesaikan"
                    ));
                }
                else if (isAllowed == "canceled")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah dibatalkan"
                    ));
                }

                _logger.LogInformation("update data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data pemesanan berhasil diselesaikan"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data pemesanan gagal diselesaikan"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data tr_pemesanan")]
        public async Task<IActionResult> UpdateToCanceled([FromBody] tr_pemesanan_update_status_to_canceled param)
        {
            try
            {
                var result = 0;

                param.user_canceled = (short)HttpContext.Items["userId"];

                var isAllowed = await this.CekStatusPemesanan(param.pemesanan_id);

                if (string.IsNullOrEmpty(isAllowed))
                {
                    result = await this._service.UpdateToCanceledTrPemesanan(param);
                }
                else if (isAllowed == "validated")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah divalidasi"
                    ));
                }
                else if (isAllowed == "closed")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah diselesaikan"
                    ));
                }
                else if (isAllowed == "canceled")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data pemesanan sudah dibatalkan"
                    ));
                }

                _logger.LogInformation("update data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data pemesanan berhasil dibatalkan"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data pemesanan gagal dibatalkan"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }


        #region Detail

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan_detail")]
        public async Task<IActionResult> GetAllDetail()
        {

            try
            {
                var result = await this._service.GetAllTrPemesananDetail();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan_detail>(),
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

        [HttpGet("{pemesanan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan_detail by id")]
        public async Task<IActionResult> GetDetailByPemesananId(long pemesanan_id)
        {

            try
            {
                var result = await this._service.GetTrPemesananDetailByPemesananId(pemesanan_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan_detail>(),
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

        [HttpPost("{pemesanan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemesanan_detail by pemesanan_id dan by params",
            description:"prefix trans_pemesanan_detail tpd, <br>" +
            "mm_setup_item msi (kode_item,barcode,nama_item), <br> " +
            "mm_setup_satuan mss (nama_satuan)")]
        public async Task<IActionResult> GetDetailByParamsAndId([FromRoute] long pemesanan_id, [FromBody] List<ParameterSearchModel> param)
        {

            try
            {

                var result = await this._service.GetAllTrPemesananDetailByPemesananIdAndParams(pemesanan_id, param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan_detail>(),
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
