
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

        protected string reenterAfterReceive;
        protected string reenterAfterDeny;

        private void Start()
        {
            string alreadyEnteredString = String.Format(Status.enterAgain, locationTitle);
            reenterAfterReceive = alreadyEnteredString;
            reenterAfterDeny = alreadyEnteredString;
        }

        public override void onDelayedEnter()
        {
            if (form.checkAlreadyEntered())
            {
                if (form.checkValid())
                {
                    messageManager.generateStandardErrorMessage(reenterAfterReceive);
                }
                else
                {
                    messageManager.generateStandardErrorMessage(reenterAfterDeny);
                }
               
            } else if (!form.checkCanEnter())
            {
                messageManager.generateStandardErrorMessage(form.cannotEnterStr);
            }
            else
            {
                canvasController.delayOpenScreen(mainScreen);
            }

        }

        public override void onImmediateEnter()
        {
            if (form.checkAlreadyEntered())
            {
                if (form.checkValid())
                {
                    messageManager.generateStandardErrorMessage(reenterAfterReceive);
                }
                else
                {
                    messageManager.generateStandardErrorMessage(reenterAfterDeny);
                }

            }
            else if (!form.checkCanEnter())
            {
                messageManager.generateStandardErrorMessage(form.cannotEnterStr);
            }
            else
            {
                canvasController.openScreen(mainScreen);
            }
        }




    }
}

