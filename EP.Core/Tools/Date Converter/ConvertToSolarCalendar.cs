using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EP.Core.Tools.Date_Converter
{
    public static class ConvertToSolarCalendar
    {
        public static string ToSolarCalendar(this DateTime gregorianCalendar)
        {
            PersianCalendar persianCalendar = new PersianCalendar();

            string date = persianCalendar.GetYear(gregorianCalendar) + "/"
                + persianCalendar.GetMonth(gregorianCalendar).ToString("00") + "/"
                + persianCalendar.GetDayOfMonth(gregorianCalendar).ToString("00");

            return date;
        }
    }
}
