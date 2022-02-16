using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupTemperaturItemInsertValidators : AbstractValidator<mm_setup_temperatur_item_insert>
    {
        public SetupTemperaturItemInsertValidators()
        {
            

            RuleFor(x => x.temperatur_item)
                            .NotNull().WithMessage("Temperatur Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Temperatur Item tidak boleh kosong!");

        }
    }


    public class SetupTemperaturItemUpdateValidators : AbstractValidator<mm_setup_temperatur_item>
    {
        public SetupTemperaturItemUpdateValidators()
        {
            


            RuleFor(x => x.id_temperatur_item)
                            .NotNull().WithMessage("Id Temperatur Item tidak boleh kosong!");
            RuleFor(x => x.temperatur_item)
                            .NotNull().WithMessage("Temperatur Item tidak boleh kosong!")
                            .NotEmpty().WithMessage("Temperatur Item tidak boleh kosong!");

        }
    }
}
