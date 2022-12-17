using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Persistance.Helper
{
    public static class Commons
    {
        public static string GetPersianDate(DateTime dateTime)
        {
            PersianCalendar pc = new PersianCalendar();
            return $"{pc.GetYear(dateTime)}/{pc.GetMonth(dateTime).ToString().PadLeft(2, '0')}/{pc.GetDayOfMonth(dateTime).ToString().PadLeft(2, '0')} {pc.GetHour(dateTime).ToString().PadLeft(2, '0')}:{pc.GetMinute(dateTime).ToString().PadLeft(2, '0')}:{pc.GetSecond(dateTime).ToString().PadLeft(2, '0')}";
        }
    }
}
