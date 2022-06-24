using Newtonsoft.Json;
using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{
    #region Header

    public record set_harga_order
    {
        public long? set_harga_order_id { get; set; } //bigint()
        public string nomor_harga_order { get; set; } //character varying(20)

        public short? id_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying(20)
        public string nama_supplier { get; set; } //character varying(20)
        public string alamat_supplier { get; set; } //character varying(20)

        public DateTime? tanggal_berlaku { get; set; } //timestamp without time zone()
        public DateTime? tanggal_berakhir { get; set; } //timestamp without time zone()
        public short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()

        //private string _details { get; set; }
        //public List<set_harga_order_detail> details
        //{
        //    get
        //    {
        //        return JsonConvert.DeserializeObject<List<set_harga_order_detail>>(this._details);
        //    }

        //    set
        //    {
        //        this._details = JsonConvert.SerializeObject(value);
        //    }
        //}
    }

    public record set_harga_order_insert
    {
        public string nomor_harga_order { get; set; } //character varying(20)
        public short? id_supplier { get; set; } //smallint()
        public DateTime? tanggal_berlaku { get; set; } //timestamp without time zone()
        internal short? user_inputed { get; set; } //smallint()

        //detail
        public List<set_harga_order_detail_insert> details { get; init; }
    }


    #endregion


    #region Detail
    public record set_harga_order_detail
    {
        public long? set_harga_order_detail_id { get; set; } //bigint()
        public long? set_harga_order_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()

        //setup item
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying(20)
        public string barcode { get; set; } //character varying(30)
        public string nama_item { get; set; } //character varying(100)

        public decimal? harga_order { get; set; } //numeric()
        public decimal? disc_prosentase_1 { get; set; } //numeric()
        public decimal? disc_prosentase_2 { get; set; } //numeric()
        public decimal? harga_order_netto { get; set; } //numeric()
        public bool? is_berlaku { get; set; } //boolean()
    }
    public record set_harga_order_detail_insert
    {
        internal long? set_harga_order_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? harga_order { get; set; } //numeric()
        public decimal? disc_prosentase_1 { get; set; } //numeric()
        public decimal? disc_prosentase_2 { get; set; } //numeric()
        public decimal? harga_order_netto { get; set; } //numeric()
        public DateTime? tanggal_berlaku { get; set; } //date()
    }

    public record set_harga_order_detail_update_berlaku
    {
        public int? id_item { get; set; } //integer()
        public DateTime? tanggal_berakhir { get; set; } //date()
    
    }

    public record set_harga_order_detail_berlaku
    {
        public int id_item { get; set; }
        public string kode_item { get; set; }
        public string nama_item { get; set; }
        public short id_grup_item { get; set; }
        public string grup_item { get; set; }
        public short id_pabrik { get; set; }
        public string nama_pabrik { get; set; }
        public Decimal harga_order { get; set; }
        public Decimal diskon1 { get; set; }
        public Decimal diskon2 { get; set; }
        public Decimal harga_order_netto { get; set; }
        public Decimal harga_order_baru { get; set; }
        public Decimal diskon1_baru { get; set; }
        public Decimal diskon2_baru { get; set; }
        public Decimal harga_order_netto_baru { get; set; }
    }

    public record get_barang_input_harga_order_by_id_supplier
    {
        public string notin { get; set; }
        public List<ParameterSearchModel> filters {get;set;}
    }
    #endregion
}
