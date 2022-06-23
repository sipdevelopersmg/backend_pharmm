using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class SetHargaOrderValidators : AbstractValidator<set_harga_order_insert>
    {
        public SetHargaOrderValidators()
        {
            


            //RuleFor(x => x.nomor_harga_order)
            //                .NotNull().WithMessage("Nomor Harga Order tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Harga Order tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.id_supplier).NotEmpty();
            RuleFor(x => x.tanggal_berlaku)
                            .NotNull().WithMessage("Tanggal Berlaku tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tanggal Berlaku tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty();
        }
    }

    public class SetHargaOrderDetailValidators : AbstractValidator<set_harga_order_detail_insert>
    {
        public SetHargaOrderDetailValidators()
        {
            
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.harga_order)
                            .NotNull().WithMessage("Harga Order tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Order tidak boleh kosong!");
            RuleFor(x => x.harga_order_netto)
                            .NotNull().WithMessage("Harga Order Netto tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Order Netto tidak boleh kosong!");
        }
    }


}
