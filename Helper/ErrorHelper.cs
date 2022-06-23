using System;
using System.Linq;
using Utility.OKResponse.Helper;

namespace Utility.ErrorResponse.Helper
{
    public static class ErrorHelper
    {
        public static ResponseModel<ErrorModel> GetErrorResponse(Exception ex)
        {
            ErrorModel error = new();
            ResponseModel<ErrorModel> ErrorResponse = new();
            try
            {
                if (ex.InnerException is not null)
                {
                    error.InMessage = ex.InnerException.Message;
                    error.InStackTrace = ex.InnerException.StackTrace;

                }
                else
                {
                    error.InMessage = string.Empty;
                    error.InStackTrace = string.Empty;
                }


                string keyField = "", tableName = "", keyName = "", errorMessage = ex.Message;

                //cek error foreign key
                if (ex.Message.Contains("violates foreign key constraint"))
                {
                    //get table name
                    tableName = ex.Message.Split('"')[1];

                    //get foreign key name
                    keyName = ex.Message.Split('"').Where(x => x.Contains("fkey")).First().ToString();

                    //get foreign key field
                    keyField = keyName.Replace("_fkey", "").Replace($"{tableName}_", "");

                    errorMessage = "[FK] Data tidak ditemukan : " + keyField;
                }

                //cek duplikat 
                if (ex.Message.Contains("duplicate key value violates unique constraint"))
                {

                    //get table name
                    tableName = ex.Message.Split('"')[1];
                    keyField = tableName;
                    errorMessage = $"[UNIQUE] Data ini sudah ada : {keyField}";
                }

                error.OutMessage = errorMessage;
                error.OutStackTrace = ex.StackTrace;

                ErrorResponse.responseResult = false;
                ErrorResponse.data = error;
                ErrorResponse.message = string.Empty;

                return ErrorResponse;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

    public class ErrorModel
    {
        public string InMessage { get; set; }
        public string InStackTrace { get; set; }
        public string OutMessage { get; set; }
        public string OutStackTrace { get; set; }
    }
}
