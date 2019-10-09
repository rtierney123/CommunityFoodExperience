using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Manage;
using System;

namespace UI
{
    public class CompletePurchaseCash : PopUp
    {
        public Store store;
        public InputField cashValue;
        public InputField ctcValue;
        public InputField eitcValue;
        public InputField snapValue;
        

        double cash;
        double ctc;
        double eitc;
        double snap;

        public void Start()
        {
           
        }

        public void pay()
        {
            try
            {
                cash = string.IsNullOrEmpty(cashValue.text) ? 0 : Convert.ToDouble(cashValue.text);
                ctc = string.IsNullOrEmpty(ctcValue.text) ? 0 : Convert.ToDouble(ctcValue.text);
                eitc = string.IsNullOrEmpty(eitcValue.text) ? 0 : Convert.ToDouble(eitcValue.text);
                snap = string.IsNullOrEmpty(snapValue.text) ? 0 : Convert.ToDouble(snapValue.text);

                store.completeFundsPayment(cash, eitc, ctc, snap);

                string resetValue = "";
                cashValue.text = resetValue;
                eitcValue.text = resetValue;
                ctcValue.text = resetValue;
                snapValue.text = resetValue;
            }
            catch (FormatException)
            {
                store.displayMustBeNumberError();
            }
   
        }


   
    }
}

