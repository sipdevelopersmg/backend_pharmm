using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_setup_pabrik
    {

        public short? id_pabrik { get; set; } //smallint()
        public string kode_pabrik { get; set; } //character varying(20)
        public string nama_pabrik { get; set; } //character varying(50)
        public string alamat_pabrik { get; set; } //character varying(100)
        public int? kode_wilayah { get; set; } //integer()
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
        public bool? is_active { get; set; } //boolean()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_deactived { get; set; } //smallint()
        public DateTime? time_deactived { get; set; } //timestamp without time zone()

    }


    public record mm_setup_pabrik_insert
    {
        public string kode_pabrik { get; set; } //character varying(20)
        public string nama_pabrik { get; set; } //character varying(50)
        public string alamat_pabrik { get; set; } //character varying(100)
        public int? kode_wilayah { get; set; } //integer()
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
        internal short? user_created { get; set; } //smallint()
    }


    public record mm_setup_pabrik_update
    {
        public short? id_pabrik { get; set; } //smallint()
        public string kode_pabrik { get; set; } //character varying(20)
        public string nama_pabrik { get; set; } //character varying(50)
        public string alamat_pabrik { get; set; } //character varying(100)
        public int? kode_wilayah { get; set; } //integer()
        public string negara { get; set; } //character varying(25)
        public string telepon { get; set; } //character varying(20)
        public string fax { get; set; } //character varying(20)
        public string kode_pos { get; set; } //character varying(20)
        public string email { get; set; } //character varying(30)
        public string contact_person { get; set; } //character varying(25)
    }

    public record mm_setup_pabrik_update_status_to_active
    {
        public Int16? id_pabrik { get; set; } //smallint()
    }

    public record mm_setup_pabrik_update_status_to_deactive
    {
        public short? id_pabrik { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()
    }



}
