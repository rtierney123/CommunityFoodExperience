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
        public Animator busAnimator;
        public GameObject purchasePopup;
        public DisableableButton useTicketButton;
        public DisableableButton useBusPassButton;

        private void OnEnable()
        {
            busAnimator.enabled = false;
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

        private void OnDisable()
        {
            if(busAnimator != null)
            {
                busAnimator.enabled = true;
                Debug.Log("bus continue");
            }
      
        }
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
            if (player.playerInfo.busPass)
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

