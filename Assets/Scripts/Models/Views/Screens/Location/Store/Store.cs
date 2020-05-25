using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
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
            openPopUp(purchaseOptions);
        }

        public void openFundsPurchase()
        {
            openPopUp(fundsPurchase);
        }

        public void openVoucherPurchase()
        {
            if (checkVoucherCart())
            {
                openPopUp(voucherPurchase);
            }

        }

        private bool checkVoucherCart()
        {
            bool fruitAdded = false;
            bool vegAdded= false;
            bool grainAdded = false;
            bool proteinAdded = false;
            bool dairyAdded = false;

            if(currencyManager.getWICVoucher() == null)
            {
                return false;
            }
            else
            {
                Dictionary<Food, int> cartContents = cart.foodInCart;
                foreach (KeyValuePair<Food, int> cartItem in cartContents)
                {
                    Food food = cartItem.Key;
                    int count = cartItem.Value;

                    FoodType foodType = food.wicType;

                    if (!cartItem.Key.wic)
                    {
                        messageManager.generateStandardErrorMessage(food.name + " is on a not a WIC item.");
                        Debug.Log("non-wic");
                        return false;
                        
                    }
                    else if ( count > 1 || (fruitAdded && foodType == FoodType.Fruit) || (vegAdded && foodType == FoodType.Veg) ||
                                (grainAdded && foodType == FoodType.Grain ) || (proteinAdded && foodType == FoodType.Protein) ||
                                (dairyAdded && foodType == FoodType.Dairy))
                    {
                        string itemTypeString = cartItem.Key.wicType.toDescriptionString();
                        string repeatedWicStatus = String.Format(Status.repeatedWIC, itemTypeString);
                        messageManager.generateStandardErrorMessage(repeatedWicStatus);
                        return false;
                    }
                    else
                    {
                        switch (foodType)
                        {
                            case FoodType.Fruit:
                                fruitAdded = true;
                                break;
                            case FoodType.Veg:
                                vegAdded = true;
                                break;
                            case FoodType.Grain:
                                grainAdded = true;
                                break;
                            case FoodType.Protein:
                                proteinAdded = true;
                                break;
                            case FoodType.Dairy:
                                dairyAdded = true;
                                break;
                        }
                    }

                }
            }


            return true;
        }

        public void openPopUp(GameObject popUp)
        {
            if (canvasController != null)
            {
                canvasController.forcePopupOpen(popUp);
            }
        }


        public void completeVoucherPayment()
        {
            currencyManager.useVoucher(cart.foodInCart);
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
                completePayment();
            } 

        }

        public void setCheckoutText(InputField cashField, InputField snapField)
        {
            cashField.text = FormatText.formatDouble(currencyManager.getCheckoutCash(cart.getTotalPrice()));
            snapField.text = FormatText.formatDouble(currencyManager.getCheckoutSNAP(cart.getTotalPrice()));
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
                messageManager.generateStandardSuccessMessage(Status.purchaseCompleted);
                return true;
            }
 
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

