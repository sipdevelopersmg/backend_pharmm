using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{

    public record akun_tr_hutang_supplier
    {

        public long? hutang_supplier_id { get; set; } //bigint()
        public DateTime? tanggal_hutang_supplier { get; set; } //date()
        public DateTime? tanggal_jatuh_tempo_pembayaran { get; set; } //date()
        public decimal? jumlah_hutang { get; set; } //numeric()
        public decimal? sudah_titip_tagihan { get; set; } //numeric()
        public decimal? belum_titip_tagihan { get; set; } //numeric()
        public decimal? sudah_dibayar { get; set; } //numeric()
        public decimal? belum_dibayar { get; set; } //numeric()
        public short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_titip_tagihan { get; set; } //smallint()
        public DateTime? time_titip_tagihan { get; set; } //timestamp without time zone()
        public short? user_bayar { get; set; } //smallint()
        public DateTime? time_bayar { get; set; } //timestamp without time zone()

    }


    public record akun_tr_hutang_supplier_insert
    {

        public DateTime? tanggal_hutang_supplier { get; set; } //timestamp without time zone()
        public DateTime? tanggal_jatuh_tempo_pembayaran { get; set; } //timestamp without time zone()
        public decimal? jumlah_hutang { get; set; } //numeric()
        public decimal? sudah_titip_tagihan { get; set; } //numeric()
        public decimal? belum_titip_tagihan { get; set; } //numeric()
        public decimal? sudah_dibayar { get; set; } //numeric()
        public decimal? belum_dibayar { get; set; } //numeric()
        internal short? user_inputed { get; set; } //smallint()

    }


}
