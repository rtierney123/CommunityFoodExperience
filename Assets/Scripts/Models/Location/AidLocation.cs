
using Manage;
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
            Debug.Log("enter");
            if (form.checkAlreadyEntered())
            {
                messageManager.generateStandardErrorMessage("Cannot enter this location twice after receiving benefits.");
            }
            else
            {
                Debug.Log("open location");
                StartCoroutine(OpenLocationScreen());
            }

        }

    }
}

