using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TransPenerimaanValidators : AbstractValidator<tr_penerimaan_insert>
    {
        public TransPenerimaanValidators()
        {



            //RuleFor(x => x.nomor_penerimaan)
            //                .NotNull().WithMessage("Nomor Penerimaan tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Penerimaan tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_penerimaan)
                            .NotNull().WithMessage("Tanggal Penerimaan tidak boleh kosong!");
            RuleFor(x => x.kode_jenis_penerimaan)
                            .NotNull().WithMessage("Kode Jenis Penerimaan tidak boleh kosong!")
                            .NotEmpty().WithMessage("Kode Jenis Penerimaan tidak boleh kosong!").MaximumLength(10);
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.id_supplier).NotEmpty();
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

            //RuleForEach(x => x.details).SetValidator(model => new TransPenerimaanDetailItemValidators());
        }

    }

    public class TransPenerimaanDetailItemValidators : AbstractValidator<tr_penerimaan_detail_item_insert>
    {
        public TransPenerimaanDetailItemValidators()
        {

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty_terima)
                            .NotNull().WithMessage("Qty Terima tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Terima tidak boleh kosong!");

        }

    }


    public class TransPenerimaanDetailUploadValidators : AbstractValidator<tr_penerimaan_detail_upload_insert>
    {
        public TransPenerimaanDetailUploadValidators()
        {


            RuleFor(x => x.penerimaan_id)
                            .NotNull().WithMessage("Penerimaan Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Penerimaan Id tidak boleh kosong!");
            RuleFor(x => x.jenis_dokumen)
                            .NotNull().WithMessage("Jenis Dokumen tidak boleh kosong!")
                            .NotEmpty().WithMessage("Jenis Dokumen tidak boleh kosong!").MaximumLength(30);
            RuleFor(x => x.file)
                            .NotNull().WithMessage("Berkas tidak boleh kosong!");

        }

    }

    public class TransPenerimaanValidasiValidators : AbstractValidator<tr_penerimaan_update_status_to_validated>
    {
        public TransPenerimaanValidasiValidators()
        {

            RuleFor(x => x.penerimaan_id).NotEmpty();
        }
    }

    public class TransPenerimaanBatalValidators : AbstractValidator<tr_penerimaan_update_status_to_canceled>
    {
        public TransPenerimaanBatalValidators()
        {

            RuleFor(x => x.penerimaan_id).NotEmpty();
        }
    }


}
