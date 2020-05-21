using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FoodPantryForm : Form
    {
       
        public override bool checkAlreadyEntered()
        {
            return player.usedFoodPantry;
        }

        protected override bool checkValid()
        {
            Debug.Log(playerInfo.zip);
            if (playerInfo.zip == "30317" || playerInfo.zip == "30307")
            {
                Debug.Log("correct zip");
                return true;
            }  else
            {
                Debug.Log("incorrect zip");
                return false;
            }
        }

        protected override void successAction()
        {
            messageManager.generateStandardSuccessMessage("Welcome to the Food Pantry", this);
            StartCoroutine(delayOpenNextScreen(nextActionTime));
            player.usedFoodPantry = true;
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage("Must be in 30317 or 30307 area code.", this);
            StartCoroutine(delayCloseScreen(nextActionTime));
            player.usedFoodPantry = true;
        }

    }
}

