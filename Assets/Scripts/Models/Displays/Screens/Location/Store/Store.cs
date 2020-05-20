using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI {
    public class Store : BaseStore
    {
        public WICVoucher voucher;

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

        }

        private void checkWIC()
        {
            if (currencyManager.getHasWIC())
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
            bool valid = true;
            Dictionary<Food, int> foods = cart.foodInCart;
            foreach(KeyValuePair<Food, int> food in foods)
            {
                for(int i = 0; i < food.Value; i++)
                {
                    FoodType foodType = food.Key.wicType;
                    if (!food.Key.wic)
                    {
                        valid = false;
                        messageManager.generateStandardErrorMessage("Non-wic item in cart.");
                        Debug.Log("non-wic");
                    }
                    else if (!currencyManager.getWICVoucher().checkValid(food.Key))
                    {
                        valid = false;
                        messageManager.generateStandardErrorMessage("Wic item category already used.");
                    }
                }
               
            }

            return valid;
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
            bool valid = validateWICPurchase();
            if (valid)
            {
                foreach (Food food in cart.foodInCart.Keys)
                {
                    currencyManager.useVoucher(food);
                }
                messageManager.generateStandardSuccessMessage("Purchase complete.");
                completePayment();
            }



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

        private bool validateWICPurchase()
        {
            bool valid = true;

            WICVoucher voucher = currencyManager.getWICVoucher();

            bool fruitUsed = false;
            bool vegUsed = false;
            bool grainUsed = false;
            bool dairyUsed = false;
            bool proteinUsed = false;

            if (voucher != null)
            {
                foreach (KeyValuePair<Food, int> food in cart.foodInCart)
                {
                    if (valid)
                    {
                        if (voucher.checkValid(food.Key))
                        {
                            switch (food.Key.wicType)
                            {
                                case FoodType.Fruit:
                                    if (fruitUsed)
                                    {
                                        valid = false;
                                        displayDuplicateError();
                                    }
                                    else
                                    {
                                        fruitUsed = true;
                                    }
                                    break;
                                case FoodType.Veg:
                                    if (vegUsed)
                                    {
                                        valid = false;
                                        displayDuplicateError();
                                    }
                                    else
                                    {
                                        vegUsed = true;
                                    }
                                    break;
                                case FoodType.Grain:
                                    if (grainUsed)
                                    {
                                        valid = false;
                                        displayDuplicateError();
                                    }
                                    else
                                    {
                                        grainUsed = true;
                                    }
                                    break;
                                case FoodType.Dairy:
                                    if (dairyUsed)
                                    {
                                        valid = false;
                                        displayDuplicateError();
                                    }
                                    else
                                    {
                                        dairyUsed = true;
                                    }
                                    break;
                                case FoodType.Protein:
                                    if (proteinUsed)
                                    {
                                        valid = false;
                                        displayDuplicateError();
                                    }
                                    else
                                    {
                                        proteinUsed = true;
                                    }
                                    break;
                            }
                        }
                        else
                        {
                            valid = false;
                            messageManager.generateStandardErrorMessage("Cannot use WIC on items in cart.", this);
                        }
                    }

                }
            }
            return valid;
        }

        private void displayDuplicateError()
        {
            messageManager.generateStandardErrorMessage("Cannot use WIC on two foods of the same type.", this);
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

