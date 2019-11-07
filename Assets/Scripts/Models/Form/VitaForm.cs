using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI
{
    public class VitaForm : Form
    {
        public double ctcAmt;
        public double eitcAmtNoChild;
        public double eitcAmtOneChild;
        public double eitcAmtTwoChild;
        public double eitcAmtMoreThreeChild;
        public CurrencyManager currencyManager;

        bool ctcEligibility;
        bool eitcEligbility;
        string ctcStatusString;
        string eitcStatusString;

        public override bool checkAlreadyEntered()
        {
            return player.usedVita;
        }

        protected override bool checkValid()
        {
            ctcEligibility = checkCTCEligibility();
            eitcEligbility = checkEITCEligibility();
            if(ctcEligibility || eitcEligbility)
            {
                return true;
            } else
            {
                return false;
            }
        }

        private bool checkCTCEligibility()
        {
            bool valid = true;
            
            if (player.federalAssistance)
            {
                ctcStatusString = "Cannot receive CTC benefits while on financial aid.";
                valid = false;
            }
            else
            {
                double monthlyIncome = player.getMonthlyIncome();
                int numChildren = player.getNumofChildren();

                switch (numChildren)
                {
                    case 0:
                        if (player.single)
                        {
                            if(monthlyIncome > 1235)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (player.married)
                        {
                            if (monthlyIncome > 1694)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    case 1:
                        if (player.single)
                        {
                            if (monthlyIncome > 3261)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (player.married)
                        {
                            if (monthlyIncome > 3721)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    case 2:
                        if (player.single)
                        {
                            if (monthlyIncome > 3705)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (player.married)
                        {
                            if (monthlyIncome > 4165)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    default:
                        if (player.single)
                        {
                            if (monthlyIncome > 3979)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (player.married)
                        {
                            if (monthlyIncome > 4439)
                            {
                                ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                }
            }
            return valid;
        }


        private bool checkEITCEligibility()
        {
            bool valid = true;
            double monthlyIncome = player.getMonthlyIncome();

            if (player.married && player.jointTax)
            {
               if(monthlyIncome > 9167)
                {
                    eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                    valid = false;
                }
            } else if(player.married && !player.jointTax)
            {
                if (monthlyIncome > 4583)
                {
                    eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                    valid = false;
                }
            } else if (player.single)
            {
                if (monthlyIncome > 6250)
                {
                    eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                    valid = false;
                }
            }
            return valid;
        }

        private string formatSuccessString(string aidType, double amt)
        {
            string aidAmt = FormatText.formatCost(amt);
            string successStr = "You receive {0} benefits. You have another {1} to spend.";
            successStr = string.Format(successStr, aidType, aidAmt);
            return successStr;
        }

        private string formatIncomeTooHighString(string aidType, double monthlyIncome)
        {
            string incomeAmt = FormatText.formatDouble(monthlyIncome);
            string errorStr = "Monthly income of {0} is too high to receive {1} benefits.";
            errorStr = string.Format(errorStr, aidType, incomeAmt);
            return errorStr;
        }

        protected override void successAction()
        {

            if (ctcEligibility)
            {
                ctcStatusString = formatSuccessString("CTC", ctcAmt);
                currencyManager.addFunds(FundsType.CTC, ctcAmt);
            }
            if (eitcEligbility)
            {
                int numChildren = player.getNumofChildren();

                switch (numChildren)
                {
                    case 0:
                        eitcStatusString = formatSuccessString("EITC", eitcAmtNoChild);
                        currencyManager.addFunds(FundsType.EITC, eitcAmtNoChild);
                        break;
                    case 1:
                        eitcStatusString = formatSuccessString("EITC", eitcAmtOneChild);
                        currencyManager.addFunds(FundsType.EITC, eitcAmtOneChild);
                        break;
                    case 2:
                        eitcStatusString = formatSuccessString("EITC", eitcAmtTwoChild);
                        currencyManager.addFunds(FundsType.EITC, eitcAmtTwoChild);
                        break;
                    default:
                        eitcStatusString = formatSuccessString("EITC", eitcAmtMoreThreeChild);
                        currencyManager.addFunds(FundsType.EITC, eitcAmtMoreThreeChild);
                        break;
                }
            }

            if (ctcEligibility && eitcEligbility)
            {
                StartCoroutine(showTwoSuccesses(ctcStatusString, eitcStatusString));
            } else if (ctcEligibility && !eitcEligbility)
            {
                StartCoroutine(showOneSuccessOneError(ctcStatusString, eitcStatusString));
            } else if (!ctcEligibility && eitcEligbility)
            {
                StartCoroutine(showOneSuccessOneError(eitcStatusString, ctcStatusString));
            }

            player.usedVita = true;

            base.successAction();

        }

        protected override void failureAction()
        {
            StartCoroutine(showTwoError(eitcStatusString, ctcStatusString));
            player.usedVita = true;

            base.failureAction();
        }


        public IEnumerator showTwoSuccesses(string msg1, string msg2)
        {
            messageManager.generateStandardSuccessMessage(msg1);
            yield return new WaitForSeconds(delayTime);
            messageManager.generateStandardSuccessMessage(msg2);

        }

        public IEnumerator showOneSuccessOneError(string successMsg, string errorMsg)
        {
            messageManager.generateStandardSuccessMessage(successMsg);
            yield return new WaitForSeconds(delayTime);
            messageManager.generateStandardErrorMessage(errorMsg);
        }

        public IEnumerator showTwoError(string msg1, string msg2)
        {
            messageManager.generateStandardErrorMessage(msg1);
            yield return new WaitForSeconds(delayTime);
            messageManager.generateStandardErrorMessage(msg2);
        }

    }
}

