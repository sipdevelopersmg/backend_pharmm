using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Pharmm.API.Models.Setup;

namespace Pharmm.API.Validator.Setup
{
    public class MmSetupJenisSettingAuditStokOpnameValidators : AbstractValidator<mm_setup_jenis_setting_audit_stok_opname_insert>
    {
        public MmSetupJenisSettingAuditStokOpnameValidators()
        {

            RuleFor(x => x.kode_jenis_setting_stok_opname)
            .NotNull().WithMessage("Kode Jenis Setting Stok Opname tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Jenis Setting Stok Opname tidak boleh kosong!").MaximumLength(5);
            RuleFor(x => x.nama_jenis_setting_stok_opname)
            .NotNull().WithMessage("Nama Jenis Setting Stok Opname tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Jenis Setting Stok Opname tidak boleh kosong!").MaximumLength(30);

        }

    }
}
