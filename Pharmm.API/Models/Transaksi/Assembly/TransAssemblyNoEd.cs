using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Header

    public record tr_assembly_no_ed
    {

        public long assembly_id { get; set; } //bigint()
        public string nomor_assembly { get; set; } //character varying(20)
        public DateTime? tanggal_assembly { get; set; } //date()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        
        public decimal qty { get; set; } //numeric()
        public decimal total_nominal { get; set; } //numeric()
        public string status_transaksi { get; set; } //character varying(10)
        public decimal jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        public string keterangan_assembly { get; set; } //character varying(150)
        public int user_inputed { get; set; } //integer()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //integer()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //integer()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying(50)

    }


    public record tr_assembly_no_ed_insert
    {

        public string nomor_assembly { get; set; } //character varying()
        public DateTime? tanggal_assembly { get; set; } //timestamp without time zone()
        public short? id_stockroom { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty { get; set; } //numeric()
        public decimal hpp_satuan { get; set; } //numeric()
        public decimal total_nominal { get; set; } //numeric()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        public string keterangan_assembly { get; set; } //character varying()
        internal short? user_inputed { get; set; } //integer()
        public List<tr_assembly_no_ed_detail_insert> details { get; set; }

    }

    public record tr_assembly_no_ed_update_to_validated
    {

        public long assembly_id { get; set; } //bigint()
        internal short? user_validated { get; set; } //integer()

    }

    public record tr_assembly_no_ed_update_to_canceled
    {

        public long assembly_id { get; set; } //bigint()
        internal short? user_canceled { get; set; } //integer()
        public string reason_canceled { get; set; } //character varying()

    }


    #endregion

    #region Detail


    public record tr_assembly_no_ed_detail
    {

        public long assembly_detail_id { get; set; } //bigint()
        public long assembly_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item_child { get; set; } //integer()
        public string kode_item { get; set; }
        public string nama_item { get; set; }
        public string barcode { get; set; }
        public decimal qty { get; set; } //numeric()
        public decimal? hpp_satuan { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()

    }


    public record tr_assembly_no_ed_detail_insert
    {

        internal long? assembly_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item_child { get; set; } //integer()
        public decimal? qty { get; set; } //numeric()
        public decimal? hpp_satuan { get; set; } //numeric()
        public decimal? sub_total { get; set; } //numeric()

    }

    #endregion

}
