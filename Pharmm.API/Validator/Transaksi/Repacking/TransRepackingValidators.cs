using FluentValidation;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;

namespace Pharmm.API.Validator
{
    public class TrRepackingInsertValidators : AbstractValidator<tr_repacking_insert>
    {

        public TrRepackingInsertValidators(
            //LocalizationHelper localizationHelper
            )
        {

            //RuleFor(x => x.nomor_repacking)
            //                .NotNull().WithMessage("Nomor Repacking tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Repacking tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_repacking).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
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



    public class TrRepackingDetailValidators : AbstractValidator<tr_repacking_detail_insert>
    {
        public TrRepackingDetailValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();

            RuleFor(x => x.id_item_child)
                            .NotNull().WithMessage("Id Item Child tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item Child tidak boleh kosong!");
            RuleFor(x => x.qty)
                            .NotNull().WithMessage("Qty tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty tidak boleh kosong!");

        }

    }

    public class TrRepackingValidasiValidators : AbstractValidator<tr_repacking_update_to_validated>
    {
        public TrRepackingValidasiValidators()
        {
            RuleFor(x => x.repacking_id).NotEmpty();

        }

    }

    public class TrRepackingBatalValidators : AbstractValidator<tr_repacking_update_to_canceled>
    {
        public TrRepackingBatalValidators()
        {
            RuleFor(x => x.repacking_id).NotEmpty();
        }

    }

}
