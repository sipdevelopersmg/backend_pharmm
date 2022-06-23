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
    public class LaporanReturPembelianController : ControllerBase
    {
        private readonly ILaporanReturPembelianService _service;
        private readonly ILogger<LaporanReturPembelianController> _logger;

        public LaporanReturPembelianController(ILaporanReturPembelianService service,
             ILogger<LaporanReturPembelianController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<laporan_retur_pembelian_by_periode>>))]
        [SwaggerOperation(summary: "untuk laporan retur pembelian per periode dan gudang")]
        public async Task<IActionResult> GeneratePerPeriodeDanGudang([FromBody] param_laporan_retur_pembelian_by_periode param)
        {

            try
            {
                var result = await this._service.GetLaporanReturPembelianPerPeriodeAndGudang(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<laporan_retur_pembelian_by_periode>(),
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
