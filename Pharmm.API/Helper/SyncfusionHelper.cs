using QueryModel.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Utility.Syncfusion.Helper
{
    public class SyncfusionHelper
    {

        public static List<ParameterSearchModel> GetParamSearchForAutoComplete(
            string fields,
            string filter
            )
        {

            var param = new List<ParameterSearchModel>();
            string[] fieldParams = fields.Split(',');

            //filter = contains(tolower(ContactName), 'tab')
            string fieldName = (string.IsNullOrEmpty(filter) ? new string[0] : filter.Split('('))[2].Split(')')[0];
            string filterValue = filter.Split(filter.Contains("'") ? "'" : "%27")[1];

            if (!string.IsNullOrEmpty(filter))
            {
                //foreach (var field in fieldParams)
                //{
                param.Add(
                new ParameterSearchModel
                {
                    columnName = fieldName
                    .Replace("/", ".")
                    .Replace("\"", ".")
                    .Replace("\\", "."),
                    filter = "like",
                    searchText = filterValue,
                    searchText2 = ""
                });
                //}
            }

            return param;
        }
    }
}
