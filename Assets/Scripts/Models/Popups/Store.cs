using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI { 
    public class Store : PopUp
    {

        public GameObject purchaseOptions;
        public GameObject fundsPurchase;
        public GameObject voucherPurchase;

        public void openPurchaseOptions()
        {
            purchaseOptions.SetActive(true);

        }

        public void openFundsPurchase()
        {
            purchaseOptions.SetActive(false);
            fundsPurchase.SetActive(true);
        }

        public void openVoucherPurchase()
        {
            purchaseOptions.SetActive(false);
            voucherPurchase.SetActive(true);
        }

        public void closeAll()
        {
            purchaseOptions.SetActive(false);
            fundsPurchase.SetActive(false);
            voucherPurchase.SetActive(false);
        }
    }
}

