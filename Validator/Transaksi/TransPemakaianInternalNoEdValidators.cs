using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    #region Header

    public class TransPemakaianInternalNoEdInsertValidators : AbstractValidator<tr_pemakaian_internal_no_ed_insert>
    {
        public TransPemakaianInternalNoEdInsertValidators()
        {
            

            //RuleFor(x => x.nomor_pemakaian_internal)
            //                .NotNull().WithMessage("Nomor Pemakaian Internal tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Pemakaian Internal tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_pemakaian_internal).NotEmpty();
            RuleFor(x => x.id_stockroom).NotEmpty();
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }
    }

    public class TransPemakaianInternalNoEdValidasiValidators : AbstractValidator<tr_pemakaian_internal_no_ed_update_to_validated>
    {
        public TransPemakaianInternalNoEdValidasiValidators()
        {
            

            RuleFor(x => x.pemakaian_internal_id).NotEmpty();

        }
    }

    public class TransPemakaianInternalNoEdBatalValidators : AbstractValidator<tr_pemakaian_internal_no_ed_update_to_canceled>
    {
        public TransPemakaianInternalNoEdBatalValidators()
        {
            

            RuleFor(x => x.pemakaian_internal_id).NotEmpty();
            RuleFor(x => x.reason_canceled).NotEmpty();

        }
    }

    #endregion

    #region Detail Item

    public class TransPemakaianInternalNoEdDetailItemInsertValidators : AbstractValidator<tr_pemakaian_internal_no_ed_detail_item_insert>
    {
        public TransPemakaianInternalNoEdDetailItemInsertValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.qty_pemakaian_internal)
                            .NotNull().WithMessage("Qty Pemakaian Internal tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Pemakaian Internal tidak boleh kosong!");


        }

    }


    #endregion

    #region Detail Item Upload


    public class TransPemakaianInternalNoEdDetailUploadInsertValidators : AbstractValidator<tr_pemakaian_internal_no_ed_detail_upload_insert>
    {
        public TransPemakaianInternalNoEdDetailUploadInsertValidators()
        {
            

            RuleFor(x => x.pemakaian_internal_id)
                            .NotNull().WithMessage("Pemakaian Internal Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Pemakaian Internal Id tidak boleh kosong!");
            RuleFor(x => x.jenis_dokumen)
                            .NotNull().WithMessage("Jenis Dokumen tidak boleh kosong!")
                            .NotEmpty().WithMessage("Jenis Dokumen tidak boleh kosong!").MaximumLength(30);
            
            RuleFor(x => x.file)
                            .NotNull().WithMessage("Berkas tidak boleh kosong!");


        }

    }

    #endregion
}
