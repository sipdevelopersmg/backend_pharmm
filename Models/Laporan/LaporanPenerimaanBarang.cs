using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Laporan
{
    #region Param
    public record param_laporan_penerimaan_per_vendor
    {

        public DateTime? start_date { get; set; } //date()
        public DateTime? end_date { get; set; } //date()
        public short? id_supplier { get; set; } //smallint()

    }

    #endregion

    public record laporan_penerimaan_per_vendor
    {

        public string kode_supplier { get; set; } //character varying()
        public string nama_supplier { get; set; } //character varying()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
        public decimal? qty { get; set; } //numeric()
        public decimal? harga_satuan { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()
        public short? user_inputed { get; set; } //smallint()
        public short? user_validated { get; set; } //smallint()

    }
}
