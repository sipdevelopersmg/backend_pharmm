using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Services.Setup;
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
    public class SetupStokItemController : ControllerBase
    {
        private ISetupStokItemService _service;
        private readonly ILogger<SetupStokItemController> _logger;

        public SetupStokItemController(ISetupStokItemService service,
        ILogger<SetupStokItemController> logger)
        {
            this._service = service;
            this._logger = logger;
        }


        #region Header

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupStokItem();

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data mm_setup_stok_item")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_stok_item param)
        {
            try
            {
                var result = await this._service.AddMmSetupStokItem(param);
                _logger.LogInformation("save data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }
                else if (result == -1)
                {

                    return Ok(ResponseHelper.GetResponseDataExisting(
                        _data: 0,
                        _objectClass: param,
                        _propertyExisting: new[] { "id_item", "id_stockroom" }
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

        #endregion

        #region Detail

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item_detail_ed")]
        public async Task<IActionResult> GetAllDetailEd()
        {

            try
            {
                var result = await this._service.GetAllMmSetupStokItemDetailEd();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_ed>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_stok_item_detail_ed")]
        public async Task<IActionResult> InsertDetailEd([FromBody] mm_setup_stok_item_detail_ed param)
        {
            try
            {
                var result = await this._service.AddMmSetupStokItemDetailEd(param);
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

        #endregion

        #region Detail Batch

        [HttpGet("{id_stockroom}/{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item_detail_batch by id_item")]
        public async Task<IActionResult> GetAllDetailBatchByIdItemAndIdStockroom([FromRoute] int id_item,
            [FromRoute]short id_stockroom)
        {

            try
            {
                var result = await this._service.GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(id_stockroom,id_item);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch>(),
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


        [HttpGet("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item_detail_batch by id_item")]
        public async Task<IActionResult> GetAllDetailBatchByIdItem(int id_item)
        {

            try
            {
                var result = await this._service.GetAllMmSetupStokItemDetailBatchByIdItem(id_item);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item_detail_batch")]
        public async Task<IActionResult> GetAllDetailBatch()
        {

            try
            {
                var result = await this._service.GetAllMmSetupStokItemDetailBatch();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_stok_item_detail_batch")]
        public async Task<IActionResult> InsertDetailBatch([FromBody] mm_setup_stok_item_detail_batch_insert param)
        {
            try
            {
                var result = await this._service.AddMmSetupStokItemDetailBatch(param);
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
        [SwaggerOperation(summary: "untuk update data penambahan stok mm_setup_stok_item_detail_batch")]
        public async Task<IActionResult> UpdatePenambahanStokDetailBatch([FromBody] mm_setup_stok_item_detail_update_penambahan_stok param)
        {
            try
            {
                var result = await this._service.UpdatePenambahanStokDetailBatch(param);
                _logger.LogInformation("update data status to active {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data sukses diupdate"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data gagal diupdate"
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
        [SwaggerOperation(summary: "untuk update data pengurangan stok mm_setup_stok_item_detail_batch")]
        public async Task<IActionResult> UpdatePenguranganStokDetailBatch([FromBody] mm_setup_stok_item_detail_update_pengurangan_stok param)
        {
            try
            {
                var result = await this._service.UpdatePenguranganStokDetailBatch(param);
                _logger.LogInformation("update data status to deactive {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data sukses diupdate"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data gagal diupdate"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        #endregion
    }
}
