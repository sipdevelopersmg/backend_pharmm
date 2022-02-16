using FluentValidation;
using Pharmm.API.Models;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupSupplierInsertRekeningValidators : AbstractValidator<mm_setup_supplier_rekening_insert>
    {
        public SetupSupplierInsertRekeningValidators()
        {
            


            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");
            RuleFor(x => x.bank)
                            .NotNull().WithMessage("Bank tidak boleh kosong!")
                            .NotEmpty().WithMessage("Bank tidak boleh kosong!");
            RuleFor(x => x.nomor_rekening)
                            .NotNull().WithMessage("Nomor Rekening tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nomor Rekening tidak boleh kosong!");
            RuleFor(x => x.nama_rekening)
                            .NotNull().WithMessage("Nama Rekening tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Rekening tidak boleh kosong!");

        }

    }

    public class SetupSupplierRekeningUpdateValidators : AbstractValidator<mm_setup_supplier_rekening_update>
    {
        public SetupSupplierRekeningUpdateValidators()
        {
            


            RuleFor(x => x.id_supplier_rekening)
                            .NotNull().WithMessage("Id Supplier Rekening tidak boleh kosong!");
            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");
            RuleFor(x => x.bank)
                            .NotNull().WithMessage("Bank tidak boleh kosong!")
                            .NotEmpty().WithMessage("Bank tidak boleh kosong!");
            RuleFor(x => x.nomor_rekening)
                            .NotNull().WithMessage("Nomor Rekening tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nomor Rekening tidak boleh kosong!");
            RuleFor(x => x.nama_rekening)
                            .NotNull().WithMessage("Nama Rekening tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Rekening tidak boleh kosong!");

        }
    }

    public class SetupSupplierRekeningUpdateToActiveValidators : AbstractValidator<mm_setup_supplier_rekening_update_status_to_active>
    {
        public SetupSupplierRekeningUpdateToActiveValidators()
        {
            


            RuleFor(x => x.id_supplier_rekening)
                            .NotNull().WithMessage("Id Supplier Rekening tidak boleh kosong!");
        }
    }

    public class SetupSupplierRekeningUpdateToDeActiveValidators : AbstractValidator<mm_setup_supplier_rekening_update_status_to_deactive>
    {
        public SetupSupplierRekeningUpdateToDeActiveValidators()
        {
            


            RuleFor(x => x.id_supplier_rekening)
                            .NotNull().WithMessage("Id Supplier Rekening tidak boleh kosong!");
        }
    }
}
