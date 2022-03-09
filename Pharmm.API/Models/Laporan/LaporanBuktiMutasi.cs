using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Laporan
{
    #region Param

    public record param_bukti_mutasi
    {
        public string nomor_mutasi { get; set; }
    }


    public record param_permintaan_mutasi
    {

        public string nomor_permintaan_mutasi { get; set; } //character varying()

    }

    #endregion

    public record laporan_bukti_mutasi_per_faktur
    {

        public long? urut { get; set; } //bigint()
        public string nomor_mutasi { get; set; } //character varying()
        public DateTime? tanggal_mutasi { get; set; } //date()
        public string kode_lokasi_peminta { get; set; } //character varying()
        public string nama_lokasi_peminta { get; set; } //character varying()
        public string kode_gudang_tujuan { get; set; } //character varying()
        public string nama_gudang_tujuan { get; set; } //character varying()
        public string komentar { get; set; } //character varying()
        public short? user_inputed { get; set; } //smallint()
        public short? user_validated { get; set; } //smallint()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
        public string keterangan_detail { get; set; } //character varying()
        public decimal? jml_diminta { get; set; } //numeric()
        public decimal? jml_dipenuhi { get; set; } //numeric()
        public decimal? jml_blm_dikirim { get; set; } //numeric()
    }

    public record laporan_permintaan_mutasi_per_faktur
    {

        public long? urut { get; set; } //bigint()
        public string nomor_permintaan_mutasi { get; set; } //character varying()
        public DateTime? tanggal_permintaan_mutasi { get; set; } //date()
        public string kode_lokasi_peminta { get; set; } //character varying()
        public string nama_lokasi_peminta { get; set; } //character varying()
        public string kode_gudang_tujuan { get; set; } //character varying()
        public string nama_gudang_tujuan { get; set; } //character varying()
        public string komentar { get; set; } //character varying()
        public short? user_inputed { get; set; } //smallint()
        public short? user_validated { get; set; } //smallint()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
        public string keterangan_detail { get; set; } //character varying()
        public decimal? jml_diminta { get; set; } //numeric()

    }

}
