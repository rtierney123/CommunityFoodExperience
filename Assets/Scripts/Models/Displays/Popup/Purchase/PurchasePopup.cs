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

        public virtual void resetBoxes()
        {

            string resetValue = "";

            if (cashValue != null)
            {
                cash = string.IsNullOrEmpty(cashValue.text) ? 0 : Convert.ToDouble(cashValue.text);
                cashValue.text = resetValue;
            }
            if (eitcValue != null)
            {
                eitc = string.IsNullOrEmpty(eitcValue.text) ? 0 : Convert.ToDouble(eitcValue.text);
                eitcValue.text = resetValue;
            }
            if (ctcValue != null)
            {
                ctc = string.IsNullOrEmpty(ctcValue.text) ? 0 : Convert.ToDouble(ctcValue.text);
                ctcValue.text = resetValue;
            }
            if (snapValue != null)
            {
                snap = string.IsNullOrEmpty(snapValue.text) ? 0 : Convert.ToDouble(snapValue.text);
                snapValue.text = resetValue;
            }

        }
    }

}

