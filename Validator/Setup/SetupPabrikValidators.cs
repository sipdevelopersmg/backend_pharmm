using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupPabrikInsertValidators : AbstractValidator<mm_setup_pabrik_insert>
    {
        public SetupPabrikInsertValidators()
        {
            

            RuleFor(x => x.kode_pabrik)
                            .NotNull().WithMessage("Kode Pabrik tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Pabrik tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.nama_pabrik)
                            .NotNull().WithMessage("Nama Pabrik tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Pabrik tidak boleh kosong!").MaximumLength(50);

        }
    }

    public class SetupPabrikUpdateValidators : AbstractValidator<mm_setup_pabrik_update>
    {
        public SetupPabrikUpdateValidators()
        {
            

            RuleFor(x => x.id_pabrik)
                            .NotNull().WithMessage("Id Pabrik tidak boleh kosong!");
            RuleFor(x => x.kode_pabrik)
                            .NotNull().WithMessage("Kode Pabrik tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Pabrik tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.nama_pabrik)
                            .NotNull().WithMessage("Nama Pabrik tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Pabrik tidak boleh kosong!").MaximumLength(50);

        }
    }

    public class SetupPabrikUpdateStatusActiveValidators : AbstractValidator<mm_setup_pabrik_update_status_to_active>
    {
        public SetupPabrikUpdateStatusActiveValidators()
        {
            


            RuleFor(x => x.id_pabrik)
                            .NotNull().WithMessage("Id Pabrik tidak boleh kosong!");

        }
    }

    public class SetupPabrikUpdateStatusDeActiveValidators : AbstractValidator<mm_setup_pabrik_update_status_to_deactive>
    {
        public SetupPabrikUpdateStatusDeActiveValidators()
        {
            


            RuleFor(x => x.id_pabrik)
                            .NotNull().WithMessage("Id Pabrik tidak boleh kosong!");


        }
    }


}
