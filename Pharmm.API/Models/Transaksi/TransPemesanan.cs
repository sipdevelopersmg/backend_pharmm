using Newtonsoft.Json;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region function Param

    public record tr_pemesanan_update_penambahan_terima
    {
        public long pemesanan_id { get; set; } //bigint()
        public decimal? jumlah_item_terima { get; set; } //numeric()
        public decimal? total_transaksi_terima { get; set; } //numeric()
    }


    public record tr_pemesanan_detail_update_penambahan_terima
    {
        public long? pemesanan_detail_id { get; set; } //Int64(-1)
        public long? pemesanan_id { get; set; } //Int64(-1)
        public decimal? qty_terima { get; set; } //Decimal(-1)
        public decimal? sub_total_terima { get; set; } //Decimal(-1)
    }

    #endregion

    #region Lookup

    //untuk lookup barang
    public record tr_pemesanan_lookup_barang
    {

        public long? kontrak_id { get; set; } //bigint()
        public long? kontrak_detail_item_id { get; set; } //bigint()
        public long? set_harga_order_id { get; set; } //bigint()
        public long? set_harga_order_detail_id { get; set; } //bigint()
        public int? id_item { get; set; } //integer()
        public string barcode { get; set; } //character varying()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal harga_satuan { get; set; }

        private string _satuans { get; set; }

        public List<mm_setup_item_satuan> satuans
        {
            get => JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);

            set
            {
                this._satuans = JsonConvert.SerializeObject(value);
            }
        }

    }

    #endregion

    #region Header

    public record tr_pemesanan
    {

        public long? pemesanan_id { get; set; } //Int64(-1)
        public string nomor_pemesanan { get; set; } //String(-1)
        public DateTime? tanggal_pemesanan { get; set; } //DateTime(-1)
        public DateTime? tanggal_expired_pemesanan { get; set; } //DateTime(-1)

        //setup stockroom
        public short? id_stockroom { get; set; } //Int16(-1)
        public string kode_stockroom { get; set; } //String(-1)
        public string nama_stockroom { get; set; } //String(-1)

        //setup stupplier
        public short? id_supplier { get; set; } //Int16(-1)
        public string kode_supplier { get; set; } //String(-1)
        public string nama_supplier { get; set; } //String(-1)
        public string alamat_supplier { get; set; } //String(-1)
        
        public string keterangan { get; set; } //String(-1)
        public decimal? jumlah_item_pesan { get; set; } //Decimal(-1)
        public decimal? jumlah_item_terima { get; set; } //Decimal(-1)
        public decimal? sub_total_1 { get; set; } //Decimal(-1)
        public decimal? total_disc { get; set; } //Decimal(-1)
        public decimal? sub_total_2 { get; set; } //Decimal(-1)
        public decimal? total_tax { get; set; } //Decimal(-1)
        public decimal? total_transaksi_pesan { get; set; } //Decimal(-1)
        public decimal? total_transaksi_terima { get; set; } //Decimal(-1)
        public string status_penerimaan { get; set; } //String(-1)
        public string status_transaksi { get; set; } //String(-1)
        public Boolean is_closed { get; set; } //Boolean(-1)
        public short? user_inputed { get; set; } //Int16(-1)
        public DateTime? time_inputed { get; set; } //DateTime(-1)
        public short? user_validated { get; set; } //Int16(-1)
        public DateTime? time_validated { get; set; } //DateTime(-1)
        public short? user_canceled { get; set; } //Int16(-1)
        public DateTime? time_canceled { get; set; } //DateTime(-1)
        public string reason_canceled { get; set; } //String(-1)
        public short? user_closed { get; set; } //Int16(-1)
        public DateTime? time_closed { get; set; } //DateTime(-1)
        public string reason_closed { get; set; } //String(-1)
        public short? user_revision { get; set; } //Int16(-1)
        public DateTime? time_revision { get; set; } //DateTime(-1)
        public string reason_revision { get; set; } //String(-1)
        public short? jumlah_revisi { get; set; } //Int16(-1)
        public short? cetakan_ke { get; set; } //Int16(-1)
        public string nomor_pemesanan_asli { get; set; } //String(-1)

        //private string _details { get; set; }
        //public List<tr_pemesanan_detail> details
        //{
        //    get
        //    {
        //        return JsonConvert.DeserializeObject<List<tr_pemesanan_detail>>(this._details);
        //    }

        //    set
        //    {
        //        this._details = JsonConvert.SerializeObject(value);
        //    }
        //}

    }

    public record tr_pemesanan_insert
    {
        public string nomor_pemesanan { get; set; }
        public DateTime? tanggal_pemesanan { get; set; }
        public DateTime? tanggal_expired_pemesanan { get; set; }
        public short id_stockroom { get; set; }
        public short id_supplier { get; set; }
        public string keterangan { get; set; }
        public decimal? jumlah_item_pesan { get; set; }
        public decimal? sub_total_1 { get; set; }
        public decimal? total_disc { get; set; }
        public decimal? sub_total_2 { get; set; }
        public decimal? total_tax { get; set; }
        public decimal? total_transaksi_pesan { get; set; }
        internal short user_inputed { get; set; }

        public List<tr_pemesanan_detail_insert> details { get; init; }
    }

    public record tr_pemesanan_update_status_to_validated
    {
        public long pemesanan_id { get; set; }
        internal short user_validated { get; set; }
    }

    public record tr_pemesanan_update_status_to_closed
    {
        public long pemesanan_id { get; set; }
        internal short user_closed { get; set; }
        public string reason_closed { get; set; } //character varying(50)
    }

    public record tr_pemesanan_update_status_to_canceled
    {
        public long pemesanan_id { get; set; }
        internal short user_canceled { get; set; }
        public string reason_canceled { get; set; } //character varying(50)
    }

    public record tr_pemesanan_get_jumlah_total_pesan_terima
    {

        public long? pemesanan_id { get; set; } //bigint()
        public decimal? jumlah_item_terima { get; set; } //numeric()
        public decimal? total_transaksi_terima { get; set; } //numeric()
        public decimal? jumlah_item_pesan { get; set; } //numeric()
        public decimal? total_transaksi_pesan { get; set; } //numeric()

    }

    #endregion


    #region Detail


    public record tr_pemesanan_get_sisa_qty
    {

        public long pemesanan_id { get; set; } //Int64(-1)
        public long pemesanan_detail_id { get; set; } //Int64(-1)
        public int? id_item { get; set; } //integer()
        public string nama_item { get; set; } //character varying()
        public decimal? sisa_qty_terima { get; set; } //numeric()

    }


    //untuk get pemesanan qty terima 
    public record tr_pemesanan_detail_lock
    {

        public long? pemesanan_detail_id { get; set; } //bigint()
        public long? pemesanan_id { get; set; } //bigint()
        public long? kontrak_id { get; set; } //bigint()
        public long? kontrak_detail_item_id { get; set; } //bigint()
        public long? set_harga_order_id { get; set; } //bigint()
        public long? set_harga_order_detail_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty_satuan_besar { get; set; } //numeric()
        public string kode_satuan_besar { get; set; } //character varying()
        public decimal? harga_satuan_besar { get; set; } //numeric()
        public short? isi { get; set; } //smallint()
        public decimal? qty_pesan { get; set; } //numeric()
        public decimal? qty_terima { get; set; } //numeric()
        public decimal? harga_satuan { get; set; } //numeric()
        public decimal? disc_prosentase_1 { get; set; } //numeric()
        public decimal? disc_nominal_1 { get; set; } //numeric()
        public decimal? disc_prosentase_2 { get; set; } //numeric()
        public decimal? disc_nominal_2 { get; set; } //numeric()
        public decimal? harga_satuan_brutto { get; set; } //numeric()
        public decimal? tax_prosentase { get; set; } //numeric()
        public decimal? tax_nominal { get; set; } //numeric()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? sub_total_pesan { get; set; } //numeric()
        public decimal? sub_total_terima { get; set; } //numeric()

    }


    public record tr_pemesanan_detail
    {

        public long? pemesanan_detail_id { get; set; } //bigint()
        public long? pemesanan_id { get; set; } //bigint()
        public long? kontrak_id { get; set; } //bigint()
        public long? kontrak_detail_item_id { get; set; } //bigint()
        public long? set_harga_order_id { get; set; } //bigint()
        public long? set_harga_order_detail_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()

        //setup item
        public string kode_item { get; set; } //character varying(20)
        public string barcode { get; set; } //character varying(30)
        public string nama_item { get; set; } //character varying(100)

        public decimal? qty_satuan_besar { get; set; } //numeric()

        //setup satuan
        public string kode_satuan_besar { get; set; } //character varying(10)
        public string nama_satuan { get; set; } //character varying(20)

        public decimal? harga_satuan_besar { get; set; } //numeric()
        public short? isi { get; set; } //smallint()
        public decimal? qty_pesan { get; set; } //numeric()
        public decimal? qty_terima { get; set; } //numeric()
        public decimal? harga_satuan { get; set; } //numeric()
        public decimal? disc_prosentase_1 { get; set; } //numeric()
        public decimal? disc_nominal_1 { get; set; } //numeric()
        public decimal? disc_prosentase_2 { get; set; } //numeric()
        public decimal? disc_nominal_2 { get; set; } //numeric()
        public decimal? harga_satuan_brutto { get; set; } //numeric()
        public decimal? tax_prosentase { get; set; } //numeric()
        public decimal? tax_nominal { get; set; } //numeric()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? sub_total_pesan { get; set; } //numeric()
        public decimal? sub_total_terima { get; set; } //numeric()

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

    public record tr_pemesanan_detail_insert
    {
        internal long pemesanan_id { get; set; }
        public long? kontrak_id { get; set; }
        public long? kontrak_detail_item_id { get; set; }
        public long? set_harga_order_id { get; set; }
        public long? set_harga_order_detail_id { get; set; }
        public short no_urut { get; set; }
        public int id_item { get; set; }
        public decimal? qty_satuan_besar { get; set; }
        public string kode_satuan_besar { get; set; }
        public decimal? harga_satuan_besar { get; set; }
        public short isi { get; set; }
        public decimal? qty_pesan { get; set; }
        public decimal? harga_satuan { get; set; }
        public decimal? disc_prosentase_1 { get; set; }
        public decimal? disc_nominal_1 { get; set; }
        public decimal? disc_prosentase_2 { get; set; }
        public decimal? disc_nominal_2 { get; set; }
        public decimal? harga_satuan_brutto { get; set; }
        public decimal? tax_prosentase { get; set; }
        public decimal? tax_nominal { get; set; }
        public decimal? harga_satuan_netto { get; set; }
        public decimal? sub_total_pesan { get; set; }
    }

    #endregion


}
