using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Laporan;
using Pharmm.API.Services.Laporan;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;


namespace Pharmm.API.Controllers.Laporan
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LaporanRepackingController : ControllerBase
    {
        private readonly ILaporanRepackingService _service;
        private readonly ILogger<LaporanRepackingController> _logger;

        public LaporanRepackingController(ILaporanRepackingService service,
             ILogger<LaporanRepackingController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<laporan_repacking_per_faktur>>))]
        [SwaggerOperation(summary: "untuk laporan repacking per faktur")]
        public async Task<IActionResult> GeneratePerFaktur([FromBody] param_laporan_repacking_per_faktur param)
        {

            try
            {
                var result = await this._service.GetLaporanRepackingPerFaktur(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<laporan_repacking_per_faktur>(),
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
