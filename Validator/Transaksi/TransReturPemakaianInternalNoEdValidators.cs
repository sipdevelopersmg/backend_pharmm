using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TransReturPemakaianInternalNoEdInsertValidators : AbstractValidator<tr_retur_pemakaian_internal_no_ed_insert>
    {
        public TransReturPemakaianInternalNoEdInsertValidators()
        {
            RuleFor(x => x.tanggal_retur_pemakaian_internal).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");
            RuleFor(x => x.total_transaksi)
                            .NotNull().WithMessage("Total Transaksi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Total Transaksi tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }

    }

    public class TransReturPemakaianInternalNoEdValidateValidators : AbstractValidator<tr_retur_pemakaian_internal_no_ed_update_to_validated>
    {
        public TransReturPemakaianInternalNoEdValidateValidators()
        {
            

            RuleFor(x => x.retur_pemakaian_internal_id).NotEmpty();

        }

    }


    public class TransReturPemakaianInternalNoedCancelValidators : AbstractValidator<tr_retur_pemakaian_internal_no_ed_update_to_canceled>
    {
        public TransReturPemakaianInternalNoedCancelValidators()
        {
            

            RuleFor(x => x.retur_pemakaian_internal_id).NotEmpty();
            RuleFor(x => x.reason_canceled).NotEmpty();

        }

    }

    public class TransReturPemakaianInternalNoEdDetailItemInsertValidators : AbstractValidator<tr_retur_pemakaian_internal_no_ed_detail_item_insert>
    {
        public TransReturPemakaianInternalNoEdDetailItemInsertValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.qty_retur_pemakaian_internal)
                            .NotNull().WithMessage("Qty Retur Pemakaian Internal tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Retur Pemakaian Internal tidak boleh kosong!");

        }

    }


    public class TransReturPemakaianInternalNoEdDetailUploadInsertValidators : AbstractValidator<tr_retur_pemakaian_internal_no_ed_detail_upload_insert>
    {
        public TransReturPemakaianInternalNoEdDetailUploadInsertValidators()
        {
            

            RuleFor(x => x.retur_pemakaian_internal_id)
                            .NotNull().WithMessage("Retur Pemakaian Internal Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Retur Pemakaian Internal Id tidak boleh kosong!");
            RuleFor(x => x.jenis_dokumen)
                            .NotNull().WithMessage("Jenis Dokumen tidak boleh kosong!")
                            .NotEmpty().WithMessage("Jenis Dokumen tidak boleh kosong!").MaximumLength(30);

            RuleFor(x => x.file)
                            .NotNull().WithMessage("Berkas tidak boleh kosong!");


        }

    }


}
