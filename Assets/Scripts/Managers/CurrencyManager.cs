using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Utility;

namespace Manage
{
    public class CurrencyManager : MonoBehaviour, IClockEventCaller
    {
        public CanvasController canvasController;
        public MessageManager messageManager;
        public Wallet walletDisplay;
        public Player player;
        public int changeDisplayTime;

        public float randFreeRidePercent = 50;
        public float randCarBreakDownPercent = 20;

        private bool fundsAdded = false;
        private GameObject plusSignPopUp;
        private bool fundsSubtracted = false;
        private GameObject minusSignPopUp;


        // Start is called before the first frame update
        void Start()
        {
            plusSignPopUp = walletDisplay.plusSign;
            minusSignPopUp = walletDisplay.minusSign;
        }

        // Update is called once per frame
        void Update()
        {
            if(canvasController.popUp == null  && !canvasController.screenDisplayed)
            {
                if (fundsAdded && canvasController.popUp == null)
                {
                    StartCoroutine(displayAddedFunds());
                }

                if (fundsSubtracted && canvasController.popUp == null)
                {
                    StartCoroutine(displaySubtractedFunds());
                }
            }

        }

        public IEnumerator displayAddedFunds()
        {
            fundsAdded = false;
            plusSignPopUp.SetActive(true);
            yield return new WaitForSeconds(changeDisplayTime);
            plusSignPopUp.SetActive(false);
        }

        public IEnumerator displaySubtractedFunds()
        {
            fundsSubtracted = false;
            minusSignPopUp.SetActive(true);
            yield return new WaitForSeconds(changeDisplayTime);
            minusSignPopUp.SetActive(false);
        }


        public void addFunds(FundsType type, double amt)
        {
            fundsAdded = true;
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
            fundsSubtracted = true;
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
        }

        public void addWICVoucher()
        {
            fundsAdded = true;
            player.addVoucher();
        }

        public void useVoucher(Food food)
        {
            fundsSubtracted = true;
            player.useVoucher(food);
        }

        public void addTokens(int numTokens)
        {
            fundsAdded = true;
            player.busTokens += numTokens;
            walletDisplay.updateWallet();
        }

        public void removeToken()
        {
            
            if(player.busTokens > 0)
            {
                fundsSubtracted = true;
                player.busTokens--;
                walletDisplay.updateWallet();
            } 
          
        }

        public void hourPassed()
        {
            float rand = Random.Range(0, 100);
            if (rand < randFreeRidePercent && player.playerInfo.hasCar)
            {
                tempDisableCar(60);
            }
        }

        public void minutePassed()
        {
            float rand = Random.Range(0, 100);
            if (rand < randFreeRidePercent && player.hasNoModeOfTransportation() && !player.hasTemporaryRide)
            {
                player.setFreeRide(true);
                messageManager.generateStandardSuccessMessage("'Hey you look like you could use a ride.' (You can take a ride to one location. You lose this ride if you move from this location)");
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
            messageManager.generateStandardErrorMessage("Oh no! Your car broke down.  You can travel by car until it is fixed.");
            yield return new WaitForSeconds(sec);
            player.playerInfo.hasCar = true;
            messageManager.generateStandardSuccessMessage("Your car is fixed. You can take the car again.");
        }



    }
}

