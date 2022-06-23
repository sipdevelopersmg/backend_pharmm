using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Controllers
{
    [ApiController]
    [Route("[action]")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> health()
        {
            try
            {
                return Ok("Healthy");
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
