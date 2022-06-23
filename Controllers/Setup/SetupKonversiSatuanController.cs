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
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupKonversiSatuanController : ControllerBase
    {

        private ISetupKonversiSatuanService _service;
        private readonly ILogger<SetupKonversiSatuanController> _logger;

        public SetupKonversiSatuanController(ISetupKonversiSatuanService service,
        ILogger<SetupKonversiSatuanController> logger)
        {
            this._service = service;
            this._logger = logger;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_konversi_satuan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_konversi_satuan by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix mm_setup_konversi_satuan = msks (kode_satuan_besar, kode_satuan_kecil, user_created), <br>" +
            "mm_setup_satuan satuan_besar (nama_satuan), <== nama field dari nama_satuan_besar <br>" +
            "mm_setup_satuan satuan_kecil (nama_satuan) <== nama field dari nama_satuan_kecil ")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupKonversiSatuanByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_konversi_satuan>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_konversi_satuan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_konversi_satuan")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupKonversiSatuan();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_konversi_satuan>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_konversi_satuan")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_konversi_satuan_insert param)
        {
            try
            {
                param.user_created = (short)HttpContext.Items["userId"];

                var result = await this._service.AddMmSetupKonversiSatuan(param);
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
                        _propertyExisting: new[] { "kode_satuan_besar", "kode_satuan_kecil" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_konversi_satuan")]
        public async Task<IActionResult> Update([FromBody] mm_setup_konversi_satuan_update param)
        {
            try
            {
                var result = await this._service.UpdateMmSetupKonversiSatuan(param);
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
                        _propertyExisting: new[] { "kode_satuan_besar", "kode_satuan_kecil" }
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

        [HttpDelete("{id_konversi_satuan}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_konversi_satuan")]
        public async Task<IActionResult> Delete([FromRoute] int id_konversi_satuan)
        {

            try
            {
                var result = await this._service.DeleteMmSetupKonversiSatuan(id_konversi_satuan);
                _logger.LogInformation("delete data {0}", id_konversi_satuan);

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
                _logger.LogError(ex, "data {0}", id_konversi_satuan);
                throw;
            }
        }
    }
}
