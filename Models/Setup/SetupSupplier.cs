using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_supplier_getall
    {

        public short? id_supplier { get; set; } //smallint()
        public short? id_tipe_supplier { get; set; } //smallint()
        public string tipe_supplier { get; set; } //character varying()
        public string kode_supplier { get; set; } //character varying()
        public string nama_supplier { get; set; } //character varying()
        public string alamat_supplier { get; set; } //character varying()
    }

    public record mm_setup_supplier
    {

        public short? id_supplier { get; set; } //smallint()
        public short? id_tipe_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying(20)
        public string nama_supplier { get; set; } //character varying(50)
        public string alamat_supplier { get; set; } //character varying(100)
        public string kode_wilayah { get; set; } //character varying(15)
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
        public string npwp { get; set; } //character varying(20)
        public short? default_hari_tempo_bayar { get; set; } //smallint()
        public short? default_hari_pengiriman { get; set; } //smallint()
        public decimal? default_prosentase_diskon { get; set; } //numeric()
        public decimal? default_prosentase_tax { get; set; } //numeric()
        public bool? is_tax { get; set; } //boolean()
        public bool? is_active { get; set; } //boolean()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_deactived { get; set; } //smallint()
        public DateTime? time_deactived { get; set; } //timestamp without time zone()

    }



    public record mm_setup_supplier_insert
    {

        public short? id_tipe_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying(20)
        public string nama_supplier { get; set; } //character varying(50)
        public string alamat_supplier { get; set; } //character varying(100)
        public string kode_wilayah { get; set; } //character varying(15)
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
        public string npwp { get; set; } //character varying(20)
        public short? default_hari_tempo_bayar { get; set; } //smallint()
        public short? default_hari_pengiriman { get; set; } //smallint()
        public decimal? default_prosentase_diskon { get; set; } //numeric()
        public decimal? default_prosentase_tax { get; set; } //numeric()
        public bool? is_tax { get; set; } //boolean()
        internal short? user_created { get; set; } //smallint()

    }

    public record mm_setup_supplier_update
    {
        public short? id_supplier { get; set; } //smallint()
        public short? id_tipe_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying(20)
        public string nama_supplier { get; set; } //character varying(50)
        public string alamat_supplier { get; set; } //character varying(100)
        public string kode_wilayah { get; set; } //character varying(15)
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
        public string npwp { get; set; } //character varying(20)
        public short? default_hari_tempo_bayar { get; set; } //smallint()
        public short? default_hari_pengiriman { get; set; } //smallint()
        public decimal? default_prosentase_diskon { get; set; } //numeric()
        public decimal? default_prosentase_tax { get; set; } //numeric()
        public bool? is_tax { get; set; } //boolean()

    }


    public record mm_setup_supplier_update_status_to_active
    {
        public Int16? id_supplier { get; set; } //smallint()
    }

    public record mm_setup_supplier_update_status_to_deactive
    {
        public Int16? id_supplier { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()
    }
}
