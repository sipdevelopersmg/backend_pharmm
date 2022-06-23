using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupItemUpdateValidators : AbstractValidator<mm_setup_item_update>
    {
        public SetupItemUpdateValidators()
        {
           
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.id_grup_item)
                            .NotNull().WithMessage("Id Grup Item tidak boleh kosong!");
            RuleFor(x => x.id_pabrik)
                            .NotNull().WithMessage("Id Pabrik tidak boleh kosong!");
            RuleFor(x => x.kode_item)
                            .NotNull().WithMessage("Kode Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Item tidak boleh kosong!");
            RuleFor(x => x.nama_item)
                            .NotNull().WithMessage("Nama Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Item tidak boleh kosong!");
            //RuleFor(x => x.kode_satuan)
            //                .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!").MaximumLength(10);
        }
    }

    public class SetupItemInsertValidators : AbstractValidator<mm_setup_item_insert>
    {
        public SetupItemInsertValidators()
        {
            


            RuleFor(x => x.id_pabrik)
                            .NotNull().WithMessage("Id Pabrik tidak boleh kosong!");
            RuleFor(x => x.id_grup_item)
                            .NotNull().WithMessage("Id Grup Item tidak boleh kosong!");
            RuleFor(x => x.kode_item)
                            .NotNull().WithMessage("Kode Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Item tidak boleh kosong!");
            RuleFor(x => x.nama_item)
                            .NotNull().WithMessage("Nama Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Item tidak boleh kosong!");
            RuleFor(x => x.kode_satuan)
                            .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!").MaximumLength(10);

            RuleFor(x => x.obat)
                .Must((model, field) => model.obat is not null)
                .When((model, field) => model.is_obat)
                .WithMessage("Obat tidak boleh kosong jika item termasuk obat");

        }

    }

    public class SetupItemUpdateToActiveValidators : AbstractValidator<mm_setup_item_update_status_to_active>
    {
        public SetupItemUpdateToActiveValidators()
        {
            


            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");

        }

    }

    public class SetupItemUpdateToDeactiveValidators : AbstractValidator<mm_setup_item_update_status_to_deactive>
    {
        public SetupItemUpdateToDeactiveValidators()
        {
            


            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");

        }

    }


    #region Item Urai

    public class MmSetupItemUraiInsertValidators : AbstractValidator<mm_setup_item_urai_insert>
    {
        public MmSetupItemUraiInsertValidators()
        {

            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.id_item_urai)
                            .NotNull().WithMessage("Id Item Urai tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item Urai tidak boleh kosong!");

        }

    }

    public class MmSetupItemUraiDeleteValidators : AbstractValidator<mm_setup_item_urai_delete>
    {
        public MmSetupItemUraiDeleteValidators()
        {

            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.id_item_urai)
                            .NotNull().WithMessage("Id Item Urai tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item Urai tidak boleh kosong!");

        }

    }
    #endregion
}
