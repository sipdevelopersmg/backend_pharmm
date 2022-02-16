using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Setup;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransAuditStokOpnameTanpaSettingController : ControllerBase
    {
        private readonly ITransAuditStokOpnameTanpaSettingService _service;
        private readonly ILogger<TransAuditStokOpnameTanpaSettingController> _logger;

        public TransAuditStokOpnameTanpaSettingController(
            ITransAuditStokOpnameTanpaSettingService service,
        ILogger<TransAuditStokOpnameTanpaSettingController> logger
        )
        {
            this._service = service;
            this._logger = logger;
        }

        #region Lookup

        [HttpGet("{audit_stok_opname_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_setting_audit_stok_opname_header_recursive>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_header by id")]
        public async Task<IActionResult> GetById(long audit_stok_opname_id)
        {

            try
            {
                var result = await this._service.GetTrAuditStokOpnameHeaderById(audit_stok_opname_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new tr_setting_audit_stok_opname_header_recursive(),
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

        [HttpPost("{id_stockroom}/{waktu_capture}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_setting_lookup_barang_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data barang by id rak, id stockroom, dan waktu capture",
            description: "prefix mm_setup_item msi (kode_item,nama_item,barcode), <br>" +
            "mm_setup_satuan mss (kode_satuan,nama_Satuan), <br> ")]
        public async Task<IActionResult> GetLookupBarangByStockroomAndWaktuCapture(
             [FromRoute] short id_stockroom,
             [FromRoute] DateTime waktu_capture,
             List<ParameterSearchModel> param
            )
        {

            try
            {
                var data = new tr_audit_stok_opname_no_setting_lookup_barang_param();
                data.id_stockroom = id_stockroom;
                data.waktu_capture = waktu_capture;
                data.param = param;

                var result = await this._service.GetLookupBarangByIdStockroomAndWaktuCapture(data);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_lookup_barang_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_setting_lookup_barang_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data batch barang by id item, id stockroom, dan waktu capture")]
        public async Task<IActionResult> GetAllBarangBatchByIdItemAndStockroomAndWaktuCapture(
            tr_audit_stok_opname_no_setting_lookup_barang_batch_param param
            )
        {

            try
            {
                var result = await this._service.GetLookupBatchByIdItemAndIdStockroomAndWaktuCapture(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_lookup_barang_batch>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_setting_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_setting_header by dynamic filter (gunakan array kosong [] untuk get all data)",
       description: "prefix tr_audit_stok_opname_no_setting_header tasoh (), <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_grup_item msgi (kode_grup_item,grup_item), <br>" +
            "mm_setup_audit_rak_storage msars (kode_rak_storage,nama_rak_storage)")]
        public async Task<IActionResult> GetAllStokOpnameByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrAuditStokOpnameHeaderByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_setting_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_setting_header yang masih open by dynamic filter (gunakan array kosong [] untuk get all data)",
       description: "prefix tr_audit_stok_opname_no_setting_header tasoh (), <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_grup_item msgi (kode_grup_item,grup_item), <br>" +
            "mm_setup_audit_rak_storage msars (kode_rak_storage,nama_rak_storage)")]
        public async Task<IActionResult> GetAllStokOpnameOpenByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrAuditStokOpnameHeaderOpenByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_setting_detail by id header dan waktu capture")]
        public async Task<IActionResult> GetDetailByHeaderIdAndWaktuCapture([FromBody] tr_audit_stok_opname_no_setting_detail_getby_headerid_and_waktu param)
        {

            try
            {
                var result = await this._service.GetTrAuditStokOpnameDetailByHeaderIdAndWaktuCapture(param);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_detail_for_adjustment_finalisasi>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_setting_detail_batch by detail id dan waktu capture")]
        public async Task<IActionResult> GetDetailBatchByDetailIdAndWaktuCapture([FromBody] tr_audit_stok_opname_no_setting_detail_batch_getby_detailid_and_waktu param)
        {

            try
            {
                var result = await this._service.GetTrAuditStokOpnameDetailBatchByDetailIdAndWaktuCapture(param);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_setting_detail_batch_for_adjustment_finalisasi>(),
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

        #endregion

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data audit stok opname")]
        public async Task<IActionResult> Insert([FromBody] tr_audit_stok_opname_no_setting_header_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.AddTrAuditStokOpnameHeader(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: 0,
                    _message: message
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk adjustment data audit stok opname")]
        public async Task<IActionResult> Adjustment([FromBody] tr_audit_stok_opname_no_setting_header_update_after_adjust param)
        {
            try
            {
                param.user_adj = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.Adjustment(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
                            _responseResult: true,
                            _message: "Data berhasil dilakukan adjustment"
                        ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: 0,
                    _message: message
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk adjustment data audit stok opname")]
        public async Task<IActionResult> Finalisasi([FromBody] tr_audit_stok_opname_no_setting_header_update_after_proses param)
        {
            try
            {
                param.user_proses = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.Finalisasi(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
                            _responseResult: true,
                            _message: "Data berhasil dilakukan finalisasi"
                        ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: 0,
                    _message: message
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
