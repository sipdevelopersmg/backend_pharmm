using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    #region Approve

    public class TrMutasiNoEdApproveValidators : AbstractValidator<tr_mutasi_no_ed_approve>
    {
        public TrMutasiNoEdApproveValidators()
        {

            RuleFor(x => x.mutasi_id)
                            .NotNull().WithMessage("Mutasi Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Mutasi Id tidak boleh kosong!");
            RuleFor(x => x.jumlah_item).NotEmpty();
            RuleFor(x => x.total_transaksi).NotEmpty();

            RuleFor(x => x.details).NotEmpty();
        }

    }


    public class TrMutasiNoEdDetailItemApproveValidators : AbstractValidator<tr_mutasi_no_ed_detail_item_approve>
    {
        public TrMutasiNoEdDetailItemApproveValidators()
        {

            RuleFor(x => x.mutasi_id).NotEmpty();
            RuleFor(x => x.nominal_mutasi).NotEmpty();
            RuleFor(x => x.qty_mutasi).NotEmpty();
        }

    }

    #endregion

    #region Header

    public class TransMutasiNoEdInsertValidators : AbstractValidator<tr_mutasi_no_ed_insert>
    {
        public TransMutasiNoEdInsertValidators()
        {
            
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

    public class TransMutasiNoEdInserPermintaantValidators : AbstractValidator<tr_mutasi_no_ed_insert_permintaan>
    {
        public TransMutasiNoEdInserPermintaantValidators()
        {
            
            RuleFor(x => x.id_stockroom_pemberi).NotEmpty();
            RuleFor(x => x.id_stockroom_penerima).NotEmpty();

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");

        }
    }

    public class TransMutasiNoEdValidasiValidators : AbstractValidator<tr_mutasi_no_ed_update_to_validated>
    {
        public TransMutasiNoEdValidasiValidators()
        {
            

            RuleFor(x => x.mutasi_id).NotEmpty();

        }
    }

    public class TransMutasiNoEdBatalValidators : AbstractValidator<tr_mutasi_no_ed_update_to_canceled>
    {
        public TransMutasiNoEdBatalValidators()
        {
            

            RuleFor(x => x.mutasi_id).NotEmpty();
            RuleFor(x => x.reason_canceled).NotEmpty();

        }
    }

    #endregion

    #region Detail Item

    public class TrMutasiNoEdDetailItemInsertValidators : AbstractValidator<tr_mutasi_no_ed_detail_item_insert>
    {
        public TrMutasiNoEdDetailItemInsertValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.qty_mutasi).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");

        }

    }

    public class TrMutasiNoEdDetailItemInsertPermintaanValidators : AbstractValidator<tr_mutasi_no_ed_detail_item_insert_permintaan>
    {
        public TrMutasiNoEdDetailItemInsertPermintaanValidators()
        {
            

            RuleFor(x => x.no_urut).NotEmpty();
            RuleFor(x => x.qty_permintaan).NotEmpty();
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");


        }

    }

    #endregion

    #region Detail Item Upload


    public class TrMutasiNoEdDetailUploadInsertValidators : AbstractValidator<tr_mutasi_no_ed_detail_upload_insert>
    {
        public TrMutasiNoEdDetailUploadInsertValidators()
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
