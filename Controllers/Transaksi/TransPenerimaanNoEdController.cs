using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Transaksi;
using Pharmm.API.Services.Transaksi;
using QueryModel.Model;
using Swashbuckle.AspNetCore.Annotations;
using Utility.OKResponse.Helper;
using Pharmm.API.Helper;
using System.IO;
using Pharmm.API.Services.Setup;
using Pharmm.API.Models.Setup;

namespace Pharmm.API.Controllers.Transaksi
{
    [Authorize]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TransPenerimaanNoEdController : ControllerBase
    {
        private ITransPenerimaanNoEdService _service;
        private ISetupUkuranDokumenService _dokumenService;
        private IMasterCounterService _kodeService;
        private ITransPemesananService _poService;
        private readonly ILogger<TransPenerimaanNoEdController> _logger;

        public TransPenerimaanNoEdController(ITransPenerimaanNoEdService service,
            ISetupUkuranDokumenService dokumenService,
        ILogger<TransPenerimaanNoEdController> logger,
        ITransPemesananService poService,
        IMasterCounterService kodeService)
        {
            this._service = service;
            this._kodeService = kodeService;
            this._dokumenService = dokumenService;
            this._logger = logger;
            this._poService = poService;
        }


        #region Lookup

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data pemesanan detail lookup penerimaan " +
            "by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_pemesanan_detail tpd" +
            "tr_pemesanan = tp , <br>" +
            "mm_setup_item = msi (kode_item,barcode,nama_item) , <br>" +
            "mm_setup_satuan = mss (nama_satuan)")]
        public async Task<IActionResult> GetLookupPemesananByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._poService.GetAllTrPemesananLookupForPenerimaan(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                             _data: new List<tr_pemesanan>(),
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

        [HttpGet("{pemesanan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemesanan_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_penerimaan_no_ed_detail_item by penerimaan_id")]
        public async Task<IActionResult> GetLookupPemesananDetailItemByPemesananId([FromRoute] long pemesanan_id)
        {

            try
            {
                var result = await this._poService.GetTrPemesananDetailLookupForPenerimaanByPemesananId(pemesanan_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemesanan_detail>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_penerimaan_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_penerimaan_no_ed by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_penerimaan_no_ed = tp, <br> " +
            "tr_pemesanan = po (nomor_pemesanan), <br>" +
            "mm_setup_jenis_penerimaan = msjp (jenis_penerimaan,is_account_payable), <br>" +
            "mm_setup_stockroom = mss (kode_stockroom,nama_stockroom) , <br>" +
            "mm_setup_shipping_method = mssm (shipping_method) , <br>" +
            "mm_setup_payment_term = mspt (payment_term) , <br>" +
            "mm_setup_supplier = sup (kode_supplier,nama_supplier,alamat_supplier)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrPenerimaanByParams(param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<tr_penerimaan_no_ed>(),
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

        [HttpGet("{penerimaan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_penerimaan_no_ed>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_penerimaan_no_ed by id")]
        public async Task<IActionResult> GetById(long penerimaan_id)
        {

            try
            {
                var result = await this._service.GetTrPenerimaanById(penerimaan_id);

                if (result is not null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
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
        [SwaggerOperation(summary: "untuk input data tr_penerimaan_no_ed")]
        public async Task<IActionResult> Insert([FromBody] tr_penerimaan_no_ed_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];

                (bool result,string message) = await this._service.AddTrPenerimaan(param);
                
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: 1,
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


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> CekStatusPenerimaan(long? penerimaan_id)
        {
            string status = "";

            var paramCekData = new List<ParameterSearchModel>
                {
                    new ParameterSearchModel
                    {
                        columnName = "penerimaan_id",
                        filter = "equal",
                        searchText = penerimaan_id.ToString(),
                        searchText2 = ""
                    }

                };

            //cek apakah ada Data penerimaan sudah closed atau sudah canceled
            var datatoValidate = await this._service.GetAllTrPenerimaanByParams(paramCekData);

            if (!string.IsNullOrEmpty(datatoValidate.First().user_canceled.ToString()))
            {

                status = "canceled";
            }
            else if (!string.IsNullOrEmpty(datatoValidate.First().user_validated.ToString()))
            {

                status = "validated";
            }


            //jika status = "" maka belum di apa2kan (validated,atau canceled)

            return status;
        }


        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data tr_penerimaan_no_ed")]
        public async Task<IActionResult> UpdateToValidated([FromBody] tr_penerimaan_no_ed_update_status_to_validated param)
        {
            try
            {
                (bool result,string message) = (false,"");

                param.user_validated = (short)HttpContext.Items["userId"];

                var isAllowed = await this.CekStatusPenerimaan(param.penerimaan_id);

                if (string.IsNullOrEmpty(isAllowed))
                {
                    (bool resultService,string messageService) = await this._service.UpdateToValidated(param);

                    result = resultService;
                    message = messageService;
                }
                else if (isAllowed == "validated")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah divalidasi"
                    ));
                }
                else if (isAllowed == "closed")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah diselesaikan"
                    ));
                }
                else if (isAllowed == "canceled")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah dibatalkan"
                    ));
                }

                _logger.LogInformation("update data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data penerimaan berhasil divalidasi"));

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
        [SwaggerOperation(summary: "untuk update data tr_penerimaan_no_ed")]
        public async Task<IActionResult> UpdateToCanceled([FromBody] tr_penerimaan_no_ed_update_status_to_canceled param)
        {
            try
            {
                var result = 0;

                param.user_canceled = (short)HttpContext.Items["userId"];

                var isAllowed = await this.CekStatusPenerimaan(param.penerimaan_id);

                if (string.IsNullOrEmpty(isAllowed))
                {
                    result = await this._service.UpdateToCanceled(param);
                }
                else if (isAllowed == "validated")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah divalidasi"
                    ));
                }
                else if (isAllowed == "closed")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah diselesaikan"
                    ));
                }
                else if (isAllowed == "canceled")
                {

                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _message: "Data penerimaan sudah dibatalkan"
                    ));
                }

                _logger.LogInformation("update data {0}", param);

                if (result > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data penerimaan berhasil dibatalkan"));

                }

                return Ok(ResponseHelper.GetResponse(
                    _data: "",
                    _message: "Data penerimaan gagal dibatalkan"
                ));


            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        #region Detail

        [HttpGet("{penerimaan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_penerimaan_no_ed_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_penerimaan_no_ed_detail_item by penerimaan_id")]
        public async Task<IActionResult> GetDetailItemByPenerimaanId(long penerimaan_id)
        {

            try
            {
                var result = await this._service.GetTrPenerimaanDetailItemByHeaderId(penerimaan_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_penerimaan_no_ed_detail_item>(),
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

        [HttpGet("{penerimaan_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_penerimaan_no_ed_detail_upload>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_penerimaan_no_ed_detail_upload by penerimaan_id")]
        public async Task<IActionResult> GetDetailUploadByPenerimaanId(long penerimaan_id)
        {

            try
            {
                var result = await this._service.GetTrPenerimaanDetailUploadByHeaderId(penerimaan_id);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_penerimaan_no_ed_detail_upload>(),
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
        [SwaggerOperation(summary: "untuk upload data tr_penerimaan_no_ed_detail_upload")]
        public async Task<IActionResult> UploadDetailBerkas([FromForm] tr_penerimaan_no_ed_detail_upload_insert param)
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

                    string bucketName = "transaksi-penerimaan";
                    string bucketNameUploaded = "transaksi-penerimaan-document";

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
                        result = await this._service.AddTrPenerimaanDetailUpload(param);
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

        [HttpDelete("{penerimaan_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data tr_penerimaan_no_ed_detail_upload")]
        public async Task<IActionResult> DeleteDetailBerkas([FromRoute] long penerimaan_detail_upload_id)
        {

            try
            {
                var result = await this._service.DeleteTrPenerimaanDetailUpload(penerimaan_detail_upload_id);
                _logger.LogInformation("delete data {0}", penerimaan_detail_upload_id);

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
                _logger.LogError(ex, "data {0}", penerimaan_detail_upload_id);
                throw;
            }
        }

        #endregion

    }
}
