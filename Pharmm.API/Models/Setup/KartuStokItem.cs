using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_kartu_stok_item_stok_akhr
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public decimal? stok_akhir { get; set; } //numeric()
        public decimal? nominal_akhir { get; set; } //numeric()

    }

    public record mm_kartu_stok_item
    {

        public long? id_kartu_stok_item { get; set; } //bigint()
        public string tahun { get; set; } //character(4)
        public string bulan { get; set; } //character(2)
        public DateTime? tanggal { get; set; } //date()
        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string nomor_ref_transaksi { get; set; } //character varying(20)
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? stok_masuk { get; set; } //numeric()
        public decimal? nominal_masuk { get; set; } //numeric()
        public decimal? stok_keluar { get; set; } //numeric()
        public decimal? nominal_keluar { get; set; } //numeric()
        public decimal? stok_akhir { get; set; } //numeric()
        public decimal? nominal_akhir { get; set; } //numeric()
        public string keterangan { get; set; } //character varying(70)
        public short? user_inputed { get; set; } //integer()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()

    }


    public record mm_kartu_stok_item_insert_penambahan_stok
    {
        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string nomor_ref_transaksi { get; set; } //character varying(20)
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? stok_masuk { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? nominal_masuk { get; set; } //numeric()
        public string keterangan { get; set; } //character varying(70)
        internal short? user_inputed { get; set; } //integer()

    }



    public record mm_kartu_stok_item_insert_pengurangan_stok
    {
        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string nomor_ref_transaksi { get; set; } //character varying(20)
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? stok_keluar { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? nominal_keluar { get; set; } //numeric()
        public string keterangan { get; set; } //character varying(70)
        internal short? user_inputed { get; set; } //integer()
    }


    public record mm_kartu_stok_item_detail_batch_insert_penambahan_stok
    {

        public long? id_kartu_stok_item { get; set; } //bigint()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //timestamp without time zone()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? stok_masuk { get; set; } //numeric()
        public decimal? nominal_masuk { get; set; } //numeric()
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()

    }

    public record mm_kartu_stok_item_detail_batch_insert_pengurangan_stok
    {

        public long? id_kartu_stok_item { get; set; } //bigint()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //timestamp without time zone()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? stok_keluar { get; set; } //numeric()
        public decimal? nominal_keluar { get; set; } //numeric()
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()

    }


    #region Detail Batch

    public record mm_kartu_stok_item_detail_batch_stok_akhr
    {

        public decimal? stok_akhir { get; set; } //numeric()
        public decimal? nominal_akhir { get; set; } //numeric()

    }

    public record mm_kartu_stok_item_detail_batch
    {

        public long? id_kartu_stok_item_detail_batch { get; set; } //bigint()
        public long? id_kartu_stok_item { get; set; } //bigint()
        public string tahun { get; set; } //character(4)
        public string bulan { get; set; } //character(4)
        public string batch_number { get; set; } //character varying(50)
        public DateTime? expired_date { get; set; } //date()
        public decimal? stok_awal { get; set; } //numeric()
        public decimal? nominal_awal { get; set; } //numeric()
        public decimal? stok_masuk { get; set; } //numeric()
        public decimal? nominal_masuk { get; set; } //numeric()
        public decimal? stok_keluar { get; set; } //numeric()
        public decimal? nominal_keluar { get; set; } //numeric()
        public decimal? stok_akhir { get; set; } //numeric()
        public decimal? nominal_akhir { get; set; } //numeric()
        public long? id_header_transaksi { get; set; } //bigint()
        public long? id_detail_transaksi { get; set; } //bigint()

    }


    #endregion

}
