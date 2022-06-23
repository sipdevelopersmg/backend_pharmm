using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TransPenerimaanNoEdValidators : AbstractValidator<tr_penerimaan_no_ed_insert>
    {
        public TransPenerimaanNoEdValidators()
        {

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

            //RuleForEach(x => x.details).SetValidator(model => new TransPenerimaanNoEdDetailItemValidators());
        }

    }

    public class TransPenerimaanNoEdDetailItemValidators : AbstractValidator<tr_penerimaan_no_ed_detail_item_insert>
    {
        public TransPenerimaanNoEdDetailItemValidators()
        {

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.qty_terima)
                            .NotNull().WithMessage("Qty Terima tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Terima tidak boleh kosong!");

        }

    }


    public class TransPenerimaanNoEdDetailUploadValidators : AbstractValidator<tr_penerimaan_no_ed_detail_upload_insert>
    {
        public TransPenerimaanNoEdDetailUploadValidators()
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

    public class TransPenerimaanNoEdValidasiValidators : AbstractValidator<tr_penerimaan_no_ed_update_status_to_validated>
    {
        public TransPenerimaanNoEdValidasiValidators()
        {

            RuleFor(x => x.penerimaan_id).NotEmpty();
        }
    }

    public class TransPenerimaanNoEdBatalValidators : AbstractValidator<tr_penerimaan_no_ed_update_status_to_canceled>
    {
        public TransPenerimaanNoEdBatalValidators()
        {

            RuleFor(x => x.penerimaan_id).NotEmpty();
        }
    }


}
