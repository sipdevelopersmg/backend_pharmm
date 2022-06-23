using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record master_counter_insert
    {

        public string kode_counter { get; set; } //character varying()
        public int? counter_max_length { get; set; } //integer()
        public bool? use_alphabet { get; set; } //boolean()
        public bool? use_dash { get; set; } //boolean()
        public bool? use_date { get; set; } //boolean()
        public string description { get; set; } //character varying()

    }
}
