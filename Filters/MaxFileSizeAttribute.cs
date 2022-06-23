using Microsoft.AspNetCore.Http;
using Pharmm.API.Services.Setup;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pharmm.API.Filters
{
    public class MaxFileSizeAttribute : ValidationAttribute
    {
        private string errorMessage = "";

        private int _maxFileSize;
        private int _maxFileSizeDefault;
        private ISetupUkuranDokumenService _service;

        public MaxFileSizeAttribute(int maxFileSize)
        {
            //ukuran file maksimal (hardcode)
            _maxFileSize = maxFileSize;
            _maxFileSizeDefault = maxFileSize;
        }

        protected override ValidationResult IsValid(
        object value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            {
                this._service = (ISetupUkuranDokumenService)validationContext.GetService(typeof(ISetupUkuranDokumenService));

                var setupFile = this._service.GetAllSetUkuranDokumen().Result;

                if (setupFile.Count > 0)
                {
                    if (setupFile.First().max_size > 0)
                    {
                        //merubah ukuran maksimal dengan setup dari tabel set_ukuran_dokumen
                        _maxFileSize = setupFile.First().max_size * 1024 * 1024;
                        errorMessage = $"{setupFile.First().error_message}";
                    }
                    else
                    {
                        _maxFileSize = _maxFileSizeDefault / (1024 * 1024);
                        errorMessage = $"Ukuran maksimal file yang diizinkan adalah { _maxFileSize} Mb";
                    }
                }

                if (file.Length > _maxFileSize)
                {
                    return new ValidationResult(GetErrorMessage());
                }
            }

            return ValidationResult.Success;
        }

        public string GetErrorMessage()
        {
            return errorMessage;
        }
    }
}
