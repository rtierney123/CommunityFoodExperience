using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage
{
    public class CurrencyManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public Wallet walletDisplay;
        public Player player;
        public int changeDisplayTime;

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

        public void addWICVoucher(WICVoucher voucher)
        {
            fundsAdded = true;
            player.addVoucher();
        }

        public void useVoucher(FoodCard food)
        {
            fundsSubtracted = true;
            player.useVoucher(food);
        }
    }
}

