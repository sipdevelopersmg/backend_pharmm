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
    public class SetupTipeStockroomController : ControllerBase
    {

        private ISetupTipeStockroomService _service;
        private readonly ILogger<SetupTipeStockroomController> _logger;

        public SetupTipeStockroomController(ISetupTipeStockroomService service,
        ILogger<SetupTipeStockroomController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_tipe_stockroom>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_tipe_stockroom")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupTipeStockroom();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_tipe_stockroom>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_tipe_stockroom")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_tipe_stockroom_insert param)
        {
            try
            {
                var result = await this._service.AddMmSetupTipeStockroom(param);
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
                        _propertyExisting: new[] { "tipe_stockroom" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_tipe_stockroom")]
        public async Task<IActionResult> Update([FromBody] mm_setup_tipe_stockroom param)
        {
            try
            {
                var result = await this._service.UpdateMmSetupTipeStockroom(param);
                _logger.LogInformation("update data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data sukses diupdate"));

                }
                else if (result == -1)
                {

                    return Ok(ResponseHelper.GetResponseDataExisting(
                        _data: "",
                        _objectClass: param,
                        _propertyExisting: new[] { "tipe_stockroom" }
                        ));
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

        [HttpDelete("{id_tipe_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_tipe_stockroom")]
        public async Task<IActionResult> Delete([FromRoute] Int16 id_tipe_stockroom)
        {

            try
            {
                var result = await this._service.DeleteMmSetupTipeStockroom(id_tipe_stockroom);
                _logger.LogInformation("delete data {0}", id_tipe_stockroom);

                if (result == 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data gagal dihapus"
                    ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _responseResult: true,
                    _message: "Data berhasil dihapus"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", id_tipe_stockroom);
                throw;
            }
        }
    }
}
