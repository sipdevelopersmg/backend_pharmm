using FluentValidation;
using Pharmm.API.Dao.Transaksi;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator.Transaksi
{

    #region Lookup

    public class TrAuditStokOpnameLookupBarangValidators : AbstractValidator<tr_audit_stok_opname_lookup_barang_param>
    {
        private readonly TransSettingAuditStokOpnameDao _dao;

        public TrAuditStokOpnameLookupBarangValidators(
            TransSettingAuditStokOpnameDao dao
            )
        {
            this._dao = dao;

            async Task<int?> CekJenisSetting(long idSetting)
            {
                var setting = await this._dao.GetTrSettingAuditStokOpnameHeaderById(idSetting);

                return setting?.id_jenis_setting_stok_opname;
            }

            RuleFor(x => x.id_rak_storage)
                .NotEmpty()
                .When((model,field) => CekJenisSetting(model.setting_stok_opname_id).Result == 3)
                .WithMessage("Rak tidak boleh kosong jika jenis setting Per Rak");
        }

        //public GetJenisSetting(long setting_stok_opname_id)
        //{
            
        //}
    }

    #endregion


        public class TrAuditStokOpnameHeaderValidators : AbstractValidator<tr_audit_stok_opname_header_insert>
    {
        private readonly TransSettingAuditStokOpnameDao _dao;

        public TrAuditStokOpnameHeaderValidators(
            TransSettingAuditStokOpnameDao dao
            )
        {
            this._dao = dao;

            async Task<int?> CekJenisSetting(long idSetting)
            {
                var setting = await this._dao.GetTrSettingAuditStokOpnameHeaderById(idSetting);

                return setting?.id_jenis_setting_stok_opname;
            }

            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.setting_stok_opname_id)
            .NotNull().WithMessage("Setting Stok Opname Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Setting Stok Opname Id tidak boleh kosong!");
            //RuleFor(x => x.id_rak_storage)
            //.NotNull().WithMessage("Id Rak Storage tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Id Rak Storage tidak boleh kosong!");

            RuleFor(x => x.id_rak_storage)
                .NotEmpty()
                .When((model, field) => CekJenisSetting(model.setting_stok_opname_id).Result == 3)
                .WithMessage("Rak tidak boleh kosong jika jenis setting Per Rak");

            RuleFor(x => x.waktu_capture_stok).NotEmpty();

            //RuleFor(x => x.jumlah_item_fisik)
            //.NotNull().WithMessage("Jumlah Item Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Jumlah Item Fisik tidak boleh kosong!");
            //RuleFor(x => x.total_nominal_fisik)
            //.NotNull().WithMessage("Total Nominal Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Total Nominal Fisik tidak boleh kosong!");
            //RuleFor(x => x.jumlah_item_sistem_capture_stok)
            //.NotNull().WithMessage("Jumlah Item Sistem Capture Stok tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Jumlah Item Sistem Capture Stok tidak boleh kosong!");
            //RuleFor(x => x.total_nominal_sistem_capture_stok)
            //.NotNull().WithMessage("Total Nominal Sistem Capture Stok tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Total Nominal Sistem Capture Stok tidak boleh kosong!");

        }
    }

    public class TrAuditStokOpnameDetailValidators : AbstractValidator<tr_audit_stok_opname_detail_insert>
    {
        public TrAuditStokOpnameDetailValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            //RuleFor(x => x.qty_fisik)
            //.NotNull().WithMessage("Qty Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty Fisik tidak boleh kosong!");
            //RuleFor(x => x.qty_sistem_capture_stok)
            //.NotNull().WithMessage("Qty Sistem Capture Stok tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty Sistem Capture Stok tidak boleh kosong!");
            RuleFor(x => x.hpp_average)
            .NotNull().WithMessage("Hpp Average tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Hpp Average tidak boleh kosong!");
            RuleFor(x => x.harga_jual)
            .NotNull().WithMessage("Harga Jual tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Jual tidak boleh kosong!");
            //RuleFor(x => x.sub_total_fisik)
            //.NotNull().WithMessage("Sub Total Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Sub Total Fisik tidak boleh kosong!");
            //RuleFor(x => x.detailBatchs).NotEmpty();


        }
    }


    public class TrAuditStokOpnameDetailBatchValidators : AbstractValidator<tr_audit_stok_opname_detail_batch_insert>
    {
        public TrAuditStokOpnameDetailBatchValidators()
        {
            RuleFor(x => x.batch_number)
            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            //RuleFor(x => x.qty_fisik)
            //.NotNull().WithMessage("Qty Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty Fisik tidak boleh kosong!");
            //RuleFor(x => x.qty_sistem_capture_stok)
            //.NotNull().WithMessage("Qty Sistem Capture Stok tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty Sistem Capture Stok tidak boleh kosong!");
            RuleFor(x => x.hpp_average)
            .NotNull().WithMessage("Hpp Average tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Hpp Average tidak boleh kosong!");
            RuleFor(x => x.harga_jual)
            .NotNull().WithMessage("Harga Jual tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Jual tidak boleh kosong!");
            //RuleFor(x => x.sub_total_fisik)
            //.NotNull().WithMessage("Sub Total Fisik tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Sub Total Fisik tidak boleh kosong!");

        }
    }

    #region Adjustment

    public class TrAuditStokOpnameAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_header_update_after_adjust>
    {
        public TrAuditStokOpnameAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            //RuleFor(x => x.jumlah_item_fisik_adj).NotEmpty();
            //RuleFor(x => x.jumlah_item_sistem_capture_stok_adj).NotEmpty();
            //RuleFor(x => x.total_nominal_fisik_adj).NotEmpty();
            //RuleFor(x => x.total_nominal_sistem_capture_stok_adj).NotEmpty();
            RuleFor(x => x.waktu_capture_stok_adj).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }


    public class TrAuditStokOpnameDetailAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_detail_update_after_adjust>
    {
        public TrAuditStokOpnameDetailAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.audit_stok_opname_detail_id).NotEmpty();
            //RuleFor(x => x.qty_fisik_adj).NotEmpty();
            //RuleFor(x => x.qty_sistem_capture_stok_adj).NotEmpty();
            //RuleFor(x => x.sub_total_fisik_adj).NotEmpty();
            //RuleFor(x => x.sub_total_sistem_capture_stok_adj).NotEmpty();
            //RuleFor(x => x.detailBatchs).NotEmpty();
        }
    }



    public class TrAuditStokOpnameDetailBatchAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_detail_batch_update_after_adjust>
    {
        public TrAuditStokOpnameDetailBatchAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.audit_stok_opname_detail_id).NotEmpty();
            RuleFor(x => x.audit_stok_opname_detail_batch_id).NotEmpty();
            //RuleFor(x => x.qty_fisik_adj).NotEmpty();
            //RuleFor(x => x.qty_sistem_capture_stok_adj).NotEmpty();
            //RuleFor(x => x.sub_total_fisik_adj).NotEmpty();
            //RuleFor(x => x.sub_total_sistem_capture_stok_adj).NotEmpty();
        }
    }

    #endregion

    #region Finalisasi

    public class TrAuditStokOpnameFinalisasiValidators : AbstractValidator<tr_audit_stok_opname_header_update_after_proses>
    {
        public TrAuditStokOpnameFinalisasiValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
        }
    }

    #endregion




}
