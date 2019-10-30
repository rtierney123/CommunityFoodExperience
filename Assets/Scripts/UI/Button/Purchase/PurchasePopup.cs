using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PurchasePopup : PopUp
    {
        public InputField cashValue;
        public InputField ctcValue;
        public InputField eitcValue;
        public InputField snapValue;


        protected double cash;
        protected double ctc;
        protected double eitc;
        protected double snap;

        public virtual void pay()
        {

            cash = string.IsNullOrEmpty(cashValue.text) ? 0 : Convert.ToDouble(cashValue.text);
            ctc = string.IsNullOrEmpty(ctcValue.text) ? 0 : Convert.ToDouble(ctcValue.text);
            eitc = string.IsNullOrEmpty(eitcValue.text) ? 0 : Convert.ToDouble(eitcValue.text);
            snap = string.IsNullOrEmpty(snapValue.text) ? 0 : Convert.ToDouble(snapValue.text);


            string resetValue = "";
            cashValue.text = resetValue;
            eitcValue.text = resetValue;
            ctcValue.text = resetValue;
            snapValue.text = resetValue;

        }
    }
}

