using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI
{
    public class SnapForm : Form
    {
        public CurrencyManager currencyManager;
        string errorString = "";


        public override bool checkAlreadyEntered()
        {
            return player.usedSnap;
        }
        
        public override bool checkValid()
        {
            if (player.usedSnap)
            {
                errorString = "Benefits already used.";
                return false;
            }

            int numHouseHold = playerInfo.numInHouse;
            double monthlyIncome = playerInfo.getTotalIncome();

            bool valid = true;

            switch (numHouseHold)
            {
                case 1:
                    if (monthlyIncome > 1000)
                    {
                        valid = false;
                    }
                    break;
                case 2:
                    if (monthlyIncome > 2440)
                    {
                        valid = false;
                    }
                    break;
                case 3:
                    if (monthlyIncome > 2440)
                    {
                        valid = false;
                    }
                    break;
                case 4:
                    if (monthlyIncome > 2440)
                    {
                        valid = false;
                    }
                    break;
                default:
                    if (monthlyIncome > 3120)
                    {
                        valid = false;
                    }
                    break;

            }
            if (!valid)
            {
                errorString = formatIncomeTooHighString(monthlyIncome, numHouseHold);
            }

            return valid;
        }

        protected override uint getProcessTimeInMinutes()
        {
            return 180;
        }


        protected override void successAction()
        {
            double monthlyIncome = playerInfo.getTotalIncome();
            double snapAmt = 0;

            if(monthlyIncome <= 1000)
            {
                snapAmt = 4.5;
            }
            else
            {
                int numHouseHold = playerInfo.numInHouse;
                switch (numHouseHold)
                {
                    case 2:
                        snapAmt = 2.75;
                        break;
                    case 3:
                        snapAmt = 3;
                        break;
                    case 4:
                        snapAmt = 3.75;
                        break;
                    default:
                        snapAmt = 4;
                        break;

                }
            }

            currencyManager.addFunds(FundsType.Snap, snapAmt);
            string successStr = formatSuccessString(monthlyIncome, snapAmt);
            messageManager.generateStandardSuccessMessage(successStr, this);
            player.usedSnap = true;

            base.successAction();
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage(errorString, this);
            player.usedSnap = true;

            base.failureAction();
        }

        private string formatSuccessString(double income, double amt)
        {
            string aidAmt = FormatText.formatCost(amt);
            string incomeAmt = FormatText.formatCost(income);
            string successStr = String.Format(Status.snapApproved, playerInfo.numInHouse, incomeAmt, aidAmt);
            return successStr;
        }

        private string formatIncomeTooHighString(double monthlyIncome, int houseSize)
        {
            string incomeAmt = FormatText.formatCost(monthlyIncome);
            string numHouse = FormatText.formatInt(houseSize);
            string errorStr = string.Format(Status.snapDenied, numHouse, incomeAmt);
            return errorStr;
        }

    }

}
