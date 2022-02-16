using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_tipe_supplier
    {
        public Int16? id_tipe_supplier { get; set; } //smallint()
        public string tipe_supplier { get; set; } //character varying(50)
        public bool? is_ap { get; set; } //boolean()
        public bool? is_active { get; set; } //boolean()    
    }

    public record mm_setup_tipe_supplier_update
    {
        public Int16? id_tipe_supplier { get; set; } //smallint()
        public string tipe_supplier { get; set; } //character varying(50)
        public bool? is_ap { get; set; } //boolean()   
    }

    public record mm_setup_tipe_supplier_insert
    {
        public string tipe_supplier { get; set; } //character varying(50)
        public bool? is_ap { get; set; } //boolean()
    }

    public record mm_setup_tipe_supplier_update_status_to_active
    {
        public Int16? id_tipe_supplier { get; set; } //smallint()
    }

    public record mm_setup_tipe_supplier_update_status_to_deactive
    {
        public Int16? id_tipe_supplier { get; set; } //smallint()
    }


}
