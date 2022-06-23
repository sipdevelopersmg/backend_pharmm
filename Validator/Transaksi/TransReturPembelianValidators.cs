using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TrReturPembelianInsertValidators : AbstractValidator<tr_retur_pembelian_insert>
    {
        public TrReturPembelianInsertValidators()
        {


            //RuleFor(x => x.nomor_retur_pembelian)
            //                .NotNull().WithMessage("Nomor Retur Pembelian tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Retur Pembelian tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_retur_pembelian)
                            .NotNull().WithMessage("Tanggal Retur Pembelian tidak boleh kosong!");
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_mekanisme_retur).NotEmpty();
            RuleFor(x => x.id_supplier).NotEmpty();
            RuleFor(x => x.jumlah_item_retur)
                            .NotNull().WithMessage("Jumlah Item Retur tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item Retur tidak boleh kosong!");
            RuleFor(x => x.total_transaksi_retur)
                            .NotNull().WithMessage("Total Transaksi Retur tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Total Transaksi Retur tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");
        }

    }

    public class TrReturPembelianDetailInsertValidators : AbstractValidator<tr_retur_pembelian_detail_insert>
    {
        public TrReturPembelianDetailInsertValidators()
        {


            //RuleFor(x => x.retur_pembelian_id)
            //                .NotNull().WithMessage("Retur Pembelian Id tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Retur Pembelian Id tidak boleh kosong!");
            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty_retur)
                            .NotNull().WithMessage("Qty Retur tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Retur tidak boleh kosong!");

        }

    }


    public class TrReturPembelianValidasiValidators : AbstractValidator<tr_retur_pembelian_update_status_to_validated>
    {
        public TrReturPembelianValidasiValidators()
        {
            RuleFor(x => x.retur_pembelian_id).NotNull();
        }
    }

    public class TrReturPembelianBatalValidators : AbstractValidator<tr_retur_pembelian_update_status_to_canceled>
    {
        public TrReturPembelianBatalValidators()
        {
            RuleFor(x => x.retur_pembelian_id).NotNull();
        }
    }

}
