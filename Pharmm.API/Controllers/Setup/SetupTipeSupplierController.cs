﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using Pharmm.API.Services;
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
    public class SetupTipeSupplierController : ControllerBase
    {

        private ISetupTipeSupplierService _service;
        private readonly ILogger<SetupTipeSupplierController> _logger;

        public SetupTipeSupplierController(ISetupTipeSupplierService service,
        ILogger<SetupTipeSupplierController> logger)
        {
            this._service = service;
            this._logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_tipe_supplier>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_tipe_supplier")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupTipeSupplier();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_tipe_supplier>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_tipe_supplier")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_tipe_supplier_insert param)
        {
            try
            {
                var result = await this._service.AddMmSetupTipeSupplier(param);
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
                        _propertyExisting: new[] { "tipe_supplier" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_tipe_supplier")]
        public async Task<IActionResult> Update([FromBody] mm_setup_tipe_supplier_update param)
        {
            try
            {
                var result = await this._service.UpdateMmSetupTipeSupplier(param);
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
                        _propertyExisting: new[] { "tipe_supplier" }
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data to active mm_setup_tipe_supplier")]
        public async Task<IActionResult> UpdateToActive([FromBody] mm_setup_tipe_supplier_update_status_to_active param)
        {
            try
            {
                var result = await this._service.UpdateToActiveMmSetupTipeSupplier(param);
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
        [SwaggerOperation(summary: "untuk update data to deactive mm_setup_tipe_supplier")]
        public async Task<IActionResult> UpdateToDeActive([FromBody] mm_setup_tipe_supplier_update_status_to_deactive param)
        {
            try
            {
                var result = await this._service.UpdateToDeActiveMmSetupTipeSupplier(param);
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
