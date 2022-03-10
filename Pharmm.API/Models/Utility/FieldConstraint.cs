using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Utility
{

    public record field_constraint
    {

        public string table_name { get; set; } //character varying()
        public string column_name { get; set; } //character varying()

    }

}
