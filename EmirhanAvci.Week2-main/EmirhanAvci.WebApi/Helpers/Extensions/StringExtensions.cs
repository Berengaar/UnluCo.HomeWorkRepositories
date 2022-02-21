using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Helpers.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNumber(this string str, bool isControl = false)
        {
            if (Regex.IsMatch(str, @"^\d+$"))
            {
                var numeric = Convert.ToInt32(str);
                if (numeric > 0)
                {
                    isControl = true;
                    return isControl;
                }
                else
                {
                    return isControl;
                }
            }
            else
            {
                return isControl;
            }
        }

        public static bool IsValidPassword(this string str, bool isUpperCase = false, bool isDigit = false)
        {
            return false;
        }
    }
}
