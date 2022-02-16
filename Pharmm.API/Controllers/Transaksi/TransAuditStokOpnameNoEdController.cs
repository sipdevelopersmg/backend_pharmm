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
    public class TransAuditStokOpnameNoEdController : ControllerBase
    {
        private readonly ISetupAuditRakStorageService _rakService;
        private readonly ITransSettingAuditStokOpnameService _settingService;
        private readonly ITransAuditStokOpnameNoEdService _service;
        private readonly ILogger<TransAuditStokOpnameNoEdController> _logger;

        public TransAuditStokOpnameNoEdController(
            ITransAuditStokOpnameNoEdService service,
        ILogger<TransAuditStokOpnameNoEdController> logger,
        ITransSettingAuditStokOpnameService settingService,
        ISetupAuditRakStorageService rakService
        )
        {
            this._service = service;
            this._logger = logger;
            this._rakService = rakService;
            this._settingService = settingService;
        }



        #region Lookup

        [HttpGet("{audit_stok_opname_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_audit_stok_opname_no_ed_header_recursive>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_ed_header by id")]
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
                    _data: new tr_audit_stok_opname_no_ed_header_recursive(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_setting_audit_stok_opname_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_setting_audit_stok_opname_header by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix tr_setting_audit_stok_opname_header tsasoh (), <br>" +
            "mm_setup_jenis_setting_audit_stok_opname msjsaso (kode_jenis_setting_stok_opname,nama_jenis_setting_stok_opname)")]
        public async Task<IActionResult> GetAllSettingStokOpnameByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._settingService.GetAllTrSettingAuditStokOpnameHeaderByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_setting_audit_stok_opname_header>(),
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

        [HttpGet("{setting_stok_opname_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_setting_audit_stok_opname_detail_stockroom>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data warehouse by setting stok opname id")]
        public async Task<IActionResult> GetAllStockroomBySettingStokOpnameId(
            long setting_stok_opname_id
            )
        {

            try
            {
                var result = await this._settingService.GetAllTrSettingAuditStokOpnameDetailStockroomBySettingStokOpnameId(setting_stok_opname_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_setting_audit_stok_opname_detail_stockroom>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_audit_rak_storage_for_stok_opname>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_rak_storage")]
        public async Task<IActionResult> GetAllRakByIdStockroomAndSettingStokOpnameId(
            mm_setup_audit_rak_storage_getby_settingid_idstockroom param
            )
        {

            try
            {
                var result = new List<mm_setup_audit_rak_storage_for_stok_opname>();

                short id_jenis_setting_stok_opname = (short)0;

                var dataSettingStokOpname = await this._settingService.GetTrSettingAuditStokOpnameHeaderById(param.setting_stok_opname_id);

                if (dataSettingStokOpname is not null)
                {
                    id_jenis_setting_stok_opname = dataSettingStokOpname.id_jenis_setting_stok_opname;

                    switch (id_jenis_setting_stok_opname)
                    {
                        case 1: result = await this._rakService.GetAllMmSetupAuditRakStorageAllItemByIdStockroom(param.id_stockroom); break;
                        case 2: result = await this._rakService.GetAllMmSetupAuditRakStoragePerGrupByIdStockroomAndSettingStokOpnameId(param.id_stockroom, param.setting_stok_opname_id); break;
                        case 3: result = await this._rakService.GetAllMmSetupAuditRakStoragePerRakByIdStockroomAndSettingStokOpnameId(param.id_stockroom, param.setting_stok_opname_id); break;
                        case 4: result = await this._rakService.GetAllMmSetupAuditRakStoragePerItemByIdStockroomAndSettingStokOpnameId(param.id_stockroom, param.setting_stok_opname_id); break;
                    }
                }

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_audit_rak_storage_for_stok_opname>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_ed_lookup_barang_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data barang by id rak, id stockroom, dan waktu capture")]
        public async Task<IActionResult> GetBarangBySetting(
            tr_audit_stok_opname_no_ed_lookup_barang_param param
            )
        {

            try
            {
                var result = await this._service.GetLookupBarangStokCapture(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_audit_stok_opname_no_ed_lookup_barang_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_ed_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_ed_header by dynamic filter (gunakan array kosong [] untuk get all data)",
       description: "prefix tr_audit_stok_opname_no_ed_header tasoh (), <br>" +
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
                    _data: new List<tr_audit_stok_opname_no_ed_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_audit_stok_opname_no_ed_header>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_ed_header yang masih open by dynamic filter (gunakan array kosong [] untuk get all data)",
       description: "prefix tr_audit_stok_opname_no_ed_header tasoh (), <br>" +
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
                    _data: new List<tr_audit_stok_opname_no_ed_header>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_audit_stok_opname_no_ed_detail_for_adjustment_finalisasi>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_audit_stok_opname_no_ed_detail by id header dan waktu capture")]
        public async Task<IActionResult> GetDetailByHeaderIdAndWaktuCapture([FromBody] tr_audit_stok_opname_no_ed_detail_getby_headerid_and_waktu param)
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
                    _data: new List<tr_audit_stok_opname_no_ed_detail_for_adjustment_finalisasi>(),
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
        public async Task<IActionResult> Insert([FromBody] tr_audit_stok_opname_no_ed_header_insert param)
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
        public async Task<IActionResult> Adjustment([FromBody] tr_audit_stok_opname_no_ed_header_update_after_adjust param)
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
        public async Task<IActionResult> Finalisasi([FromBody] tr_audit_stok_opname_no_ed_header_update_after_proses param)
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
