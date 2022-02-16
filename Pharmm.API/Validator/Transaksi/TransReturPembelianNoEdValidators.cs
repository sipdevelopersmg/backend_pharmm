using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TrReturPembelianNoEdInsertValidators : AbstractValidator<tr_retur_pembelian_no_ed_insert>
    {
        public TrReturPembelianNoEdInsertValidators()
        {
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

    public class TrReturPembelianNoEdDetailInsertValidators : AbstractValidator<tr_retur_pembelian_no_ed_detail_insert>
    {
        public TrReturPembelianNoEdDetailInsertValidators()
        {
            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.qty_retur)
                            .NotNull().WithMessage("Qty Retur tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Retur tidak boleh kosong!");

        }

    }


    public class TrReturPembelianNoEdValidasiValidators : AbstractValidator<tr_retur_pembelian_no_ed_update_status_to_validated>
    {
        public TrReturPembelianNoEdValidasiValidators()
        {
            RuleFor(x => x.retur_pembelian_id).NotNull();
        }
    }

    public class TrReturPembelianNoEdBatalValidators : AbstractValidator<tr_retur_pembelian_no_ed_update_status_to_canceled>
    {
        public TrReturPembelianNoEdBatalValidators()
        {
            RuleFor(x => x.retur_pembelian_id).NotNull();
        }
    }

}
