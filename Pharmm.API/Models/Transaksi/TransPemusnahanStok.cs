using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Header

    public record tr_pemusnahan_stok
    {

        public long pemusnahan_stok_id { get; set; } //bigint()
        public string nomor_pemusnahan_stok { get; set; } //character varying()
        public DateTime? tanggal_pemusnahan_stok { get; set; } //date()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public string status_transaksi { get; set; } //character varying()
        public decimal jumlah_item { get; set; } //numeric()
        public decimal total_transaksi { get; set; } //numeric()
        public string keterangan { get; set; } //character varying()
        public int user_inputed { get; set; } //integer()
        public DateTime time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //integer()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //integer()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()

    }


    public record tr_pemusnahan_stok_insert
    {

        public string nomor_pemusnahan_stok { get; set; } //character varying()
        public DateTime? tanggal_pemusnahan_stok { get; set; } //timestamp without time zone()
        public short id_stockroom { get; set; } //smallint()
        public decimal jumlah_item { get; set; } //numeric()
        public decimal total_transaksi { get; set; } //numeric()
        public string keterangan { get; set; } //character varying()
        internal short user_inputed { get; set; } //integer()
        public List<tr_pemusnahan_stok_detail_insert> details { get; set; }
    }


    public record tr_pemusnahan_stok_update_to_validated
    {

        public long pemusnahan_stok_id { get; set; } //bigint()
        internal short? user_validated { get; set; } //integer()

    }

    public record tr_pemusnahan_stok_update_to_canceled
    {

        public long pemusnahan_stok_id { get; set; } //bigint()
        internal short? user_canceled { get; set; } //integer()
        public string reason_canceled { get; set; } //character varying()

    }


    #endregion

    #region Detail

    public record tr_pemusnahan_stok_detail
    {

        public long pemusnahan_stok_detail_id { get; set; } //bigint()
        public long pemusnahan_stok_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public decimal qty { get; set; } //numeric()
        public decimal hpp_satuan { get; set; } //numeric()
        public decimal sub_total { get; set; } //numeric()

    }

    public record tr_pemusnahan_stok_detail_insert
    {

        internal long pemusnahan_stok_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //timestamp without time zone()
        public decimal qty { get; set; } //numeric()
        public decimal hpp_satuan { get; set; } //numeric()
        public decimal sub_total { get; set; } //numeric()

    }


    #endregion
}
