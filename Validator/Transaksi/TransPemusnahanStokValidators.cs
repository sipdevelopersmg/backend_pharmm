using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator.Transaksi
{
    public class TrPemusnahanStokValidators : AbstractValidator<tr_pemusnahan_stok_insert>
    {
        public TrPemusnahanStokValidators()
        {

            //RuleFor(x => x.nomor_pemusnahan_stok)
            //                .NotNull().WithMessage("Nomor Pemusnahan Stok tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Pemusnahan Stok tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_pemusnahan_stok).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");

            RuleFor(x => x.total_transaksi).NotEmpty().NotEqual(0);

        }
    }

    public class TrPemusnahanStokDetailValidators : AbstractValidator<tr_pemusnahan_stok_detail_insert>
    {
        public TrPemusnahanStokDetailValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty)
                            .NotNull().WithMessage("Qty tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty tidak boleh kosong!");

        }
    }


    public class TrPemusnahanStokValidasiValidators : AbstractValidator<tr_pemusnahan_stok_update_to_validated>
    {
        public TrPemusnahanStokValidasiValidators()
        {
            RuleFor(x => x.pemusnahan_stok_id).NotEmpty();

        }
    }



    public class TrPemusnahanStokBatalValidators : AbstractValidator<tr_pemusnahan_stok_update_to_canceled>
    {
        public TrPemusnahanStokBatalValidators()
        {
            RuleFor(x => x.pemusnahan_stok_id).NotEmpty();

        }
    }

}
