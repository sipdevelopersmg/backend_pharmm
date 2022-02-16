using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_supplier_rekening
    {
        public short? id_supplier_rekening { get; set; } //Int16(-1)
        public short? id_supplier { get; set; } //Int16(-1)
        //setup supplier
        public string kode_supplier { get; set; } //String(-1)
        public string nama_supplier { get; set; } //String(-1)
        public string alamat_supplier { get; set; } //String(-1)
        
        public string bank { get; set; } //String(-1)
        public string nomor_rekening { get; set; } //String(-1)
        public string nama_rekening { get; set; } //String(-1)
        public string mata_uang { get; set; } //String(-1)
        public Boolean is_active { get; set; } //Boolean(-1)
        public short? user_created { get; set; } //Int16(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)
        public short? user_deactived { get; set; } //Int16(-1)
        public DateTime? time_deactived { get; set; } //DateTime(-1)

    }



    public record mm_setup_supplier_rekening_insert
    {

        public Int16? id_supplier { get; set; } //smallint()
        public string bank { get; set; } //character varying(30)
        public string nomor_rekening { get; set; } //character varying(30)
        public string nama_rekening { get; set; } //character varying(50)
        public string mata_uang { get; set; } //character varying(10)
        internal short? user_created { get; set; } //smallint()

    }

    public record mm_setup_supplier_rekening_update
    {

        public Int16? id_supplier_rekening { get; set; } //smallint()
        public Int16? id_supplier { get; set; } //smallint()
        public string bank { get; set; } //character varying(30)
        public string nomor_rekening { get; set; } //character varying(30)
        public string nama_rekening { get; set; } //character varying(50)
        public string mata_uang { get; set; } //character varying(10)

    }

    public record mm_setup_supplier_rekening_update_status_to_active
    {
        public Int16? id_supplier_rekening { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()
    }

    public record mm_setup_supplier_rekening_update_status_to_deactive
    {
        public Int16? id_supplier_rekening { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()
    }


}
