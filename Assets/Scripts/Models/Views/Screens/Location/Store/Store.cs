using JetBrains.Annotations;
using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Utility;

namespace UI {
    public class Store : BaseStore
    {

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public DisableableButton wicButton;
        public GameObject purchaseButton;

        public override void reset()
        {
            base.reset();
            purchaseButton.SetActive(false);
        }

        public override void enter()
        {
            base.enter();
            purchaseButton.SetActive(true);
        }

        private void OnEnable()
        {
            checkWIC();
        }

        public override void updateView()
        {
            base.updateView();
            checkWIC();

        }

        private void checkWIC()
        {
            if (currencyManager.checkHasVoucher())
            {
                wicButton.enable();

            }
            else
            {
                wicButton.disable();
            }
        }

        public void openPurchaseOptions()
        {
            canvasController.openPopup(purchaseOptions);
        }

        public void openFundsPurchase()
        {
            canvasController.addToPopUpBackLog(fundsPurchase);
            canvasController.closePopUp();
        }

        public void openVoucherPurchase()
        {
            if (checkVoucherCart())
            {
               canvasController.addToPopUpBackLog(voucherPurchase);
               canvasController.closePopUp();
            }

        }

        private bool checkVoucherCart()
        {
            List<Food> foods = cart.getFoodList();

            if (currencyManager.getWICVoucher() == null)
            {
                return false;
            }
            else if (foods.Count > 5)
            {
                messageManager.generateStandardErrorMessage(Status.tooManyWIC);
            }
            else
            {
                foreach (Food food in foods)
                {
                    if (!food.wic)
                    {
                        messageManager.generateStandardErrorMessage(food.name + " is on a not a WIC item.");
                        return false;

                    }

                }
            }

            List<FoodType> types = currencyManager.getWICArray(foods);
            if(types == null)
            {
                FoodType duplicate = currencyManager.findWICDuplicate(foods);
                displayDuplicateWIC(duplicate);
                return false;
            }

            return true;
        }

        private void displayDuplicateWIC(FoodType foodtype)
        {
            string itemTypeString = foodtype.toDescriptionString();
            string repeatedWicStatus = String.Format(Status.repeatedWIC, itemTypeString);
            messageManager.generateStandardErrorMessage(repeatedWicStatus);
        }

        public void completeVoucherPayment()
        {
            List<FoodType> types = currencyManager.getWICArray(cart.getFoodList());
            currencyManager.useVoucher(types);
            messageManager.generateStandardSuccessMessage(Status.wicRedeemed);
            completePayment();

        }

        public void completeFundsPayment(double cash, double snap)
        {

            if(validateFundsPurchase(cash, snap))
            {
                currencyManager.subtractFunds(FundsType.Cash, cash);
                currencyManager.subtractFunds(FundsType.Snap, snap);

                messageManager.generateStandardSuccessMessage(Status.purchaseCompleted);
                canvasController.closePopUp();
                
                completePayment();
            }

        }

        public void setCheckoutText(InputField cashField, InputField snapField, Text total)
        {
            cashField.text = FormatText.formatDouble(currencyManager.getCheckoutCash(cart.getTotalPrice()));
            snapField.text = FormatText.formatDouble(currencyManager.getCheckoutSNAP(cart.getTotalPrice()));
            total.text = "Total: " + FormatText.formatCost(cart.getTotalPrice());
        }


        private bool validateFundsPurchase(double cash, double snap)
        {
            Dictionary<Food, int> cartFood = cart.foodInCart;
            if (snap > 0)
            {
                foreach (Food food in cart.foodInCart.Keys)
                {
                    if (food.premade)
                    {
                        messageManager.generateStandardErrorMessage(Status.snapOnPremade);
                        return false;
                    }
                }
            }

            if (!currencyManager.validateCashPayment(cash))
            {
                messageManager.generateStandardErrorMessage(Status.insufficientCash);
                return false;
            }
            else if (!currencyManager.validateSNAPPayment(snap))
            {
                messageManager.generateStandardErrorMessage(Status.insufficientSnap);
                return false;
            }
            else if (roundTwoDecimal(cash + snap) != roundTwoDecimal(cart.getTotalPrice()))
            {
                messageManager.generateStandardErrorMessage(Status.totalMismatch);
                return false;
            }
            else
            {
                return true;
            }
 
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

