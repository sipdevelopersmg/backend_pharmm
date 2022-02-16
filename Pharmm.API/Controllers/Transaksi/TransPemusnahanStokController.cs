using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services;
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
    public class TransPemusnahanStokController : ControllerBase
    {
        private ISetupStokItemService _stokItemService;
        private ITransPemusnahanStokService _service;
        private readonly ILogger<TransPemusnahanStokController> _logger;

        public TransPemusnahanStokController(
            ITransPemusnahanStokService service,
            ISetupStokItemService stokItemService,
            ILogger<TransPemusnahanStokController> logger)
        {
            this._logger = logger;
            this._service = service;
            this._stokItemService = stokItemService;
        }



        #region Lookup

        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang by id_stockroom",
            description: "prefix mm_setup_item_urai, <br>" +
            "mm_setup_stok_item_detail_batch batch (expired_date,barcode_batch_number,harga_satuan_netto,batch_number,qty_on_hand), <br>" +
            "mm_setup_item item (id_item,kode_item,nama_item,barcode), <br>" +
            "mm_setup_satuan mss (nama_satuan) ")]
        public async Task<IActionResult> GetLookupBarangByIdStockroom(
            [FromRoute] short id_stockroom,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemDetailBatchByIdStockroomAndParams(id_stockroom, param);

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

        #endregion

        #region Header

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemusnahan_stok>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemusnahan_stok by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix tr_pemusnahan_stok tps, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrPemusnahanStokByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemusnahan_stok>(),
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

        [HttpGet("{pemusnahan_stok_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_pemusnahan_stok>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemusnahan_stok by id")]
        public async Task<IActionResult> GetById(long pemusnahan_stok_id)
        {

            try
            {
                var result = await this._service.GetTrPemusnahanStokById(pemusnahan_stok_id);

                if (result != null)
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
        [ApiExplorerSettings(IgnoreApi = false)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<short?>))]
        [SwaggerOperation(summary: "untuk input data tr_pemusnahan_stok")]
        public async Task<IActionResult> Insert([FromBody] tr_pemusnahan_stok_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.AddTrPemusnahanStok(param);
                _logger.LogInformation("save data pemusnahan stok {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
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

        [HttpPut]
        [ApiExplorerSettings(IgnoreApi = false)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk validasi data tr_pemusnahan_stok")]
        public async Task<IActionResult> Validasi([FromBody] tr_pemusnahan_stok_update_to_validated param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.UpdateToValidated(param);
                _logger.LogInformation("validasi pemusnahan stok {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data berhasil divalidasi"));

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
        [ApiExplorerSettings(IgnoreApi = false)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk batalkan data tr_pemusnahan_stok")]
        public async Task<IActionResult> Batal([FromBody] tr_pemusnahan_stok_update_to_canceled param)
        {
            try
            {
                param.user_canceled = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.UpdateToCanceled(param);
                _logger.LogInformation("batalkan pemusnahan stok {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data berhasil dibatalkan"));

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

        [HttpGet("{pemusnahan_stok_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemusnahan_stok_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemusnahan_stok_detail by pemusnahan stok id")]
        public async Task<IActionResult> GetAllDetailByByHeaderId([FromRoute]long pemusnahan_stok_id)
        {

            try
            {
                var result = await this._service.GetTrPemusnahanStokDetailByHeaderId(pemusnahan_stok_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemusnahan_stok_detail>(),
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
