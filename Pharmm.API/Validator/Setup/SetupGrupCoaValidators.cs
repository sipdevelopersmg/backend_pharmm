using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupGrupCoaUpdateValidators : AbstractValidator<akun_setup_grup_coa>
    {
        public SetupGrupCoaUpdateValidators()
        {
            

            RuleFor(x => x.id_grup_coa)
                .NotNull().WithMessage("Id Grup Coa tidak boleh kosong!");
            RuleFor(x => x.grup_coa)
                            .NotNull().WithMessage("Grup Coa tidak boleh kosong!")
                            .NotEmpty().WithMessage("Grup Coa tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.deskripsi)
                            .NotNull().WithMessage("Deskripsi tidak boleh kosong!")
                            .NotEmpty().WithMessage("Deskripsi tidak boleh kosong!").MaximumLength(100);

        }

    }

    public class SetupGrupCoaInsertValidators : AbstractValidator<akun_setup_grup_coa_insert>
    {
        public SetupGrupCoaInsertValidators()
        {
            

            RuleFor(x => x.grup_coa)
                            .NotNull().WithMessage("Grup Coa tidak boleh kosong!")
                            .NotEmpty().WithMessage("Grup Coa tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.deskripsi)
                            .NotNull().WithMessage("Deskripsi tidak boleh kosong!")
                            .NotEmpty().WithMessage("Deskripsi tidak boleh kosong!").MaximumLength(100);

        }

    }

}
