using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Laporan
{
    #region Param


    public record param_laporan_retur_pembelian_by_periode
    {

        public int? bulan { get; set; } //integer()
        public int? tahun { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()

    }

    #endregion

    public record laporan_retur_pembelian_by_periode
    {

        public string nomor_retur_pembelian { get; set; } //character varying()
        public DateTime? tanggal_retur_pembelian { get; set; } //date()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
        public decimal? qty_retur { get; set; } //numeric()
        public decimal? harga_satuan_retur { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()

    }



}
