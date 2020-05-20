using Manage;
using Microsoft.SqlServer.Server;
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

        private void Start()
        {
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
            Debug.Log("open");
            if (checkVoucherCart())
            {
                Debug.Log("voucher valid");
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
                        messageManager.generateStandardErrorMessage("Cannot use voucher on more than one " + cartItem.Key.wicType.toDescriptionString() + " item.");
                        Debug.Log("count " + cartItem.Value);
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
            Debug.Log("voucher payment complete");
            currencyManager.useVoucher(cart.foodInCart);
            messageManager.generateStandardSuccessMessage("WIC voucher has been redeemed.");
            completePayment();
            

        }

        public void completeFundsPayment(double cash, double snap)
        {

            if(validateFundsPurchase(cash, snap))
            {
                currencyManager.subtractFunds(FundsType.Cash, cash);
                currencyManager.subtractFunds(FundsType.Snap, snap);

                messageManager.generateStandardSuccessMessage("Purchase complete.");
                completePayment();
            } 

        }

        public void displayMustBeNumberError()
        {
            messageManager.generateStandardErrorMessage("Input must be a number.");
        }

        public void setCheckoutText(InputField cashField, InputField snapField)
        {
            cashField.text = FormatText.formatDouble(currencyManager.getCheckoutCash(cart.getTotalPrice()));
            Debug.Log("cash " + currencyManager.getCheckoutCash(cart.getTotalPrice()));
            snapField.text = FormatText.formatDouble(currencyManager.getCheckoutSNAP(cart.getTotalPrice()));
            Debug.Log("snap " + currencyManager.getCheckoutSNAP(cart.getTotalPrice()));
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
                        messageManager.generateStandardErrorMessage("Cannot use SNAP funds on premade food.");
                        return false;
                    }
                }
            }

            if (!currencyManager.validateCashPayment(cash))
            {
                messageManager.generateStandardErrorMessage("Not enough cash.");
                return false;
            }
            else if (!currencyManager.validateSNAPPayment(snap))
            {
                messageManager.generateStandardErrorMessage("Not enough SNAP fund.");
                return false;
            }
            else if (roundTwoDecimal(cash + snap) != roundTwoDecimal(cart.getTotalPrice()))
            {
                messageManager.generateStandardErrorMessage("Total amount does not match.");
                return false;
            }
            else
            {
                messageManager.generateStandardSuccessMessage("Purchase complete.");
                return true;
            }
 
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

