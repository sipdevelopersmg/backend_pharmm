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
    public class SetupMekanismeReturController : ControllerBase
    {

        private ISetupMekanismeReturService _service;
        private readonly ILogger<SetupMekanismeReturController> _logger;

        public SetupMekanismeReturController(ISetupMekanismeReturService service,
        ILogger<SetupMekanismeReturController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_mekanisme_retur>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_mekanisme_retur")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupMekanismeRetur();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_mekanisme_retur>(),
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
    }
}
