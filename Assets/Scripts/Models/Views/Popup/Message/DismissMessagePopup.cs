using Manage;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class DismissMessagePopup : MessagePopup
    {
        private View dismissedDisplay;

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
