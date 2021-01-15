using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class Numeric
    {

        /// <summary>
        /// c# does not have an isnumeric function
        /// this function is created to allow for isnumeric checks that
        /// take into consideration $ , and %
        /// </summary>        
        public static bool isNumeric(string val, System.Globalization.NumberStyles NumberStyle)
        {
            Double result;
            //attempt to parse the string argument to a numeric based on numberstyle given)
            return Double.TryParse(val, NumberStyle, System.Globalization.CultureInfo.CurrentCulture, out result);

            //usage:
            //if (isNumeric(Value, System.Globalization.NumberStyles.Float))
            //{
            //    //code for numeric data here
            //}


        }

        /// <summary>
        /// clears formatting characters from a string (e.g. $ or %)
        /// </summary>
        /// <param name="formatted">the formatted text (e.g. $100.00)</param>
        /// <param name="format">the character(s) to replace (e.g. $)</param>
        /// <returns></returns>
        public static string ClearFormatting(string formatted, string format)
        {
            //return string without format
            return formatted.Replace(format, "");
        }

       
    }
}
