using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_mekanisme_retur
    {

        public short? id_mekanisme_retur { get; set; } //smallint()
        public string mekanisme_retur { get; set; } //character varying(20)

    }


}
