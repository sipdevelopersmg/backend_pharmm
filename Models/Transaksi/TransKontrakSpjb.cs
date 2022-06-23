using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pharmm.API.Filters;
using Pharmm.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Update Param

    public record tr_kontrak_spjb_update_penambahan_pesan
    {
        public long kontrak_id { get; set; } //bigint()
        public decimal? jumlah_item_pesan { get; set; } //numeric()
        public decimal? total_transaksi_pesan { get; set; } //numeric()
    }


    public record tr_kontrak_spjb_detail_update_penambahan_pesan
    {
        public long? kontrak_detail_item_id { get; set; } //Int64(-1)
        public long? kontrak_id { get; set; } //Int64(-1)
        public decimal? qty_pesan { get; set; } //Decimal(-1)
        public decimal? sub_total_pesan { get; set; } //Decimal(-1)
    }

    public record tr_kontrak_spjb_update_penambahan_terima
    {
        public long kontrak_id { get; set; } //bigint()
        public decimal? jumlah_item_terima { get; set; } //numeric()
        public decimal? total_transaksi_terima { get; set; } //numeric()
    }


    public record tr_kontrak_spjb_detail_update_penambahan_terima
    {
        public long? kontrak_detail_item_id { get; set; } //Int64(-1)
        public long? kontrak_id { get; set; } //Int64(-1)
        public decimal? qty_terima { get; set; } //Decimal(-1)
        public decimal? sub_total_terima { get; set; } //Decimal(-1)
    }

    #endregion

    #region Header

    public record tr_kontrak_spjb
    {
        public long? kontrak_id { get; set; } //bigint()

        //setup supplier
        public Int16? id_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //String(-1)
        public string nama_supplier { get; set; } //String(-1)
        public string alamat_supplier { get; set; } //String(-1)

        public string nomor_kontrak_spjb { get; set; } //character varying(20)
        public string nomor_kontrak { get; set; } //character varying(30)
        public string judul_kontrak { get; set; } //character varying(200)
        public DateTime? tanggal_ttd_kontrak { get; set; } //date()
        public DateTime? tanggal_berlaku_kontrak { get; set; } //date()
        public DateTime? tanggal_berakhir_kontrak { get; set; } //date()
        public decimal? jumlah_item_kontrak { get; set; } //numeric()
        public decimal? jumlah_item_pesan { get; set; } //numeric()
        public decimal? jumlah_item_terima { get; set; } //numeric()
        public decimal? total_transaksi_kontrak { get; set; } //numeric()
        public decimal? total_transaksi_pesan { get; set; } //numeric()
        public decimal? total_transaksi_terima { get; set; } //numeric()
        public string tahun_anggaran { get; set; } //character(4)
        public string keterangan { get; set; } //character varying(200)
        public string status_transaksi { get; set; } //character varying(10)
        public bool? is_closed { get; set; } //boolean()
        internal short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        internal short? user_validated { get; set; } //smallint()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        internal short? user_canceled { get; set; } //smallint()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying(50)
        internal short? user_closed { get; set; } //smallint()
        public DateTime? time_closed { get; set; } //timestamp without time zone()
        public string reason_closed { get; set; } //character varying(50)
        internal short? user_revision { get; set; } //smallint()
        public DateTime? time_revision { get; set; } //timestamp without time zone()
        public string reason_revision { get; set; } //character varying(50)
        public Int16? jumlah_revisi { get; set; } //smallint()
        public string nomor_kontrak_spjb_asli { get; set; } //character varying(20)
    }

    public record tr_kontrak_spjb_insert
    {
        public Int16? id_supplier { get; set; } //smallint()
        public string nomor_kontrak_spjb { get; set; } //character varying(20)
        public string nomor_kontrak { get; set; } //character varying(30)
        public string judul_kontrak { get; set; } //character varying(200)
        public DateTime? tanggal_ttd_kontrak { get; set; } //date()
        public DateTime? tanggal_berlaku_kontrak { get; set; } //date()
        public DateTime? tanggal_berakhir_kontrak { get; set; } //date()
        public decimal? jumlah_item_kontrak { get; set; } //numeric()
        //public decimal? jumlah_item_pesan { get; set; } //numeric()
        //public decimal? jumlah_item_terima { get; set; } //numeric()
        public decimal? total_transaksi_kontrak { get; set; } //numeric()
        //public decimal? total_transaksi_pesan { get; set; } //numeric()
        //public decimal? total_transaksi_terima { get; set; } //numeric()
        public string tahun_anggaran { get; set; } //character(4)
        public string keterangan { get; set; } //character varying(200)
        internal short? user_inputed { get; set; } //smallint()

        public List<tr_kontrak_spjb_detail_item_insert> details { get; init; }
    }

    #endregion

    #region DETAIL ITEM


    public record tr_kontrak_spjb_get_sisa_qty
    {

        public long kontrak_id { get; set; } //Int64(-1)
        public long kontrak_detail_item_id { get; set; } //Int64(-1)
        public int? id_item { get; set; } //integer()
        public string nama_item { get; set; } //character varying()
        public decimal? sisa_qty_pesan { get; set; } //numeric()
        public decimal? sisa_qty_terima { get; set; } //numeric()

    }

    public record tr_kontrak_spjb_detail_item
    {


        public long? kontrak_detail_item_id { get; set; } //Int64(-1)
        public long? kontrak_id { get; set; } //Int64(-1)
        public short? no_urut { get; set; } //Int16(-1)
        public int? id_item { get; set; } //Int32(-1)
        public string kode_item { get; set; } //String(-1)
        public string barcode { get; set; } //String(-1)
        public string nama_item { get; set; } //String(-1)
        public DateTime? tanggal_maksimal_expired_date { get; set; } //DateTime(-1)
        public decimal? qty_kontrak_satuan_besar { get; set; } //Decimal(-1)
        public string kode_satuan_besar { get; set; } //String(-1)
        public short? isi { get; set; } //Int16(-1)
        public decimal? qty_kontrak { get; set; } //Decimal(-1)
        public decimal? qty_pesan { get; set; } //Decimal(-1)
        public decimal? qty_terima { get; set; } //Decimal(-1)
        public decimal? harga_satuan { get; set; } //Decimal(-1)
        public decimal? sub_total_kontrak { get; set; } //Decimal(-1)
        public decimal? sub_total_pesan { get; set; } //Decimal(-1)
        public decimal? sub_total_terima { get; set; } //Decimal(-1)

    }

    public record tr_kontrak_spjb_detail_item_insert
    {

        internal long? kontrak_id { get; set; } //bigint()
        public Int16? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public DateTime? tanggal_maksimal_expired_date { get; set; } //date()
        public decimal? qty_kontrak_satuan_besar { get; set; } //numeric()
        public string kode_satuan_besar { get; set; } //character varying(10)
        public Int16? isi { get; set; } //smallint()
        public decimal? qty_kontrak { get; set; } //numeric()
        //public decimal? qty_pesan { get; set; } //numeric()
        //public decimal? qty_terima { get; set; } //numeric()
        public decimal? harga_satuan { get; set; } //numeric()
        public decimal? sub_total_kontrak { get; set; } //numeric()
        //public decimal? sub_total_pesan { get; set; } //numeric()
        //public decimal? sub_total_terima { get; set; } //numeric()

    }

    #endregion

    #region DETAIL UPLOAD

    public record tr_kontrak_spjb_detail_upload
    {

        public long? kontrak_detail_upload_id { get; set; } //bigint()
        public long? kontrak_id { get; set; } //bigint()
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


    public record tr_kontrak_spjb_detail_upload_insert
    {
        public long? kontrak_id { get; set; } //bigint()
        public string jenis_dokumen { get; set; } //character varying(30)
        internal string url_dokumen { get; set; } //text()
        public string keterangan { get; set; } //character varying(200)

        //[MaxFileSize(2 * 1024 * 1024)]
        //[AllowedExtensions(new string[] { ".pdf" })]
        public IFormFile file { get; set; }
    }

    #endregion

}
