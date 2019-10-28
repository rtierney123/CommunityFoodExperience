using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class FormatText
    {
        public static string formatDouble(double number)
        {
            return number.ToString();
        }

        public static string formatCost(double number)
        {
            return "$" + number.ToString();
        }

        public static string formatBool(bool value)
        {
            if (value)
            {
                return "Yes";
            } else
            {
                return "No";
            }
        }
    }
}

