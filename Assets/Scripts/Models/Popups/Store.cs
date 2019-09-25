using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class Store : PopUp
    {

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public CanvasController canvasController;


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

