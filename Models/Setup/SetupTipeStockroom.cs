using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_setup_tipe_stockroom
    {

        public short? id_tipe_stockroom { get; set; } //smallint()
        public string tipe_stockroom { get; set; } //character varying(20)
        public string keterangan { get; set; } //character varying(50)
    }

    public record mm_setup_tipe_stockroom_insert
    {
        public string tipe_stockroom { get; set; } //character varying(20)
        public string keterangan { get; set; } //character varying(50)
    }


}
