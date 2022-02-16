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

    public class TrAuditStokOpnameNoEdLookupBarangValidators : AbstractValidator<tr_audit_stok_opname_no_ed_lookup_barang_param>
    {
        private readonly TransSettingAuditStokOpnameDao _dao;

        public TrAuditStokOpnameNoEdLookupBarangValidators(
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
                .When((model, field) => CekJenisSetting(model.setting_stok_opname_id).Result == 3)
                .WithMessage("Rak tidak boleh kosong jika jenis setting Per Rak");
        }

        //public GetJenisSetting(long setting_stok_opname_id)
        //{

        //}
    }

    #endregion


    public class TrAuditStokOpnameNoEdHeaderNoEdValidators : AbstractValidator<tr_audit_stok_opname_no_ed_header_insert>
    {
        private readonly TransSettingAuditStokOpnameDao _dao;

        public TrAuditStokOpnameNoEdHeaderNoEdValidators(
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

        }
    }

    public class TrAuditStokOpnameNoEdDetailValidators : AbstractValidator<tr_audit_stok_opname_no_ed_detail_insert>
    {
        public TrAuditStokOpnameNoEdDetailValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.hpp_average)
            .NotNull().WithMessage("Hpp Average tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Hpp Average tidak boleh kosong!");
            RuleFor(x => x.harga_jual)
            .NotNull().WithMessage("Harga Jual tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Jual tidak boleh kosong!");


        }
    }

    #region Adjustment

    public class TrAuditStokOpnameNoEdAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_no_ed_header_update_after_adjust>
    {
        public TrAuditStokOpnameNoEdAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.waktu_capture_stok_adj).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }


    public class TrAuditStokOpnameNoEdDetailAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_no_ed_detail_update_after_adjust>
    {
        public TrAuditStokOpnameNoEdDetailAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.audit_stok_opname_detail_id).NotEmpty();
        }
    }

    #endregion

    #region Finalisasi

    public class TrAuditStokOpnameNoEdFinalisasiValidators : AbstractValidator<tr_audit_stok_opname_no_ed_header_update_after_proses>
    {
        public TrAuditStokOpnameNoEdFinalisasiValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
        }
    }

    #endregion




}
