using FluentValidation;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;

namespace Pharmm.API.Validator
{
    public class TrRepackingNoEdInsertValidators : AbstractValidator<tr_repacking_no_ed_insert>
    {

        public TrRepackingNoEdInsertValidators(
            )
        {
            RuleFor(x => x.tanggal_repacking).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
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



    public class TrRepackingNoEdDetailValidators : AbstractValidator<tr_repacking_no_ed_detail_insert>
    {
        public TrRepackingNoEdDetailValidators()
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

    public class TrRepackingNoEdValidasiValidators : AbstractValidator<tr_repacking_no_ed_update_to_validated>
    {
        public TrRepackingNoEdValidasiValidators()
        {
            RuleFor(x => x.repacking_id).NotEmpty();

        }

    }

    public class TrRepackingNoEdBatalValidators : AbstractValidator<tr_repacking_no_ed_update_to_canceled>
    {
        public TrRepackingNoEdBatalValidators()
        {
            RuleFor(x => x.repacking_id).NotEmpty();
        }

    }

}
