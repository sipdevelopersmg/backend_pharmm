using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupCoaUpdateValidators : AbstractValidator<akun_setup_coa_update>
    {
        public SetupCoaUpdateValidators()
        {
            

            RuleFor(x => x.id_coa)
                            .NotNull().WithMessage("Id Coa tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Coa tidak boleh kosong!");
            RuleFor(x => x.id_grup_coa)
                            .NotEmpty().WithMessage("Id Grup Coa tidak boleh kosong!");
            RuleFor(x => x.kode_coa)
                            .NotNull().WithMessage("Kode Coa tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Coa tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.deskripsi)
                            .NotNull().WithMessage("Deskripsi tidak boleh kosong!")
                            .NotEmpty().WithMessage("Deskripsi tidak boleh kosong!").MaximumLength(100);
            RuleFor(x => x.saldo)
                            .NotNull().WithMessage("Saldo tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Saldo tidak boleh kosong!");

        }

    }

    public class SetupCoaInsertValidators : AbstractValidator<akun_setup_coa_insert>
    {
        public SetupCoaInsertValidators()
        {
            

            RuleFor(x => x.id_grup_coa)
                            .NotEmpty().WithMessage("Id Grup Coa tidak boleh kosong!");
            RuleFor(x => x.kode_coa)
                            .NotNull().WithMessage("Kode Coa tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Coa tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.deskripsi)
                            .NotNull().WithMessage("Deskripsi tidak boleh kosong!")
                            .NotEmpty().WithMessage("Deskripsi tidak boleh kosong!").MaximumLength(100);
            //RuleFor(x => x.id_coa_parent)
            //                .NotNull().WithMessage("Id Coa Parent tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Id Coa Parent tidak boleh kosong!");
            RuleFor(x => x.saldo)
                            .NotNull().WithMessage("Saldo tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Saldo tidak boleh kosong!");

        }

    }

    public class SetupCoaUpdateToActiveValidators : AbstractValidator<akun_setup_coa_update_status_to_active>
    {
        public SetupCoaUpdateToActiveValidators()
        {
            


            RuleFor(x => x.id_coa)
                            .NotNull().WithMessage("Id Coa tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Coa tidak boleh kosong!");

        }

    }

    public class SetupCoaUpdateToDeActiveValidators : AbstractValidator<akun_setup_coa_update_status_to_deactive>
    {
        public SetupCoaUpdateToDeActiveValidators()
        {
            


            RuleFor(x => x.id_coa)
                            .NotNull().WithMessage("Id Coa tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Coa tidak boleh kosong!");

        }
    }
}
