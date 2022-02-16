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
    public class SetupJenisSettingAuditStokOpnameController : ControllerBase
    {

        private ISetupJenisSettingAuditStokOpnameService _service;
        private readonly ILogger<SetupJenisSettingAuditStokOpnameController> _logger;

        public SetupJenisSettingAuditStokOpnameController(ISetupJenisSettingAuditStokOpnameService service,
        ILogger<SetupJenisSettingAuditStokOpnameController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data mm_setup_jenis_setting_audit_stok_opname")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_jenis_setting_audit_stok_opname_insert param)
        {
            try
            {
                var result = await this._service.AddMmSetupJenisSettingAuditStokOpname(param);
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
                        _propertyExisting: new[] { "nama_jenis_setting_stok_opname" }
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

    }
}
