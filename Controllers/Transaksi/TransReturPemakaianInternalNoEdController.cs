using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Setup;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility.OKResponse.Helper;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransReturPemakaianInternalNoEdController : ControllerBase
    {

        private IMasterCounterService _kodeService;
        private ISetupUkuranDokumenService _dokumenService;
        private ITransReturPemakaianInternalNoEdService _service;
        private readonly ILogger<TransReturPemakaianInternalNoEdController> _logger;

        public TransReturPemakaianInternalNoEdController(
            ITransReturPemakaianInternalNoEdService service,
            ISetupUkuranDokumenService dokumenService,
        ILogger<TransReturPemakaianInternalNoEdController> logger,
        IMasterCounterService kodeService)
        {
            this._service = service;
            this._logger = logger;
            this._kodeService = kodeService;
            this._dokumenService = dokumenService;
        }

        #region Header

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pemakaian_internal_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pemakaian_internal_no_ed by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pemakaian_internal_no_ed = trpi, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrReturPemakaianInternalByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pemakaian_internal_no_ed>(),
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

        [HttpGet("{retur_pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_retur_pemakaian_internal_no_ed>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pemakaian_internal_no_ed by id")]
        public async Task<IActionResult> GetById(long retur_pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrReturPemakaianInternalById(retur_pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new tr_retur_pemakaian_internal_no_ed(),
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
        [SwaggerOperation(summary: "untuk input data tr_retur_pemakaian_internal_no_ed")]
        public async Task<IActionResult> Insert([FromBody] tr_retur_pemakaian_internal_no_ed_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                var result = await this._service.AddTrReturPemakaianInternal(param);
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
        //[ApiExplorerSettings(IgnoreApi = false)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update to validated data tr_pemakaian_internal")]
        public async Task<IActionResult> Validasi([FromBody] tr_retur_pemakaian_internal_no_ed_update_to_validated param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];

                (bool result, long id, string message) = await this._service.UpdateToValidated(param);
                _logger.LogInformation("validasi data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Transaksi berhasil divalidasi"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: message
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
        [SwaggerOperation(summary: "untuk update to canceled data tr_pemakaian_internal")]
        public async Task<IActionResult> Batal([FromBody] tr_retur_pemakaian_internal_no_ed_update_to_canceled param)
        {
            try
            {
                param.user_canceled = (short)HttpContext.Items["userId"];

                (bool result,long id,string message) = await this._service.UpdateToCanceled(param);

                _logger.LogInformation("validasi data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Transaksi berhasil dibatalkan"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
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

        #region Detail Item

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pemakaian_internal_no_ed_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pemakaian_internal_no_ed_detail_item by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_retur_pemakaian_internal_no_ed_detail_item = trpidi, <br>" +
            "mm_setup_satuan mss (kode_satuan_nama_satuan), <br>" +
            "mm_setup_item msi (kode_item, barcode, nama_item)")]
        public async Task<IActionResult> GetAllDetailItemByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrReturPemakaianInternalDetailItemByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pemakaian_internal_no_ed_detail_item>(),
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

        [HttpGet("{retur_pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pemakaian_internal_no_ed_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pemakaian_internal_no_ed_detail_item by id")]
        public async Task<IActionResult> GetDetailItemByHeaderId(long retur_pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrReturPemakaianInternalDetailItemByHeaderId(retur_pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pemakaian_internal_no_ed_detail_item>(),
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

        #region Detail Upload

        [HttpGet("{retur_pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_retur_pemakaian_internal_no_ed_detail_upload>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_retur_pemakaian_internal_no_ed_detail_upload by id")]
        public async Task<IActionResult> GetDetailUploadByHeaderId(long retur_pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrReturPemakaianInternalDetailUploadByHeaderId(retur_pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_retur_pemakaian_internal_no_ed_detail_upload>(),
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
        [SwaggerOperation(summary: "untuk upload data tr_retur_pemakaian_internal_no_ed_detail_upload")]
        public async Task<IActionResult> UploadDetailBerkas([FromForm] tr_retur_pemakaian_internal_no_ed_detail_upload_insert param)
        {
            try
            {
                long result = 0;

                //url berkas jika berhasil upload
                //string urlBerkas = "";
                var formFile = param.file;

                //get ukuran limit dokumen (dalam Mb)
                var dokumenSetup = await this._dokumenService.GetAllSetUkuranDokumen();
                long limitSize = 4 * 1024 * 1024;  //default limit

                if (dokumenSetup.Count > 0)
                {
                    limitSize = dokumenSetup[0].max_size * 1024 * 1024;
                }

                if (formFile.Length > 0)
                {

                    string bucketName = "transaksi-retur-pemakaian-internal";
                    string bucketNameUploaded = "transaksi-retur-pemakaian-internal-document";

                    param.url_dokumen = "";


                    var resultUpload = await UploadHelper.UploadPdfToMINIOAsync(
                                        formFile,
                                        bucketName,
                                        limitSize
                                        );

                    //jika gagal upload berkas
                    if (resultUpload.Item1 == false)
                    {
                        //jika gagal upload
                        return Ok(ResponseHelper.GetResponse(
                            _data: 0,
                            _message: resultUpload.Item2
                        ));
                    }
                    else
                    {
                        param.url_dokumen = Path.Combine(bucketNameUploaded, resultUpload.Item2);
                        result = await this._service.AddTrReturPemakaianInternalDetailUpload(param);
                    }

                    _logger.LogInformation("save data {0}", param);

                    //jika berhasil input data transaksi
                    if (result > 0)
                    {

                        return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                    }
                    else
                    {
                        //jika gagal input data transaksi maka hapus gambar yang diupload
                        await UploadHelper.RemoveObjectFromBucketByPath(param.url_dokumen);

                    }
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

        [HttpDelete("{retur_pemakaian_internal_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data tr_retur_pemakaian_internal_no_ed_detail_upload")]
        public async Task<IActionResult> DeleteDetailBerkas([FromRoute] long retur_pemakaian_internal_detail_upload_id)
        {

            try
            {
                var result = await this._service.DeleteTrReturPemakaianInternalDetailUpload(retur_pemakaian_internal_detail_upload_id);
                _logger.LogInformation("delete data {0}", retur_pemakaian_internal_detail_upload_id);

                if (result == 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data gagal dihapus"
                    ));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _responseResult: true,
                    _message: "Data berhasil dihapus"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", retur_pemakaian_internal_detail_upload_id);
                throw;
            }
        }

        #endregion

    }
}
