using Newtonsoft.Json;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Param Update


    public record tr_retur_pembelian_no_ed_update_status_to_validated
    {
        public long retur_pembelian_id { get; set; }
        internal short user_validated { get; set; }
    }


    public record tr_retur_pembelian_no_ed_update_status_to_canceled
    {
        public long retur_pembelian_id { get; set; }
        internal short user_canceled { get; set; }
        public string reason_canceled { get; set; } //character varying(50)
    }

    #endregion


    #region Lookup


    #endregion

    #region Header

    public record tr_retur_pembelian_no_ed_cek_status
    {

        public bool? is_validated { get; set; } //boolean()
        public bool? is_canceled { get; set; } //boolean()

    }

    public record tr_retur_pembelian_no_ed
    {
        public long? retur_pembelian_id { get; set; } //bigint()
        public string nomor_retur_pembelian { get; set; } //character varying()
        public DateTime? tanggal_retur_pembelian { get; set; } //date()
        public DateTime? tanggal_jatuh_tempo_pelunasan_retur { get; set; } //date()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public short? id_mekanisme_retur { get; set; } //smallint()
        public string mekanisme_retur { get; set; } //character varying()
        public short? id_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying()
        public string nama_supplier { get; set; } //character varying()
        public string alamat_supplier { get; set; } //character varying()
        public long? penerimaan_id { get; set; } //bigint()
        public string nomor_penerimaan { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
        public decimal? jumlah_item_retur { get; set; } //numeric()
        public decimal? total_transaksi_retur { get; set; } //numeric()
        public decimal? jumlah_item_ditukar_dengan { get; set; } //numeric()
        public decimal? total_transaksi_ditukar_dengan { get; set; } //numeric()
        public decimal? total_transaksi_potong_tagihan { get; set; } //numeric()
        public bool? is_lunas { get; set; } //boolean()
        public decimal? belum_terbayar { get; set; } //numeric()
        public string nota_pajak { get; set; } //character varying()
        public short? user_inputed { get; set; } //integer()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //integer()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //integer()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()
        public short? user_lunas_tukar_barang { get; set; } //integer()
        public DateTime? time_lunas_tukar_barang { get; set; } //timestamp without time zone()
        public string status_transaksi { get; set; }
    }


    public record tr_retur_pembelian_no_ed_insert
    {

        public string nomor_retur_pembelian { get; set; } //character varying()
        public DateTime? tanggal_retur_pembelian { get; set; } //timestamp without time zone()
        public DateTime? tanggal_jatuh_tempo_pelunasan_retur { get; set; } //timestamp without time zone()
        public short? id_stockroom { get; set; } //smallint()
        public short? id_mekanisme_retur { get; set; } //smallint()
        public short? id_supplier { get; set; } //smallint()
        internal long? penerimaan_id { get => null; } //bigint()
        public string keterangan { get; set; } //character varying()
        public decimal? jumlah_item_retur { get; set; } //numeric()
        public decimal? total_transaksi_retur { get; set; } //numeric()
        public decimal? jumlah_item_ditukar_dengan { get; set; } //numeric()
        public decimal? total_transaksi_ditukar_dengan { get; set; } //numeric()
        public decimal? total_transaksi_potong_tagihan { get; set; } //numeric()
        public string nota_pajak { get; set; } //character varying()
        internal short? user_inputed { get; set; } //integer()
        public List<tr_retur_pembelian_no_ed_detail_insert> details { get; init; }
    }


    #endregion


    #region Detail

    public record tr_retur_pembelian_no_ed_detail
    {
        public long? retur_pembelian_detail_id { get; set; } //bigint()
        public long? retur_pembelian_id { get; set; } //bigint()
        public long? penerimaan_id { get; set; } //bigint()
        public long? penerimaan_detail_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_satuan_besar { get; set; } //numeric()
        public string kode_satuan_besar { get; set; } //character varying()
        public string nama_satuan_besar { get; set; } //character varying()
        public short? isi { get; set; } //smallint()
        public decimal qty_retur { get; set; } //numeric()
        public decimal? harga_satuan_retur { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()

    }

    public record tr_retur_pembelian_no_ed_detail_insert
    {

        internal long? retur_pembelian_id { get; set; } //bigint()
        internal long? penerimaan_id { get => null; } //bigint()
        internal long? penerimaan_detail_id { get => null; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty_satuan_besar { get; set; } //numeric()
        public string kode_satuan_besar { get; set; } //character varying()
        public short? isi { get; set; } //smallint()
        public decimal? qty_retur { get; set; } //numeric()
        public decimal? harga_satuan_retur { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()

    }

    #endregion

    #region Detail Penukaran

    public record tr_retur_pembelian_no_ed_detail_penukaran
    {

        public long? retur_pembelian_detail_penukaran_id { get; set; } //Int64(-1)
        public long? retur_pembelian_id { get; set; } //Int64(-1)
        public short? no_urut { get; set; } //Int16(-1)
        public int? id_item { get; set; } //Int32(-1)
        public decimal? qty_satuan_besar { get; set; } //Decimal(-1)
        public string kode_satuan_besar { get; set; } //String(-1)
        public short? isi { get; set; } //Int16(-1)
        public decimal? qty_penukaran { get; set; } //Decimal(-1)
        public decimal? harga_satuan_penukaran { get; set; } //Decimal(-1)
        public decimal? sub_total { get; set; } //Decimal(-1)
        public short? id_stockroom { get; set; } //Int16(-1)
        public string kode_stockroom { get; set; } //String(-1)
        public string nama_stockroom { get; set; } //String(-1)

    }


    #endregion
}
