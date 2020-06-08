using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Manage;
using System;

namespace UI
{
    public class CompletePurchaseCash : PurchasePopup
    {
        public Store store;
        public Text totalText;
        public void OnEnable()
        {
            store.setCheckoutText(cashValue, snapValue, totalText);
        }

        public void pay()
        {

            base.resetBoxes();


            store.completeFundsPayment(cash, snap);

        }


   
    }
}

