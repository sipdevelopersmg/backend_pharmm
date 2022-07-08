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
    public class SetupCoaController : ControllerBase
    {

        private ISetupCoaService _service;
        private readonly ILogger<SetupCoaController> _logger;

        public SetupCoaController(ISetupCoaService service,
        ILogger<SetupCoaController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<akun_setup_coa>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_coa by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix akun_setup_coa = asc, field = id_coa_parent, id_grup_coa, kode_coa, deskripsi, is_active, user_created ")]
        public async Task<IActionResult> GetAllByParamsTriasGanteng([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllAkunSetupCoaByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<akun_setup_coa>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<akun_setup_coa>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_coa by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix akun_setup_coa = asc, field = id_coa_parent, id_grup_coa, kode_coa, deskripsi, is_active, user_created ")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllAkunSetupCoaByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<akun_setup_coa>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<akun_setup_coa>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_coa")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllAkunSetupCoa();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<akun_setup_coa>(),
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

        [HttpGet("{id_coa}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<akun_setup_coa>))]
        [SwaggerOperation(summary: "untuk mendapatkan data akun_setup_coa by id")]
        public async Task<IActionResult> GetById(int id_coa)
        {

            try
            {
                var result = await this._service.GetAkunSetupCoaById(id_coa);

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
        [SwaggerOperation(summary: "untuk input data akun_setup_coa")]
        public async Task<IActionResult> Insert([FromBody] akun_setup_coa_insert param)
        {
            try
            {
                var result = await this._service.AddAkunSetupCoa(param);
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
                        _propertyExisting: new[] { "kode_coa" }
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
        [SwaggerOperation(summary: "untuk update data akun_setup_coa")]
        public async Task<IActionResult> Update([FromBody] akun_setup_coa_update param)
        {
            try
            {
                var result = await this._service.UpdateAkunSetupCoa(param);
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
                        _propertyExisting: new[] { "kode_coa"}
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data to active akun_setup_coa")]
        public async Task<IActionResult> UpdateToActive([FromBody] akun_setup_coa_update_status_to_active param)
        {
            try
            {
                var result = await this._service.UpdateToActiveAkunSetupCoa(param);
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
        [SwaggerOperation(summary: "untuk update data to deactive akun_setup_coa")]
        public async Task<IActionResult> UpdateToDeActive([FromBody] akun_setup_coa_update_status_to_deactive param)
        {
            try
            {
                param.user_deactived = (short)HttpContext.Items["userId"];

                var result = await this._service.UpdateToDeActiveAkunSetupCoa(param);
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

    }
}
