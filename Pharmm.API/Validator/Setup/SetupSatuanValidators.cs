using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupSatuanInsertValidators : AbstractValidator<mm_setup_satuan_insert>
    {
        public SetupSatuanInsertValidators()
        {
            


            RuleFor(x => x.kode_satuan)
                            .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!");
            RuleFor(x => x.nama_satuan)
                            .NotNull().WithMessage("Nama Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Satuan tidak boleh kosong!");


        }
    }

    public class SetupSatuanUpdateValidators : AbstractValidator<mm_setup_satuan_update>
    {
        public SetupSatuanUpdateValidators()
        {
            


            RuleFor(x => x.kode_satuan)
                            .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!");
            RuleFor(x => x.nama_satuan)
                            .NotNull().WithMessage("Nama Satuan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nama Satuan tidak boleh kosong!");

        }
    }


    public class SetupSatuanUpdateActiveValidators : AbstractValidator<mm_setup_satuan_update_status_to_active>
    {
        public SetupSatuanUpdateActiveValidators()
        {
            

            RuleFor(x => x.kode_satuan)
                .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!").MaximumLength(10);


        }
    }


    public class SetupSatuanUpdateDeActiveValidators : AbstractValidator<mm_setup_satuan_update_status_to_deactive>
    {
        public SetupSatuanUpdateDeActiveValidators()
        {
            

            RuleFor(x => x.kode_satuan)
                .NotNull().WithMessage("Kode Satuan tidak boleh kosong!")
                .NotEmpty().WithMessage("Kode Satuan tidak boleh kosong!").MaximumLength(10);


        }
    }
}
