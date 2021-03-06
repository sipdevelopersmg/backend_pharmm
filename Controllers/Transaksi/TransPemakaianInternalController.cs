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
    public class TransPemakaianInternalController : ControllerBase
    {

        private IMasterCounterService _kodeService;
        private ITransPemakaianInternalService _service;
        private ISetupUkuranDokumenService _dokumenService;
        private readonly ILogger<TransPemakaianInternalController> _logger;
        private readonly ISetupStokItemService _stokItemService;

        public TransPemakaianInternalController(ITransPemakaianInternalService service,
            ISetupUkuranDokumenService dokumenService,
        ILogger<TransPemakaianInternalController> logger,
        IMasterCounterService kodeService,
        ISetupStokItemService stokItemService)
        {
            this._service = service;
            this._kodeService = kodeService;
            this._dokumenService = dokumenService;
            this._logger = logger;
            this._stokItemService = stokItemService;
        }

        #region Lookup

        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data lookup barang by id_stockroom",
            description: "prefix stok item mssi(id_item,id_stockroom,qty_on_hand,qty_stok_kritis)," +
            "setup_item (kode_item,barcode,nama_item)")]
        public async Task<IActionResult> GetLookupBarangByIdStockroom(
            [FromRoute] short id_stockroom,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemByIdStockroomAndParams(id_stockroom, param);

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


        [HttpGet("{id_stockroom}/{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_stok_item_detail_batch_lookup>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_stok_item_detail_batch by id_item")]
        public async Task<IActionResult> GetLookupBarangByIdItemAndIdStockroom([FromRoute] int id_item,
            [FromRoute] short id_stockroom)
        {

            try
            {
                var result = await this._stokItemService.GetAllMmSetupStokItemDetailBatchByIdStockroomAndIdItem(id_stockroom, id_item);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_stok_item_detail_batch>(),
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


        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk mendapatkan nomor pemakaian internal")]
        public async Task<IActionResult> GetNoPemakaianInternal()
        {

            try
            {
                var dataCounter = new master_counter_insert
                {
                    kode_counter = PrefixStaticHelper.prefixKodePemakaianInternal,
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal yang open " +
            "by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_pemakaian_internal = tpi, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllOpenByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllOpenTrPemakaianInternalByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_pemakaian_internal = tpi, <br>" +
            "mm_setup_stockroom mss (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrPemakaianInternalByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal>(),
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

        [HttpGet("{pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_pemakaian_internal>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal by id")]
        public async Task<IActionResult> GetById(long pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrPemakaianInternalById(pemakaian_internal_id);

                if (result != null)
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
        [SwaggerOperation(summary: "untuk input data tr_pemakaian_internal")]
        public async Task<IActionResult> Insert([FromBody] tr_pemakaian_internal_insert param)
        {
            try
            {
                param.user_inputed = (short)HttpContext.Items["userId"];
                (bool result,long id,string message) = await this._service.AddTrPemakaianInternal(param);
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

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk update data tr_pemakaian_internal")]
        public async Task<IActionResult> Validasi([FromBody] tr_pemakaian_internal_update_to_validated param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];
                (bool result,long id,string message) = await this._service.UpdateTrPemakaianInternalValidated(param);
                

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
        [SwaggerOperation(summary: "untuk update data tr_pemakaian_internal_no_ed")]
        public async Task<IActionResult> Batal([FromBody] tr_pemakaian_internal_update_to_canceled param)
        {
            try
            {
                param.user_canceled = (short)HttpContext.Items["userId"];
                (bool result, long id, string message) = await this._service.UpdateTrPemakaianInternalCanceled(param);


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

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<string> GetStatusPemakaianInternal(long pemakaian_internal_id)
        {

            string status = "";

            //cek status pemakaian_internal
            var datatoValidate = await this._service.GetTrPemakaianInternalById(pemakaian_internal_id);

            if (datatoValidate is not null)
            {
                if (!string.IsNullOrEmpty(datatoValidate.user_validated.ToString()))
                {
                    status = "validated";
                }
                else if (!string.IsNullOrEmpty(datatoValidate.user_canceled.ToString()))
                {

                    status = "canceled";
                }
            }
            else
            {
                status = "not found";
            }


            //jika status = "" maka belum di apa2kan (validated atau canceled)

            return status;
        }

        #endregion

        #region Detail Item

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal_detail_item by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_pemakaian_internal_detail_item = tpidi, <br>" +
            "mm_setup_item msi (kode_item, barcode, nama_item), <br> " +
            "mm_setup_satuan mss (nama_satuan)")]
        public async Task<IActionResult> GetAllDetailByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrPemakaianInternalDetailItemByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal_detail_item>(),
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

        [HttpGet("{pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal_detail_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal_detail_item by id")]
        public async Task<IActionResult> GetDetailByHeaderId(long pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrPemakaianInternalDetailItemByPemakaianInternalId(pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal_detail_item>(),
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

        #region Detail Item Batch

        [HttpGet("{pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal_detail_item_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal_detail_item_batch by id")]
        public async Task<IActionResult> GetDetailItemBatchByHeaderId(long pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrPemakaianInternalDetailItemBatchByPemakaianInternalId(pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal_detail_item_batch>(),
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


        [HttpGet("{pemakaian_internal_detail_item_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal_detail_item_batch>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal_detail_item_batch by id")]
        public async Task<IActionResult> GetDetailItemBatchByDetailItemId(long pemakaian_internal_detail_item_id)
        {

            try
            {
                var result = await this._service.GetTrPemakaianInternalDetailItemBatchByDetailItemId(pemakaian_internal_detail_item_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal_detail_item_batch>(),
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

        [HttpGet("{pemakaian_internal_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_pemakaian_internal_detail_upload>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_pemakaian_internal_detail_upload by id")]
        public async Task<IActionResult> GetDetailUploadByHeaderId(long pemakaian_internal_id)
        {

            try
            {
                var result = await this._service.GetTrPemakaianInternalDetailUploadByPemakaianInternalId(pemakaian_internal_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_pemakaian_internal_detail_upload>(),
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
        [SwaggerOperation(summary: "untuk upload data tr_pemakaian_internal_detail_upload")]
        public async Task<IActionResult> UploadDetailBerkas([FromForm] tr_pemakaian_internal_detail_upload_insert param)
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

                    string bucketName = "transaksi-pemakaian-internal";
                    string bucketNameUploaded = "transaksi-pemakaian-internal-document";

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
                        result = await this._service.AddTrPemakaianInternalDetailUpload(param);
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

        [HttpDelete("{pemakaian_internal_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data tr_pemakaian_internal_detail_upload")]
        public async Task<IActionResult> DeleteDetailBerkas([FromRoute] long pemakaian_internal_detail_upload_id)
        {

            try
            {
                var result = await this._service.DeleteTrPemakaianInternalDetailUpload(pemakaian_internal_detail_upload_id);
                _logger.LogInformation("delete data {0}", pemakaian_internal_detail_upload_id);

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
                _logger.LogError(ex, "data {0}", pemakaian_internal_detail_upload_id);
                throw;
            }
        }

        #endregion
    }
}
