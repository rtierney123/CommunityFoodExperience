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

        public float chanceTaxSeason;
        public float chanceVolunteerThere;

        bool ctcEligibility;
        bool eitcEligbility;
        bool isTaxSeason = false;
        bool isVolunteer = false;


        public override void reset()
        {
            base.reset();
            float rand = UnityEngine.Random.Range(0, 100);
            isTaxSeason = (rand < chanceTaxSeason);
            rand = UnityEngine.Random.Range(0, 100);
            isVolunteer = (rand < chanceVolunteerThere);
        }


        public override bool checkAlreadyEntered()
        {
            return player.usedVita;
        }

        public override bool checkCanEnter()
        {
            if (!isTaxSeason)
            {
                cannotEnterStr = Status.vitaNotTaxSeason;
            } else if (!isVolunteer)
            {
                cannotEnterStr = Status.vitaNoVolunteer;
            }
            return (isTaxSeason && isVolunteer);
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
                double monthlyIncome = playerInfo.getJobIncome();
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
            double monthlyIncome = playerInfo.getJobIncome();
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


        protected override void successAction()
        {
            int numChildren = playerInfo.getNumofChildren();
            double eitcReceived = 0;
            double monthlyIncome = playerInfo.getJobIncome();
            if (ctcEligibility)
            {
                currencyManager.addFunds(FundsType.CTC, ctcAmt);
            }
            if (eitcEligbility)
            {
                

                switch (numChildren)
                {
                    case 0:
                        eitcReceived = eitcAmtNoChild;
                        break;
                    case 1:
                        eitcReceived = eitcAmtOneChild;
                        break;
                    case 2:
                        eitcReceived = eitcAmtTwoChild;
                        break;
                    default:
                        eitcReceived = eitcAmtMoreThreeChild;
                        break;
                }
                currencyManager.addFunds(FundsType.EITC, eitcReceived);
            }

            if (ctcEligibility && eitcEligbility)
            {
                string doubleSuccess = String.Format(Status.bothEitcCTC, numChildren, 
                    FormatText.formatCost(monthlyIncome), FormatText.formatCost(eitcReceived));
                messageManager.generateStandardSuccessMessage(doubleSuccess);
            } else if (ctcEligibility && !eitcEligbility)
            {
                string ctcSuccess = String.Format(Status.justCtc, numChildren, FormatText.formatCost(monthlyIncome));
                messageManager.generateStandardSuccessMessage(ctcSuccess);
            } else if (!ctcEligibility && eitcEligbility)
            {
                string eitcSuccess = String.Format(Status.justEitc, numChildren,
                    FormatText.formatCost(monthlyIncome), FormatText.formatCost(eitcReceived));
                messageManager.generateStandardSuccessMessage(eitcSuccess);
            }

            player.usedVita = true;

            base.successAction();

        }

        protected override void failureAction()
        {

            messageManager.generateStandardErrorMessage(Status.neitherEitcCTC);
            player.usedVita = true;

            base.failureAction();
        }

      

    }
}

