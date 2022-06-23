using FluentValidation;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupSupplierValidators : AbstractValidator<mm_setup_supplier_insert>
    {
        public SetupSupplierValidators()
        {
            

            RuleFor(x => x.id_tipe_supplier)
                            .NotNull().WithMessage("Id Tipe Supplier tidak boleh kosong!");
            RuleFor(x => x.kode_supplier)
                            .NotNull().WithMessage("Kode Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Supplier tidak boleh kosong!");
            RuleFor(x => x.nama_supplier)
                            .NotNull().WithMessage("Nama Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Supplier tidak boleh kosong!");

        }
    }

    public class SetupSupplierUpdateValidators : AbstractValidator<mm_setup_supplier_update>
    {
        public SetupSupplierUpdateValidators()
        {
            


            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");
            RuleFor(x => x.id_tipe_supplier)
                            .NotNull().WithMessage("Id Tipe Supplier tidak boleh kosong!");
            RuleFor(x => x.kode_supplier)
                            .NotNull().WithMessage("Kode Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Supplier tidak boleh kosong!");
            RuleFor(x => x.nama_supplier)
                            .NotNull().WithMessage("Nama Supplier tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Supplier tidak boleh kosong!");

        }

    }

    public class SetupSupplierUpdateToActiveValidators : AbstractValidator<mm_setup_supplier_update_status_to_active>
    {
        public SetupSupplierUpdateToActiveValidators()
        {
            


            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");

        }

    }

    public class MmSetupSupplierValidators : AbstractValidator<mm_setup_supplier_update_status_to_deactive>
    {
        public MmSetupSupplierValidators()
        {
            


            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");

        }

    }
}
