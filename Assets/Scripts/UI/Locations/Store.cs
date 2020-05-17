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
        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public Text cashText;
        public Text eitcText;
        public Text ctcText;
        public Text snapText;

        public DisableableButton wicButton;

        public WICVoucher voucher;


        private void Start()
        {
            displayPlayerInfo();
        }

        private void OnEnable()
        {
            displayPlayerInfo();
            checkWIC();
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

        private void displayPlayerInfo()
        {
            double cash = currencyManager.getCashAvailable();
            double snap = currencyManager.getSnapAvailable();

            cashText.text = FormatText.formatCost(cash);
            //eitcText.text = FormatText.formatCost(eitc);
            //ctcText.text = FormatText.formatCost(ctc);
            snapText.text = FormatText.formatCost(snap);


            if (currencyManager.getHasWIC())
            {
                voucher.gameObject.SetActive(true);
                voucher.copy(currencyManager.getWICVoucher());
            } else
            {
                voucher.gameObject.SetActive(false);
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
                displayPlayerInfo();
            }



        }

        public void completeFundsPayment(double cash, double eitc, double ctc, double snap)
        {

            if(validateFundsPurchase(cash, eitc, ctc, snap))
            {
                currencyManager.subtractFunds(FundsType.Cash, cash);
                currencyManager.subtractFunds(FundsType.EITC, eitc);
                currencyManager.subtractFunds(FundsType.CTC, ctc);
                currencyManager.subtractFunds(FundsType.Snap, snap);

                messageManager.generateStandardSuccessMessage("Purchase complete.");
                completePayment();
                displayPlayerInfo();
            } 

        }

        public void displayMustBeNumberError()
        {
            messageManager.generateStandardErrorMessage("Input must be a number.");
        }

        private bool validateFundsPurchase(double cash, double eitc, double ctc, double snap)
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
           
            if (currencyManager.validateCashPayment(cash))
            {
                messageManager.generateStandardErrorMessage( "Not enough cash.");
                return false;
            }
            else if (currencyManager.validateCashPayment(snap))
            {
                messageManager.generateStandardErrorMessage("Not enough SNAP fund.");
                return false;
            }
            else if (roundTwoDecimal(cash + ctc + eitc + snap) != roundTwoDecimal(cart.getTotalPrice()))
            {
                messageManager.generateStandardErrorMessage("Total amount does not match");
                return false;
            }
            else
            {
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
                            messageManager.generateStandardErrorMessage("Cannot use WIC on items in cart.");
                        }
                    }

                }
            }
            return valid;
        }

        private void displayDuplicateError()
        {
            messageManager.generateStandardErrorMessage("Cannot use WIC on two foods of the same type.");
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }

    }
}

