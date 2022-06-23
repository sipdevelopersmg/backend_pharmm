using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_stockroom
    {

        public Int16? id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying(15)
        public string nama_stockroom { get; set; } //character varying(50)
        public short? id_tipe_stockroom { get; set; } //smallint()
        public string tipe_stockroom { get; set; }
        public string store_type { get; set; } //character varying(10)
        public string gl_no { get; set; } //character varying(50)
        public string gl_dept_name { get; set; } //character varying(50)
        public Int16? id_stockroom_parent { get; set; } //smallint()
        public bool? is_show_persediaan { get; set; } //boolean()
        public bool? is_active { get; set; } //boolean()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_deactived { get; set; } //smallint()
        public DateTime? time_deactived { get; set; } //timestamp without time zone()

    }


    public record mm_setup_stockroom_insert
    {
        public string kode_stockroom { get; set; } //character varying(15)
        public string nama_stockroom { get; set; } //character varying(50)
        public short? id_tipe_stockroom { get; set; } //smallint()
        public string store_type { get; set; } //character varying(10)
        public string gl_no { get; set; } //character varying(50)
        public string gl_dept_name { get; set; } //character varying(50)
        public Int16? id_stockroom_parent { get; set; } //smallint()
        public bool? is_show_persediaan { get; set; } //boolean()
        internal short? user_created { get; set; } //smallint()

    }


    public record mm_setup_stockroom_update
    {
        public Int16? id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying(15)
        public string nama_stockroom { get; set; } //character varying(50)
        public short? id_tipe_stockroom { get; set; } //smallint()
        public string store_type { get; set; } //character varying(10)
        public string gl_no { get; set; } //character varying(50)
        public string gl_dept_name { get; set; } //character varying(50)
        public Int16? id_stockroom_parent { get; set; } //smallint()
        public bool? is_show_persediaan { get; set; } //boolean()

    }


    public record mm_setup_stockroom_update_status_to_active
    {
        public Int16? id_stockroom { get; set; } //smallint()

    }


    public record mm_setup_stockroom_update_status_to_deactive
    {
        public Int16? id_stockroom { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()

    }
}
