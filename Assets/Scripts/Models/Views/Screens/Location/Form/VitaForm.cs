using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
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


        public override bool checkAlreadyEntered()
        {
            return player.usedVita;
        }

        public override bool checkValid()
        {
            if (player.usedVita)
            {
                return false;
            }
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

        private bool checkEITCEligibility()
        {
            bool valid = true;

            if (!playerInfo.paysTaxes)
            {
                valid = false;
            }
            else if (playerInfo.socialSecurityIncome > 0 || playerInfo.temporaryAssistance >0)
            {
                valid = false;
            }
            else
            {
                double monthlyIncome = playerInfo.getMonthlyIncome();
                int numChildren = playerInfo.getNumofChildren();
                switch (numChildren)
                {
                    case 0:
                        if (playerInfo.single)
                        {
                            if(monthlyIncome > 1235)
                            {
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 1694)
                            {
                                valid = false;
                            }
                        }
                        break;
                    case 1:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3261)
                            {
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 3721)
                            {
                                valid = false;
                            }
                        }
                        break;
                    case 2:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3705)
                            {
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 4165)
                            {
                                valid = false;
                            }
                        }
                        break;
                    default:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3979)
                            {
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 4439)
                            {
                                valid = false;
                            }
                        }
                        break;
                }
            }
            return valid;
        }


        private bool checkCTCEligibility()
        {
            bool valid = true;
            double monthlyIncome = playerInfo.getTotalIncome();
            int numKids = playerInfo.getNumofChildren();

            if (!playerInfo.paysTaxes)
            {
                valid = false;
            }
            else if(numKids == 0)
            {
                valid = false;
            }
            else
            {
                if (playerInfo.married && playerInfo.jointTax)
                {
                    if (monthlyIncome > 9167)
                    {
                        valid = false;
                    }
                }
                else if (playerInfo.married && !playerInfo.jointTax)
                {
                    if (monthlyIncome > 4583)
                    {
                        valid = false;
                    }
                }
                else if (playerInfo.single)
                {
                    if (monthlyIncome > 6250)
                    {
                        valid = false;
                    }
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
            int numChildren = playerInfo.getNumofChildren();
            double eitcReceived = 0;
            if (ctcEligibility)
            {
                //ctcStatusString = formatSuccessString("CTC", ctcAmt);
                currencyManager.addFunds(FundsType.CTC, ctcAmt);
            }
            if (eitcEligbility)
            {
                

                switch (numChildren)
                {
                    case 0:
                        //eitcStatusString = formatSuccessString("EITC", eitcAmtNoChild);
                        eitcReceived = eitcAmtNoChild;
                        break;
                    case 1:
                        //eitcStatusString = formatSuccessString("EITC", eitcAmtOneChild);
                        eitcReceived = eitcAmtOneChild;
                        break;
                    case 2:
                        //eitcStatusString = formatSuccessString("EITC", eitcAmtTwoChild);
                        eitcReceived = eitcAmtTwoChild;
                        break;
                    default:
                        //eitcStatusString = formatSuccessString("EITC", eitcAmtMoreThreeChild);
                        eitcReceived = eitcAmtMoreThreeChild;
                        break;
                }
                currencyManager.addFunds(FundsType.EITC, eitcReceived);
            }

            if (ctcEligibility && eitcEligbility)
            {
                string doubleSuccess = String.Format(Status.bothEitcCTC, numChildren, 
                    FormatText.formatCost(playerInfo.monthlyIncome), FormatText.formatCost(eitcReceived));
                messageManager.generateStandardSuccessMessage(doubleSuccess);
                //StartCoroutine(showTwoSuccesses(ctcStatusString, eitcStatusString));
            } else if (ctcEligibility && !eitcEligbility)
            {
                string ctcSuccess = String.Format(Status.justCtc, numChildren, FormatText.formatCost(playerInfo.monthlyIncome));
                messageManager.generateStandardSuccessMessage(ctcSuccess);
                //StartCoroutine(showOneSuccessOneError(ctcStatusString, eitcStatusString));
            } else if (!ctcEligibility && eitcEligbility)
            {
                string eitcSuccess = String.Format(Status.justEitc, numChildren,
                    FormatText.formatCost(playerInfo.monthlyIncome), FormatText.formatCost(eitcReceived));
                messageManager.generateStandardSuccessMessage(eitcSuccess);
                // StartCoroutine(showOneSuccessOneError(eitcStatusString, ctcStatusString));
            }

            player.usedVita = true;

            base.successAction();

        }

        protected override void failureAction()
        {

            messageManager.generateStandardErrorMessage(Status.neitherEitcCTC);
            //StartCoroutine(showTwoError(eitcStatusString, ctcStatusString));
            player.usedVita = true;

            base.failureAction();
        }


        public IEnumerator showTwoSuccesses(string msg1, string msg2)
        {
            messageManager.generateStandardSuccessMessage(msg1, this);
            yield return new WaitForSeconds(nextActionTime);
            messageManager.generateStandardSuccessMessage(msg2, this);

        }

        public IEnumerator showOneSuccessOneError(string successMsg, string errorMsg)
        {
            messageManager.generateStandardSuccessMessage(successMsg, this);
            yield return new WaitForSeconds(nextActionTime);
            messageManager.generateStandardErrorMessage(errorMsg, this);
        }

        public IEnumerator showTwoError(string msg1, string msg2)
        {
            messageManager.generateStandardErrorMessage(msg1, this);
            yield return new WaitForSeconds(nextActionTime);
            messageManager.generateStandardErrorMessage(msg2, this);
        }

      

    }
}

