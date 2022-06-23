using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    #region Header

    public class TransPemakaianInternalInsertValidators : AbstractValidator<tr_pemakaian_internal_insert>
    {
        public TransPemakaianInternalInsertValidators()
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

    public class TransPemakaianInternalValidasiValidators : AbstractValidator<tr_pemakaian_internal_update_to_validated>
    {
        public TransPemakaianInternalValidasiValidators()
        {
            

            RuleFor(x => x.pemakaian_internal_id).NotEmpty();

        }
    }

    public class TransPemakaianInternalBatalValidators : AbstractValidator<tr_pemakaian_internal_update_to_canceled>
    {
        public TransPemakaianInternalBatalValidators()
        {
            

            RuleFor(x => x.pemakaian_internal_id).NotEmpty();
            RuleFor(x => x.reason_canceled).NotEmpty();

        }
    }

    #endregion

    #region Detail Item

    public class TransPemakaianInternalDetailItemInsertValidators : AbstractValidator<tr_pemakaian_internal_detail_item_insert>
    {
        public TransPemakaianInternalDetailItemInsertValidators()
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


    #region Detail Item Batch

    public class TransPemakaianInternalDetailItemBatchInsertValidators : AbstractValidator<tr_pemakaian_internal_detail_item_batch>
    {
        public TransPemakaianInternalDetailItemBatchInsertValidators()
        {
            

            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty_pemakaian_internal)
                            .NotNull().WithMessage("Qty Mutasi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Mutasi tidak boleh kosong!");

        }

    }

    #endregion

    #region Detail Item Upload


    public class TransPemakaianInternalDetailUploadInsertValidators : AbstractValidator<tr_pemakaian_internal_detail_upload_insert>
    {
        public TransPemakaianInternalDetailUploadInsertValidators()
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
