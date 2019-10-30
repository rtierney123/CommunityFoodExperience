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
  

        public override void pay()
        {

            base.pay();

            store.completeFundsPayment(cash, eitc, ctc, snap);
         
        }


   
    }
}

