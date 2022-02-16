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
    public class SetupAuditPenanggungJawabRakStorageController : ControllerBase
    {

        private ISetupAuditPenanggungJawabRakStorageService _service;
        private readonly ILogger<SetupAuditPenanggungJawabRakStorageController> _logger;

        public SetupAuditPenanggungJawabRakStorageController(ISetupAuditPenanggungJawabRakStorageService service,
        ILogger<SetupAuditPenanggungJawabRakStorageController> logger)
        {
            this._service = service;
            this._logger = logger;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_audit_penanggung_jawab_rak_storage>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_penanggung_jawab_rak_storage by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix mm_setup_audit_penanggung_jawab_rak_storage msapjrs (),<br>" +
            "mm_setup_supplier mss (kode_supplier, nama_supplier, alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupAuditPenanggungJawabRakStorageByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_audit_penanggung_jawab_rak_storage>(),
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

        [HttpGet("{id_penanggung_jawab_rak_storage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<mm_setup_audit_penanggung_jawab_rak_storage>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_penanggung_jawab_rak_storage by id")]
        public async Task<IActionResult> GetById(int id_penanggung_jawab_rak_storage)
        {

            try
            {
                var result = await this._service.GetMmSetupAuditPenanggungJawabRakStorageById(id_penanggung_jawab_rak_storage);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data mm_setup_audit_penanggung_jawab_rak_storage")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_audit_penanggung_jawab_rak_storage_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                var result = await this._service.AddMmSetupAuditPenanggungJawabRakStorage(param);
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
                        _propertyExisting: new[] { "field_existing" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_audit_penanggung_jawab_rak_storage")]
        public async Task<IActionResult> Update([FromBody] mm_setup_audit_penanggung_jawab_rak_storage_update param)
        {
            try
            {
                var result = await this._service.UpdateMmSetupAuditPenanggungJawabRakStorage(param);
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
                        _propertyExisting: new[] { "field_existing" }
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

        [HttpDelete("{id_penanggung_jawab_rak_storage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_audit_penanggung_jawab_rak_storage")]
        public async Task<IActionResult> Delete([FromRoute] int id_penanggung_jawab_rak_storage)
        {

            try
            {
                var result = await this._service.DeleteMmSetupAuditPenanggungJawabRakStorage(id_penanggung_jawab_rak_storage);
                _logger.LogInformation("delete data {0}", id_penanggung_jawab_rak_storage);

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
                _logger.LogError(ex, "data {0}", id_penanggung_jawab_rak_storage);
                throw;
            }
        }
    }
}
