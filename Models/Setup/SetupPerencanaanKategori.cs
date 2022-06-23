using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_perencanaan_kategori
    {

        public Int16? id_kategori { get; set; } //smallint()
        public string kategori { get; set; } //character varying(300)
    }

    public record mm_setup_perencanaan_kategori_insert
    {
        public string kategori { get; set; } //character varying(300)
    }

}
