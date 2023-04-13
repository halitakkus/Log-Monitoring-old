using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Core.Extensions
{
    public static class DateTimeExtensions
    {
        public static int GetMonthNumberFromFullMonthName(this string monthName, CultureInfo? cultureInfo = null)
        {
            if(cultureInfo == null) { 
                cultureInfo = new CultureInfo("tr-TR");
            }
            var monthNumber = Enumerable.Range(1, 12)
                .Where(i => monthName.ToLower().Equals(cultureInfo.DateTimeFormat.GetMonthName(i).ToLower()))
                .FirstOrDefault();
            
            return monthNumber;
        }
    }
}
