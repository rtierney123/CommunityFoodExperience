﻿using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class Store : BaseLocation
    {
        public GameObject successPopup;

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public Text cashText;
        public Text eitcText;
        public Text ctcText;
        public Text snapText;

        public Cart cart;

        public WICVoucher voucher;


        private void Start()
        {
            displayPlayerInfo();
        }

        private void OnEnable()
        {
            displayPlayerInfo();
        }

        private void displayPlayerInfo()
        {
            double cash = player.money;
            double eitc = player.eitcFunds;
            double ctc = player.ctcFunds;
            double snap = player.snapFunds;

            cashText.text = player.formatFunds(cash);
            eitcText.text = player.formatFunds(eitc);
            ctcText.text = player.formatFunds(ctc);
            snapText.text = player.formatFunds(snap);

            voucher.copy(player.wicVoicher);
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
            HashSet<Food> foods = cart.foodInCart;
            foreach(Food food in foods)
            {
                FoodType foodType = food.wicType;
                if(!food.wic)
                {
                    valid = false;
                    messageManager.generateStandardErrorMessage("Non-wic item in cart.");
                    Debug.Log("non-wic");
                }
                else if (!player.wicVoicher.checkValid(food))
                {
                    valid = false;
                    messageManager.generateStandardErrorMessage("Wic item category already used.");
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
                foreach (Food food in cart.foodInCart)
                {
                    nutritionManager.addNutrition(food);
                    currencyManager.useVoucher(food);

                }

                cart.clearAll();
                canvasController.forcePopupOpen(successPopup);
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

                foreach (Food item in cart.foodInCart)
                {
                    nutritionManager.addNutrition(item);
                }
                cart.clearAll();
                canvasController.forcePopupOpen(successPopup);
                displayPlayerInfo();
            } 

        }

        public void displayMustBeNumberError()
        {
            messageManager.generateStandardErrorMessage("Input must be a number.");
        }

        private bool validateFundsPurchase(double cash, double eitc, double ctc, double snap)
        {
            HashSet<Food> cartFood = cart.foodInCart;
            if (snap > 0)
            {
                foreach (Food food in cart.foodInCart)
                {
                    if (food.premade)
                    {
                        messageManager.generateStandardErrorMessage("Cannot use SNAP funds on premade food.");
                        return false;
                    }
                }
            }
           
            if (player.money < cash)
            {
                messageManager.generateStandardErrorMessage( "Not enough cash.");
                return false;
            }
            else if (player.ctcFunds < ctc)
            {
                messageManager.generateStandardErrorMessage("Not enough CTC fund.");
                return false;

            }
            else if (player.eitcFunds < eitc)
            {
                messageManager.generateStandardErrorMessage("Not enough EITC fund.");
                return false;
            }
            else if (player.snapFunds < snap)
            {
                messageManager.generateStandardErrorMessage("Not enough SNAP fund.");
                return false;
            }
            else if (roundTwoDecimal(cash + ctc + eitc + snap) != roundTwoDecimal(Convert.ToDouble(cart.totalText.text)))
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

            WICVoucher voucher = player.wicVoicher;

            bool fruitUsed = false;
            bool vegUsed = false;
            bool grainUsed = false;
            bool dairyUsed = false;
            bool proteinUsed = false;

            if (voucher != null)
            {
                foreach (Food food in cart.foodInCart)
                {
                    if (valid)
                    {
                        if (voucher.checkValid(food))
                        {
                            switch (food.wicType)
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

