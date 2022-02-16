using Humanizer;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Utility.OKResponse.Helper
{
    public class ResponseHelper
    {
        public static ResponseModel<T> GetResponse<T>(T _data = default,
            bool _responseResult = false, string _message = "")
        {

            ResponseModel<T> response = new();

            try
            {
                response.responseResult = _responseResult;
                response.data = _data;
                response.message = _message;
                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static ResponseModel<T> GetResponseDataExisting<T>(
            T _data = default,
            object _objectClass = null,
            string[] _propertyExisting = null)
        {

            ResponseModel<T> response = new();

            try
            {
                string message = "";
                _propertyExisting = _propertyExisting ?? new string[0];

                response.responseResult = false;
                response.data = _data;
                response.message = "";

                if (_propertyExisting.Length > 0)
                {
                    message += "Data dengan ";

                    foreach (var property in _propertyExisting)
                    {
                        message += $"{property.Humanize().ToLower()} '{_objectClass.GetType().GetProperty(property).GetValue(_objectClass, null)}' dan ";
                    }

                    message = message.Remove(message.LastIndexOf("dan"));
                    message += "sudah ada";

                    response.message = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(message.ToLower());
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }

    public class ResponseModel<T>
    {
        public bool responseResult { get; set; } = false;
        public T data { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
