using Manage;
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
        
        protected override bool checkValid()
        {
            int numHouseHold = player.numInHouse;
            double monthlyIncome = player.getMonthlyIncome();

            bool valid = true;
            switch (numHouseHold)
            {
                case 0:
                    if (monthlyIncome > 1316)
                    {
                        valid = false;
                    }
                    break;
                case 1:
                    if (monthlyIncome > 1784)
                    {
                        valid = false;
                    }
                    break;
                case 2:
                    if (monthlyIncome > 2252)
                    {
                        valid = false;
                    }
                    break;
                case 4:
                    if (monthlyIncome > 2720)
                    {
                        valid = false;
                    }
                    break;
                default:
                    if (monthlyIncome > 3188)
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

        protected override void successAction()
        {
            double monthlyIncome = player.getMonthlyIncome();
            double snapAmt = 0;
            if(monthlyIncome <= 799)
            {
                snapAmt = 4.5;
            } else if (monthlyIncome <= 1000 && monthlyIncome>= 800)
            {
                snapAmt = 2.5;
            } else if (monthlyIncome <= 2794 && monthlyIncome>= 1001)
            {
                snapAmt = 1;
            } else
            {
                Debug.Log("Error should not be sucessful");
            }

            currencyManager.addFunds(FundsType.Snap, snapAmt);
            string successStr = formatSuccessString(snapAmt);
            messageManager.generateStandardSuccessMessage(successStr);
            player.usedSnap = true;

            base.successAction();
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage(errorString);
            player.usedSnap = true;

            base.failureAction();
        }

        private string formatSuccessString(double amt)
        {
            string aidAmt = FormatText.formatCost(amt);
            string successStr = "You receive SNAP {0} food stamps. You can spend this amount on non-premade food items.";
            successStr = string.Format(successStr, aidAmt);
            return successStr;
        }

        private string formatIncomeTooHighString(double monthlyIncome, int houseSize)
        {
            string incomeAmt = FormatText.formatDouble(monthlyIncome);
            string numHouse = FormatText.formatInt(houseSize);
            string errorStr = "Monthly income of {0} for a house of {1} is too high to receive SNAP benefits.";
            errorStr = string.Format(errorStr, incomeAmt, numHouse);
            return errorStr;
        }

    }

}
