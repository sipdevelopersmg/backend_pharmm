using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record mm_setup_audit_penanggung_jawab_rak_storage
    {

        public int? id_penanggung_jawab_rak_storage { get; set; } //integer()
        public string nik_penanggung_jawab_rak_storage { get; set; } //character varying()
        public string nama_penanggung_jawab_rak_storage { get; set; } //character varying()
        public short? id_supplier { get; set; } //smallint()
        public string kode_supplier { get; set; } //character varying()
        public string nama_supplier { get; set; } //character varying()
        public string alamat_supplier { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
        public short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()

    }

    public record mm_setup_audit_penanggung_jawab_rak_storage_insert
    {

        public string nik_penanggung_jawab_rak_storage { get; set; } //character varying()
        public string nama_penanggung_jawab_rak_storage { get; set; } //character varying()
        public short id_supplier { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()

    }

    public record mm_setup_audit_penanggung_jawab_rak_storage_update
    {

        public int id_penanggung_jawab_rak_storage { get; set; } //integer()
        public string nik_penanggung_jawab_rak_storage { get; set; } //character varying()
        public string nama_penanggung_jawab_rak_storage { get; set; } //character varying()
        public short id_supplier { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()

    }



}
