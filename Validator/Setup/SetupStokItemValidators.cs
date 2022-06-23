using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupStokItemValidators : AbstractValidator<mm_setup_stok_item>
    {
        public SetupStokItemValidators()
        {
            


            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.id_stockroom)
                            .NotNull().WithMessage("Id Stockroom tidak boleh kosong!");
            //RuleFor(x => x.qty_on_hand)
            //                .NotNull().WithMessage("Qty On Hand tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty On Hand tidak boleh kosong!");
        }
    }


    public class SetupStokItemDetailEdValidators : AbstractValidator<mm_setup_stok_item_detail_ed>
    {
        public SetupStokItemDetailEdValidators()
        {
            


            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.id_stockroom)
                            .NotNull().WithMessage("Id Stockroom tidak boleh kosong!");
            RuleFor(x => x.expired_date)
                            .NotNull().WithMessage("Expired Date tidak boleh kosong!");
            //RuleFor(x => x.qty_on_hand)
            //                .NotNull().WithMessage("Qty On Hand tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Qty On Hand tidak boleh kosong!");
        }
    }


}
