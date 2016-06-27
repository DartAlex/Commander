using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commander
{
    class NumberFormat
    {
        public static string DigitNumber(long value)
        {
            string result = "";
            float intenger;
            float fractional;

            while ((intenger = value / 1000) != 0)
            {
                fractional = value % 1000;
                result = " " + fractional.ToString().PadLeft(3, '0') + result;
                value = Convert.ToInt32(intenger);
            }

            fractional = value % 1000;
            result = fractional.ToString() + result;

            return (result);
        }
    }
}
