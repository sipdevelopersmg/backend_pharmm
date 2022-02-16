using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Setup;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransReturPembelianController : ControllerBase
    {

        private ITransReturPembelianService _service;
        private IMasterCounterService _kodeService;
        private ISetupStokItemService _stokItemService;
        private readonly ILogger<TransReturPembelianController> _logger;

        public TransReturPembelianController(ITransReturPembelianService service,
        ILogger<TransReturPembelianController> logger,
            ISetupStokItemService stokItemService,
        IMasterCounterService kodeService)
        {
            this._service = service;
            this._stokItemService = stokItemService;
            this._kodeService = kodeService;
            this._logger = logger;
        }

        #region Lookup


        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang ed", 
            description: "prefix mm_setup_stok_item_detail_batch = batch, <br>" +
            "mm_setup_item = item")]
        public async Task<IActionResult> GetLookupBarangEDByIdStockroom([FromRoute] short id_stockroom, [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(id_stockroom, param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch_lookup>(),
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


        [HttpPost("{id_stockroom}/{barcode_batch_number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang ed",
            description: "prefix mm_setup_stok_item_detail_batch = batch, <br>" +
            "mm_setup_item = item")]
        public async Task<IActionResult> GetLookupBarangEDByIdStockroomAndBarcodeBatch(
            [FromRoute] short id_stockroom, 
            [FromRoute] string barcode_batch_number,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemDetailBatchByIdStockroomAndBarcodeBatchNumberAndParams(id_stockroom, barcode_batch_number, param);

                if (result is not null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch_lookup>(),
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

        #region Header

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pembelian = trp, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_mekanisme_retur msmr (mekanisme_retur), <br>" +
            "mm_setup_supplier sup (kode_supplier,nama_supplier,alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrReturPembelianByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pembelian>(),
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

        [HttpGet("{retur_pembelian_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_retur_pembelian>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian by id")]
        public async Task<IActionResult> GetById(long retur_pembelian_id)
        {

            try
            {
                var result = await this._service.GetTrReturPembelianById(retur_pembelian_id);

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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk mendapatkan nomor retur pembelian")]
        public async Task<IActionResult> GetNoReturPembelian()
        {

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
        [SwaggerOperation(summary: "untuk input data tr_retur_pembelian")]
        public async Task<IActionResult> Insert([FromBody] tr_retur_pembelian_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                var result = await this._service.AddTrReturPembelian(param);
                _logger.LogInformation("save data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: 0,
                    _message: "Data gagal disimpan"
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
        [SwaggerOperation(summary: "untuk update data tr_retur_pembelian")]
        public async Task<IActionResult> UpdateToValidated([FromBody] tr_retur_pembelian_update_status_to_validated param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];

                (bool result, string message) = await this._service.UpdateToValidated(param);


                _logger.LogInformation("update data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data retur pembelian berhasil divalidasi"
                        ));

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
        [SwaggerOperation(summary: "untuk update data tr_penerimaan")]
        public async Task<IActionResult> UpdateToCanceled([FromBody] tr_retur_pembelian_update_status_to_canceled param)
        {
            try
            {

                param.user_canceled = (short)HttpContext.Items["userId"];

                (bool result,string message) = await this._service.UpdateToCanceled(param);

                _logger.LogInformation("update data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data penerimaan berhasil dibatalkan"));

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

        #endregion


        #region Detail

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_detail by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pembelian_detail = trpd, <br>" +
            "mm_setup_item msi (kode_item,barcode,nama_item)")]
        public async Task<IActionResult> GetAllDetailByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrReturPembelianDetailByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pembelian_detail>(),
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

        [HttpGet("{retur_pembelian_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_detail by retur_pembelian_id")]
        public async Task<IActionResult> GetDetailByReturPembelianId(long retur_pembelian_id)
        {

            try
            {
                var result = await this._service.GetTrReturPembelianDetailByReturPembelianId(retur_pembelian_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pembelian_detail_penukaran>(),
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


        #region Detail Penukaran

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_detail_penukaran>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_detail_penukaran by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pembelian_detail_penukaran = trpdp")]
        public async Task<IActionResult> GetAllDetailPenukaranByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrReturPembelianDetailPenukaranByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pembelian_detail_penukaran>(),
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

        [HttpGet("{retur_pembelian_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_detail_penukaran>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_detail_penukaran by retur_pembelian_id")]
        public async Task<IActionResult> GetDetailPenukaranByReturPembelianId(long retur_pembelian_id)
        {

            try
            {
                var result = await this._service.GetTrReturPembelianDetailPenukaranByReturPembelianId(retur_pembelian_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pembelian_detail_penukaran>(),
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
