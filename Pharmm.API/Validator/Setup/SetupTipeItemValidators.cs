using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupTipeItemUpdateValidators : AbstractValidator<mm_setup_tipe_item_update>
    {
        public SetupTipeItemUpdateValidators()
        {
            


            RuleFor(x => x.id_tipe_item)
                            .NotNull().WithMessage("Id Tipe Item tidak boleh kosong!");
            RuleFor(x => x.kode_tipe_item)
                            .NotNull().WithMessage("Kode Tipe Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Tipe Item tidak boleh kosong!");
            RuleFor(x => x.tipe_item)
                            .NotNull().WithMessage("Tipe Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Item tidak boleh kosong!");

        }

    }

    public class SetupTipeItemInsertValidators : AbstractValidator<mm_setup_tipe_item_insert>
    {
        public SetupTipeItemInsertValidators()
        {
            

            RuleFor(x => x.kode_tipe_item)
                            .NotNull().WithMessage("Kode Tipe Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Tipe Item tidak boleh kosong!");
            RuleFor(x => x.tipe_item)
                            .NotNull().WithMessage("Tipe Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Item tidak boleh kosong!");
            //RuleFor(x => x.id_coa)
            //                .NotNull().WithMessage("ID Coa tidak boleh kosong!")
            //                .NotEmpty().WithMessage("ID Coa tidak boleh kosong!");

        }

    }

    public class SetupTipeItemUpdateStatusActiveValidators : AbstractValidator<mm_setup_tipe_item_update_status_to_active>
    {
        public SetupTipeItemUpdateStatusActiveValidators()
        {
            


            RuleFor(x => x.id_tipe_item)
                            .NotNull().WithMessage("Id Tipe Item tidak boleh kosong!");

        }
    }

    public class SetupTipeItemUpdateStatusDeActiveValidators : AbstractValidator<mm_setup_tipe_item_update_status_to_deactive>
    {
        public SetupTipeItemUpdateStatusDeActiveValidators()
        {
            


            RuleFor(x => x.id_tipe_item)
                            .NotNull().WithMessage("Id Tipe Item tidak boleh kosong!");

        }
    }


}
