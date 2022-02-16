using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Pharmm.API.Helper
{
    public class StringHelper
    {
        //aa_bb_bb => AaBbCc
        public static string CamelCase(string s,string separator = "_")
        {
            TextInfo info = CultureInfo.CurrentCulture.TextInfo;
            var x = info.ToTitleCase(s).Replace(separator, string.Empty);

            return x;
        }

        //formating NamaUser to Nama User
        public static string LabelFormat(string value, string pemisah = " ")
        {

            var r = new Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])",RegexOptions.IgnorePatternWhitespace);

            string label = r.Replace(value, " ");
            label = label.Replace("_", "");


            string newLabel = Regex.Replace(label, @"(^\w)|(\s\w)", m => m.Value.ToUpper());

            //replace multiple space with single space
            newLabel = Regex.Replace(newLabel, @"\s+", " ");

            return newLabel;
        }


        public static string ToTitleCase(string str)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

    }
}
