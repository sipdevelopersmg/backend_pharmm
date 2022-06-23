using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    #region Update Param

    public record mm_setup_stok_item_update_stok
    {

        public int id_item { get; set; } //integer()
        public Int16 id_stockroom { get; set; } //smallint()
        public decimal qty_on_hand { get; set; } //numeric()

    }


    public record mm_setup_stok_item_detail_update_penambahan_stok
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string batch_number { get; set; } //character varying(50)
        public DateTime? expired_date { get; set; } //date()
        public decimal? qty_on_hand { get; set; } //numeric()
        public string barcode_batch_number { get; set; }
    }

    public record mm_setup_stok_item_detail_update_penambahan_stok_with_harga
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string batch_number { get; set; } //character varying(50)
        public DateTime? expired_date { get; set; } //date()
        public decimal? qty_on_hand { get; set; } //numeric()
        public string barcode_batch_number { get; set; }
        public decimal? harga_satuan_netto { get; set; } //numeric()
    }


    public record mm_setup_stok_item_detail_update_pengurangan_stok
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string batch_number { get; set; } //character varying(50)
        public DateTime? expired_date { get; set; } //date()
        public decimal? qty_on_hand { get; set; } //numeric()
    }

    #endregion

    //untuk get sisa stok di transaksi
    public record mm_setup_stok_item_get_sisa_stok_lock
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? qty_stok_kritis { get; set; } //numeric()

    }

    public record mm_setup_stok_item_lookup
    {

        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public short? id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; }
        public string nama_stockroom { get; set; }

        //untuk yg berkaitan dengan uang (kas masuk / keluar)
        public decimal? harga_beli_terakhir { get; set; }

        //untuk yg berkaitan dgn stok (tidak ada kas masuk / keluar yg ada hanya stok masuk / keluar)
        public decimal? hpp_average { get; set; }
        
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? qty_stok_kritis { get; set; } //numeric()


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

    public record mm_setup_stok_item_data_barang_and_stockroom
    {

        public int? id_item { get; set; } //integer()
        public string barcode { get; set; } //character varying()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public short? id_stockroom { get; set; } //smallint()
        public string nama_stockroom { get; set; } //character varying()

    }


    public record mm_setup_stok_item
    {

        public int? id_item { get; set; } //integer()
        public Int16? id_stockroom { get; set; } //smallint()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? qty_stok_kritis { get; set; } //numeric()

    }

    public record mm_setup_stok_item_detail_ed
    {

        public int? id_item { get; set; } //integer()
        public Int16? id_stockroom { get; set; } //smallint()
        public DateTime? expired_date { get; set; } //date()
        public string batch_number { get; set; } //character varying(50)
        public decimal? qty_on_hand { get; set; } //numeric()

    }

    #region Detail Batch

    public record mm_setup_stok_item_detail_batch
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string batch_number { get; set; } //character varying(50)
        public DateTime? expired_date { get; set; } //date()
        public string barcode_batch_number { get; set; } //character varying(80)
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? qty_on_hand { get; set; } //numeric()
    }

    public record mm_setup_stok_item_detail_batch_insert
    {

        public int? id_item { get; set; } //integer()
        public short? id_stockroom { get; set; } //smallint()
        public string batch_number { get; set; } //character varying()
        public decimal? qty_on_hand { get; set; } //numeric()
        public DateTime? expired_date { get; set; } //timestamp without time zone()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()

    }


    //untuk tr mutasi
    public record mm_setup_stok_item_detail_batch_lookup
    {

        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public string kode_satuan { get; set; }
        public string nama_satuan { get; set; }


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

    public record mm_setup_stok_item_detail_no_batch_lookup
    {

        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public string kode_satuan { get; set; }
        public string nama_satuan { get; set; }


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

    //untuk repacking / assembly hanya menampilkan 1 satuan
    public record mm_setup_stok_item_detail_batch_lookup_satuan
    {

        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public string kode_satuan { get; set; }
        public string nama_satuan { get; set; }
    }

    public record mm_setup_stok_item_detail_batch_get_sisa_stok_lock
    {

        public int? id_item { get; set; } //integer()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public decimal? qty_on_hand { get; set; } //numeric()

    }


    //lookup barang urai
    public record mm_setup_stok_item_lookup_urai
    {
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public decimal? hpp_average { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? qty_urai { get; set; } //numeric()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
    }

    //lookup barang assembly
    public record mm_setup_stok_item_detail_batch_lookup_assembly
    {
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public string barcode_batch_number { get; set; } //character varying()
        public decimal? harga_satuan_netto { get; set; } //numeric()
        public string batch_number { get; set; } //character varying()
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? hpp_average { get; set; }
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? qty_assembly { get; set; } //numeric()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
    }

    #endregion

}
