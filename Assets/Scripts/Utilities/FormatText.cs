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
            string s = "$" + number.ToString();
            if (s.Length == 2)
            {
                s += ".";
            }
            int n = s.Length;
            for (int i = 0; i < 5 - n; i++)
            {
                s += "0";
            }

            return s;
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

