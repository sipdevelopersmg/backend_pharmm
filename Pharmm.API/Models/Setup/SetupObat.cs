using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Setup
{
    public record phar_setup_obat
    {

        public int? id_item { get; set; } //integer()

        public short? id_grup_obat { get; set; } //smallint()
        public string kode_grup_obat { get; set; } //character varying(20)
        public string nama_grup_obat { get; set; } //character varying(120)

        public short? id_cara_pakai_obat { get; set; } //smallint()
        public string kode_cara_pakai_obat { get; set; } //character varying(20)
        public string cara_pakai_obat { get; set; } //character varying(120)

        public short? id_rute_pemberian_obat { get; set; } //smallint()
        public string nama_rute_pemberian_obat { get; set; } //character varying(20)
        public string rute_pasien { get; set; } //character varying(200)
        public short? ordered { get; set; } //smallint()
        public bool? is_parenteral { get; set; } //boolean()

        public int? id_restriksi_obat { get; set; } //integer()
        public string nama_restriksi { get; set; } //character varying(700)

        public short? id_peresepan_maksimal { get; set; } //smallint()
        public string peresepan_maksimal { get; set; } //character varying(700)
        public decimal? nilai_maksimal { get; set; } //numeric()
        public short? id_parameter_maksimal { get; set; } //smallint()

        public short? kandungan_obat { get; set; } //smallint()
        public bool? is_fornas { get; set; } //boolean()
        private string _details { get; set; }

        public List<phar_setup_obat_detail> details
        {

            get
            {
                return JsonConvert.DeserializeObject<List<phar_setup_obat_detail>>(this._details);
            }

            set => this._details = JsonConvert.SerializeObject(value);

        }
    }

    public record phar_setup_obat_insert_from_barang
    {

        internal int? id_item { get; set; } //integer()
        public short? id_grup_obat { get; set; } //smallint()
        public short? id_cara_pakai_obat { get; set; } //smallint()
        public short? id_rute_pemberian_obat { get; set; } //smallint()
        public int? id_restriksi_obat { get; set; } //integer()
        public short? id_peresepan_maksimal { get; set; } //smallint()
        public short? kandungan_obat { get; set; } //smallint()
        public bool? is_fornas { get; set; } //boolean(),
        public bool? is_narkotika { get; set; } //boolean()
        public List<phar_setup_obat_detail_insert_from_barang> details { get; set; }
    }

    public record phar_setup_obat_insert
    {

        public int? id_item { get; set; } //integer()
        public short? id_grup_obat { get; set; } //smallint()
        public short? id_cara_pakai_obat { get; set; } //smallint()
        public short? id_rute_pemberian_obat { get; set; } //smallint()
        public int? id_restriksi_obat { get; set; } //integer()
        public short? id_peresepan_maksimal { get; set; } //smallint()
        public short? kandungan_obat { get; set; } //smallint()
        public bool? is_fornas { get; set; } //boolean()
        public bool? is_narkotika { get; set; } //boolean()
        public List<phar_setup_obat_detail_insert> details { get; set; }
    }

    public record phar_setup_obat_update
    {

        public int? id_item { get; set; } //integer()
        public short? id_grup_obat { get; set; } //smallint()
        public short? id_cara_pakai_obat { get; set; } //smallint()
        public short? id_rute_pemberian_obat { get; set; } //smallint()
        public int? id_restriksi_obat { get; set; } //integer()
        public short? id_peresepan_maksimal { get; set; } //smallint()
        public short? kandungan_obat { get; set; } //smallint()
        public bool? is_fornas { get; set; } //boolean()
        public bool? is_narkotika { get; set; } //boolean()
    }


    public record phar_setup_obat_delete
    {
        public int? id_item { get; set; } //integer()
        public short? id_grup_obat { get; set; } //smallint()
    }



    #region Setup Obat Detail

    public record phar_setup_obat_detail_lock
    {

        public long? id_obat_detail { get; set; } //bigint()
        public int? id_item { get; set; } //integer()
        public decimal? harga_netto_apotek { get; set; } //numeric()
        public decimal? prosentase_profit { get; set; } //numeric()
        public decimal? prosentase_ppn { get; set; } //numeric()
        public decimal? harga_jual_apotek { get; set; } //numeric()
        public DateTime? tgl_berlaku { get; set; } //date()
        public DateTime? tgl_berakhir { get; set; } //date()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_edited { get; set; } //integer()
        public DateTime? time_edited { get; set; } //timestamp without time zone()
        public bool? is_active { get; set; } //boolean()

    }


    public record phar_setup_obat_detail
    {

        public long? id_obat_detail { get; set; } //bigint()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? harga_beli_terakhir { get; set; } //numeric()
        public decimal? hpp_average { get; set; } //numeric()
        public decimal? harga_netto_apotek { get; set; } //numeric()
        public decimal? prosentase_profit { get; set; } //numeric()
        public decimal? prosentase_ppn { get; set; } //numeric()
        public decimal? harga_jual_apotek { get; set; } //numeric()
        public DateTime? tgl_berlaku { get; set; } //date()
        public DateTime? tgl_berakhir { get; set; } //date()
        public short? user_created { get; set; } //smallint()
        public DateTime? time_created { get; set; } //timestamp without time zone()
        public short? user_edited { get; set; } //integer()
        public DateTime? time_edited { get; set; } //timestamp without time zone()
        public bool? is_active { get; set; } //boolean()

    }



    public record phar_setup_obat_detail_insert_from_barang
    {

        internal int? id_item { get; set; } //integer()
        public decimal? harga_netto_apotek { get; set; } //numeric()
        public decimal? prosentase_profit { get; set; } //numeric()
        public decimal? prosentase_ppn { get; set; } //numeric()
        public decimal? harga_jual_apotek { get; set; } //numeric()
        public DateTime? tgl_berlaku { get; set; } //date()
        internal DateTime? tgl_berakhir { get; set; } //date()
        internal short? user_created { get; set; } //smallint()

    }

    public record phar_setup_obat_detail_insert
    {

        public int? id_item { get; set; } //integer()
        public decimal? harga_netto_apotek { get; set; } //numeric()
        public decimal? prosentase_profit { get; set; } //numeric()
        public decimal? prosentase_ppn { get; set; } //numeric()
        internal decimal? harga_jual_apotek { get; set; } //numeric()
        public DateTime? tgl_berlaku { get; set; } //date()
        internal DateTime? tgl_berakhir { get; set; } //date()
        internal short? user_created { get; set; } //smallint()

    }

    public record phar_setup_obat_detail_update_status
    {

        public long? id_obat_detail { get; set; } //bigint()
        internal DateTime? tgl_berakhir { get; set; } //date()
        internal short? user_edited { get; set; } //integer()

    }

    #endregion
}
