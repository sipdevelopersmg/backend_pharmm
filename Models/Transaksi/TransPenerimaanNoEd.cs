using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pharmm.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Param Function

    public record tr_penerimaan_no_ed_detail_item_update_qty_retur
    {

        public long? penerimaan_detail_item_id { get; set; } //bigint()
        public decimal? qty_diretur { get; set; } //numeric()

    }


    public record tr_penerimaan_no_ed_update_status_to_validated
    {
        public long penerimaan_id { get; set; }
        internal short user_validated { get; set; }
    }


    public record tr_penerimaan_no_ed_update_status_to_canceled
    {
        public long penerimaan_id { get; set; }
        internal short user_canceled { get; set; }
        public string reason_canceled { get; set; } //character varying(50)
    }

    #endregion

    #region Header


    public record tr_penerimaan_no_ed
    {
        public long? penerimaan_id { get; set; } //bigint()
        public string nomor_penerimaan { get; set; } //character varying()
        public DateTime? tanggal_penerimaan { get; set; } //date()
        public string kode_jenis_penerimaan { get; set; } //character varying()
        public string jenis_penerimaan { get; set; } //character varying()
        public bool is_account_payable { get; set; } //boolean()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public short? id_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying()
        public string nama_supplier { get; set; } //character varying()
        public string alamat_supplier { get; set; } //character varying()
        public long? pemesanan_id { get; set; } //bigint()
        public string nomor_pemesanan { get; set; } //character varying()
        public string nomor_surat_jalan_supplier { get; set; } //character varying()
        public DateTime? tanggal_surat_jalan_supplier { get; set; } //date()
        public short? id_shipping_method { get; set; } //smallint()
        public string shipping_method { get; set; } //character varying()
        public short? id_payment_term { get; set; } //smallint()
        public string payment_term { get; set; } //character varying()
        public DateTime? tanggal_jatuh_tempo_bayar { get; set; } //date()
        public string keterangan { get; set; } //character varying()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? sub_total_1 { get; set; } //numeric()
        public decimal? total_disc { get; set; } //numeric()
        public decimal? sub_total_2 { get; set; } //numeric()
        public decimal? total_tax { get; set; } //numeric()
        public string status_transaksi { get; set; }
        public decimal? total_transaksi { get; set; } //numeric()
        public decimal? biaya_kirim { get; set; } //numeric()
        public decimal? biaya_asuransi { get; set; } //numeric()
        public decimal? biaya_lain { get; set; } //numeric()
        public decimal? potongan_nominal { get; set; } //numeric()
        public decimal? potongan_prosentase { get; set; } //numeric()
        public decimal? total_uang_muka { get; set; } //numeric()
        public decimal? total_tagihan { get; set; } //numeric()
        public short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //smallint()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //smallint()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()

        //private string _details { get; set; } //String(-1)
        //public List<tr_penerimaan_no_ed_detail_item> details
        //{
        //    get
        //    {
        //        return JsonConvert.DeserializeObject<List<tr_penerimaan_no_ed_detail_item>>(this._details);
        //    }

        //    set
        //    {
        //        this._details = JsonConvert.SerializeObject(value);
        //    }
        //}
    }

    public record tr_penerimaan_no_ed_insert
    {
        public string nomor_penerimaan { get; set; }
        public DateTime tanggal_penerimaan { get; set; }
        public string kode_jenis_penerimaan { get; set; }
        public short id_stockroom { get; set; }
        public short id_supplier { get; set; }
        public long? pemesanan_id { get; set; }
        public string nomor_surat_jalan_supplier { get; set; }
        public DateTime tanggal_surat_jalan_supplier { get; set; }
        public short? id_shipping_method { get; set; }
        public short? id_payment_term { get; set; }
        public DateTime tanggal_jatuh_tempo_bayar { get; set; }
        public string keterangan { get; set; }
        public decimal jumlah_item { get; set; }
        public decimal sub_total_1 { get; set; }
        public decimal total_disc { get; set; }
        public decimal sub_total_2 { get; set; }
        public decimal total_tax { get; set; }
        public decimal total_transaksi { get; set; }
        public decimal biaya_kirim { get; set; }
        public decimal biaya_asuransi { get; set; }
        public decimal biaya_lain { get; set; }
        public decimal potongan_nominal { get; set; }
        public decimal potongan_prosentase { get; set; }
        public decimal total_uang_muka { get; set; }
        public decimal total_tagihan { get; set; }
        internal short user_inputed { get; set; }
        internal bool is_with_ed { get => false; }

        public List<tr_penerimaan_no_ed_detail_item_insert> details { get; init; }
    }


    #endregion


    #region Detail Item

    public record tr_penerimaan_no_ed_detail_item
    {
        public long? penerimaan_detail_item_id { get; set; } //bigint()
        public long? penerimaan_id { get; set; } //bigint()
        public long? pemesanan_id { get; set; } //bigint()
        public long? pemesanan_detail_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()

        //setup item
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //String(-1)
        public string barcode { get; set; } //String(-1)
        public string nama_item { get; set; } //String(-1)

        public decimal? qty_satuan_besar { get; set; } //numeric()
        public string kode_satuan_besar { get; set; } //character varying(10)
        public decimal? harga_satuan_besar { get; set; } //numeric()
        public short? isi { get; set; } //smallint()
        public decimal qty_terima { get; set; } //numeric()
        public decimal? qty_diretur { get; set; } //numeric()
        public decimal? harga_satuan { get; set; } //numeric()
        public decimal? disc_prosentase_1 { get; set; } //numeric()
        public decimal? disc_nominal_1 { get; set; } //numeric()
        public decimal? disc_prosentase_2 { get; set; } //numeric()
        public decimal? disc_nominal_2 { get; set; } //numeric()
        public decimal? harga_satuan_brutto { get; set; } //numeric()
        public decimal? tax_prosentase { get; set; } //numeric()
        public decimal? tax_nominal { get; set; } //numeric()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()
    }

    public record tr_penerimaan_no_ed_detail_item_insert
    {
        internal long penerimaan_id { get; set; }
        public long? pemesanan_id { get; set; }
        public long? pemesanan_detail_id { get; set; }
        public short no_urut { get; set; }
        public int id_item { get; set; }
        public decimal qty_satuan_besar { get; set; }
        public string kode_satuan_besar { get; set; }
        public decimal harga_satuan_besar { get; set; }
        public short isi { get; set; }
        public decimal qty_terima { get; set; }
        public decimal harga_satuan { get; set; }
        public decimal disc_prosentase_1 { get; set; }
        public decimal disc_nominal_1 { get; set; }
        public decimal disc_prosentase_2 { get; set; }
        public decimal disc_nominal_2 { get; set; }
        public decimal harga_satuan_brutto { get; set; }
        public decimal tax_prosentase { get; set; }
        public decimal tax_nominal { get; set; }
        public decimal harga_satuan_netto { get; set; }
        public decimal sub_total { get; set; }
    }

    public record tr_penerimaan_no_ed_get_sisa_qty_retur
    {

        public long? penerimaan_id { get; set; } //bigint()
        public long? penerimaan_detail_item_id { get; set; } //bigint()
        public int? id_item { get; set; } //integer()
        public string nama_item { get; set; } //character varying()
        public decimal? sisa_qty_diretur { get; set; } //numeric()

    }

    #endregion


    #region Detail Upload

    public record tr_penerimaan_no_ed_detail_upload
    {

        public long? penerimaan_detail_upload_id { get; set; } //bigint()
        public long? penerimaan_id { get; set; } //bigint()
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

    public record tr_penerimaan_no_ed_detail_upload_insert
    {
        public long penerimaan_id { get; set; }
        public string jenis_dokumen { get; set; }
        internal string url_dokumen { get; set; }
        public string keterangan { get; set; }
        public IFormFile file { get; set; }
    }

    #endregion

}
