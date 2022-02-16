using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_setup_jenis_setting_audit_stok_opname
    {

        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string kode_jenis_setting_stok_opname { get; set; } //character varying(5)
        public string nama_jenis_setting_stok_opname { get; set; } //character varying(30)

    }

    public record mm_setup_jenis_setting_audit_stok_opname_insert
    {

        public string kode_jenis_setting_stok_opname { get; set; } //character varying()
        public string nama_jenis_setting_stok_opname { get; set; } //character varying()

    }



}
