using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class Store : PopUp
    {
        public Player player;
        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public CanvasController canvasController;
        public ErrorManager errorManager;

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;


        public Text cashText;
        public Text eitcText;
        public Text ctcText;
        public Text snapText;

        public Cart cart;


        private void Start()
        {
            double cash = player.money;
            double eitc = player.eitcFunds;
            double ctc = player.ctcFunds;
            double snap = player.snapFunds;
            cashText.text = player.formatFunds(cash);
            eitcText.text = player.formatFunds(eitc);
            ctcText.text = player.formatFunds(ctc);
            snapText.text = player.formatFunds(snap);
        }

        public void openPurchaseOptions()
        {
            openPopUp(purchaseOptions);
        }

        public void openFundsPurchase()
        {
            openPopUp(fundsPurchase);
        }

        public void openVoucherPurchase()
        {
            openPopUp(voucherPurchase);
        }

        public void openPopUp(GameObject popUp)
        {
            if (canvasController != null)
            {
                canvasController.forcePopupOpen(popUp);
            }
        }


        public void closeScreen()
        {

            closePopUps();
            canvasController.closeScreen(this.gameObject);
        }


        public void completeVoucherPayment()
        {
            closePopUps();
        }

        public void completeFundsPayment(double cash, double eitc, double ctc, double snap)
        {

            if(validateFundsPurchase(cash, eitc, ctc, snap))
            {
                currencyManager.subtractFunds(FundsType.Cash, cash);
                currencyManager.subtractFunds(FundsType.EITC, eitc);
                currencyManager.subtractFunds(FundsType.CTC, ctc);
                currencyManager.subtractFunds(FundsType.Snap, snap);

                foreach (Food item in cart.foodInCart)
                {
                    nutritionManager.addNutrition(item);
                }
                cart.clearAll();
                closePopUps();
            } 
            
         
        }


        private void closePopUps()
        {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
        }


        public void displayMustBeNumberError()
        {
            errorManager.generateStandardMessage("Input must be a number.");
        }

        private bool validateFundsPurchase(double cash, double eitc, double ctc, double snap)
        {
            
            if (player.money < cash)
            {
                errorManager.generateStandardMessage( "Not enough cash.");
                return false;
            }
            else if (player.ctcFunds < ctc)
            {
                errorManager.generateStandardMessage("Not enough CTC fund.");
                return false;

            }
            else if (player.eitcFunds < eitc)
            {
                errorManager.generateStandardMessage("Not enough EITC fund.");
                return false;
            }
            else if (player.snapFunds < snap)
            {
                errorManager.generateStandardMessage("Not enough SNAP fund.");
                return false;
            }
            else if (roundTwoDecimal(cash + ctc + eitc + snap) != roundTwoDecimal(Convert.ToDouble(cart.totalText.text)))
            {
                errorManager.generateStandardMessage("Total amount does not match");
                return false;
            }
            else
            {
                return true;
            }
 
        }

        private bool validateWICPurchase()
        {
            return true;
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

