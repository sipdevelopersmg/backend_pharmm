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
    public class LaporanBuktiMutasiController : ControllerBase
    {
        private readonly ILaporanBuktiMutasiService _service;
        private readonly ILogger<LaporanBuktiMutasiController> _logger;

        public LaporanBuktiMutasiController(ILaporanBuktiMutasiService service,
             ILogger<LaporanBuktiMutasiController> logger)
        {
            this._service = service;
            this._logger = logger;
        }


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<laporan_bukti_mutasi_per_faktur>>))]
        [SwaggerOperation(summary: "untuk laporan bukti mutasi per faktur")]
        public async Task<IActionResult> GenerateByFaktur([FromBody] param_bukti_mutasi param)
        {

            try
            {
                var result = await this._service.GetLaporanBuktiMutasiByFaktur(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<laporan_bukti_mutasi_per_faktur>(),
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


        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<laporan_permintaan_mutasi_per_faktur>>))]
        [SwaggerOperation(summary: "untuk laporan bukti permintaan mutasi per faktur")]
        public async Task<IActionResult> GeneratePermintaanMutasiByFaktur([FromBody] param_permintaan_mutasi param)
        {

            try
            {
                var result = await this._service.GetLaporanPermintaanMutasiByFaktur(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<laporan_permintaan_mutasi_per_faktur>(),
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
