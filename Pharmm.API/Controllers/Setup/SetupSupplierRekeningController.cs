using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using Pharmm.API.Services;
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
    public class SetupSupplierRekeningController : ControllerBase
    {

        private ISetupSupplierRekeningService _service;
        private readonly ILogger<SetupSupplierRekeningController> _logger;

        public SetupSupplierRekeningController(ISetupSupplierRekeningService service,
        ILogger<SetupSupplierRekeningController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_supplier_rekening>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_supplier_rekening by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix mm_setup_supplier_rekening = mssr (id_supplier, bank, nomor_rekening, nama_rekening), <br> " +
            "mm_setup_supplier mss (kode_supplier,nama_supplier, alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupSupplierRekeningByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_supplier_rekening>(),
                         _responseResult: true,
                         _message: "data tidak ditemukan"
                         ));
                }

                return Ok(ResponseHelper.GetResponse(
                    _data: result,
                    _responseResult: true
                ));

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                throw;
            }
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_supplier_rekening>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_supplier_rekening")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupSupplierRekening();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_supplier_rekening>(),
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

        [HttpGet("{id_supplier_rekening}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<mm_setup_supplier_rekening>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_supplier_rekening by id")]
        public async Task<IActionResult> GetById(Int16 id_supplier_rekening)
        {

            try
            {
                var result = await this._service.GetMmSetupSupplierRekeningById(id_supplier_rekening);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result[0],
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
        [SwaggerOperation(summary: "untuk input data mm_setup_supplier_rekening")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_supplier_rekening_insert param)
        {
            try
            {
                param.user_created = (short)HttpContext.Items["userId"];

                var result = await this._service.AddMmSetupSupplierRekening(param);
                _logger.LogInformation("save data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
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
        [SwaggerOperation(summary: "untuk update data mm_setup_supplier_rekening")]
        public async Task<IActionResult> Update([FromBody] mm_setup_supplier_rekening_update param)
        {
            try
            {

                var result = await this._service.UpdateMmSetupSupplierRekening(param);
                _logger.LogInformation("update data {0}", param);

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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data to active mm_setup_supplier_rekening")]
        public async Task<IActionResult> UpdateToActive([FromBody] mm_setup_supplier_rekening_update_status_to_active param)
        {
            try
            {
                var result = await this._service.UpdateToActiveMmSetupSupplierRekening(param);
                _logger.LogInformation("update data status to active {0}", param);

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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data to deactive mm_setup_supplier_rekening")]
        public async Task<IActionResult> UpdateToDeActive([FromBody] mm_setup_supplier_rekening_update_status_to_deactive param)
        {
            try
            {
                param.user_deactived = (short)HttpContext.Items["userId"];

                var result = await this._service.UpdateToDeActiveMmSetupSupplierRekening(param);
                _logger.LogInformation("update data status to deactive {0}", param);

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

    }
}
