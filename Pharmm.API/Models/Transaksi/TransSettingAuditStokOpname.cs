using Newtonsoft.Json;
using Pharmm.API.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Models.Transaksi
{


    #region Header

    //untuk history setting stok opname
    public record tr_setting_audit_stok_opname_header_recursive
    {

        public long setting_stok_opname_id { get; set; } //bigint()
        public string no_setting_stok_opname { get; set; } //character varying()
        public DateTime tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string kode_jenis_setting_stok_opname { get; set; } //character varying()
        public string nama_jenis_setting_stok_opname { get; set; } //character varying()
        public bool is_active { get; set; } //boolean()
        public string keterangan { get; set; } //character varying()
        public short user_inputed { get; set; } //smallint()
        public string user_name_inputed { get => HttpDataPisHelper.GetDataUser(user_inputed).Result?.full_name; } //smallint()
        public DateTime time_inputed { get; set; } //timestamp without time zone()

        private string _grups { get; set; }
        private string _items { get; set; }
        private string _raks { get; set; }
        private string _stockrooms { get; set; }

        public List<tr_setting_audit_stok_opname_detail_grup> grups
        {
            get => JsonConvert.DeserializeObject<List<tr_setting_audit_stok_opname_detail_grup>>(this._grups);

            set
            {
                this._grups = JsonConvert.SerializeObject(value);
            }
        }

        public List<tr_setting_audit_stok_opname_detail_item> items
        {
            get => JsonConvert.DeserializeObject<List<tr_setting_audit_stok_opname_detail_item>>(this._items);

            set
            {
                this._items = JsonConvert.SerializeObject(value);
            }
        }

        public List<tr_setting_audit_stok_opname_detail_rak_storage> raks
        {
            get => JsonConvert.DeserializeObject<List<tr_setting_audit_stok_opname_detail_rak_storage>>(this._raks);

            set
            {
                this._raks = JsonConvert.SerializeObject(value);
            }
        }

        public List<tr_setting_audit_stok_opname_detail_stockroom> stockrooms
        {
            get => JsonConvert.DeserializeObject<List<tr_setting_audit_stok_opname_detail_stockroom>>(this._stockrooms);

            set
            {
                this._stockrooms = JsonConvert.SerializeObject(value);
            }
        }

    }

    public record tr_setting_audit_stok_opname_header
    {

        public long setting_stok_opname_id { get; set; } //bigint()
        public string no_setting_stok_opname { get; set; } //character varying(20)
        public DateTime? tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string kode_jenis_setting_stok_opname { get; set; }
        public string nama_jenis_setting_stok_opname { get; set; }
        public bool is_active { get; set; } //boolean()
        public string keterangan { get; set; } //character varying(50)
        public short user_inputed { get; set; } //smallint()
        public DateTime? time_inputed { get; set; } //timestamp without time zone()

    }

    public record tr_setting_audit_stok_opname_header_item_insert
    {

        public string no_setting_stok_opname { get; set; } //character varying()
        public DateTime? tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        public List<tr_setting_audit_stok_opname_detail_item_insert> details { get; set; }
        public List<tr_setting_audit_stok_opname_detail_stockroom_insert> detailGudangs { get; set; }

    }

    public record tr_setting_audit_stok_opname_header_rak_storage_insert
    {

        public string no_setting_stok_opname { get; set; } //character varying()
        public DateTime? tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        public List<tr_setting_audit_stok_opname_detail_rak_storage_insert> details { get; set; }
        public List<tr_setting_audit_stok_opname_detail_stockroom_insert> detailGudangs { get; set; }

    }

    public record tr_setting_audit_stok_opname_header_grup_insert
    {

        public string no_setting_stok_opname { get; set; } //character varying()
        public DateTime? tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        public List<tr_setting_audit_stok_opname_detail_grup_insert> details { get; set; }
        public List<tr_setting_audit_stok_opname_detail_stockroom_insert> detailGudangs { get; set; }

    }

    public record tr_setting_audit_stok_opname_header_semua_item_insert
    {

        public string no_setting_stok_opname { get; set; } //character varying()
        public DateTime? tanggal_stok_opname { get; set; } //timestamp without time zone()
        public short id_jenis_setting_stok_opname { get; set; } //smallint()
        public string keterangan { get; set; } //character varying()
        internal short? user_inputed { get; set; } //smallint()
        public List<tr_setting_audit_stok_opname_detail_stockroom_insert> details { get; set; }
    }

    #endregion


    #region Detail

    public record tr_setting_audit_stok_opname_detail_item
    {

        public long setting_stok_opname_id { get; set; } //bigint()
        public int id_item { get; set; } //integer()
        public string kode_item { get; set; } //character varying()
        public string nama_item { get; set; } //character varying()
        public string barcode { get; set; } //character varying()

    }

    public record tr_setting_audit_stok_opname_detail_item_insert
    {

        internal long setting_stok_opname_id { get; set; } //bigint()
        public int id_item { get; set; } //integer()

    }

    public record tr_setting_audit_stok_opname_detail_rak_storage
    {

        public long setting_stok_opname_id { get; set; } //bigint()
        public int id_rak_storage { get; set; } //integer()
        public string kode_rak_storage { get; set; } //character varying()
        public string nama_rak_storage { get; set; } //character varying()

    }

    public record tr_setting_audit_stok_opname_detail_rak_storage_insert
    {

        internal long setting_stok_opname_id { get; set; } //bigint()
        public int id_rak_storage { get; set; } //integer()

    }

    public record tr_setting_audit_stok_opname_detail_grup
    {

        public long setting_stok_opname_id { get; set; } //bigint()
        public short id_grup_item { get; set; } //smallint()
        public string kode_grup_item { get; set; } //character varying()
        public string grup_item { get; set; } //character varying()

    }

    public record tr_setting_audit_stok_opname_detail_grup_insert
    {

        internal long setting_stok_opname_id { get; set; } //bigint()
        public int id_grup_item { get; set; } //integer()

    }

    public record tr_setting_audit_stok_opname_detail_stockroom
    {

        public short id_stockroom { get; set; } //smallint()
        public string kode_stockroom { get; set; } //character varying()
        public string nama_stockroom { get; set; } //character varying()

    }

    public record tr_setting_audit_stok_opname_detail_stockroom_insert
    {

        internal long setting_stok_opname_id { get; set; } //bigint()
        public short id_stockroom { get; set; } //integer()

    }

    #endregion
}
