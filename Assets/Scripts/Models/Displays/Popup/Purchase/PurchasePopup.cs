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
        public InputField snapValue;


        protected double cash;
        protected double snap;

        public override void onDismiss()
        {
            base.onDismiss();
            resetBoxes();
        }
        public virtual void resetBoxes()
        {

            string resetValue = "";

            if (cashValue != null)
            {
                cash = string.IsNullOrEmpty(cashValue.text) ? 0 : Convert.ToDouble(cashValue.text);
                cashValue.text = resetValue;
            }
            if (snapValue != null)
            {
                snap = string.IsNullOrEmpty(snapValue.text) ? 0 : Convert.ToDouble(snapValue.text);
                snapValue.text = resetValue;
            }

        }
    }

}

