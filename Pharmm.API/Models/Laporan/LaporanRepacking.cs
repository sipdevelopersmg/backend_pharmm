using Pharmm.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Laporan
{
    #region Param

    public record param_laporan_repacking_per_faktur
    {

        public string nomor_repacking { get; set; } //character varying()
    }

    #endregion

    public record laporan_repacking_per_faktur
    {

        public long? urut { get; set; } //bigint()
        public string nomor_repacking { get; set; } //character varying()
        public DateTime? tanggal_repacking { get; set; } //date()
        public string keterangan_repacking { get; set; } //character varying()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public string kode_item_dari { get; set; } //character varying()
        public string nama_item_dari { get; set; } //character varying()
        public decimal? qty_dari { get; set; } //numeric()
        public string kode_satuan_dari { get; set; } //character varying()
        public string nama_satuan_dari { get; set; } //character varying()
        public string kode_item_jadi { get; set; } //character varying()
        public string nama_item_jadi { get; set; } //character varying()
        public decimal? qty_jadi { get; set; } //numeric()
        public string kode_satuan_jadi { get; set; } //character varying()
        public string nama_satuan_jadi { get; set; } //character varying()
        public decimal? hpp_satuan { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()
        public short? user_inputed { get; set; } //smallint()
        public short? user_validated { get; set; } //smallint()
        public string user_name_validated { get => HttpDataPisHelper.GetDataUser((user_validated is null ? (short)0 : (short)user_validated)).Result?.full_name?.ToString(); } //smallint()

    }

}
