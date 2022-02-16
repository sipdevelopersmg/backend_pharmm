using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator.Setup
{
    public class SetupAuditRakStorageValidators : AbstractValidator<mm_setup_audit_rak_storage_insert>
    {
        public SetupAuditRakStorageValidators()
        {

            RuleFor(x => x.kode_rak_storage)
            .NotNull().WithMessage("Kode Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Rak Storage tidak boleh kosong!").MaximumLength(15);
            RuleFor(x => x.nama_rak_storage)
            .NotNull().WithMessage("Nama Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Rak Storage tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.id_stockroom).NotEmpty();

        }
    }

    public class SetupAuditRakStorageUpdateValidators : AbstractValidator<mm_setup_audit_rak_storage_update>
    {
        public SetupAuditRakStorageUpdateValidators()
        {


            RuleFor(x => x.id_rak_storage)
            .NotNull().WithMessage("Id Rak Storage tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Rak Storage tidak boleh kosong!");
            RuleFor(x => x.kode_rak_storage)
            .NotNull().WithMessage("Kode Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Rak Storage tidak boleh kosong!").MaximumLength(15);
            RuleFor(x => x.nama_rak_storage)
            .NotNull().WithMessage("Nama Rak Storage tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Rak Storage tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_penanggung_jawab_rak_storage).NotEmpty();
        }
    }


    public class SetupAuditRakStorageUpdateRakBarangValidators : AbstractValidator<mm_setup_item_update_rak_storage>
    {
        public SetupAuditRakStorageUpdateRakBarangValidators()
        {

            RuleFor(x => x.id_item).NotEmpty();

            RuleFor(x => x.id_rak_storage).NotEmpty();
        }
    }


}
