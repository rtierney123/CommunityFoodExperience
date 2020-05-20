﻿using Manage;
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
            
            if (playerInfo.socialSecurityIncome > 0 || playerInfo.temporaryAssistance >0)
            {
                eitcStatusString = "Cannot receive EITC benefits while recieving Social Security or other temporary assistance. All wages must be earned from a job.";
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
                                eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 1694)
                            {
                                eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    case 1:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3261)
                            {
                                eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 3721)
                            {
                                eitcStatusString = formatIncomeTooHighString("EITC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    case 2:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3705)
                            {
                                eitcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 4165)
                            {
                                eitcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        break;
                    default:
                        if (playerInfo.single)
                        {
                            if (monthlyIncome > 3979)
                            {
                                eitcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                                valid = false;
                            }
                        }
                        else if (playerInfo.married)
                        {
                            if (monthlyIncome > 4439)
                            {
                                eitcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
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

            if(numKids == 0)
            {
                ctcStatusString = "Must have a child to be eligable for CTC.";
                valid = false;
            }
            else
            {
                if (playerInfo.married && playerInfo.jointTax)
                {
                    if (monthlyIncome > 9167)
                    {
                        ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                        valid = false;
                    }
                }
                else if (playerInfo.married && !playerInfo.jointTax)
                {
                    if (monthlyIncome > 4583)
                    {
                        ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
                        valid = false;
                    }
                }
                else if (playerInfo.single)
                {
                    if (monthlyIncome > 6250)
                    {
                        ctcStatusString = formatIncomeTooHighString("CTC", monthlyIncome);
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

            if (ctcEligibility)
            {
                ctcStatusString = formatSuccessString("CTC", ctcAmt);
                currencyManager.addFunds(FundsType.CTC, ctcAmt);
            }
            if (eitcEligbility)
            {
                int numChildren = playerInfo.getNumofChildren();

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
