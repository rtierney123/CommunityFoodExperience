using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseTokenPopup : MonoBehaviour
{
    public CanvasController canvasController;
    public CurrencyManager currencyManager;
    public MessageManager messageManager;

    public void purchaseTokens(int num)
    {
        canvasController.addToPopUpBackLog(this.gameObject);
        double amt = num * 2.5;
        if (currencyManager.validateCashPayment(amt))
        {
            currencyManager.subtractFunds(FundsType.Cash, amt);
            currencyManager.addTokens(num);
            messageManager.generateStandardSuccessMessage("Payment successful");
        }
        else
        {
            messageManager.generateStandardErrorMessage("Not enough cash.");
        }
    }
}
