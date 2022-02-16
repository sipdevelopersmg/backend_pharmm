using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupItemSatuanValidators : AbstractValidator<mm_setup_item_satuan_insert>
    {
        public SetupItemSatuanValidators()
        {
            


            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.kode_satuan)
                            .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.isi)
                            .NotNull().WithMessage("Isi tidak boleh kosong!");

        }
    }
}
