using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record set_ukuran_dokumen
    {
        public Int16? id { get; set; } //smallint()
        public string error_message { get; set; } //character varying(50)
        public int max_size { get; set; } //integer()
    }
}
