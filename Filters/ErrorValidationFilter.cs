using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Utility.ErrorResponse.Helper;
using Utility.OKResponse.Helper;
using Pharmm.API.Helper;
using Pharmm.API.Dao.Utility;

namespace Utility.Validation.Filter
{
    public class ErrorValidationFilter : IExceptionFilter, IActionFilter
    {
        private readonly UtilityDao _utility;
        public ErrorValidationFilter(UtilityDao utility)
        {
            this._utility = utility;
        }

        public void OnException(ExceptionContext context)
        {
            var ex = context.Exception;

            //jika exception message bukan unique 
            if (!ErrorHelper.GetErrorResponse(ex).data.OutMessage.ToLower().Contains("[unique]"))
            {
                context.Result = new BadRequestObjectResult(ErrorHelper.GetErrorResponse(ex));
            }
            else
            {
                var error = ErrorHelper.GetErrorResponse(ex);

                //format = [UNIQUE] Data ini sudah ada : CONSTRAINT_NAME
                string validatorMessage = error.data.OutMessage;
                string constName = validatorMessage.Split(':')[1].Trim();

                var fieldConstraints = this._utility.GetFieldConstraint(constName).Result;

                string msg = "[UNIQUE] Data dengan ";

                if (fieldConstraints.Count > 0)
                {
                    foreach (var field in fieldConstraints)
                    {
                        msg += $"{field.column_name} dan ";
                    }

                    msg = msg.Remove(msg.LastIndexOf("dan")).TrimEnd() + " ini sudah ada";

                }
                else
                {
                    msg = error.data.OutMessage;
                }

                context.Result = new OkObjectResult(new
                {

                    responseResult = false,
                    data = msg,
                    message = "validasi gagal"
                });
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var validatorModel = new List<string>();
            foreach (var keyModelStatePair in context.ModelState)
            {
                var key = keyModelStatePair.Key;
                var errors = keyModelStatePair.Value.Errors;
                //var errorList = new ErrorList {title = key};

                if (errors.Count > 0)
                {
                    foreach (var t in errors)
                    {
                        string message = GenerateErrorMessage(key, t.ErrorMessage);
                        validatorModel.Add(message);
                    }

                }

            }

            ResponseModel<List<string>> response = new()
            {
                responseResult = false,
                data = validatorModel,
                message = "validasi gagal"
            };

            if (!context.ModelState.IsValid)
            {
                context.Result = new OkObjectResult(response);
            }
        }

        public string GenerateErrorMessage(string key, string error)
        {
            //cek apakah pesan error dari detail ? 
            bool isDetail = key.Contains("[");

            //memisah element array dari string [0] => 0
            Regex regexSeparator = new Regex(@"\[([^\]]+)\]");
            var splitter = regexSeparator.Split(key);

            key = "";
            foreach (var item in splitter)
            {
                int n;
                bool isNumber = int.TryParse(item, out n);

                if (isNumber)
                {
                    //menambah index element array => string
                    key += $" baris ke {n + 1} ";
                }
                else
                {
                    //detailBatch => Detail Batch
                    key += $"{StringHelper.LabelFormat(item)}";
                }

            }

            //detail ke ....
            string detailPrefix = isDetail ?
                $"{key.Remove(key.Remove(key.Length - 1).LastIndexOf(".") + 1).Replace(".", "")}: " : "";

            return StringHelper.ToTitleCase($"{detailPrefix}{error}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }

    public class ErrorList
    {
        public string message { get; set; }
    }


}