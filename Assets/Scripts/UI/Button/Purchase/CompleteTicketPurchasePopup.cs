﻿using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CompleteTicketPurchasePopup : PurchasePopup
    {
     


        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }
    }
}
