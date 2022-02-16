using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TrPemesananValidators : AbstractValidator<tr_pemesanan_insert>
    {
        public TrPemesananValidators()
        {


            //RuleFor(x => x.nomor_pemesanan)
            //                .NotNull().WithMessage("Nomor Pemesanan tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Pemesanan tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_pemesanan)
                            .NotNull().WithMessage("Tanggal Pemesanan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tanggal Pemesanan tidak boleh kosong!");
            RuleFor(x => x.tanggal_expired_pemesanan)
                            .NotNull().WithMessage("Tanggal Expired Pemesanan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tanggal Expired Pemesanan tidak boleh kosong!");
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_supplier).NotEmpty();
            RuleFor(x => x.jumlah_item_pesan)
                            .NotNull().WithMessage("Jumlah Item Pesan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item Pesan tidak boleh kosong!");
            RuleFor(x => x.sub_total_1)
                            .NotNull().WithMessage("Sub Total 1 tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Sub Total 1 tidak boleh kosong!");
            RuleFor(x => x.sub_total_2)
                            .NotNull().WithMessage("Sub Total 2 tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Sub Total 2 tidak boleh kosong!");
            RuleFor(x => x.total_transaksi_pesan)
                            .NotNull().WithMessage("Total Transaksi Pesan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Total Transaksi Pesan tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }
    }


    public class TrPemesananDetailValidators : AbstractValidator<tr_pemesanan_detail_insert>
    {
        public TrPemesananDetailValidators()
        {

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.qty_pesan)
                            .NotNull().WithMessage("Qty Pesan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Pesan tidak boleh kosong!");
            RuleFor(x => x.harga_satuan)
                            .NotNull().WithMessage("Harga Satuan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Satuan tidak boleh kosong!");
            RuleFor(x => x.harga_satuan_brutto)
                            .NotNull().WithMessage("Harga Satuan Brutto tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Satuan Brutto tidak boleh kosong!");
            RuleFor(x => x.harga_satuan_netto)
                            .NotNull().WithMessage("Harga Satuan Netto tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Satuan Netto tidak boleh kosong!");
            RuleFor(x => x.sub_total_pesan)
                            .NotNull().WithMessage("Sub Total Pesan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Sub Total Pesan tidak boleh kosong!");

        }

    }

    public class TrPemesananValidasiValidators : AbstractValidator<tr_pemesanan_update_status_to_validated>
    {
        public TrPemesananValidasiValidators()
        {

            RuleFor(x => x.pemesanan_id).NotEmpty();
        }
    }


    public class TrPemesananBatalValidators : AbstractValidator<tr_pemesanan_update_status_to_canceled>
    {
        public TrPemesananBatalValidators()
        {

            RuleFor(x => x.pemesanan_id).NotEmpty();
        }
    }

    public class TrPemesananClosedValidators : AbstractValidator<tr_pemesanan_update_status_to_closed>
    {
        public TrPemesananClosedValidators()
        {

            RuleFor(x => x.pemesanan_id).NotEmpty();
        }
    }

}
