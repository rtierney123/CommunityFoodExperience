﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FoodPantryForm : Form
    {
        public GameObject enterButton;

        public override void reset()
        {
            base.reset();
            enterButton.SetActive(false);
        }

        public override bool checkAlreadyEntered()
        {
            return player.usedFoodPantry;
        }

        public override bool checkValid()
        {
            if (playerInfo.zip == "30317" || playerInfo.zip == "30307")
            {
                return true;
            }  else
            {
                return false;
            }
        }

        protected override uint getProcessTimeInMinutes()
        {
            return 120;
        }

        protected override void successAction()
        {
            signButton.gameObject.SetActive(false);
            enterButton.SetActive(true);
            messageManager.generateStandardSuccessMessage(Status.enterFoodPantry, this);
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage(Status.deniedFoodPantry, this);
        }

    }
}

