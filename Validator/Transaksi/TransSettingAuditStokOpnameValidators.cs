using FluentValidation;
using Pharmm.API.Models.Transaksi;

namespace Pharmm.API.Validator.Transaksi
{
    #region Header

    public class TrSettingAuditStokOpnameHeaderGrupValidators : AbstractValidator<tr_setting_audit_stok_opname_header_grup_insert>
    {
        public TrSettingAuditStokOpnameHeaderGrupValidators()
        {
            RuleFor(x => x.tanggal_stok_opname).NotEmpty();
            RuleFor(x => x.id_jenis_setting_stok_opname).NotEmpty();

            RuleFor(x => x.details).NotEmpty();
        }
    }

    public class TrSettingAuditStokOpnameHeaderItemValidators : AbstractValidator<tr_setting_audit_stok_opname_header_item_insert>
    {
        public TrSettingAuditStokOpnameHeaderItemValidators()
        {
            RuleFor(x => x.tanggal_stok_opname).NotEmpty();
            RuleFor(x => x.id_jenis_setting_stok_opname).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }

    public class TrSettingAuditStokOpnameHeaderRakValidators : AbstractValidator<tr_setting_audit_stok_opname_header_rak_storage_insert>
    {
        public TrSettingAuditStokOpnameHeaderRakValidators()
        {
            RuleFor(x => x.tanggal_stok_opname).NotEmpty();
            RuleFor(x => x.id_jenis_setting_stok_opname).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }

    public class TrSettingAuditStokOpnameHeaderStockroomValidators : AbstractValidator<tr_setting_audit_stok_opname_header_semua_item_insert>
    {
        public TrSettingAuditStokOpnameHeaderStockroomValidators()
        {
            RuleFor(x => x.tanggal_stok_opname).NotEmpty();
            RuleFor(x => x.id_jenis_setting_stok_opname).NotEmpty();
            RuleFor(x => x.details).NotEmpty();
        }
    }

    #endregion

    #region Detail

    public class TrSettingAuditStokOpnameDetailStockroomValidators : AbstractValidator<tr_setting_audit_stok_opname_detail_stockroom_insert>
    {
        public TrSettingAuditStokOpnameDetailStockroomValidators()
        {

            RuleFor(x => x.id_stockroom).NotEmpty();
        }
    }

    public class TrSettingAuditStokOpnameDetailRakStorageValidators : AbstractValidator<tr_setting_audit_stok_opname_detail_rak_storage_insert>
    {
        public TrSettingAuditStokOpnameDetailRakStorageValidators()
        {

            RuleFor(x => x.id_rak_storage)
            .NotNull().WithMessage("Id Rak Storage tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Rak Storage tidak boleh kosong!");
        }
    }

    public class TrSettingAuditStokOpnameDetailItemValidators : AbstractValidator<tr_setting_audit_stok_opname_detail_item_insert>
    {
        public TrSettingAuditStokOpnameDetailItemValidators()
        {

            RuleFor(x => x.id_item)
            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
        }
    }

    public class TrSettingAuditStokOpnameDetailGrupValidators : AbstractValidator<tr_setting_audit_stok_opname_detail_grup_insert>
    {
        public TrSettingAuditStokOpnameDetailGrupValidators()
        {
            RuleFor(x => x.id_grup_item).NotEmpty();
        }
    }

    #endregion

}
