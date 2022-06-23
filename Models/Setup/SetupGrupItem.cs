using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_grup_item_insert
    {
        public short? id_tipe_item { get; set; } //smallint()
        public string kode_grup_item { get; set; } //character varying(20)
        public string grup_item { get; set; } //character varying(50)
        public int? last_no { get; set; } //integer()
        public int? id_coa_persediaan { get; set; } //integer()
        public int? id_coa_pendapatan { get; set; } //integer()
        public int? id_coa_biaya { get; set; } //integer()
        internal short? user_created { get; set; } //smallint()
    }


    public record mm_setup_grup_item
    {

        public short id_grup_item { get; set; } //short(-1)
        public short id_tipe_item { get; set; } //short(-1)
        public string kode_tipe_item { get; set; } //String(-1)
        public string tipe_item { get; set; } //String(-1)
        public string kode_grup_item { get; set; } //String(-1)
        public string grup_item { get; set; } //String(-1)
        public int? last_no { get; set; } //Int32(-1)
        public int? id_coa_persediaan { get; set; } //Int32(-1)
        public string kode_coa_persediaan { get; set; } //String(-1)
        public string deskripsi_coa_persediaan { get; set; } //String(-1)
        public int? id_coa_pendapatan { get; set; } //Int32(-1)
        public string kode_coa_pendapatan { get; set; } //String(-1)
        public string deskripsi_coa_pendapatan { get; set; } //String(-1)
        public int? id_coa_biaya { get; set; } //Int32(-1)
        public string kode_coa_coa_biaya { get; set; } //String(-1)
        public string deskripsi_coa_biaya { get; set; } //String(-1)
        public Boolean is_active { get; set; } //Boolean(-1)
        public short user_created { get; set; } //short(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)
        public short user_deactived { get; set; } //short(-1)
        public DateTime? time_deactived { get; set; } //DateTime(-1)

    }

    public record mm_setup_grup_item_update
    {

        public short? id_grup_item { get; set; } //smallint()
        public short? id_tipe_item { get; set; } //smallint()
        public string kode_grup_item { get; set; } //character varying(20)
        public string grup_item { get; set; } //character varying(50)
        public int? last_no { get; set; } //integer()
        public int? id_coa_persediaan { get; set; } //integer()
        public int? id_coa_pendapatan { get; set; } //integer()
        public int? id_coa_biaya { get; set; } //integer()

    }

    public record mm_setup_grup_item_update_status_to_active
    {

        public short? id_grup_item { get; set; } //smallint()

    }


    public record mm_setup_grup_item_update_status_to_deactive
    {

        public short? id_grup_item { get; set; } //smallint()
        internal short? user_deactived { get; set; } //smallint()

    }

}
