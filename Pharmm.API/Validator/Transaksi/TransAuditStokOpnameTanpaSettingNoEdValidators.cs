using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator.Transaksi
{
    public class TrAuditStokOpnameTanpaSettingNoEdHeaderValidators : AbstractValidator<tr_audit_stok_opname_no_ed_setting_header_insert>
    {
        public TrAuditStokOpnameTanpaSettingNoEdHeaderValidators()
        {
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.waktu_capture_stok).NotEmpty();

        }
    }

    public class TrAuditStokOpnameTanpaSettingNoEdDetailValidators : AbstractValidator<tr_audit_stok_opname_no_ed_setting_detail_insert>
    {
        public TrAuditStokOpnameTanpaSettingNoEdDetailValidators()
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

    public class TrAuditStokOpnameTanpaSettingNoEdAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_no_ed_setting_header_update_after_adjust>
    {
        public TrAuditStokOpnameTanpaSettingNoEdAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.waktu_capture_stok_adj).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }


    public class TrAuditStokOpnameTanpaSettingNoEdDetailAdjustmentValidators : AbstractValidator<tr_audit_stok_opname_no_ed_setting_detail_update_after_adjust>
    {
        public TrAuditStokOpnameTanpaSettingNoEdDetailAdjustmentValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
            RuleFor(x => x.audit_stok_opname_detail_id).NotEmpty();
        }
    }

    #endregion

    #region Finalisasi

    public class TrAuditStokOpnameTanpaSettingNoEdFinalisasiValidators : AbstractValidator<tr_audit_stok_opname_no_ed_setting_header_update_after_proses>
    {
        public TrAuditStokOpnameTanpaSettingNoEdFinalisasiValidators()
        {
            RuleFor(x => x.audit_stok_opname_id).NotEmpty();
        }
    }

    #endregion




}
