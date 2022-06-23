using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Header
    public record tr_retur_pemakaian_internal
    {

        public long? retur_pemakaian_internal_id { get; set; } //bigint()
        public string nomor_retur_pemakaian_internal { get; set; } //character varying()
        public DateTime? tanggal_retur_pemakaian_internal { get; set; } //date()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public string keterangan_retur_pemakaian_internal { get; set; } //character varying()
        public string pic_pemberi { get; set; } //character varying()
        public string pic_penerima { get; set; } //character varying()
        public DateTime? time_serah_terima { get; set; } //timestamp without time zone()
        public string status_transaksi { get; set; } //character varying()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        public short? user_inputed { get; set; } //integer()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //integer()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //integer()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()
    }

    public record tr_retur_pemakaian_internal_insert
    {

        public string nomor_retur_pemakaian_internal { get; set; } //character varying()
        public DateTime? tanggal_retur_pemakaian_internal { get; set; } //timestamp without time zone()
        public short? id_stockroom { get; set; } //smallint()
        public string keterangan_retur_pemakaian_internal { get; set; } //character varying()
        public string pic_pemberi { get; set; } //character varying()
        public string pic_penerima { get; set; } //character varying()
        public DateTime? time_serah_terima { get; set; } //timestamp without time zone()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        internal short? user_inputed { get; set; } //integer()

        public List<tr_retur_pemakaian_internal_detail_item_insert> details { get; init; }

    }

    public record tr_retur_pemakaian_internal_update_to_validated
    {

        public long retur_pemakaian_internal_id { get; set; } //bigint()
        internal short? user_validated { get; set; } //integer()

    }

    public record tr_retur_pemakaian_internal_update_to_canceled
    {

        public long retur_pemakaian_internal_id { get; set; } //bigint()
        internal short? user_canceled { get; set; } //integer()
        public string reason_canceled { get; set; } //character varying()

    }


    #endregion

    #region Detail Item


    public record tr_retur_pemakaian_internal_detail_item
    {

        public long? retur_pemakaian_internal_detail_item_id { get; set; } //bigint()
        public long? retur_pemakaian_internal_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_satuan_besar_retur_pemakaian_internal { get; set; } //numeric()
        public string kode_satuan_besar_retur_pemakaian_internal { get; set; } //character varying()
        public string nama_satuan_besar_retur_pemakaian_internal { get; set; } //character varying()
        public short? isi_retur_pemakaian_internal { get; set; } //smallint()
        public decimal qty_retur_pemakaian_internal { get; set; } //numeric()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public decimal hpp_satuan { get; set; } //numeric()
        public decimal? nominal_retur_pemakaian_internal { get; set; } //numeric()
        public string keterangan_retur_pemakaian_internal { get; set; } //character varying()

        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans
        {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }

    }

    public record tr_retur_pemakaian_internal_detail_item_insert
    {

        internal long? retur_pemakaian_internal_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty_satuan_besar_retur_pemakaian_internal { get; set; } //numeric()
        public string kode_satuan_besar_retur_pemakaian_internal { get; set; } //character varying()
        public short? isi_retur_pemakaian_internal { get; set; } //smallint()
        public decimal? qty_retur_pemakaian_internal { get; set; } //numeric()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //timestamp without time zone()
        public decimal? hpp_satuan { get; set; } //numeric()
        public decimal? nominal_retur_pemakaian_internal { get; set; } //numeric()
        public string keterangan_retur_pemakaian_internal { get; set; } //character varying()

    }


    #endregion

    #region Detail Upload


    public record tr_retur_pemakaian_internal_detail_upload
    {

        public long? retur_pemakaian_internal_detail_upload_id { get; set; } //bigint()
        public long? retur_pemakaian_internal_id { get; set; } //bigint()
        public string jenis_dokumen { get; set; } //character varying(30)
        internal string path_dokumen { get; set; }
        private string _url_dokumen { get; set; }
        public string url_dokumen
        {
            get
            {
                return this._url_dokumen;
            }
            set
            {
                var reqParams = new Dictionary<string, string>{
                        { "response-content-type", "application/pdf" }
                    };

                this.path_dokumen = value;
                this._url_dokumen = UploadHelper.GetFileLinkByPath(value, reqParams).Result;
            }
        } //text()
        public string keterangan { get; set; } //character varying(200)

    }

    public record tr_retur_pemakaian_internal_detail_upload_insert
    {

        public long? retur_pemakaian_internal_id { get; set; } //bigint()
        public string jenis_dokumen { get; set; }
        internal string url_dokumen { get; set; }
        public string keterangan { get; set; }
        public IFormFile file { get; set; }

    }

    #endregion
}
