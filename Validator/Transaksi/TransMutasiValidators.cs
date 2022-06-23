using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    #region Approve

    public class TrMutasiApproveValidators : AbstractValidator<tr_mutasi_approve>
    {
        public TrMutasiApproveValidators()
        {

            RuleFor(x => x.mutasi_id)
                            .NotNull().WithMessage("Mutasi Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Mutasi Id tidak boleh kosong!");
            RuleFor(x => x.jumlah_item).NotEmpty();
            RuleFor(x => x.total_transaksi).NotEmpty();

            RuleFor(x => x.details).NotEmpty();
        }

    }


    public class TrMutasiDetailItemApproveValidators : AbstractValidator<tr_mutasi_detail_item_approve>
    {
        public TrMutasiDetailItemApproveValidators()
        {

            RuleFor(x => x.mutasi_id).NotEmpty();
            RuleFor(x => x.nominal_mutasi).NotEmpty();
            RuleFor(x => x.qty_mutasi).NotEmpty();
            RuleFor(x => x.detailBatch).NotEmpty();
        }

    }

    public class TrMutasiDetailBatchApproveValidators : AbstractValidator<tr_mutasi_detail_item_batch_insert>
    {
        public TrMutasiDetailBatchApproveValidators()
        {

            RuleFor(x => x.mutasi_detail_item_id);
            RuleFor(x => x.mutasi_id);
            RuleFor(x => x.batch_number).NotEmpty().MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty_mutasi);


        }

    }


    #endregion

    #region Header

    public class TransMutasiInsertValidators : AbstractValidator<tr_mutasi_insert>
    {
        public TransMutasiInsertValidators()
        {
            

            //RuleFor(x => x.nomor_mutasi)
            //                .NotNull().WithMessage("Nomor Mutasi tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Mutasi tidak boleh kosong!").MaximumLength(20);
            RuleFor(x => x.tanggal_mutasi).NotEmpty();
            RuleFor(x => x.id_stockroom_pemberi).NotEmpty();
            RuleFor(x => x.id_stockroom_penerima).NotEmpty();
            RuleFor(x => x.jumlah_item)
                            .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");
            RuleFor(x => x.total_transaksi)
                            .NotNull().WithMessage("Total Transaksi tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }
    }

    public class TransMutasiInserPermintaantValidators : AbstractValidator<tr_mutasi_insert_permintaan>
    {
        public TransMutasiInserPermintaantValidators()
        {
            

            //RuleFor(x => x.nomor_mutasi)
            //                .NotNull().WithMessage("Nomor Mutasi tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Mutasi tidak boleh kosong!").MaximumLength(20);
            //RuleFor(x => x.tanggal_mutasi).NotEmpty();
            RuleFor(x => x.id_stockroom_pemberi).NotEmpty();
            RuleFor(x => x.id_stockroom_penerima).NotEmpty();
            //RuleFor(x => x.jumlah_item)
            //                .NotNull().WithMessage("Jumlah Item tidak boleh kosong!")
            //                .NotEqual(0).WithMessage("Jumlah Item tidak boleh kosong!");
            //RuleFor(x => x.total_transaksi)
            //                .NotNull().WithMessage("Total Transaksi tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }
    }

    public class TransMutasiValidasiValidators : AbstractValidator<tr_mutasi_update_to_validated>
    {
        public TransMutasiValidasiValidators()
        {
            

            RuleFor(x => x.mutasi_id).NotEmpty();

        }
    }

    public class TransMutasiBatalValidators : AbstractValidator<tr_mutasi_update_to_canceled>
    {
        public TransMutasiBatalValidators()
        {
            

            RuleFor(x => x.mutasi_id).NotEmpty();
            RuleFor(x => x.reason_canceled).NotEmpty();

        }
    }

    #endregion

    #region Detail Item

    public class TrMutasiDetailItemInsertValidators : AbstractValidator<tr_mutasi_detail_item_insert>
    {
        public TrMutasiDetailItemInsertValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.qty_mutasi).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");

            RuleFor(x => x.detailBatchs).NotEmpty().WithMessage("Detail batch tidak boleh kosong!");

        }

    }

    public class TrMutasiDetailItemInsertPermintaanValidators : AbstractValidator<tr_mutasi_detail_item_insert_permintaan>
    {
        public TrMutasiDetailItemInsertPermintaanValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.qty_permintaan).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");

            //RuleFor(x => x.detailBatchs).NotEmpty().WithMessage("Detail batch tidak boleh kosong!");

        }

    }

    #endregion


    #region Detail Item Batch

    public class TrMutasiDetailItemBatchInsertValidators : AbstractValidator<tr_mutasi_detail_item_batch>
    {
        public TrMutasiDetailItemBatchInsertValidators()
        {
            

            RuleFor(x => x.batch_number)
                            .NotNull().WithMessage("Batch Number tidak boleh kosong!")
                            .NotEmpty().WithMessage("Batch Number tidak boleh kosong!").MaximumLength(50);
            RuleFor(x => x.expired_date).NotEmpty();
            RuleFor(x => x.qty_mutasi)
                            .NotNull().WithMessage("Qty Mutasi tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Qty Mutasi tidak boleh kosong!");

        }

    }

    #endregion

    #region Detail Item Upload


    public class TrMutasiDetailUploadInsertValidators : AbstractValidator<tr_mutasi_detail_upload_insert>
    {
        public TrMutasiDetailUploadInsertValidators()
        {
            

            RuleFor(x => x.mutasi_id)
                            .NotNull().WithMessage("Mutasi Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Mutasi Id tidak boleh kosong!");
            RuleFor(x => x.jenis_dokumen)
                            .NotNull().WithMessage("Jenis Dokumen tidak boleh kosong!")
                            .NotEmpty().WithMessage("Jenis Dokumen tidak boleh kosong!").MaximumLength(30);
            RuleFor(x => x.file)
                            .NotNull().WithMessage("Berkas tidak boleh kosong!");

        }

    }

    #endregion
}
