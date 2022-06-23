using System;
using System.Collections.Generic;
using System.Text;

namespace QueryModel.Utility
{
    public class ParameterLimit
    {
        public int row_count { get; set; }
        public int page { get; set; }
    
    }

    public class ParameterQuery
    {
        public static string GetQueryFiltersByParams(List<Model.ParameterSearchModel> param, bool withOr = false)
        {
            string filters = string.Empty;

            foreach (var item in param)
            {
                string text = string.Empty;

                if (filters != string.Empty)
                    filters += withOr ? " OR " : " And ";


                item.searchText = item.searchText.ToLower();
                item.searchText2 = item.searchText2.ToLower();

                if (item.filter == "equal")
                {
                    if (!string.IsNullOrEmpty(item.searchText2))
                    {
                        item.columnName = "lower(" + item.columnName + ")";
                    }
                }
                else if (item.filter == "in")
                {
                    if (!string.IsNullOrEmpty(item.searchText2))
                    {
                        item.columnName = "lower(" + item.columnName + ")";
                    }
                }
                else if (item.filter == "like")
                {
                    item.columnName = "lower(" + item.columnName + ")";
                }


                if (item.filter == "like")
                {
                    text += item.columnName + " like '%" + item.searchText + "%'";
                }
                //equals integer rules
                else if (item.filter == "equal" && string.IsNullOrEmpty(item.searchText2))
                {
                    text += item.columnName + (string.IsNullOrEmpty(item.searchText) ? " = '0'" : " = " + item.searchText);
                }
                //equal string rules
                else if (item.filter == "equal" && !string.IsNullOrEmpty(item.searchText2))
                {
                    text += item.columnName + " = '" + item.searchText2 + "'";
                }
                else if (item.filter == "between")
                {
                    text += item.columnName + " between '" + item.searchText + "' and '" + item.searchText2 + "'";
                }
                //filter multiple param with same fieldName, data example : 1,2,3,4,5 or 'a','b','c','d','e'
                else if (item.filter == "in")
                {
                    if (string.IsNullOrEmpty(item.searchText2))
                        text += item.columnName + " in (" + item.searchText + ")";
                    else
                    {
                        string[] datas = item.searchText2.Split(",");

                        if (datas.Length > 0)
                        {
                            string paramsData = item.searchText2; // a,b,c,d => 'a','b','c','d'


                            foreach (var data in datas)
                            {
                                paramsData = paramsData.Replace(data, "'" + data + "'");
                            }

                            text += item.columnName + " in (" + paramsData + ")";
                        }
                        else
                        {
                            text += item.columnName + " in ()";
                        }
                    }
                }
                filters += text;
            }
            return filters;
        }
    
        public static ParameterLimit GetLimitOffset(int page = 1,int row_count = 10)
        {
            return new ParameterLimit
            {
                row_count = row_count,
                page = row_count * (page - 1) //offset dimulai dari 0, jika diset page 1 maka offsetnya 0
            };
        }
    }
}
