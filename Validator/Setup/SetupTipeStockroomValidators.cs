using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupTipeStockroomInsertValidators : AbstractValidator<mm_setup_tipe_stockroom_insert>
    {
        public SetupTipeStockroomInsertValidators()
        {
            

            RuleFor(x => x.tipe_stockroom)
                            .NotNull().WithMessage("Tipe Stockroom tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Stockroom tidak boleh kosong!")
                            .MaximumLength(20);

        }
    }

    public class SetupTipeStockroomUpdateValidators : AbstractValidator<mm_setup_tipe_stockroom>
    {
        public SetupTipeStockroomUpdateValidators()
        {
            


            RuleFor(x => x.id_tipe_stockroom)
                            .NotEmpty().WithMessage("Id Tipe Stockroom tidak boleh kosong!");
            RuleFor(x => x.tipe_stockroom)
                            .NotNull().WithMessage("Tipe Stockroom tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Stockroom tidak boleh kosong!")
                            .MaximumLength(20);

        }
    }


}
