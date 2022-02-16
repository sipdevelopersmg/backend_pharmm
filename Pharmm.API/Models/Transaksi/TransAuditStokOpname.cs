using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{

    #region Function Params

    public record tr_audit_stok_opname_detail_getby_headerid_and_waktu
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public DateTime waktu_capture { get; set; } //timestamp without time zone()

    }


    public record tr_audit_stok_opname_detail_batch_getby_detailid_and_waktu
    {

        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public DateTime waktu_capture { get; set; } //timestamp without time zone()

    }


    public record tr_audit_stok_opname_header_update_after_proses
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public string keterangan_proses { get; set; }
        internal short user_proses { get; set; } //smallint()
    }


    public record tr_audit_stok_opname_header_update_after_adjust
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public DateTime waktu_capture_stok_adj { get; set; } //timestamp without time zone()
        public decimal jumlah_item_fisik_adj { get; set; } //numeric()
        public decimal total_nominal_fisik_adj { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok_adj { get; set; } //numeric()
        public string keterangan_adj { get; set; } //character varying()
        internal short user_adj { get; set; } //integer()
        public List<tr_audit_stok_opname_detail_update_after_adjust> details { get; set; }

    }



    public record tr_audit_stok_opname_detail_update_after_adjust
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public decimal qty_fisik_adj { get; set; } //numeric()
        public decimal qty_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal sub_total_fisik_adj { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok_adj { get; set; } //numeric()
        public List<tr_audit_stok_opname_detail_batch_update_after_adjust> detailBatchs { get; set; }

    }

    public record tr_audit_stok_opname_detail_batch_update_after_adjust
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public long audit_stok_opname_detail_batch_id { get; set; } //bigint()
        public decimal qty_fisik_adj { get; set; } //numeric()
        public decimal qty_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal sub_total_fisik_adj { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok_adj { get; set; } //numeric()

    }



    #endregion

    #region Barang

    #region Parameter

    public record tr_audit_stok_opname_lookup_barang_param
    {
        public long setting_stok_opname_id { get; set; } //bigint()
        public int? id_rak_storage { get; set; } //integer()
        public short id_stockroom { get; set; } //smallint()
        public DateTime waktu_capture { get; set; } //timestamp without time zone()
    }

    public record tr_audit_stok_opname_lookup_barang_batch_param
    {

        public int id_item { get; set; } //integer()
        public short id_stockroom { get; set; } //smallint()
        public DateTime waktu_capture { get; set; } //timestamp without time zone()

    }

    #endregion

    public record tr_audit_stok_opname_lookup_barang_header
    {

        public long num { get; set; } //bigint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; }
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok { get; set; } //numeric()

    }

    public record tr_audit_stok_opname_lookup_barang_batch
    {

        public long num { get; set; } //bigint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; }
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok { get; set; } //numeric()

    }

    #endregion


    #region Header

    public record tr_audit_stok_opname_header
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public string nomor_audit_stok_opname { get; set; } //character varying()
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public long setting_stok_opname_id { get; set; } //bigint()
        public int id_grup_item { get; set; } //integer()
        public string kode_grup_item { get; set; } //character varying()
        public string grup_item { get; set; } //character varying()
        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public DateTime waktu_capture_stok { get; set; } //timestamp without time zone()
        public DateTime waktu_capture_stok_adj { get; set; } //timestamp without time zone()
        public decimal jumlah_item_fisik { get; set; } //numeric()
        public decimal total_nominal_fisik { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok { get; set; } //numeric()
        public decimal jumlah_item_fisik_adj { get; set; } //numeric()
        public decimal total_nominal_fisik_adj { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal jumlah_item_proses_selisih { get; set; } //numeric()
        public decimal total_nominal_proses_selisih { get; set; } //numeric()
        public string keterangan_entry { get; set; } //character varying()
        public string keterangan_adj { get; set; } //character varying()
        public string keterangan_proses { get; set; } //character varying()
        public string status_trans { get; set; } //character varying()
        public short user_inputed { get; set; } //smallint()
        public DateTime time_inputed { get; set; } //timestamp without time zone()
        public short user_adj { get; set; } //smallint()
        public DateTime waktu_adj { get; set; } //timestamp without time zone()
        public short user_proses { get; set; } //smallint()
        public DateTime waktu_proses { get; set; } //timestamp without time zone()
        public short user_canceled { get; set; } //smallint()
        public DateTime time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying()

    }

    public record tr_audit_stok_opname_header_recursive
    {

        public long audit_stok_opname_id { get; set; } //bigint()
        public string nomor_audit_stok_opname { get; set; } //character varying(20)
        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()
        public long setting_stok_opname_id { get; set; } //bigint()
        public int id_grup_item { get; set; } //integer()
        public string kode_grup_item { get; set; } //character varying()
        public string grup_item { get; set; } //character varying()
        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()
        public DateTime? waktu_capture_stok { get; set; } //timestamp without time zone()
        public DateTime? waktu_capture_stok_adj { get; set; } //timestamp without time zone()
        public decimal jumlah_item_fisik { get; set; } //numeric()
        public decimal total_nominal_fisik { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok { get; set; } //numeric()
        public decimal? jumlah_item_fisik_adj { get; set; } //numeric()
        public decimal? total_nominal_fisik_adj { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal? jumlah_item_proses_selisih { get; set; } //numeric()
        public decimal? total_nominal_proses_selisih { get; set; } //numeric()
        public string keterangan_entry { get; set; } //character varying(50)
        public string keterangan_adj { get; set; } //character varying(50)
        public string keterangan_proses { get; set; } //character varying(50)
        public string status_trans { get; set; } //character varying(10)
        internal short user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()
        public short? user_adj { get; set; } //integer()
        public DateTime? waktu_adj { get; set; } //timestamp without time zone()
        public short? user_proses { get; set; } //integer()
        public DateTime? waktu_proses { get; set; } //timestamp without time zone()
        public short? user_canceled { get; set; } //smallint()
        public DateTime? time_canceled { get; set; } //timestamp without time zone()
        public string reason_canceled { get; set; } //character varying(50)

        private string _details { get; set; }

        public List<tr_audit_stok_opname_detail> details
        {
            get => JsonConvert.DeserializeObject<List<tr_audit_stok_opname_detail>>(this._details.Replace("_batchs", "batchs"));

            set
            {
                this._details = JsonConvert.SerializeObject(value);
            }
        }

    }

    public record tr_audit_stok_opname_header_insert
    {

        internal string nomor_audit_stok_opname { get; set; } //character varying()
        public short id_stockroom { get; set; } //smallint()
        public long setting_stok_opname_id { get; set; } //bigint()
        public int? id_grup_item { get; set; } //integer()
        public int? id_rak_storage { get; set; } //integer()
        public DateTime? waktu_capture_stok { get; set; } //timestamp without time zone()
        public decimal jumlah_item_fisik { get; set; } //numeric()
        public decimal total_nominal_fisik { get; set; } //numeric()
        public decimal jumlah_item_sistem_capture_stok { get; set; } //numeric()
        public decimal total_nominal_sistem_capture_stok { get; set; } //numeric()
        public string keterangan_entry { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        public List<tr_audit_stok_opname_detail_insert> details { get; set; }

    }


    #endregion

    #region Detail

    public record tr_audit_stok_opname_detail_for_adjustment_finalisasi
    {

        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public long audit_stok_opname_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal qty_proses_selisih { get; set; } //numeric()
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal sub_total_proses_selisih { get; set; } //numeric()

        internal string _batchs { get; set; }
        internal List<tr_audit_stok_opname_detail_batch_for_adjustment_finalisasi> _listBatchs { get; set; }

        public List<tr_audit_stok_opname_detail_batch_for_adjustment_finalisasi> batchs
        {
            get
            {
                //dari json maupun dari list
                if (this._batchs is not null)
                {
                    return JsonConvert.DeserializeObject<List<tr_audit_stok_opname_detail_batch_for_adjustment_finalisasi>>(this._batchs);
                }
                else
                {
                    return _listBatchs;
                }
            }

            set
            {
                this._batchs = JsonConvert.SerializeObject(value);
                this._listBatchs = value;
            }
        }

    }

    public record tr_audit_stok_opname_detail
    {

        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public long audit_stok_opname_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string barcode { get; set; } //character varying()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()

        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal? qty_fisik_adj { get; set; } //numeric()
        public decimal? qty_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal? qty_proses_selisih { get; set; } //numeric()
        public string keterangan { get; set; } //character varying(30)
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal? sub_total_sistem_capture_stok { get; set; } //numeric()
        public decimal? sub_total_fisik_adj { get; set; } //numeric()
        public decimal? sub_total_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal? sub_total_proses_selisih { get; set; } //numeric()

        internal string _batchs { get; set; }
        internal List<tr_audit_stok_opname_detail_batch> _listBatchs { get; set; }

        public List<tr_audit_stok_opname_detail_batch> batchs
        {
            get
            {
                //dari json maupun dari list
                if (this._batchs is not null)
                {
                    return JsonConvert.DeserializeObject<List<tr_audit_stok_opname_detail_batch>>(this._batchs);
                }
                else
                {
                    return _listBatchs;
                }
            }

            set
            {
                this._batchs = JsonConvert.SerializeObject(value);
                this._listBatchs = value;
            }
        }

    }
    public record tr_audit_stok_opname_detail_insert
    {
        internal long audit_stok_opname_id { get; set; } //bigint()
        public short no_urut { get; set; } //smallint()
        public int id_item { get; set; } //integer()
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok { get; set; } //numeric()
        public List<tr_audit_stok_opname_detail_batch_insert> detailBatchs { get; set; }
    }

    #endregion

    #region Detail Batch

    public record tr_audit_stok_opname_detail_batch_for_adjustment_finalisasi
    {

        public long audit_stok_opname_detail_batch_id { get; set; } //bigint()
        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public long audit_stok_opname_id { get; set; } //bigint()

        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()

        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal qty_proses_selisih { get; set; } //numeric()
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal sub_total_proses_selisih { get; set; } //numeric()

    }


    public record tr_audit_stok_opname_detail_batch
    {

        public long audit_stok_opname_detail_batch_id { get; set; } //bigint()
        public long audit_stok_opname_detail_id { get; set; } //bigint()
        public long audit_stok_opname_id { get; set; } //bigint()
        public string batch_number { get; set; } //character varying()
        public DateTime? expired_date { get; set; } //date()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; }
        public string nama_item { get; set; }
        public string barcode { get; set; }
        public string kode_satuan { get; set; } //character varying()
        public string nama_satuan { get; set; } //character varying()

        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal qty_fisik_adj { get; set; } //numeric()
        public decimal qty_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal qty_proses_selisih { get; set; } //numeric()
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok { get; set; } //numeric()
        public decimal sub_total_fisik_adj { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok_adj { get; set; } //numeric()
        public decimal sub_total_proses_selisih { get; set; } //numeric()

    }

    public record tr_audit_stok_opname_detail_batch_insert
    {

        internal long audit_stok_opname_detail_id { get; set; } //bigint()
        internal long audit_stok_opname_id { get; set; } //bigint()
        public string batch_number { get; set; } //character varying()
        public DateTime expired_date { get; set; } //timestamp without time zone()
        public string keterangan { get; set; } //character varying()
        public decimal hpp_average { get; set; } //numeric()
        public decimal harga_jual { get; set; } //numeric()
        public decimal qty_fisik { get; set; } //numeric()
        public decimal qty_sistem_capture_stok { get; set; } //numeric()
        public decimal sub_total_fisik { get; set; } //numeric()
        public decimal sub_total_sistem_capture_stok { get; set; } //numeric()
    }


    #endregion

}
