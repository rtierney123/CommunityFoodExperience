
using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Model
{
    public class AidLocation : Location
    {
        public Form form;
        public MessageManager messageManager;

        public override void onEnter()
        {
            if (form.checkAlreadyEntered())
            {
                string alreadyEnteredString = String.Format(Status.enterAgain, locationTitle);
                messageManager.generateStandardErrorMessage(alreadyEnteredString);
            }
            else
            {
                StartCoroutine(OpenLocationScreen());
            }

        }

    }
}

