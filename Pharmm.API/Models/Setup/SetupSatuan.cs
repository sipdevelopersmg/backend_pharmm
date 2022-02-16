using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_satuan_insert
    {
      public string kode_satuan { get; set; } //character varying(10)
      public string nama_satuan { get; set; } //character varying(20)
      internal short? user_created { get; set; } //smallint()
    }

    public record mm_setup_satuan_update
    {
        public string kode_satuan { get; set; } //character varying(10)
        public string nama_satuan { get; set; } //character varying(20)
    }

    public record mm_setup_satuan
    {
        public string kode_satuan { get; set; } //character varying(10)
        public string nama_satuan { get; set; } //character varying(20)
        public bool? is_active { get; set; } //boolean()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_deactived { get; set; } //smallint()
        public DateTime? time_deactived { get; set; } //timestamp without time zone()
    }


    public record mm_setup_satuan_update_status_to_active
    {

        public string kode_satuan { get; set; } //character varying(10)
    }

    public record mm_setup_satuan_update_status_to_deactive
    {

        public string kode_satuan { get; set; } //character varying(10)
        internal short? user_deactived { get; set; } //smallint()
    }

}
