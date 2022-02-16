using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_temperatur_item
    {

        public Int16? id_temperatur_item { get; set; } //smallint()
        public string temperatur_item { get; set; } //character varying(25)
        public string keterangan { get; set; } //character varying(50)
    }

    public record mm_setup_temperatur_item_insert
    {
        public string temperatur_item { get; set; } //character varying(25)
        public string keterangan { get; set; } //character varying(50)
    }


}
