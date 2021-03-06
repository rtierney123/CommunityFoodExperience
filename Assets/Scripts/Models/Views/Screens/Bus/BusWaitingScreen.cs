﻿using JetBrains.Annotations;
using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BusWaitingScreen : BusAnimationScreen
    {

        protected override void Update()
        {
            base.Update();
            if (progressBar.getComplete() && this.gameObject.activeInHierarchy)
            {
                if (nextScreen != null)
                {
                    canvasController.openScreen(nextScreen);
                }
                progressBar.resetLoading();
            }
        }

        public override void onAttemptDismiss()
        {
            base.onAttemptDismiss();
            progressBar.pauseLoading();
            messageManager.generateDismissPopup("Are you sure you want to cancel? If you want to take the bus later, you will have to wait again.", this);
            messageDisplayed = true;
        }

        public override void onDismiss()
        {
            base.onDismiss();
            progressBar.resetLoading();
            progressBar.resumeLoading();
            canvasController.closeScreen();
            messageDisplayed = false;
        }

        public override void onCancelDismiss()
        {
            base.onCancelDismiss();
            progressBar.resumeLoading();
            messageDisplayed = false;
        }

    }

}
