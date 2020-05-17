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
            if (currencyManager.getHasTickets())
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
            if (currencyManager.getHasBusPass())
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
            if(currencyManager.getHasTickets())
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
            if (currencyManager.getHasBusPass())
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
            canvasController.addToPopUpBackLog(this.gameObject);
            canvasController.forcePopupOpen(purchasePopup);
        }
    }
}

