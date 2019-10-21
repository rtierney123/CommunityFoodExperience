using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FoodPantryForm : Form
    {

        protected override bool checkValid()
        {
            Debug.Log(player.zip);
            if (player.zip == "30317" || player.zip == "30307")
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
            messageManager.generateStandardSuccessMessage("Welcome to the Food Pantry");
            StartCoroutine(delayOpenNextScreen());
        }

        protected override void failureAction()
        {
            messageManager.generateStandardErrorMessage("Must be in 30317 or 30307 area code.");
        }

    }
}

