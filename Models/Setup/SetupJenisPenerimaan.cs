using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_jenis_penerimaan
    {

        public string kode_jenis_penerimaan { get; set; } //character varying(10)
        public string jenis_penerimaan { get; set; } //character varying(30)
        public bool? is_account_payable { get; set; } //boolean()

    }


}
