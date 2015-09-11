using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuickRecord
{
    public static class Helpers
    {
        public static string AddZeroes(int number, int digits = 2)
        {
            return number.ToString().PadLeft(digits, '0');
        }

        public static string HourTo12(int hour)
        {
            if (hour == 0)
            {
                return 12.ToString();
            }
            if (hour > 12)
            {
                return AddZeroes(hour - 12);
            }
            return AddZeroes(hour);
        }
    }
}
