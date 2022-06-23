using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{


    public record akun_setup_coa
    {
        public int? id_coa { get; set; } //Int32(-1)
        public int? id_coa_parent { get; set; } //Int32(-1)
        public Int16 id_grup_coa { get; set; } //Int16(-1)

        //grup coa
        public string grup_coa { get; set; } //String(-1)
        public string deskripsi_grup_coa { get; set; } //String(-1)

        public string kode_coa { get; set; } //String(-1)
        public string deskripsi { get; set; } //String(-1)
        public decimal? saldo { get; set; } //Decimal(-1)
        public Boolean is_active { get; set; } //Boolean(-1)
        public Int16 user_created { get; set; } //Int16(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)
        public Int16 user_deactived { get; set; } //Int16(-1)
        public DateTime? time_deactived { get; set; } //DateTime(-1)

    }

    public record akun_setup_coa_update
    {

        public int? id_coa { get; set; } //integer()
        public int? id_coa_parent { get; set; } //integer()
        public Int16? id_grup_coa { get; set; } //smallint()
        public string kode_coa { get; set; } //character varying(20)
        public string deskripsi { get; set; } //character varying(100)    
        public decimal? saldo { get; set; } //numeric()  

    }

    public record akun_setup_coa_insert
    {
        public int? id_coa_parent { get; set; } //integer()
        public Int16? id_grup_coa { get; set; } //smallint()
        public string kode_coa { get; set; } //character varying(20)
        public string deskripsi { get; set; } //character varying(100)
        public decimal? saldo { get; set; } //numeric()
        internal short? user_created { get; set; } //smallint()

    }


    public record akun_setup_coa_update_status_to_active
    {

        public int? id_coa { get; set; } //integer()

    }


    public record akun_setup_coa_update_status_to_deactive
    {

        public int? id_coa { get; set; } //integer()
        internal short? user_deactived { get; set; } //smallint()
    }


}
