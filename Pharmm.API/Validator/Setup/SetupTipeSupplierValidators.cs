using FluentValidation;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupTipeSupplierInsertValidators : AbstractValidator<mm_setup_tipe_supplier_insert>
    {
        public SetupTipeSupplierInsertValidators()
        {
            

            RuleFor(x => x.tipe_supplier)
                            .NotNull().WithMessage("Tipe Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Supplier tidak boleh kosong!");

        }

    }

    public class SetupTipeSupplierUpdateValidators : AbstractValidator<mm_setup_tipe_supplier_update>
    {
        public SetupTipeSupplierUpdateValidators()
        {
            


            RuleFor(x => x.id_tipe_supplier)
                            .NotNull().WithMessage("Id Tipe Supplier tidak boleh kosong!");
            RuleFor(x => x.tipe_supplier)
                            .NotNull().WithMessage("Tipe Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tipe Supplier tidak boleh kosong!");

        }

    }

    public class SetupTipeSupplierUpdateToActiveValidators : AbstractValidator<mm_setup_tipe_supplier_update_status_to_active>
    {
        public SetupTipeSupplierUpdateToActiveValidators()
        {
            


            RuleFor(x => x.id_tipe_supplier)
                            .NotNull().WithMessage("Id Tipe Supplier tidak boleh kosong!");

        }

    }


    public class SetupTipeSupplierUpdateToDeActiveValidators : AbstractValidator<mm_setup_tipe_supplier_update_status_to_deactive>
    {
        public SetupTipeSupplierUpdateToDeActiveValidators()
        {
            


            RuleFor(x => x.id_tipe_supplier)
                            .NotNull().WithMessage("Id Tipe Supplier tidak boleh kosong!");

        }

    }
}
