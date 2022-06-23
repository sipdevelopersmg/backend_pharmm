using FluentValidation;
using Pharmm.API.Models.Transaksi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Validator
{
    public class TrKontrakSpjbInsertValidators : AbstractValidator<tr_kontrak_spjb_insert>
    {
        public TrKontrakSpjbInsertValidators()
        {
            

            RuleFor(x => x.id_supplier)
                            .NotNull().WithMessage("Id Supplier tidak boleh kosong!");
            //RuleFor(x => x.nomor_kontrak_spjb)
            //                .NotNull().WithMessage("Nomor Kontrak Spjb tidak boleh kosong!")
            //                .NotEmpty().WithMessage("Nomor Kontrak Spjb tidak boleh kosong!");
            RuleFor(x => x.nomor_kontrak)
                            .NotNull().WithMessage("Nomor Kontrak tidak boleh kosong!")
                            .NotEmpty().WithMessage("Nomor Kontrak tidak boleh kosong!");
            RuleFor(x => x.judul_kontrak)
                            .NotNull().WithMessage("Judul Kontrak tidak boleh kosong!")
                            .NotEmpty().WithMessage("Judul Kontrak tidak boleh kosong!");
            RuleFor(x => x.tanggal_ttd_kontrak)
                            .NotNull().WithMessage("Tanggal Ttd Kontrak tidak boleh kosong!");
            RuleFor(x => x.tanggal_berlaku_kontrak)
                            .NotNull().WithMessage("Tanggal Berlaku Kontrak tidak boleh kosong!");
            RuleFor(x => x.tanggal_berakhir_kontrak)
                            .NotNull().WithMessage("Tanggal Berakhir Kontrak tidak boleh kosong!");
            RuleFor(x => x.total_transaksi_kontrak)
                            .NotNull().WithMessage("Total Transaksi Kontrak tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Total Transaksi Kontrak tidak boleh kosong!");
            RuleFor(x => x.tahun_anggaran)
                            .NotNull().WithMessage("Tahun Anggaran tidak boleh kosong!")
                            .NotEmpty().WithMessage("Tahun Anggaran tidak boleh kosong!");

            RuleFor(x => x.details).NotEmpty().WithMessage("Detail item tidak boleh kosong!");
            //RuleFor(x => x.user_inputed)
            //                .NotNull().WithMessage("User Inputed tidak boleh kosong!");
        }
    }

    public class TrKontrakSpjbDetailItemInsertValidators : AbstractValidator<tr_kontrak_spjb_detail_item_insert>
    {
        public TrKontrakSpjbDetailItemInsertValidators()
        {

            RuleFor(x => x.no_urut)
                            .NotNull().WithMessage("No Urut tidak boleh kosong!");
            RuleFor(x => x.id_item)
                            .NotNull().WithMessage("Id Item tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Id Item tidak boleh kosong!");
            RuleFor(x => x.harga_satuan)
                            .NotNull().WithMessage("Harga Satuan tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Harga Satuan tidak boleh kosong!");
            RuleFor(x => x.sub_total_kontrak)
                            .NotNull().WithMessage("Sub Total Kontrak tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Sub Total Kontrak tidak boleh kosong!");

        }

    }

    public class TrKontrakSpjbDetailUploadInsertValidators : AbstractValidator<tr_kontrak_spjb_detail_upload_insert>
    {
        public TrKontrakSpjbDetailUploadInsertValidators()
        {
            

            RuleFor(x => x.kontrak_id)
                            .NotNull().WithMessage("Kontrak Id tidak boleh kosong!")
                            .NotEqual(0).WithMessage("Kontrak Id tidak boleh kosong!");
            RuleFor(x => x.jenis_dokumen)
                            .NotNull().WithMessage("Jenis Dokumen tidak boleh kosong!")
                            .NotEmpty().WithMessage("Jenis Dokumen tidak boleh kosong!");
            RuleFor(x => x.file)
                            .NotNull().WithMessage("Berkas tidak boleh kosong!");

        }

    }




}
