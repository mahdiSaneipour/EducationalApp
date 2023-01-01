using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Tools.ConvertNumber
{
    public static class ConvertNumber
    {
        public static string GetPersianNumber(this string englishNumber)
        {
            string[] persian = new string[10] { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            for (int j = 0; j < persian.Length; j++)
                englishNumber = englishNumber.Replace(persian[j], j.ToString());

            return englishNumber;
        }

    }
}
