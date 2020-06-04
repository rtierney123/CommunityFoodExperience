using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml.XPath;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace Manage
{
    public class CurrencyManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public MessageManager messageManager;
        public Wallet walletDisplay;
        public FooterDisplay footerDisplay;
        public Player player;
        public int changeDisplayTime;



        // Update is called once per frame
        void Update()
        {

        }

        public void update()
        {
            footerDisplay.updateView();
        }


        public void addFunds(FundsType type, double amt)
        {
            switch (type)
            {
                case FundsType.Cash:
                    player.addCash(amt);
                    break;
                case FundsType.Snap:
                    player.addSnap(amt);
                    break;
                case FundsType.EITC:
                    player.addEITC(amt);
                    player.addCash(amt);
                    break;
                case FundsType.CTC:
                    player.addCTC(amt);
                    player.addCash(amt);
                    break;
            }
            walletDisplay.updateWallet();
            update();
        }
        public double getCheckoutCash(double total)
        {
            return total - getCheckoutSNAP(total);
        }

        public double getCheckoutSNAP(double total)
        {
            if (player.snapFunds >= total)
            {
                return total;
            }
            else if ((total - player.money) < 0 || (player.money + player.snapFunds < total))
            {
                return player.snapFunds;
            }
            else
            {
                return total - player.money;
            }
        }

        public double getCashAvailable()
        {
            return player.money;
        }

        public double getSnapAvailable()
        {
            return player.snapFunds;
        }

        public int getTicketsAvailable()
        {
            return player.busTickets;
        }

        public bool getHasBusPass()
        {
            return player.playerInfo.busPass;
        }
        public bool getHasTickets()
        {
            return player.busTickets > 0;
        }

        public bool checkHasVoucher()
        {
            return (player.voucher != null) && player.voucher.active;
        }
        public WICVoucher getWICVoucher()
        {
            return player.voucher;
        }

        public bool validateCashPayment(double amt)
        {
            return amt <= player.money;
        }

        public bool validateSNAPPayment(double amt)
        {
            return amt <= player.snapFunds;
        }


        public bool validatePayment(double cash, double ctc, double eitc, double snap, double totalDue)
        {
            if (player.money < cash)
            {
                messageManager.generateStandardErrorMessage("Not enough cash.");
                return false;
            }
            else if (player.ctcAcquired < ctc)
            {
                messageManager.generateStandardErrorMessage("Not enough CTC fund.");
                return false;

            }
            else if (player.eitcAcquired < eitc)
            {
                messageManager.generateStandardErrorMessage("Not enough EITC fund.");
                return false;
            }
            else if (player.snapFunds < snap)
            {
                messageManager.generateStandardErrorMessage("Not enough SNAP fund.");
                return false;
            }
            else if (FormatNumber.roundTwoDecimal(cash + ctc + eitc + snap) != FormatNumber.roundTwoDecimal(totalDue))
            {
                messageManager.generateStandardErrorMessage("Total amount does not match");
                return false;
            }
            else
            {
                return true;
            }
        }

      

        public void subtractFunds(FundsType type, double amt)
        {
            switch (type)
            {
                case FundsType.Cash:
                    player.subtractCash(amt);
                    break;
                case FundsType.Snap:
                    player.subtractSnap(amt);
                    break;
                case FundsType.EITC:
                    player.subtractEITC(amt);
                    break;
                case FundsType.CTC:
                    player.subtractCTC(amt);
                    break;
            }
            walletDisplay.updateWallet();
            footerDisplay.updateView();
        }

        public void addWICVoucher()
        {
            player.addVoucher();
            footerDisplay.updateView();
        }

        public void useVoucher(List<FoodType> types)
        {
            foreach(FoodType food in types)
            {
                player.updateVoucher(food);
            }
            player.voucher.deactivate();
            footerDisplay.updateView();
        }

        public List<FoodType> getWICArray(List<Food> foods)
        {
            Food[] foodArray = new Food[5];
            if(foods.Count > 5)
            {
                return null;
            }
            else
            {
                int index = 0;
                foreach(Food food in foods)
                {
                    foodArray[index] = food;
                    index++;
                }
                for(int i = index; i < 5; i++)
                {
                    foodArray[i] = null;
                }
            }
            Debug.Log("size foods " + foods.Count);
            
            Food[] result = findValidPermutation(foodArray, 0, 4);


            List<FoodType> wicList = new List<FoodType>();
            if(result != null)
            {
                for(int i = 0; i < 5; i++)
                {
                    if(result[i] != null)
                    {
                        FoodType validType = wicNumToFoodType(i);
                        wicList.Add(validType);
                    }
                   
                }
                return wicList;
            }
            else
            {
                return null;
            }

        }

        public FoodType findWICDuplicate(List<Food> foods)
        {
            int[] bins = new int[5];
            foreach(Food food in foods)
            {
                FoodType[] wicTypes = food.wicType;
                foreach(FoodType type in wicTypes)
                {
                    Debug.Log("find duplicate");
                    int num = foodTypeToWICNum(type);
                    if(num >= 0)
                    {
                        bins[num]++;
                        if(bins[num] > 1)
                        {
                            return wicNumToFoodType(num);
                        }
                    }
                }
            }

            return FoodType.None;
        }

        private Food[] findValidPermutation(Food[] foods, int start, int end)
        {

            if (start == end)
            {
                if (checkValidWICSoln(foods))
                {
                    return foods;
                }

            }
            else
            {
                for (int i = start; i <= end; i++)
                {
                    swapTwoFoods(ref foods[start], ref foods[i]);
                    Food[] soln = findValidPermutation(foods, start + 1, end);
                    if (soln != null)
                    {
                        return soln;
                    }
                    swapTwoFoods(ref foods[start], ref foods[i]);
                }

            }


            return null;
        }

        public void swapTwoFoods(ref Food a, ref Food b)
        {
            Food temp = a;
            a = b;
            b = temp;
        }

        private bool checkValidWICSoln(Food[] soln)
        {
            int index = 0;
            foreach(Food food in soln)
            {
                if(food != null)
                {
                    FoodType[] foodTypes = food.wicType;
                    FoodType typeIndex = wicNumToFoodType(index);
                    if (!Array.Exists(foodTypes, element => element == typeIndex))
                    {
                        return false;
                    }
                }
                index++;
            }

            return true;
        }

        private int foodTypeToWICNum(FoodType foodType)
        {

            switch (foodType)
            {
                case FoodType.Fruit:
                    return 0;
                case FoodType.Veg:
                    return 1;
                case FoodType.Grain:
                    return 2;
                case FoodType.Protein:
                    return 3;
                case FoodType.Dairy:
                    return 4;
            }

            return -1;
        }

        private FoodType wicNumToFoodType(int num)
        {
            switch (num)
            {
                case 0:
                    return FoodType.Fruit;
                case 1:
                    return FoodType.Veg;
                case 2:
                    return FoodType.Grain;
                case 3:
                    return FoodType.Protein;
                case 4:
                    return FoodType.Dairy;
            }

            return FoodType.None;
        }


        


        public void addTokens(int numTokens)
        {
            player.busTickets += numTokens;
            walletDisplay.updateWallet();
        }

        public void removeToken()
        {
            
            if(player.busTickets > 0)
            {
                player.busTickets--;
                walletDisplay.updateWallet();
            } 
          
        }

    }
}

