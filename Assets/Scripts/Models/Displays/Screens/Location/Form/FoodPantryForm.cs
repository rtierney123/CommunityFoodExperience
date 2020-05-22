using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FoodPantryForm : Form
    {
        public GameObject enterButton;

        private void OnEnable()
        {
            greetingLayout.SetActive(true);
            formLayout.SetActive(false);
            signButton.gameObject.SetActive(false);
            enterButton.SetActive(false);
        }

        public override bool checkAlreadyEntered()
        {
            return player.usedFoodPantry;
        }

        public override bool checkValid()
        {
            Debug.Log(playerInfo.zip);
            if (playerInfo.zip == "30317" || playerInfo.zip == "30307")
            {
                return true;
            }  else
            {
                return false;
            }
        }

        protected override void successAction()
        {
            signButton.gameObject.SetActive(false);
            enterButton.SetActive(true);
            player.usedFoodPantry = true;
            messageManager.generateStandardSuccessMessage(Status.enterFoodPantry, this);
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage(Status.deniedFoodPantry, this);
            player.usedFoodPantry = true;
        }

    }
}

