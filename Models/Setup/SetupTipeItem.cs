using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_tipe_item
    {
        public short? id_tipe_item { get; set; } //smallint()
        public string kode_tipe_item { get; set; } //character varying(20)
        public string tipe_item { get; set; } //character varying(50)
        public bool? is_active { get; set; } //boolean()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_deactived { get; set; } //smallint()
        public DateTime? time_deactived { get; set; } //timestamp without time zone()

    }

    public record mm_setup_tipe_item_update
    {
        public short? id_tipe_item { get; set; } //smallint()
        public string kode_tipe_item { get; set; } //character varying(20)
        public string tipe_item { get; set; } //character varying(50)

    }

    public record mm_setup_tipe_item_insert
    {
        public string kode_tipe_item { get; set; } //character varying(20)
        public string tipe_item { get; set; } //character varying(50)
        internal short? user_created { get; set; } //smallint()

    }

    public record mm_setup_tipe_item_update_status_to_active
    {

        public Int16? id_tipe_item { get; set; } //smallint()

    }

    public record mm_setup_tipe_item_update_status_to_deactive
    {

        public Int16? id_tipe_item { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()

    }
}
