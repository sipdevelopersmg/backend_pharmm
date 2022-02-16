using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_shipping_method
    {

        public short? id_shipping_method { get; set; } //smallint()
        public string shipping_method { get; set; } //character varying()
    }


}
