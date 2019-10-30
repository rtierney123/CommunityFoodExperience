using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BusFarePopup : PopUp
    {
        public CanvasController canvasController;
        public CurrencyManager currencyManager;
        public NavigationManager navigationManager;
        public Player player;
        public GameObject purchasePopup;

        public void useToken()
        {
            currencyManager.removeToken();
            navigationManager.handleTakeBusEvent();
        }

        public void usePass()
        {
            navigationManager.handleTakeBusEvent();
        }
        
        public void goToPurchaseToken()
        {
            canvasController.forcePopupOpen(purchasePopup);
        }
    }
}

