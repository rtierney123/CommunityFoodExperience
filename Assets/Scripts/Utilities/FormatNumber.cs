using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Utility
{
    public static class FormatNumber
    {
        public static double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }
    }
}

