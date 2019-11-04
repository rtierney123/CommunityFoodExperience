﻿using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BusFarePopup : PopUp
    {
        public CanvasController canvasController;
        public CurrencyManager currencyManager;
        public MessageManager messageManager;
        public NavigationManager navigationManager;
        public Player player;
        public GameObject purchasePopup;

        public void useToken()
        {
            if(player.busTokens > 0)
            {
                currencyManager.removeToken();
                navigationManager.handleTakeBusEvent();
            } else
            {
                canvasController.addToPopUpBackLog(this.gameObject);
                messageManager.generateStandardErrorMessage("You do not possess any tickets.");
            }
     
        }

        public void usePass()
        {
            if (player.busPass)
            {
                navigationManager.handleTakeBusEvent();
            } else
            {
                canvasController.addToPopUpBackLog(this.gameObject);
                messageManager.generateStandardErrorMessage("You do not possess any bus pass.");
            }
           
        }
        
        public void goToPurchaseToken()
        {
            canvasController.forcePopupOpen(purchasePopup);
        }
    }
}

