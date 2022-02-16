using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Services.Setup;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;


namespace Pharmm.API.Controllers.Setup
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class KartuStokitemController : ControllerBase
    {

        private IKartuStokItemService _service;
        private readonly ILogger<KartuStokitemController> _logger;

        public KartuStokitemController(IKartuStokItemService service,
        ILogger<KartuStokitemController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_kartu_stok_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_kartu_stok_item by dynamic filter (gunakan array kosong [] untuk get all data)", description: "prefix mm_kartu_stok_item = mksi")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmKartuStokItemByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_kartu_stok_item>(),
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

        [HttpPost("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_kartu_stok_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_kartu_stok_item by id item dan dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_kartu_stok_item = mksi (id_item,id_stockroom,tanggal)")]
        public async Task<IActionResult> GetAllByIdItemAndParams(
            [FromRoute] int id_item,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmKartuStokItemByIdItemAndParams(id_item, param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_kartu_stok_item>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_kartu_stok_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_kartu_stok_item")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmKartuStokItem();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_kartu_stok_item>(),
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


        #region Detail Batch


        [HttpGet("{id_kartu_stok_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_kartu_stok_item_detail_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_kartu_stok_item_detail_batch")]
        public async Task<IActionResult> GetDetailByIdKartuStok(long id_kartu_stok_item)
        {

            try
            {
                var result = await this._service.GetMmKartuStokItemDetailBatchByHeaderId(id_kartu_stok_item);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_kartu_stok_item_detail_batch>(),
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



        [HttpGet("{batch_number}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_kartu_stok_item_detail_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_kartu_stok_item_detail_batch")]
        public async Task<IActionResult> GetDetailByBatchNumber(string batch_number)
        {

            try
            {
                var result = await this._service.GetMmKartuStokItemDetailBatchByBatchNumber(batch_number);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_kartu_stok_item_detail_batch>(),
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
