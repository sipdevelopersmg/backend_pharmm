using FluentValidation;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;

namespace Pharmm.API.Validator
{
    public class TrAssemblyInsertValidators : AbstractValidator<tr_assembly_insert>
    {

        public TrAssemblyInsertValidators(
            //LocalizationHelper localizationHelper
            )
        {

            //RuleFor(x => x.nomor_assembly)
            //                .NotNull().WithMessage("Nomor Assembly tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Assembly tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_assembly).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            //RuleFor(x => x.batch_number)
            //                .NotNull().WithMessage("Batch Number tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            //RuleFor(x => x.expired_date).NotEmpty();
            //RuleFor(x => x.expired_date)
            //    .Must((model, field) => localizationHelper.IsDateBeforeOrToday(field) == false)
            //    .WithMessage("Expired date tidak boleh kurang dari tanggal sekarang");

            RuleFor(x => x.qty)
                            .NotNull().WithMessage("Qty tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty tidak boleh kosong!");
            RuleFor(x => x.total_nominal)
                            .NotNull().WithMessage("Total Nominal tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Total Nominal tidak boleh kosong!");
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty();

        }

    }



    public class TrAssemblyDetailValidators : AbstractValidator<tr_assembly_detail_insert>
    {
        public TrAssemblyDetailValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();

            RuleFor(x => x.id_item_child)
                            .NotNull().WithMessage("Id Item Child tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item Child tidak boleh kosong!");
            RuleFor(x => x.qty)
                            .NotNull().WithMessage("Qty tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty tidak boleh kosong!");
            RuleFor(x => x.batch_number).NotEmpty();
            RuleFor(x => x.expired_date).NotEmpty();

        }

    }

    public class TrAssemblyValidasiValidators : AbstractValidator<tr_assembly_update_to_validated>
    {
        public TrAssemblyValidasiValidators()
        {
            RuleFor(x => x.assembly_id).NotEmpty();

        }

    }

    public class TrAssemblyBatalValidators : AbstractValidator<tr_assembly_update_to_canceled>
    {
        public TrAssemblyBatalValidators()
        {
            RuleFor(x => x.assembly_id).NotEmpty();
        }

    }

}
