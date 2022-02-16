using FluentValidation;
using Pharmm.API.Models.Setup;
using Pharmm.API.Models.Transaksi;

namespace Pharmm.API.Validator
{
    public class TrAssemblyNoEdInsertValidators : AbstractValidator<tr_assembly_no_ed_insert>
    {

        public TrAssemblyNoEdInsertValidators(
            )
        {
            RuleFor(x => x.tanggal_assembly).NotEmpty();
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



    public class TrAssemblyNoEdDetailValidators : AbstractValidator<tr_assembly_no_ed_detail_insert>
    {
        public TrAssemblyNoEdDetailValidators()
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

    public class TrAssemblyNoEdValidasiValidators : AbstractValidator<tr_assembly_no_ed_update_to_validated>
    {
        public TrAssemblyNoEdValidasiValidators()
        {
            RuleFor(x => x.assembly_id).NotEmpty();

        }

    }

    public class TrAssemblyNoEdBatalValidators : AbstractValidator<tr_assembly_no_ed_update_to_canceled>
    {
        public TrAssemblyNoEdBatalValidators()
        {
            RuleFor(x => x.assembly_id).NotEmpty();
        }

    }

}
