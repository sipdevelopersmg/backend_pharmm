using Pharmm.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.FromAPI
{
	public record dokter_getall
	{
		public int id_dokter { get; set; }
		public long id_person { get; set; }
		public short id_spesialisasi_dokter { get; set; }
		public string kode_dokter { get; set; }
		public string full_name { get; set; }
		public string spesialisasi_dokter { get; set; }
		public string no_surat_ijin_praktek { get; set; }
		public DateTime tgl_exp_surat_ijin_praktek { get; set; }
		public string no_str { get; set; }
		public DateTime tgl_exp_str { get; set; }
		public short id_smf { get; set; }
		public short id_status_dokter { get; set; }
	}

}
