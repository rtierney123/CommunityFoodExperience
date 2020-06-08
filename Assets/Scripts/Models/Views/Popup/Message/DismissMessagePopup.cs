using Manage;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DismissMessagePopup : PopUp
    {
        public Text text;
        private View dismissedDisplay;

        public void setText(string str)
        {
            text.text = str;
        }
        public void setDismissed(View display)
        {
            dismissedDisplay = display;
        }
        public void onDismissedView()
        {
            dismissedDisplay.onDismiss();
            canvasController.closePopUp();
        }

        public void onCancelDismissView()
        {
            dismissedDisplay.onCancelDismiss();
            canvasController.closePopUp();
        }
    }

}
