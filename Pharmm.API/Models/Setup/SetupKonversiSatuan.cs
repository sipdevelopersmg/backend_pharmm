using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{

    public record mm_setup_konversi_satuan_insert
    {

        public string kode_satuan_besar { get; set; } //character varying(10)
        public string kode_satuan_kecil { get; set; } //character varying(10)
        public Single? faktor_konversi { get; set; } //real()
        public Single? faktor_dekonversi { get; set; } //real()
        internal short? user_created { get; set; } //smallint()

    }

    public record mm_setup_konversi_satuan_update
    {

        public int id_konversi_satuan { get; set; } //integer()
        public string kode_satuan_besar { get; set; } //character varying(10)
        public string kode_satuan_kecil { get; set; } //character varying(10)
        public Single? faktor_konversi { get; set; } //real()
        public Single? faktor_dekonversi { get; set; } //real()

    }

    public record mm_setup_konversi_satuan
    {
        public int? id_konversi_satuan { get; set; } //Int32(-1)
        public string kode_satuan_besar { get; set; } //String(-1)
        public string nama_satuan_besar { get; set; } //String(-1)
        public string kode_satuan_kecil { get; set; } //String(-1)
        public string nama_satuan_kecil { get; set; } //String(-1)
        public float faktor_konversi { get; set; } //Single(-1)
        public float faktor_dekonversi { get; set; } //Single(-1)
        public short? user_created { get; set; } //Int16(-1)
        public DateTime? time_created { get; set; } //DateTime(-1)

    }


}
