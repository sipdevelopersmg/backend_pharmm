using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupGrupItemValidators : AbstractValidator<mm_setup_grup_item_update>
    {
        public SetupGrupItemValidators()
        {
            


            RuleFor(x => x.id_grup_item)
                            .NotNull().WithMessage("Id Grup Item tidak boleh kosong!");
            RuleFor(x => x.id_tipe_item)
                            .NotNull().WithMessage("Id Tipe Item tidak boleh kosong!");
            RuleFor(x => x.kode_grup_item)
                            .NotNull().WithMessage("Kode Grup Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Grup Item tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.grup_item)
                            .NotNull().WithMessage("Grup Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Grup Item tidak boleh kosong!").MaximumLength(50);



        }

    }

    public class SetupGrupItemInsertValidators : AbstractValidator<mm_setup_grup_item_insert>
    {
        public SetupGrupItemInsertValidators()
        {
            

            RuleFor(x => x.id_tipe_item)
                            .NotNull().WithMessage("Id Tipe Item tidak boleh kosong!");
            RuleFor(x => x.kode_grup_item)
                            .NotNull().WithMessage("Kode Grup Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Grup Item tidak boleh kosong!");
            RuleFor(x => x.grup_item)
                            .NotNull().WithMessage("Grup Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Grup Item tidak boleh kosong!");

        }

    }

    public class SetupGrupItemUpdateStatusActiveValidators : AbstractValidator<mm_setup_grup_item_update_status_to_active>
    {
        public SetupGrupItemUpdateStatusActiveValidators()
        {
            


            RuleFor(x => x.id_grup_item)
                            .NotNull().WithMessage("Id Grup Item tidak boleh kosong!");

        }

    }


    public class SetupGrupItemUpdateStatusDeActiveValidators : AbstractValidator<mm_setup_grup_item_update_status_to_deactive>
    {
        public SetupGrupItemUpdateStatusDeActiveValidators()
        {
            


            RuleFor(x => x.id_grup_item)
                            .NotNull().WithMessage("Id Grup Item tidak boleh kosong!");

        }

    }

}
