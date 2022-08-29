using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PremiumGiveSystemAPI.Helper
{
    public static class Helper
    {
        public static string ToDateReportFormat(this string date)
        {
            var sDate = date.Split('/');
            return sDate[0].PadLeft(2, '0') + "/" + sDate[1].PadLeft(2, '0') + "/" + sDate[2].PadLeft(4, '0');
        }
    }
}
