using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public class SetupAuditPenanggungJawabRakStorageValidators : AbstractValidator<mm_setup_audit_penanggung_jawab_rak_storage_insert>
    {
        public SetupAuditPenanggungJawabRakStorageValidators()
        {

            RuleFor(x => x.nik_penanggung_jawab_rak_storage)
            .NotNull().WithMessage("Nik Penanggung Jawab Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nik Penanggung Jawab Rak Storage tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.nama_penanggung_jawab_rak_storage)
            .NotNull().WithMessage("Nama Penanggung Jawab Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Penanggung Jawab Rak Storage tidak boleh kosong!").MaximumLength(50);
        }
    }

    public class SetupAuditPenanggungJawabRakStorageUpdateValidators : AbstractValidator<mm_setup_audit_penanggung_jawab_rak_storage_update>
    {
        public SetupAuditPenanggungJawabRakStorageUpdateValidators()
        {

            RuleFor(x => x.id_penanggung_jawab_rak_storage)
            .NotNull().WithMessage("Id Penanggung Jawab Rak Storage tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Penanggung Jawab Rak Storage tidak boleh kosong!");
            RuleFor(x => x.nik_penanggung_jawab_rak_storage)
            .NotNull().WithMessage("Nik Penanggung Jawab Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nik Penanggung Jawab Rak Storage tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.nama_penanggung_jawab_rak_storage)
            .NotNull().WithMessage("Nama Penanggung Jawab Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Penanggung Jawab Rak Storage tidak boleh kosong!").MaximumLength(50);

        }
    }





}
