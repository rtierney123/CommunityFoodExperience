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
            if(canvasController.popUp == null  && !canvasController.screenDisplayed)
            {

            }

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
                    break;
                case FundsType.CTC:
                    player.addCTC(amt);
                    break;
            }
            walletDisplay.updateWallet();
            footerDisplay.update();
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

        public bool getHasWIC()
        {
            return player.hasWic;
        }

        public bool getHasTickets()
        {
            return player.busTickets > 0;
        }

        public WICVoucher getWICVoucher()
        {
            return player.wicVoicher;
        }

        public bool validateCashPayment(double amt)
        {
            return amt < player.money;
        }

        public bool validateSNAPPayment(double amt)
        {
            return amt < player.snapFunds;
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
            footerDisplay.update();
        }

        public void addWICVoucher()
        {
            player.addVoucher();
        }

        public void useVoucher(Food food)
        {
            player.useVoucher(food);
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

