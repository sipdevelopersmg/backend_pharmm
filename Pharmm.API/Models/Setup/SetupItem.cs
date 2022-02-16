using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Utility.JsonTypeHelper.Helper;

namespace Pharmm.API.Models.Setup
{

    #region param function

    public record mm_setup_item_update_harga_perolehan
    {
        public int? id_item { get; set; } //integer()
        public decimal? qty_terima { get; set; } //numeric()
        public decimal? harga_beli_netto { get; set; } //numeric()

    }

    #endregion


    public record mm_setup_item_with_rak
    {

        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public decimal harga_beli_terakhir { get; set; } //numeric()
        public decimal hpp_average { get; set; } //numeric()
        public bool is_active { get; set; } //boolean()
        public bool is_obat { get; set; } //boolean()

    }

    public record mm_setup_item
    {
        public int? id_item { get; set; } //Int32(-1)

        //grup item
        public short? id_grup_item { get; set; } //Int16(-1)
        public string kode_grup_item { get; set; } //String(-1)
        public string grup_item { get; set; } //String(-1)

        //setup pabrik
        public short? id_pabrik { get; set; } //Int16(-1)
        public string kode_pabrik { get; set; } //String(-1)
        public string nama_pabrik { get; set; } //String(-1)
        public string alamat_pabrik { get; set; } //String(-1)

        //Setup supplier
        public short? id_supplier { get; set; } //Int16(-1)
        public string kode_supplier { get; set; } //String(-1)
        public string nama_supplier { get; set; } //String(-1)
        public string alamat_supplier { get; set; } //String(-1)
        
        public string kode_item { get; set; } //String(-1)
        public string barcode { get; set; } //String(-1)
        public string nama_item { get; set; } //String(-1)
        
        //Setup satuan
        public string kode_satuan { get; set; } //String(-1)
        public string nama_satuan { get; set; } //String(-1)
        
        //Setup temperature item
        public short? id_temperatur_item { get; set; } //Int16(-1)
        public string temperatur_item { get; set; } //String(-1)

        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? batas_maksimal_pesan { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_pakai { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_mutasi { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_jual { get; set; } //Decimal(-1)
        public decimal? batas_stok_kritis { get; set; } //Decimal(-1)
        public decimal? prosentase_stok_kritis { get; set; } //Decimal(-1)
        public decimal? harga_beli_terakhir { get; set; } //Decimal(-1)
        public decimal? hpp_average { get; set; } //Decimal(-1)
        public decimal? prosentase_default_profit { get; set; } //Decimal(-1)
        public Boolean is_ppn { get; set; } //Boolean(-1)
        public Boolean is_active { get; set; } //Boolean(-1)
        public short? user_created { get; set; } //Int16(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)
        public short? user_deactived { get; set; } //Int16(-1)
        public DateTime? time_deactived { get; set; } //DateTime(-1)
        public bool? is_obat { get; set; }
        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }
    }


    public record mm_setup_item_with_stok
    {
        public int? id_item { get; set; } //Int32(-1)

        //grup item
        public short? id_grup_item { get; set; } //Int16(-1)
        public string kode_grup_item { get; set; } //String(-1)
        public string grup_item { get; set; } //String(-1)

        //setup pabrik
        public short? id_pabrik { get; set; } //Int16(-1)
        public string kode_pabrik { get; set; } //String(-1)
        public string nama_pabrik { get; set; } //String(-1)
        public string alamat_pabrik { get; set; } //String(-1)

        //Setup supplier
        public short? id_supplier { get; set; } //Int16(-1)
        public string kode_supplier { get; set; } //String(-1)
        public string nama_supplier { get; set; } //String(-1)
        public string alamat_supplier { get; set; } //String(-1)

        public string kode_item { get; set; } //String(-1)
        public string barcode { get; set; } //String(-1)
        public string nama_item { get; set; } //String(-1)

        //Setup satuan
        public string kode_satuan { get; set; } //String(-1)
        public string nama_satuan { get; set; } //String(-1)

        //Setup temperature item
        public short? id_temperatur_item { get; set; } //Int16(-1)
        public string temperatur_item { get; set; } //String(-1)

        //stok
        public decimal? qty_on_hand { get; set; } //numeric()
        public decimal? batas_maksimal_pesan { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_pakai { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_mutasi { get; set; } //Decimal(-1)
        public decimal? batas_maksimal_jual { get; set; } //Decimal(-1)
        public decimal? batas_stok_kritis { get; set; } //Decimal(-1)
        public decimal? prosentase_stok_kritis { get; set; } //Decimal(-1)
        public decimal? harga_beli_terakhir { get; set; } //Decimal(-1)
        public decimal? hpp_average { get; set; } //Decimal(-1)
        public decimal? prosentase_default_profit { get; set; } //Decimal(-1)
        public Boolean is_ppn { get; set; } //Boolean(-1)
        public Boolean is_active { get; set; } //Boolean(-1)
        public short? user_created { get; set; } //Int16(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)
        public short? user_deactived { get; set; } //Int16(-1)
        public DateTime? time_deactived { get; set; } //DateTime(-1)
        public bool? is_obat { get; set; }
        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans
        {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }
    }


    public record mm_setup_item_update
    {
        public int? id_item { get; set; } //integer()
        public short? id_grup_item { get; set; } //smallint()
        public short? id_pabrik { get; set; } //smallint()
        public short? id_supplier { get; set; } //smallint()
        public string kode_item { get; set; } //character varying(20)
        public string barcode { get; set; } //character varying(30)
        public string nama_item { get; set; } //character varying(100)
        //public string kode_satuan { get; set; } //character varying(10)
        public short? id_temperatur_item { get; set; } //smallint()
        public decimal? batas_maksimal_pesan { get; set; } //numeric()
        public decimal? batas_maksimal_pakai { get; set; } //numeric()
        public decimal? batas_maksimal_mutasi { get; set; } //numeric()
        public decimal? batas_maksimal_jual { get; set; } //numeric()
        public decimal? batas_stok_kritis { get; set; } //numeric()
        public decimal? prosentase_stok_kritis { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public decimal? prosentase_default_profit { get; set; } //numeric()
        public bool? is_ppn { get; set; } //boolean()
    }

    public record mm_setup_item_insert
    {
        public short? id_grup_item { get; set; } //smallint()
        public short? id_pabrik { get; set; } //smallint()
        public short? id_supplier { get; set; } //smallint()
        public string kode_item { get; set; } //character varying(20)
        public string barcode { get; set; } //character varying(30)
        public string nama_item { get; set; } //character varying(100)
        public string kode_satuan { get; set; } //character varying(10)
        public short? id_temperatur_item { get; set; } //smallint()
        public decimal? batas_maksimal_pesan { get; set; } //numeric()
        public decimal? batas_maksimal_pakai { get; set; } //numeric()
        public decimal? batas_maksimal_mutasi { get; set; } //numeric()
        public decimal? batas_maksimal_jual { get; set; } //numeric()
        public decimal? batas_stok_kritis { get; set; } //numeric()
        public decimal? prosentase_stok_kritis { get; set; } //numeric()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public decimal? prosentase_default_profit { get; set; } //numeric()
        public bool? is_ppn { get; set; } //boolean()
        public bool is_obat { get; set; } //boolean()
        internal short? user_created { get; set; } //smallint()

        public phar_setup_obat_insert_from_barang obat { get; init; }

    }

    public record mm_setup_item_update_rak_storage
    {

        public int id_item { get; set; } //integer()
        public int id_rak_storage { get; set; } //integer()
        internal short? user_set_rak_storage { get; set; }

    }

    public record mm_setup_item_update_status_to_active
    {

        public int? id_item { get; set; } //integer()
    }

    public record mm_setup_item_update_status_to_deactive
    {

        public int? id_item { get; set; } //integer()
        internal short? user_deactived { get; set; } //smallint()

    }

    #region Item Urai

    public record mm_setup_item_urai
    {
        public int? id_item_urai { get; set; } //integer()
        public string kode_item_urai { get; set; } //character varying()
        public string barcode_item_urai { get; set; } //character varying()
        public string nama_item_urai { get; set; } //character varying()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_urai { get; set; } //numeric()

    }


    public record mm_setup_item_urai_insert
    {
        public int id_item { get; set; } //integer()
        public int id_item_urai { get; set; } //integer()
        public decimal? qty_urai { get; set; } //numeric()

    }


    public record mm_setup_item_urai_delete
    {
        public int id_item { get; set; } //integer()
        public int id_item_urai { get; set; } //integer()

    }

    #endregion

    #region Item Assembly

    public record mm_setup_item_assembly
    {
        public int? id_item_assembly { get; set; } //integer()
        public string kode_item_assembly { get; set; } //character varying()
        public string barcode_item_assembly { get; set; } //character varying()
        public string nama_item_assembly { get; set; } //character varying()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_assembly { get; set; } //numeric()

    }


    public record mm_setup_item_assembly_insert
    {
        public int id_item { get; set; } //integer()
        public int id_item_assembly { get; set; } //integer()
        public decimal? qty_assembly { get; set; } //numeric()

    }


    public record mm_setup_item_assembly_delete
    {
        public int id_item { get; set; } //integer()
        public int id_item_assembly { get; set; } //integer()

    }

    #endregion

}
