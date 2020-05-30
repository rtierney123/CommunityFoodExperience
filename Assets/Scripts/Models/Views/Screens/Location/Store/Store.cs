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

           // List<FoodType[]> wicTypes = new List<FoodType[]>();
           int totalCount = 0;
            List<FoodType[]> wicPossible = new List<FoodType[]>();
            if (currencyManager.getWICVoucher() == null)
            {
                return false;
            }
            else
            {
                Dictionary<Food, int> cartContents = cart.foodInCart;
                
                int index = 0;

                foreach (KeyValuePair<Food, int> cartItem in cartContents)
                {
                    Food food = cartItem.Key;
                    int count = cartItem.Value;
                    totalCount += count;
                    if (totalCount > 5)
                    {
                        messageManager.generateStandardErrorMessage(Status.tooManyWIC);
                    } else if (!cartItem.Key.wic)
                    {
                        messageManager.generateStandardErrorMessage(food.name + " is on a not a WIC item.");
                        return false;

                    } else if (count > 1)
                    {
                        if (cartItem.Key.wicType.Length < 1)
                        {
                            Debug.Log("Marked as WIC item but not identified by type.");
                            return false;
                        }
                        else
                        {
                            displayDuplicateWIC(cartItem.Key.wicType[0]);
                            return false;
                        }

                    }
                    else
                    {
                        wicPossible.Add(food.wicType);
                    }
                }

            }
            /*

            (int numBins, FoodType duplicatedBins) = checkBinNumber(cart.foodInCart, 5, totalCount);
            if (duplicatedBins != FoodType.None)
            {
                displayDuplicateWIC(duplicatedBins);
                return false;
            }
            */

            List<FoodType> types = currencyManager.getWICArray(cart.foodInCart.Keys.ToList());
            if(types == null)
            {
                FoodType duplicate = currencyManager.findWICDuplicate(cart.foodInCart.Keys.ToList());
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

        public (int, FoodType) checkBinNumber(Dictionary<Food, int> cartContents, int n, int totalCount)
        {
            int[] bin = new int[n];
            foreach (KeyValuePair<Food, int> cartItem in cartContents)
            {
                Food food = cartItem.Key;
                foreach (FoodType type in food.wicType)
                {
                    switch (type)
                    {
                        case FoodType.Fruit:
                            bin[0]++;
                            break;
                        case FoodType.Veg:
                            bin[1]++;
                            break;
                        case FoodType.Grain:
                            bin[2]++;
                            break;
                        case FoodType.Protein:
                            bin[3]++;
                            break;
                        case FoodType.Dairy:
                            bin[4]++;
                            break;
                    }
                }
            }

            int binCount = 0;
            int maxBin = 0;
            int max = 0;
            int index = 0;
            foreach (int c in bin)
            {
                if (c > 0)
                {
                    binCount++;
                }

                if (c > max)
                {
                    maxBin = index;
                    max = c;
                }
                index++;
            }

            FoodType duplicated = FoodType.None;

            if(binCount < totalCount)
            {
                switch (maxBin)
                {
                    case 0:
                        duplicated = FoodType.Fruit;
                        break;
                    case 1:
                        duplicated = FoodType.Veg;
                        break;
                    case 2:
                        duplicated = FoodType.Grain;
                        break;
                    case 3:
                        duplicated = FoodType.Protein;
                        break;
                    case 4:
                        duplicated = FoodType.Dairy;
                        break;
                }
            }


            return (binCount, duplicated);
        }

        public void checkCombinations()
        {

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

