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
    public class SetupGrupCoaController : ControllerBase
    {

        private ISetupGrupCoaService _service;
        private readonly ILogger<SetupGrupCoaController> _logger;

        public SetupGrupCoaController(ISetupGrupCoaService service,
        ILogger<SetupGrupCoaController> logger)
        {
            this._service = service;
            this._logger = logger;
        }



        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<akun_setup_grup_coa>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_grup_coa by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix akun_setup_grup_coa = asgc")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllAkunSetupGrupCoaByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<akun_setup_grup_coa>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<akun_setup_grup_coa>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_grup_coa")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllAkunSetupGrupCoa();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<akun_setup_grup_coa>(),
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

        [HttpGet("{id_grup_coa}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<akun_setup_grup_coa>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_grup_coa by id")]
        public async Task<IActionResult> GetById(Int16 id_grup_coa)
        {

            try
            {
                var result = await this._service.GetAkunSetupGrupCoaById(id_grup_coa);

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data akun_setup_grup_coa")]
        public async Task<IActionResult> Insert([FromBody] akun_setup_grup_coa_insert param)
        {
            try
            {
                var result = await this._service.AddAkunSetupGrupCoa(param);
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
                        _propertyExisting: new[] { "grup_coa" }
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
        [SwaggerOperation(summary: "untuk update data akun_setup_grup_coa")]
        public async Task<IActionResult> Update([FromBody] akun_setup_grup_coa param)
        {
            try
            {
                var result = await this._service.UpdateAkunSetupGrupCoa(param);
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
                        _propertyExisting: new[] { "grup_coa" }
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

        [HttpPut("{id_grup_coa}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data grup coa to active")]
        public async Task<IActionResult> UpdateToActive([FromRoute]short id_grup_coa)
        {
            try
            {
                var result = await this._service.UpdateToActiveAkunSetupGrupCoa(id_grup_coa);
                _logger.LogInformation("update status to active data {0}", id_grup_coa);

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
                _logger.LogError(ex, "data {0}", id_grup_coa);
                throw;
            }
        }


        [HttpPut("{id_grup_coa}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data grup coa to deactive")]
        public async Task<IActionResult> UpdateToDeActive([FromRoute] short id_grup_coa)
        {
            try
            {
                var result = await this._service.UpdateToDeActiveAkunSetupGrupCoa(id_grup_coa);
                _logger.LogInformation("update status to deactive data {0}", id_grup_coa);

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
                _logger.LogError(ex, "data {0}", id_grup_coa);
                throw;
            }
        }

        [HttpDelete("{id_grup_coa}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data akun_setup_grup_coa")]
        public async Task<IActionResult> Delete([FromRoute] Int16 id_grup_coa)
        {

            try
            {
                var result = await this._service.DeleteAkunSetupGrupCoa(id_grup_coa);
                _logger.LogInformation("delete data {0}", id_grup_coa);

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
                _logger.LogError(ex, "data {0}", id_grup_coa);
                throw;
            }
        }
    }
}
