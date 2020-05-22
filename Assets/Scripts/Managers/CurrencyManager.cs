using System.Collections;
using System.Collections.Generic;
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

        public void useVoucher(Dictionary<Food, int> cart)
        {
            foreach(Food food in cart.Keys)
            {
                int count = cart[food];
                for(int i = 0; i < count; i++)
                {
                    player.updateVoucher(food);
                }
            }
            player.voucher.deactivate();
            footerDisplay.updateView();
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


        public void tempDisableCar(float sec)
        {
            if (player.playerInfo.hasCar)
            {
                StartCoroutine(startDisable(sec));
            }
         
        }

        private IEnumerator startDisable(float sec)
        {
            player.playerInfo.hasCar = false;
            messageManager.generateMainScreenOnlyErrorMessage("Oh no! Your car broke down.  You cannot travel by car until it is fixed.");
            yield return new WaitForSeconds(sec);
            player.playerInfo.hasCar = true;
            messageManager.generateMainScreenOnlySuccessMessage("Your car is fixed. You can take the car again.");
        }


    }
}

