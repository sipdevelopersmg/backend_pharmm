using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    #region Parameter

    public record mm_setup_audit_rak_storage_getby_settingid_idstockroom
    {
        public long setting_stok_opname_id { get; set; }
        public short id_stockroom { get; set; }
    }

    #endregion

    #region Audit Rak

    public record mm_setup_audit_rak_storage
    {

        public int? id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public short? id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public int? id_penanggung_jawab_rak_storage { get; set; } //integer()
        public string nama_penanggung_jawab_rak_storage { get; set; } //character varying()
        public string keterangan { get; set; } //character varying()
        public short? user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_set_penanggung_jawab_rak_storage { get; set; } //smallint()
        public DateTime? time_set_penanggung_jawab_rak_storage { get; set; } //timestamp without time zone()

    }

    public record mm_setup_audit_rak_storage_insert
    {

        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public short id_stockroom { get; set; } //smallint()
        public int id_penanggung_jawab_rak_storage { get; set; } //integer()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        internal short? user_set_penanggung_jawab_rak_storage { get; set; }

    }

    public record mm_setup_audit_rak_storage_update
    {

        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public short id_stockroom { get; set; } //smallint()
        public int id_penanggung_jawab_rak_storage { get; set; } //integer()
        public string keterangan { get; set; } //character varying()
        internal short? user_set_penanggung_jawab_rak_storage { get; set; }

    }

    public record mm_setup_audit_rak_storage_for_stok_opname
    {

        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()

    }

    #endregion





}
