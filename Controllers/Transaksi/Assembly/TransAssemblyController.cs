using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services;
using Pharmm.API.Services.Setup;
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
    public class TransAssemblyController : ControllerBase
    {
        private ISetupStokItemService _stokItemService;
        private ITransAssemblyService _service;
        private readonly ILogger<TransAssemblyController> _logger;

        public TransAssemblyController(
            ITransAssemblyService service,
            ISetupStokItemService stokItemService,
            ILogger<TransAssemblyController> logger)
        {
            this._logger = logger;
            this._service = service;
            this._stokItemService = stokItemService;
        }

        #region Lookup

        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang by id_stockroom",
            description: "prefix mm_setup_item msi (kode_item,nama_item,barcode, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom))")]
        public async Task<IActionResult> GetLookupBarangByIdStockroom(
            [FromRoute] short id_stockroom,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemByIdStockroomAndParams(id_stockroom, param);

                if (result is not null)
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


        [HttpPost("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch_lookup_assembly>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item_assembly by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_setup_item_assembly, <br>" +
            "mm_setup_stok_item_detail_batch batch (expired_date,barcode_batch_number,harga_satuan_netto,batch_number,qty_on_hand), <br>" +
            "mm_setup_item item (id_item,kode_item,nama_item,barcode,) ")]
        public async Task<IActionResult> GetDetailAssemblyByHeaderIdAndParams(
            int id_item,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllStokItemAssemblyDetailBatchByHeaderIdAndParams(id_item, param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch_lookup_assembly>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_assembly>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_assembly by dynamic filter (gunakan array kosong [] untuk get all data)", description: "prefix tr_assembly = tr")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrAssemblyByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_assembly>(),
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

        [HttpGet("{assembly_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_assembly>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_assembly by id")]
        public async Task<IActionResult> GetById(long assembly_id)
        {

            try
            {
                var result = await this._service.GetTrAssemblyById(assembly_id);

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
        [SwaggerOperation(summary: "untuk input data tr_assembly")]
        public async Task<IActionResult> Insert([FromBody] tr_assembly_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.AddTrAssembly(param);
                _logger.LogInformation("save data assembly {0}", param);

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
        [SwaggerOperation(summary: "untuk validasi data tr_assembly")]
        public async Task<IActionResult> Validasi([FromBody] tr_assembly_update_to_validated param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.UpdateTrAssemblyValidated(param);
                _logger.LogInformation("validasi assembly {0}", param);

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
        [SwaggerOperation(summary: "untuk batalkan data tr_assembly")]
        public async Task<IActionResult> Batal([FromBody] tr_assembly_update_to_canceled param)
        {
            try
            {
                param.user_canceled = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.UpdateTrAssemblyCanceled(param);
                _logger.LogInformation("batalkan assembly {0}", param);

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

        [HttpGet("{assembly_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_assembly_detail>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_assembly_detail by id")]
        public async Task<IActionResult> GetDetailByHeaderId(long assembly_id)
        {

            try
            {
                var result = await this._service.GetTrAssemblyDetailByHeaderId(assembly_id);

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


        #endregion
    }
}
