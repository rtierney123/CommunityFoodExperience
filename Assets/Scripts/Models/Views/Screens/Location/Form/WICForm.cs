using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI
{
    public class WICForm : Form
    {
        //string errorMessage;
        public CurrencyManager currencyManager;

        public override bool checkAlreadyEntered()
        {
            return player.usedWIC;
        }

        public override bool checkValid()
        {
            double monthlyIncome = playerInfo.getFormIncome();
            int numEligible = playerInfo.getNumEligableForWic();
            if (numEligible > 0)
            {
                //errorMessage = "Made too much income to receive WIC benefits.";
                switch (playerInfo.numInHouse)
                {
                    case (1):
                        if (monthlyIncome > 1872)
                        {
                            return false;
                        }
                        break;
                    case (2):
                        if (monthlyIncome > 2538)
                        {
                            return false;
                        }
                        break;
                    case (3):
                        if (monthlyIncome > 3204)
                        {
                            return false;
                        }
                        break;
                    case (4):
                        if (monthlyIncome > 3870)
                        {
                            return false;
                        }
                        break;
                    case (5):
                        if (monthlyIncome > 4536)
                        {
                            return false;
                        }
                        break;
                }
                return true;
            } else
            {
               // errorMessage = "Must be pregnant person in household or have child under 5 to receive WIC.";
                return false;
            }
        }

        protected override void successAction()
        {
            string successString = String.Format(Status.wicApproved, playerInfo.numInHouse, FormatText.formatCost(playerInfo.monthlyIncome));
            messageManager.generateStandardSuccessMessage(successString);
            player.usedWIC = true;
            currencyManager.addWICVoucher();
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage(Status.wicDenied);
            player.usedWIC = true;
        }

    }
}

