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
        public MessageManager messageManager;
        public NavigationManager navigationManager;
        public Player player;
        public Bus bus;
        public DisableableButton useTicketButton;
        public DisableableButton useBusPassButton;
        public GameObject purchasePopup;

        private void OnEnable()
        {
            checkTickets();
            checkPass();
        }

        private void checkTickets()
        {
            if (player.busTokens > 0)
            {
                useTicketButton.enable();
            }
            else
            {
                useTicketButton.disable();
            }
        }

        private void checkPass()
        {
            if (player.playerInfo.busPass)
            {
                useBusPassButton.enable();
            }
            else
            {
                useBusPassButton.disable();
            }
        }


        public void useToken()
        {
            if(player.busTokens > 0)
            {
                currencyManager.removeToken();
                navigationManager.handleTakeBusEvent();
                canvasController.closePopUp();
            } else
            {
                canvasController.addToPopUpBackLog(this.gameObject);
                messageManager.generateStandardErrorMessage("You do not possess any tickets.");
            }
     
        }

        public void usePass()
        {
            if (player.playerInfo.busPass)
            {
                navigationManager.handleTakeBusEvent();
                canvasController.closePopUp();
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

