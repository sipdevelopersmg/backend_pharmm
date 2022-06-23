using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Pharmm.API.Models.Setup;
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
    public class SetupItemController : ControllerBase
    {
        private ISetupItemSatuanService _itemSatuanService;
        private ISetupObatService _obatService;
        private ISetupItemService _service;
        private readonly ILogger<SetupItemController> _logger;

        public SetupItemController(
            ISetupItemSatuanService _itemSatuanService,
            ISetupObatService obatService,
            ISetupItemService service,
        ILogger<SetupItemController> logger)
        {
            this._service = service;
            this._logger = logger;
            this._obatService = obatService;
            this._itemSatuanService = _itemSatuanService;
        }


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
        public async Task<IActionResult> GetAllByParams([FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupItemByParams(param);

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


        [HttpPost("{id_stockroom}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_with_stok>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item by param (gunakan array kosong [] untuk get all data)",
            description: "prefix mm_setup_item = msi (id_grup_item, id_pabrik, id_supplier, kode_item, barcode, nama_item, " +
            "id_temperatur_item, <br> " +
            "mm_setup_supplier mss (kode_supplier,nama_supplier,alamat_supplier), <br> " +
            "mm_setup_grup_item msgi (kode_grup_item,grup_item), <br> " +
            "mm_setup_pabrik msp (kode_pabrik,nama_pabrik,alamat_pabrik), <br>" +
            "mm_setup_satuan mss (kode_satuan,nama_satuan), <br>" +
            "mm_setup_temperatur_item msti (temperatur_item)")]
        public async Task<IActionResult> GetLookupBarangByIdWarehouseAndParams([FromRoute] short id_stockroom, [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._service.GetAllMmSetupItemByIdStockroomAndParams(id_stockroom, param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<mm_setup_item_with_stok>(),
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item")]
        public async Task<IActionResult> GetAll()
        {

            try
            {
                var result = await this._service.GetAllMmSetupItem();

                if (result.Count > 0)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item>(),
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

        [HttpGet("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<mm_setup_item>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item by id")]
        public async Task<IActionResult> GetById(int id_item)
        {

            try
            {
                var result = await this._service.GetMmSetupItemById(id_item);

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
        [SwaggerOperation(summary: "untuk input data mm_setup_item")]
        public async Task<IActionResult> Insert([FromBody] mm_setup_item_insert param)
        {
            try
            {

                param.user_created = (short)HttpContext.Items["userId"];

                (bool result, string message) = await this._service.AddMmSetupItem(param);
                _logger.LogInformation("save data {0}", param);

                if (result && message.IndexOf("exist") <= -1)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }
                else if (result && message.IndexOf("exist") > -1)
                {

                    return Ok(ResponseHelper.GetResponseDataExisting(
                        _data: 0,
                        _objectClass: param,
                        _propertyExisting: new[] { "nama_item" }
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
        [SwaggerOperation(summary: "untuk update data mm_setup_item")]
        public async Task<IActionResult> Update([FromBody] mm_setup_item_update param)
        {
            try
            {
                var result = await this._service.UpdateMmSetupItem(param);
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
                        _propertyExisting: new[] { "nama_item" }
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
        [SwaggerOperation(summary: "untuk update data to active mm_setup_item")]
        public async Task<IActionResult> UpdateToActive([FromBody] mm_setup_item_update_status_to_active param)
        {
            try
            {
                var result = await this._service.UpdateToActiveMmSetupItem(param);
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
        [SwaggerOperation(summary: "untuk update data to deactive mm_setup_item")]
        public async Task<IActionResult> UpdateToDeActive([FromBody] mm_setup_item_update_status_to_deactive param)
        {
            try
            {
                param.user_deactived = (short)HttpContext.Items["userId"];

                var result = await this._service.UpdateToDeActiveMmSetupItem(param);
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

        #region Setup Item Satuan

        [HttpGet("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_satuan>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item_satuan by id item")]
        public async Task<IActionResult> GetItemSatuanByIdItem(int id_item)
        {

            try
            {
                var result = await this._itemSatuanService.GetMmSetupItemSatuanByIdItem(id_item);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item_satuan>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_item_satuan")]
        public async Task<IActionResult> InsertItemSatuan([FromBody] List<mm_setup_item_satuan_insert> param)
        {
            try
            {
                (bool result, long id, string message) = await this._itemSatuanService.AddMmSetupItemSatuanMultiple(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: result,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }


                //if (id < 0)
                //{

                //    return Ok(ResponseHelper.GetResponseDataExisting(
                //        _data: 0,
                //        _objectClass: param,
                //        _propertyExisting: new[] { "id_item", "kode_satuan" }
                //        ));
                //}

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
        [SwaggerOperation(summary: "untuk update data mm_setup_item_satuan")]
        public async Task<IActionResult> UpdateItemSatuan([FromBody] List<mm_setup_item_satuan_insert> param)
        {
            try
            {
                (bool result, long id, string message) = await this._itemSatuanService.UpdateMmSetupItemSatuanMultiple(param);
                _logger.LogInformation("update data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: "",
                        _responseResult: true,
                        _message: "Data sukses diupdate"));

                }


                //if (id < 0)
                //{
                //    return Ok(ResponseHelper.GetResponseDataExisting(
                //        _data: "",
                //        _objectClass: param,
                //        _propertyExisting: new[] { "id_item", "kode_satuan" }
                //        ));
                //}

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

        #region Phar Setup Obat

        [HttpPost("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<phar_setup_obat_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data phar_setup_obat_detail by dynamic filter (gunakan array kosong [] untuk get all data)",
            description: "prefix phar_setup_obat_detail = psod")]
        public async Task<IActionResult> GetAllObatDetailByParams(
            [FromRoute] int id_item,
            [FromBody] List<ParameterSearchModel> param)
        {

            try
            {
                var result = await this._obatService.GetAllPharSetupObatDetailByIdAndParams(id_item, param);

                if (!result.Any())
                {
                    return Ok(ResponseHelper.GetResponse(
                         _data: new List<phar_setup_obat_detail>(),
                         _responseResult: true
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<phar_setup_obat_detail>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data phar_setup_obat_detail")]
        public async Task<IActionResult> GetAllObatDetail()
        {

            try
            {
                var result = await this._obatService.GetAllPharSetupObatDetail();

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<phar_setup_obat_detail>(),
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
        [SwaggerOperation(summary: "untuk input data phar_setup_obat_detail")]
        public async Task<IActionResult> InsertObatDetail([FromBody] phar_setup_obat_detail_insert param)
        {
            try
            {
                param.user_created = (short)HttpContext.Items["userId"];

                (bool result,long id,string msg) = await this._obatService.AddPharSetupObatDetail(param);
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
                    _message: msg
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
        [SwaggerOperation(summary: "untuk update data phar_setup_obat_detail")]
        public async Task<IActionResult> UpdateObatDetailToDeactive([FromBody] phar_setup_obat_detail_update_status param)
        {
            try
            {
                param.user_edited = (short)HttpContext.Items["userId"];

                var result = await this._obatService.UpdateStatusToDeactivePharSetupObatDetail(param);
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

        #endregion


        #region Barang Urai

        [HttpGet("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_urai>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item_urai by header id")]
        public async Task<IActionResult> GetItemUraiByHeaderId(int id_item)
        {

            try
            {
                var result = await this._service.GetMmSetupItemUraiByHeaderId(id_item);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item_urai>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_item_urai")]
        public async Task<IActionResult> InsertItemUrai([FromBody] mm_setup_item_urai_insert param)
        {
            try
            {
                (bool result, int id, string message) = await this._service.AddMmSetupItemUrai(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }

                if (id == -1)
                {

                    return Ok(ResponseHelper.GetResponseDataExisting(
                        _data: 0,
                        _objectClass: param,
                        _propertyExisting: new[] { "id_item" }
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

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_item_urai")]
        public async Task<IActionResult> DeleteItemUrai([FromBody] mm_setup_item_urai_delete param)
        {

            try
            {
                var result = await this._service.DeleteMmSetupItemUrai(param);
                _logger.LogInformation("delete data {0}", param);

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
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        #endregion


        #region Barang Assembly

        [HttpGet("{id_item}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<List<mm_setup_item_assembly>>))]
        [SwaggerOperation(summary: "untuk mendapatkan data mm_setup_item_assembly by header id")]
        public async Task<IActionResult> GetItemAssemblyByHeaderId(int id_item)
        {

            try
            {
                var result = await this._service.GetMmSetupItemAssemblyByHeaderId(id_item);

                if (result != null)
                {
                    return Ok(ResponseHelper.GetResponse(
                        _data: result,
                        _responseResult: true,
                        _message: ""
                    ));
                }


                return Ok(ResponseHelper.GetResponse(
                    _data: new List<mm_setup_item_assembly>(),
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
        [SwaggerOperation(summary: "untuk input data mm_setup_item_assembly")]
        public async Task<IActionResult> InsertItemAssembly([FromBody] mm_setup_item_assembly_insert param)
        {
            try
            {
                (bool result, int id, string message) = await this._service.AddMmSetupItemAssembly(param);
                _logger.LogInformation("save data {0}", param);

                if (result)
                {
                    return Ok(ResponseHelper.GetResponse(
                            _data: id,
                            _responseResult: true,
                            _message: "Data berhasil disimpan"
                        ));

                }

                if (id == -1)
                {

                    return Ok(ResponseHelper.GetResponseDataExisting(
                        _data: 0,
                        _objectClass: param,
                        _propertyExisting: new[] { "id_item" }
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

        [HttpDelete()]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResponseModel<string>))]
        [SwaggerOperation(summary: "untuk delete data mm_setup_item_assembly")]
        public async Task<IActionResult> DeleteItemAssembly([FromBody] mm_setup_item_assembly_delete param)
        {

            try
            {
                var result = await this._service.DeleteMmSetupItemAssembly(param);
                _logger.LogInformation("delete data {0}", param);

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
                _logger.LogError(ex, "data {0}", param);
                throw;
            }
        }

        #endregion

    }



}
