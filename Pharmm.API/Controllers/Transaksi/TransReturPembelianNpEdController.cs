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
    public class TransReturPembelianNoEdController : ControllerBase
    {

        private ITransReturPembelianNoEdService _service;
        private IMasterCounterService _kodeService;
        private ISetupStokItemService _stokItemService;
        private readonly ILogger<TransReturPembelianNoEdController> _logger;

        public TransReturPembelianNoEdController(ITransReturPembelianNoEdService service,
        ILogger<TransReturPembelianNoEdController> logger,
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang ed", 
            description: "prefix mm_setup_stok_item_detail_batch = batch, <br>" +
            "mm_setup_item = item")]
        public async Task<IActionResult> GetLookupBarangByIdStockroom([FromRoute] short id_stockroom, [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemByIdStockroomAndParams(id_stockroom, param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_lookup>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_no_ed by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pembelian_no_ed = trp, <br>" +
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
                    _data: new List<tr_retur_pembelian_no_ed>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_retur_pembelian_no_ed>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_no_ed by id")]
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data tr_retur_pembelian_no_ed")]
        public async Task<IActionResult> Insert([FromBody] tr_retur_pembelian_no_ed_insert param)
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
        [SwaggerOperation(summary: "untuk update data tr_retur_pembelian_no_ed")]
        public async Task<IActionResult> UpdateToValidated([FromBody] tr_retur_pembelian_no_ed_update_status_to_validated param)
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
        public async Task<IActionResult> UpdateToCanceled([FromBody] tr_retur_pembelian_no_ed_update_status_to_canceled param)
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

        [HttpGet("{retur_pembelian_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pembelian_no_ed_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pembelian_no_ed_detail by retur_pembelian_id")]
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
                    _data: new List<tr_retur_pembelian_no_ed_detail_penukaran>(),
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
