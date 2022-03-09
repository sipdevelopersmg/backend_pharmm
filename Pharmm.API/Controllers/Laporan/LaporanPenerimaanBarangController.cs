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
    public class LaporanPenerimaanBarangController : ControllerBase
    {
        private readonly ILaporanPenerimaanBarangService _service;
        private readonly ILogger<LaporanPenerimaanBarangController> _logger;

        public LaporanPenerimaanBarangController(ILaporanPenerimaanBarangService service,
             ILogger<LaporanPenerimaanBarangController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<laporan_penerimaan_per_vendor>>))]
        [SwaggerOperation(summary: "untuk laporan penerimaan barang per vendor")]
        public async Task<IActionResult> GeneratePerVendor([FromBody] param_laporan_penerimaan_per_vendor param)
        {

            try
            {
                var result = await this._service.GetLaporanPenerimaanBarangPerVendor(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<laporan_penerimaan_per_vendor>(),
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
