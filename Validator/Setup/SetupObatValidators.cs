using Pharmm.API.Models.Setup;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Farmasi.API.Validators.Setup
{
    public class SetupObatInsertValidators : AbstractValidator<phar_setup_obat_insert_from_barang>
    {
        public SetupObatInsertValidators()
        {
            

            RuleFor(x => x.id_grup_obat).NotEmpty();

        }
    }


    #region Setup Obat Detail

    public class SetupObatDetailInsertValidators : AbstractValidator<phar_setup_obat_detail_insert>
    {
        public SetupObatDetailInsertValidators()
        {

            RuleFor(x => x.id_item).NotEmpty();
            RuleFor(x => x.harga_jual_apotek).NotEmpty();
            RuleFor(x => x.tgl_berlaku).NotEmpty();

        }

    }


    public class SetupObatDetailUpdateStatusValidators : AbstractValidator<phar_setup_obat_detail_update_status>
    {
        public SetupObatDetailUpdateStatusValidators()
        {

            RuleFor(x => x.id_obat_detail).NotEmpty();
            RuleFor(x => x.tgl_berakhir).NotEmpty();

        }
    }

    public class SetupObatDetailInsertFromBarangValidators : AbstractValidator<phar_setup_obat_detail_insert_from_barang>
    {
        public SetupObatDetailInsertFromBarangValidators()
        {
            

            RuleFor(x => x.harga_jual_apotek)
                            .NotNull().WithMessage("Harga Jual Apotek tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Jual Apotek tidak boleh kosong!");

        }

    }


    #endregion
}
