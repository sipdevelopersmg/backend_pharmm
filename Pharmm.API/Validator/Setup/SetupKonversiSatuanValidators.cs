using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetupKonversiSatuanInsertValidators : AbstractValidator<mm_setup_konversi_satuan_insert>
    {
        public SetupKonversiSatuanInsertValidators()
        {
            

            RuleFor(x => x.kode_satuan_besar)
                 .NotNull().WithMessage("Kode Satuan Besar tidak boleh kosong!")
                 .NotEmpty().WithMessage("Kode Satuan Besar tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.kode_satuan_kecil)
                            .NotNull().WithMessage("Kode Satuan Kecil tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan Kecil tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.faktor_konversi).NotEmpty().WithMessage("Faktor Konversi tidak boleh kosong!");
            RuleFor(x => x.faktor_dekonversi)
                            .NotNull().WithMessage("Faktor Dekonversi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Faktor Dekonversi tidak boleh kosong!");


        }
    }

    public class SetupKonversiSatuanUpdateValidators : AbstractValidator<mm_setup_konversi_satuan_update>
    {
        public SetupKonversiSatuanUpdateValidators()
        {
            


            RuleFor(x => x.id_konversi_satuan)
                            .NotNull().WithMessage("Id Konversi Satuan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Konversi Satuan tidak boleh kosong!");
            RuleFor(x => x.kode_satuan_besar)
                             .NotNull().WithMessage("Kode Satuan Besar tidak boleh kosong!")
                             .NotEmpty().WithMessage("Kode Satuan Besar tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.kode_satuan_kecil)
                            .NotNull().WithMessage("Kode Satuan Kecil tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Satuan Kecil tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.faktor_konversi)
                            .NotNull().WithMessage("Faktor Konversi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Faktor Konversi tidak boleh kosong!");
            RuleFor(x => x.faktor_dekonversi)
                            .NotNull().WithMessage("Faktor Dekonversi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Faktor Dekonversi tidak boleh kosong!");

        }

    }
}
