using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_payment_term
    {

        public short? id_payment_term { get; set; } //smallint()
        public string payment_term { get; set; } //character varying(30)

    }


}
