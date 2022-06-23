using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Pharmm.API.Helper;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{

    #region Header

    public record tr_mutasi_no_ed
    {

        public long? mutasi_id { get; set; } //bigint()
        public string nomor_mutasi { get; set; } //character varying()
        public DateTime? tanggal_mutasi { get; set; } //date()
        public short id_stockroom_pemberi { get; set; } //smallint()
        public string kode_stockroom_pemberi { get; set; } //character varying()
        public string nama_stockroom_pemberi { get; set; } //character varying()
        public short id_stockroom_penerima { get; set; } //smallint()
        public string kode_stockroom_penerima { get; set; } //character varying()
        public string nama_stockroom_penerima { get; set; } //character varying()
        public string status_mutasi { get; set; } //character varying()
        public string nomor_permintaan_mutasi { get; set; } //character varying()
        public DateTime? tanggal_permintaan_mutasi { get; set; } //date()
        public DateTime? tanggal_expired_permintaan_mutasi { get; set; } //date()
        public string keterangan_permintaan_mutasi { get; set; } //character varying()
        public string keterangan_mutasi { get; set; } //character varying()
        public string pic_pemberi_mutasi { get; set; } //character varying()
        public string pic_penerima_mutasi { get; set; } //character varying()
        public DateTime? time_serah_terima { get; set; } //timestamp without time zone()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        public short? user_permintaan_mutasi { get; set; } //integer()
        public DateTime? time_permintaan_mutasi { get; set; } //timestamp without time zone()
        public short? user_inputed { get; set; } //integer()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_validated { get; set; } //integer()
        public DateTime? time_validated { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //integer()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()
    }

    public record tr_mutasi_no_ed_approve
    {

        public long mutasi_id { get; set; } //bigint()
        internal string nomor_mutasi { get; set; } //character varying()
        public string keterangan_mutasi { get; set; } //character varying()
        public string pic_pemberi_mutasi { get; set; } //character varying()
        public string pic_penerima_mutasi { get; set; } //character varying()
        public DateTime? time_serah_terima { get; set; } //timestamp without time zone()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        internal short? user_validated { get; set; } //integer()


        public List<tr_mutasi_no_ed_detail_item_approve> details { get; init; }
    }


    public record tr_mutasi_no_ed_insert
    {

        public string nomor_mutasi { get; set; } //character varying()
        public DateTime? tanggal_mutasi { get; set; } //timestamp without time zone()
        public short? id_stockroom_pemberi { get; set; } //smallint()
        public short? id_stockroom_penerima { get; set; } //smallint()
        public string keterangan_mutasi { get; set; } //character varying()
        public string pic_pemberi_mutasi { get; set; } //character varying()
        public string pic_penerima_mutasi { get; set; } //character varying()
        public DateTime? time_serah_terima { get; set; } //timestamp without time zone()
        public decimal? jumlah_item { get; set; } //numeric()
        public decimal? total_transaksi { get; set; } //numeric()
        internal short? user_inputed { get; set; } //integer()

        public List<tr_mutasi_no_ed_detail_item_insert> details { get; init; }

    }


    public record tr_mutasi_no_ed_insert_permintaan
    {

        public short id_stockroom_pemberi { get; set; } //smallint()
        public short id_stockroom_penerima { get; set; } //smallint()
        public string nomor_permintaan_mutasi { get; set; } //character varying()
        public DateTime? tanggal_permintaan_mutasi { get; set; } //timestamp without time zone()
        public string keterangan_permintaan_mutasi { get; set; } //character varying()
        public decimal? jumlah_item { get; set; } //numeric()
        internal short? user_permintaan_mutasi { get; set; } //integer()

        public List<tr_mutasi_no_ed_detail_item_insert_permintaan> details { get; init; }

    }


    public record tr_mutasi_no_ed_update_to_validated
    {

        public long? mutasi_id { get; set; } //bigint()
        internal short? user_validated { get; set; } //integer()

    }


    public record tr_mutasi_no_ed_update_to_canceled
    {

        public long? mutasi_id { get; set; } //bigint()
        internal short? user_canceled { get; set; } //integer()
        public string reason_canceled { get; set; } //character varying()

    }

    #endregion


    #region Detail Item

    public record tr_mutasi_no_ed_detail_item_approve
    {
        public long mutasi_detail_item_id { get; set; } //bigint()
        public long mutasi_id { get; set; } //bigint()
        public int no_urut { get; set; }
        public int id_item { get; set; }

        public decimal qty_satuan_besar_mutasi { get; set; } //numeric()
        public string kode_satuan_besar_mutasi { get; set; } //character varying()
        public short isi_mutasi { get; set; } //smallint()
        public decimal qty_mutasi { get; set; } //numeric()
        public decimal? nominal_mutasi { get; set; } //numeric()
        public string keterangan_mutasi { get; set; } //character varying()

    }


    public record tr_mutasi_no_ed_detail_item
    {

        public long? mutasi_detail_item_id { get; set; } //bigint()
        public long? mutasi_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_satuan_besar_permintaan { get; set; } //numeric()
        public string kode_satuan_besar_permintaan { get; set; } //character varying()
        public string nama_satuan_besar_permintaan { get; set; } //character varying()
        public short? isi_permintaan { get; set; } //smallint()
        public decimal? qty_permintaan { get; set; } //numeric()
        public string keterangan_permintaan { get; set; } //character varying()
        public decimal? qty_satuan_besar_mutasi { get; set; } //numeric()
        public string kode_satuan_besar_mutasi { get; set; } //character varying()
        public string nama_satuan_besar_mutasi { get; set; } //character varying()
        public short? isi_mutasi { get; set; } //smallint()
        public decimal? qty_mutasi { get; set; } //numeric()
        public decimal? nominal_mutasi { get; set; } //numeric()
        public string keterangan_mutasi { get; set; } //character varying()

        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans
        {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }

    }


    public record tr_mutasi_no_ed_detail_item_with_hpp
    {

        public long? mutasi_detail_item_id { get; set; } //bigint()
        public long? mutasi_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_satuan_besar_permintaan { get; set; } //numeric()
        public string kode_satuan_besar_permintaan { get; set; } //character varying()
        public string nama_satuan_besar_permintaan { get; set; } //character varying()
        public short? isi_permintaan { get; set; } //smallint()
        public decimal? qty_permintaan { get; set; } //numeric()
        public string keterangan_permintaan { get; set; } //character varying()
        public decimal? qty_satuan_besar_mutasi { get; set; } //numeric()
        public string kode_satuan_besar_mutasi { get; set; } //character varying()
        public string nama_satuan_besar_mutasi { get; set; } //character varying()
        public short? isi_mutasi { get; set; } //smallint()
        public decimal? qty_mutasi { get; set; } //numeric()
        public decimal? hpp_average { get; set; }
        public decimal? nominal_mutasi { get; set; } //numeric()
        public string keterangan_mutasi { get; set; } //character varying()

        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans
        {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }

    }

    //untuk default mutasi ambil dari permintaan
    public record tr_mutasi_no_ed_detail_item_for_default_mutasi
    {

        public long? mutasi_detail_item_id { get; set; } //bigint()
        public long? mutasi_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal? qty_satuan_besar_permintaan { get; set; } //numeric()
        public string kode_satuan_besar_permintaan { get; set; } //character varying()
        public string nama_satuan_besar_permintaan { get; set; } //character varying()
        public short? isi_permintaan { get; set; } //smallint()
        public decimal? qty_permintaan { get; set; } //numeric()
        public string keterangan_permintaan { get; set; } //character varying()

        public decimal? qty_satuan_besar_mutasi { get => qty_satuan_besar_permintaan; } //numeric()
        public string kode_satuan_besar_mutasi { get; set; } //character varying()
        public string nama_satuan_besar_mutasi { get; set; } //character varying()
        public short? isi_mutasi { get; set; } //smallint()
        public decimal? qty_mutasi { get; set; } //numeric()
        public decimal? nominal_mutasi { get; set; } //numeric()
        public string keterangan_mutasi { get; set; } //character varying()

        private string _satuans { get; set; }
        public List<mm_setup_item_satuan> satuans
        {

            get
            {
                return JsonConvert.DeserializeObject<List<mm_setup_item_satuan>>(this._satuans);
            }

            set => this._satuans = JsonConvert.SerializeObject(value);

        }

    }


    public record tr_mutasi_no_ed_detail_item_insert_permintaan
    {

        internal long? mutasi_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty_satuan_besar_permintaan { get; set; } //numeric()
        public string kode_satuan_besar_permintaan { get; set; } //character varying()
        public short? isi_permintaan { get; set; } //smallint()
        public decimal? qty_permintaan { get; set; } //numeric()
        public string keterangan_permintaan { get; set; } //character varying()

    }


    public record tr_mutasi_no_ed_detail_item_insert
    {

        internal long? mutasi_id { get; set; } //bigint()
        public short? no_urut { get; set; } //smallint()
        public int? id_item { get; set; } //integer()
        public decimal? qty_satuan_besar_mutasi { get; set; } //numeric()
        public string kode_satuan_besar_mutasi { get; set; } //character varying()
        public short? isi_mutasi { get; set; } //smallint()
        public decimal? qty_mutasi { get; set; } //numeric()
        public decimal? nominal_mutasi { get; set; } //numeric()
        public string keterangan_mutasi { get; set; } //character varying()

    }

    #endregion

    #region Detail Upload

    public record tr_mutasi_no_ed_detail_upload
    {

        public long? mutasi_detail_upload_id { get; set; } //bigint()
        internal long? mutasi_id { get; set; } //bigint()
        public string jenis_dokumen { get; set; } //character varying()
        public string path_dokumen { get; set; }
        private string _url_dokumen { get; set; }
        public string url_dokumen
        {
            get => this._url_dokumen;
            set
            {

                var reqParams = new Dictionary<string, string>
                {
                        { "response-content-type", "application/pdf" }
                };

                this.path_dokumen = value;
                this._url_dokumen = UploadHelper.GetFileLinkByPath(value,reqParams).Result;
            }
        } //string()

        public string keterangan { get; set; } //character varying()

    }


    public record tr_mutasi_no_ed_detail_upload_insert
    {

        public long? mutasi_id { get; set; } //bigint()
        public string jenis_dokumen { get; set; } //character varying()
        internal string url_dokumen { get; set; } //string()
        public string keterangan { get; set; } //character varying()
        public IFormFile file { get; set; }

    }



    #endregion

}
