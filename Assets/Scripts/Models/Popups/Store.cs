using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI { 
    public class Store : PopUp
    {

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public CanvasController canvasController;

        public Text cashText;
        public Text eitcText;
        public Text ctcText;
        public Text snapText;

        public Player player;

        private void Start()
        {
            double cash = player.money;
            double eitc = player.eitcFunds;
            double ctc = player.ctcFunds;
            double snap = player.snapFunds;
            cashText.text = player.formatFunds(cash);
            eitcText.text = player.formatFunds(eitc);
            ctcText.text = player.formatFunds(ctc);
            snapText.text = player.formatFunds(snap);
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
            Debug.Log("Voucher");
            openPopUp(voucherPurchase);
        }

        public void openPopUp(GameObject popUp)
        {
            if (canvasController != null)
            {
                canvasController.forcePopupOpen(popUp);
            }
        }


        public void closeScreen()
        {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
            this.gameObject.SetActive(false);
        }

        public void completePayment()
        {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
        }
       

     
    }
}

