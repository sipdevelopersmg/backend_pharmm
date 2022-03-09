using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.JwtHelper.Helper;
using Utility.OKResponse.Helper;

namespace Pharmm.API.Controllers.Laporan
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LaporanConfigController : ControllerBase
    {
        public readonly IConfiguration _config;

        public LaporanConfigController(IConfiguration config)
        {
            this._config = config;
        }


        [HttpGet]
        public async Task<IActionResult> Authentication()
        {
            var token = JwtHelper.generateJwtToken(7, this._config.GetValue<string>("JwtSettings:Secret"));

            return Ok(
                ResponseHelper.GetResponse(
                    _data:token,
                    _responseResult:true
                    ));
        }

    }
}
