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
    public class SetupAuditRakStorageController : ControllerBase
    {
        private ISetupItemService _itemService;
        private ISetupAuditRakStorageService _service;
        private readonly ILogger<SetupAuditRakStorageController> _logger;

        public SetupAuditRakStorageController(ISetupAuditRakStorageService service,
        ILogger<SetupAuditRakStorageController> logger,
        ISetupItemService itemService)
        {
            this._itemService = itemService;
            this._service = service;
            this._logger = logger;
        }

        #region Item

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_with_rak>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data barang belum punya rak")]
        public async Task<IActionResult> GetLookupAllBarangBelumRak(List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._itemService.GetMmSetupItemBelumRakParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item_with_rak>(),
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

        [HttpPost("{id_rak_storage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_with_rak>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data barang by id rak")]
        public async Task<IActionResult> GetLookupAllBarangByIdRak([FromRoute] int id_rak_storage,
            [FromBody]List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._itemService.GetMmSetupItemByIdRakParams(id_rak_storage,param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item_with_rak>(),
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


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> UpdateRakBarang([FromBody] mm_setup_item_update_rak_storage param)
        {
            try
            {
                param.user_set_rak_storage = (short)HttpContext.Items["userId"];
                var result = await this._itemService.UpdateRak(param);
                _logger.LogInformation("update rak barang {0}", param);

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

        [HttpDelete("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> HapusRakFromBarang([FromRoute]int id_item)
        {
            try
            {
                var result = await this._itemService.HapusRak(id_item);
                _logger.LogInformation("hapus rak dari barang {0}", id_item);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data sukses dihapus"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data gagal dihapus"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", id_item);
                throw;
            }
        }

        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_audit_rak_storage>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_rak_storage by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix mm_setup_audit_rak_storage msars (), <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_audit_penanggung_jawab_rak_storage msapjrs (nama_penanggung_jawab_rak_storage)")]
        public async Task<IActionResult> GetAllRakByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupAuditRakStorageByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_audit_rak_storage>(),
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

        [HttpGet("{id_rak_storage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<mm_setup_audit_rak_storage>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_rak_storage by id")]
        public async Task<IActionResult> GetRakById(int id_rak_storage)
        {

            try
            {
                var result = await this._service.GetMmSetupAuditRakStorageById(id_rak_storage);

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
        [SwaggerOperation(summary: "untuk input data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> InsertRak([FromBody] mm_setup_audit_rak_storage_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                param.user_set_penanggung_jawab_rak_storage = (short)HttpContext.Items["userId"];
                var result = await this._service.AddMmSetupAuditRakStorage(param);
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
                        _propertyExisting: new[] { "nama_rak_storage" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> UpdateRak([FromBody] mm_setup_audit_rak_storage_update param)
        {
            try
            {
                param.user_set_penanggung_jawab_rak_storage = (short)HttpContext.Items["userId"];
                var result = await this._service.UpdateMmSetupAuditRakStorage(param);
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
                        _propertyExisting: new[] { "nama_rak_storage" }
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

        [HttpDelete("{id_rak_storage}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> DeleteRak([FromRoute] int id_rak_storage)
        {

            try
            {
                var result = await this._service.DeleteMmSetupAuditRakStorage(id_rak_storage);
                _logger.LogInformation("delete data {0}", id_rak_storage);

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
                _logger.LogError(ex, "data {0}", id_rak_storage);
                throw;
            }
        }
    }
}
