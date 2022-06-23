using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.FromAPI
{
    public class Pasien_antrian
    {
        public long id_register { get; set; }
        public string no_register { get; set; }
        public string no_rekam_medis { get; set; }
        public string jenis_rawat { get; set; }
        public string nama_pasien { get; set; }
        public string tgl_lahir { get; set; }
        public DateTime tgl_masuk { get; set; }
        public string nama_foto { get; set; }
        public string nama_poli { get; set; }
        public short id_debitur { get; set; }
        public string nama_debitur { get; set; }
        public string bed_no { get; set; }
        public string room_descr { get; set; }
        public string usia { get; set; }

    }
}
