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
    public class TransMutasiNoEdController : ControllerBase
    {
        private ITransMutasiNoEdService _service;
        private ISetupUkuranDokumenService _dokumenService;
        private ISetupStokItemService _stokItemService;

        private readonly ILogger<TransMutasiNoEdController> _logger;

        public TransMutasiNoEdController(ITransMutasiNoEdService service,
            ISetupUkuranDokumenService dokumenService,
            ISetupStokItemService stokItemService,
        ILogger<TransMutasiNoEdController> logger)
        {
            this._service = service;
            this._stokItemService = stokItemService;
            this._dokumenService = dokumenService;
            this._logger = logger;
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
                    _data: new List<mm_setup_stok_item_lookup>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_mutasi_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed permintaan by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_mutasi_no_ed = tm, <br>" +
            "mm_setup_stockroom mss_pemberi (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_stockroom mss_penerima (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetHistoryPermintaanByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetMutasiPermintaanByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_mutasi_no_ed>(),
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


        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_mutasi_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed permintaan by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_mutasi_no_ed = tm, <br>" +
            "mm_setup_stockroom mss_pemberi (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_stockroom mss_penerima (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllPermintaanByIdStockroomAndParams(
            [FromRoute] short id_stockroom,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetMutasiPermintaanByIdStockroomPemberiAndParams(id_stockroom, param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_mutasi_no_ed>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_mutasi_no_ed>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix tr_mutasi_no_ed = tm, <br>" +
            "mm_setup_stockroom mss_pemberi (kode_stockroom,nama_stockroom), <br>" +
            "mm_setup_stockroom mss_penerima (kode_stockroom,nama_stockroom)")]
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllTrMutasiByParams(param);

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_mutasi_no_ed>(),
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

        [HttpGet("{mutasi_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<tr_mutasi_no_ed>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed by id")]
        public async Task<IActionResult> GetById(long mutasi_id)
        {

            try
            {
                var result = await this._service.GetTrMutasiById(mutasi_id);

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
        [SwaggerOperation(summary: "untuk input data tr_mutasi_no_ed")]
        public async Task<IActionResult> Insert([FromBody] tr_mutasi_no_ed_approve param)
        {
            try
            {
                param.user_validated = (short)HttpContext.Items["userId"];
                (bool result,string message) = await this._service.ApproveMutasi(param);
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

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<Int16?>))]
        [SwaggerOperation(summary: "untuk input data tr_mutasi_no_ed")]
        public async Task<IActionResult> InsertPermintaan([FromBody] tr_mutasi_no_ed_insert_permintaan param)
        {
            try
            {
                param.user_permintaan_mutasi = (short)HttpContext.Items["userId"];
                var result = await this._service.AddTrMutasiPermintaan(param);
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
        [SwaggerOperation(summary: "untuk update data tr_mutasi_no_ed")]
        public async Task<IActionResult> UpdateToCanceled([FromBody] tr_mutasi_no_ed_update_to_canceled param)
        {
            try
            {
                param.user_canceled = (short)HttpContext.Items["userId"];

                (bool result,string message) = await this._service.UpdateTrMutasiCanceled(param);

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

        [HttpGet("{mutasi_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_mutasi_no_ed_detail_item_with_hpp>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed_detail_item by id")]
        public async Task<IActionResult> GetDetailByMutasiId(long mutasi_id)
        {

            try
            {
                var result = await this._service.GetTrMutasiDetailItemWithHppByMutasiId(mutasi_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_mutasi_no_ed_detail_item_with_hpp>(),
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

        [HttpGet("{mutasi_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<tr_mutasi_no_ed_detail_upload>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data tr_mutasi_no_ed_detail_upload by id")]
        public async Task<IActionResult> GetDetailUploadByMutasiId(long mutasi_id)
        {

            try
            {
                var result = await this._service.GetTrMutasiDetailUploadByMutasiId(mutasi_id);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<tr_mutasi_no_ed_detail_upload>(),
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
        [SwaggerOperation(summary: "untuk upload data tr_mutasi_no_ed_detail_upload")]
        public async Task<IActionResult> UploadDetailBerkas([FromForm] tr_mutasi_no_ed_detail_upload_insert param)
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

                    string bucketName = "transaksi-mutasi";
                    string bucketNameUploaded = "transaksi-mutasi-document";

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
                        result = await this._service.AddTrMutasiDetailUpload(param);
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

        [HttpDelete("{mutasi_detail_upload_id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data tr_mutasi_no_ed_detail_upload")]
        public async Task<IActionResult> DeleteDetailBerkas([FromRoute] long mutasi_detail_upload_id)
        {

            try
            {
                var result = await this._service.DeleteTrMutasiDetailUpload(mutasi_detail_upload_id);
                _logger.LogInformation("delete data {0}", mutasi_detail_upload_id);

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
                _logger.LogError(ex, "data {0}", mutasi_detail_upload_id);
                throw;
            }
        }

        #endregion
    }
}
