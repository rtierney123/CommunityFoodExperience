using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class FoodPantry : BaseStore
    {
        private bool transactionComplete = false;
        public DisableableButton completeButton;
        public GameObject lessThanMaxPopUp;
        public Player player;

        private void OnEnable()
        {
            transactionComplete = false;
        }

        public override void reset()
        {
            completeButton.enable();
        }

        public override bool validateCart()
        {
            if (!transactionComplete)
            {
                if (cart.getCartCount() > 2)
                {
                    messageManager.generateStandardErrorMessage("Cannot have more than 2 items in cart.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
          
        }


        public void checkFreeTransaction()
        {
            if(cart.getCartCount() < 2)
            {
                canvasController.openPopup(lessThanMaxPopUp);
            }
            else
            {
                completeFreeTransaction();
            }
        }

        public void completeFreeTransaction()
        {
            if (validateCart())
            {
                messageManager.generateStandardSuccessMessage(Status.leaveFoodPantry);
                transactionComplete = true;
                completePayment();
                completeButton.disable();
                player.usedFoodPantry = true;

            }
         
        }
    }
}

