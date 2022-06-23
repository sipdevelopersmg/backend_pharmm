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
    public class TransSettingAuditStokOpnameController : ControllerBase
    {

        private ITransSettingAuditStokOpnameService _service;

        private readonly ISetupAuditRakStorageService _rakService;
        private readonly ISetupItemService _itemService;
        private readonly ISetupGrupItemService _grupService;
        private readonly ISetupStockroomService _gudangService;

        private readonly ISetupJenisSettingAuditStokOpnameService _jenisSetting;
        private readonly ILogger<TransSettingAuditStokOpnameController> _logger;

        public TransSettingAuditStokOpnameController(ITransSettingAuditStokOpnameService service,
            ISetupJenisSettingAuditStokOpnameService jenisSetting,
            ISetupItemService itemService,
            ISetupGrupItemService grupService,
            ISetupStockroomService gudangService,
            ISetupAuditRakStorageService rakService,
        ILogger<TransSettingAuditStokOpnameController> logger)
        {
            this._service = service;
            this._logger = logger;

            this._jenisSetting = jenisSetting;
            this._itemService = itemService;
            this._grupService = grupService;
            this._gudangService = gudangService;
            this._rakService = rakService;
        }

        [HttpGet("{setting_stok_opname_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_setting_audit_stok_opname_header_recursive>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_setting_audit_stok_opname_header by id")]
        public async Task<IActionResult> GetById(long setting_stok_opname_id)
        {

            try
            {
                var result = await this._service.GetTrSettingAuditStokOpnameHeaderByIdRecursive(setting_stok_opname_id);

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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_setting_audit_stok_opname_header_recursive>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data history tr_setting_audit_stok_opname_header by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix tr_setting_audit_stok_opname_header tsasoh (),<br>" +
            "mm_setup_jenis_setting_audit_stok_opname msjsaso(kode_jenis_setting_stok_opname,nama_jenis_setting_stok_opname)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrSettingAuditStokOpnameHeaderByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_setting_audit_stok_opname_header_recursive>(),
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


        #region Jenis Setting

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_jenis_setting_audit_stok_opname>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data jenis setting")]
        public async Task<IActionResult> GetAllJenisSetting()
        {

            try
            {
                var result = await this._jenisSetting.GetAllMmSetupJenisSettingAuditStokOpname();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_jenis_setting_audit_stok_opname>(),
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

        #region Lookup

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item by param (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_setup_item = msi (id_grup_item, id_pabrik, id_supplier, kode_item, barcode, nama_item, " +
            "id_temperatur_item, <br> " +
            "mm_setup_supplier mss (kode_supplier,nama_supplier,alamat_supplier), <br> " +
            "mm_setup_grup_item msgi (kode_grup_item,grup_item), <br> " +
            "mm_setup_pabrik msp (kode_pabrik,nama_pabrik,alamat_pabrik), <br>" +
            "mm_setup_satuan mss (kode_satuan,nama_satuan), <br>" +
            "mm_setup_temperatur_item msti (temperatur_item)")]
        public async Task<IActionResult> GetAllItemByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._itemService.GetAllMmSetupItemByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_item>(),
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_grup_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_grup_item by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_setup_grup_item = msgi (id_tipe_item, kode_grup_item, grup_item, is_active, user_created), <br>" +
            "akun_setup_coa persediaan (kode_coa_persediaan,deskripsi_coa_persediaan), <br>" +
            "akun_setup_coa pendapatan (kode_coa_pendapatan,deskripsi_coa_pendapatan), <br>" +
            "akun_setup_coa biaya (kode_coa_coa_biaya,deskripsi_coa_biaya) ")]
        public async Task<IActionResult> GetAllGrupItemByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._grupService.GetAllMmSetupGrupItemByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_grup_item>(),
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

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stockroom>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stockroom by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix mm_setup_stockroom mss (),<br>" +
            "mm_setup_tipe_stockroom msts(tipe_stockroom)")]
        public async Task<IActionResult> GetAllGudangByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._gudangService.GetAllMmSetupStockroomByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stockroom>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_audit_rak_storage>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_audit_rak_storage by dynamic filter (gunakan array kosong [] untuk get all data)",
        description: "prefix mm_setup_audit_rak_storage msars (), <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_audit_penanggung_jawab_rak_storage msapjrs (nama_penanggung_jawab_rak_storage)")]
        public async Task<IActionResult> GetAllRakByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._rakService.GetAllMmSetupAuditRakStorageByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_audit_rak_storage>(),
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

        #region Header

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data tr_setting_audit_stok_opname_header")]
        public async Task<IActionResult> InsertSettingGrup([FromBody] tr_setting_audit_stok_opname_header_grup_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result,long id,string message) = await this._service.AddTrSettingAuditStokOpnameGrup(param);
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
        [SwaggerOperation(summary: "untuk input data tr_setting_audit_stok_opname_header")]
        public async Task<IActionResult> InsertSettingItem([FromBody] tr_setting_audit_stok_opname_header_item_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.AddTrSettingAuditStokOpnameItem(param);
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
        
        [ApiExplorerSettings(IgnoreApi = false)]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data tr_setting_audit_stok_opname_header")]
        public async Task<IActionResult> InsertSettingSemuaItem([FromBody] tr_setting_audit_stok_opname_header_semua_item_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.AddTrSettingAuditStokOpnameSemuaItem(param);
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
        [SwaggerOperation(summary: "untuk input data tr_setting_audit_stok_opname_header")]
        public async Task<IActionResult> InsertSettingRak([FromBody] tr_setting_audit_stok_opname_header_rak_storage_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.AddTrSettingAuditStokOpnameRakStorage(param);
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

        #endregion


    }
}
