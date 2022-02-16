using FluentValidation;
using Pharmm.API.Models.Setup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Services.Setup
{
    public class SetupPerencanaanKategoriValidators : AbstractValidator<mm_setup_perencanaan_kategori>
    {
        public SetupPerencanaanKategoriValidators()
        {
            


            RuleFor(x => x.id_kategori)
                            .NotNull().WithMessage("Id Kategori tidak boleh kosong!");
            RuleFor(x => x.kategori)
                            .NotNull().WithMessage("Kategori tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kategori tidak boleh kosong!");

        }

    }

    public class SetupPerencanaanKategoriInsertValidators : AbstractValidator<mm_setup_perencanaan_kategori_insert>
    {
        public SetupPerencanaanKategoriInsertValidators()
        {
            

            RuleFor(x => x.kategori)
                            .NotNull().WithMessage("Kategori tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kategori tidak boleh kosong!");

        }

    }


}
