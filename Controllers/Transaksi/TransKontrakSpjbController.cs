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
    public class TransKontrakSpjbController : ControllerBase
    {

        private ITransKontrakSpjbService _service;
        private IMasterCounterService _kodeService;
        private ISetupUkuranDokumenService _dokumenService;
        private readonly ILogger<TransKontrakSpjbController> _logger;

        public TransKontrakSpjbController(ITransKontrakSpjbService service, ISetupUkuranDokumenService dokumenService,
        ILogger<TransKontrakSpjbController> logger,
        IMasterCounterService kodeService)
        {
            this._service = service;
            this._dokumenService = dokumenService;
            this._logger = logger;
            this._kodeService = kodeService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_kontrak_spjb>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb")]
        public async Task<IActionResult> GetAllHeader()
        {

            try
            {
                var result = await this._service.GetAllTrKontrakSpjb();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_kontrak_spjb>(),
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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk mendapatkan nomor kontrak")]
        public async Task<IActionResult> GetNomorKontrak()
        {

            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodeSPJB,
                    counter_max_length = 6,
                    use_alphabet = false,
                    use_dash = true,
                    use_date = true,
                    description = ""
                };

                var result = await this._kodeService.GenerateKode(dataCounter);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: "",
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

        [HttpGet("{kontrak_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_kontrak_spjb>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb by id")]
        public async Task<IActionResult> GetById(long kontrak_id)
        {

            try
            {
                var result = await this._service.GetTrKontrakSpjbById(kontrak_id);

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_kontrak_spjb>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb by dynamic filter (gunakan array kosong [] untuk get all data)", 
            description: "prefix tr_kontrak_spjb = tks, <br>" +
            "mm_setup_supplier sup (kode_supplier,nama_supplier,alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrKontrakSpjbByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_kontrak_spjb>(),
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

        [HttpGet("{kontrak_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_kontrak_spjb_detail_upload>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb_detail_upload by kontrak_detail_upload_id")]
        public async Task<IActionResult> GetDetailUploadById(long kontrak_detail_upload_id)
        {

            try
            {
                var result = await this._service.GetTrKontrakSpjbDetailUploadByKontrakDetailUploadId(kontrak_detail_upload_id);

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

        [HttpGet("{kontrak_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_kontrak_spjb_detail_upload>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb_detail_upload by kontrak_id")]
        public async Task<IActionResult> GetDetailUploadByKontrakId(long kontrak_id)
        {

            try
            {
                var result = await this._service.GetTrKontrakSpjbDetailUploadByKontrakId(kontrak_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_kontrak_spjb_detail_item>(),
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

        [HttpGet("{kontrak_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_kontrak_spjb_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_kontrak_spjb_detail_item by kontrak_id")]
        public async Task<IActionResult> GetDetailItemByKontrakId(long kontrak_id)
        {

            try
            {
                var result = await this._service.GetTrKontrakSpjbDetailItemByKontrakId(kontrak_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_kontrak_spjb_detail_item>(),
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
        [SwaggerOperation(summary: "untuk input data tr_kontrak_spjb")]
        public async Task<IActionResult> Insert([FromBody] tr_kontrak_spjb_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];

                (bool result,string message) = await this._service.AddTrKontrakSpjb(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
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
        [SwaggerOperation(summary: "untuk upload data tr_kontrak_spjb_detail_upload")]
        public async Task<IActionResult> UploadDetailBerkas([FromForm] tr_kontrak_spjb_detail_upload_insert param)
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

                    string bucketName = "transaksi-kontrak-spjb";
                    string bucketNameUploaded = "transaksi-kontrak-spjb-document";
                    
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
                        result = await this._service.AddTrKontrakSpjbDetailUpload(param);
                    }

                    _logger.LogInformation("save data {0}", param);

                    //jika berhasil input data transaksi
                    if (result > 0)
                    {
                        //var reqParams = new Dictionary<string, string>{
                        //        { "response-content-type", "application/pdf" }
                        //    };

                        ////if success get berkas link
                        //urlBerkas = await UploadHelper.GetFileLinkAsync(
                        //    bucketNameUploaded,
                        //    resultUpload.Item2,
                        //    reqParams);

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

        [HttpDelete("{kontrak_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data tr_kontrak_spjb_detail_upload")]
        public async Task<IActionResult> DeleteDetailBerkas([FromRoute]long kontrak_detail_upload_id)
        {

            try
            {
                var result = await this._service.DeleteTrKontrakSpjbDetailUpload(kontrak_detail_upload_id);
                _logger.LogInformation("delete data {0}", kontrak_detail_upload_id);

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
                _logger.LogError(ex, "data {0}", kontrak_detail_upload_id);
                throw;
            }
        }
    }
}
