using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CommunityKitchenStore : BaseStore
    {
        private bool transactionComplete = false;
        public override bool validateCart()
        {
            if (!transactionComplete)
            {

                if (cart.getCartCount() > 1)
                {
                    messageManager.generateStandardErrorMessage("Cannot have more than 1 bowl of soup.");
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                messageManager.generateStandardErrorMessage("Complete a transaction here twice.");
                return false;
            }

        }

        public void completeFreeTransaction()
        {
            if (validateCart())
            {
                messageManager.generateStandardSuccessMessage("Transaction complete.");
                transactionComplete = true;
                completePayment();
                StartCoroutine(delayCloseScreen());
            }

        }
    }
}