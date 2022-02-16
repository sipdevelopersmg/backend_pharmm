using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_setup_item_satuan
    {

        public int id_item { get; set; } //integer()
        public string kode_satuan { get; set; } //character varying(10)
        public string nama_satuan { get; set; }
        public short? isi { get; set; } //smallint()
        public bool? is_satuan_beli { get; set; } //boolean()
    }

    public record mm_setup_item_satuan_insert
    {

        public int id_item { get; set; } //integer()
        public string kode_satuan { get; set; } //character varying(10)
        public short? isi { get; set; } //smallint()
        public bool? is_satuan_beli { get; set; } //boolean()
    }

    public record mm_setup_item_satuan_delete
    {

        public int? id_item { get; set; } //integer()
        public string kode_satuan { get; set; } //character varying(10)
    }
}
