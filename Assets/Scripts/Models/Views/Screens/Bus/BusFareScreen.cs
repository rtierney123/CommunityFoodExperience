using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace UI
{
    public class BusFareScreen : Screen
    {
        public CurrencyManager currencyManager;
        public NavigationManager navigationManager;

        public DisableableButton useTicketButton;
        public DisableableButton useBusPassButton;

        private void OnEnable()
        {
            checkTickets();
            checkPass();
            canvasController.disablePopups();
        }

        public override void onAttemptDismiss()
        {
            base.onAttemptDismiss();
            messageManager.generateDismissPopup("Are you sure you want to cancel? If you do, the bus will leave without you, and you will have to wait for it again.", this);
        }

        public override void onDismiss()
        {
            base.onDismiss();
            navigationManager.handleStartBusEvent();
            canvasController.enablePopups();
            canvasController.closeCurrentScreen();
        }

        public override void updateView()
        {
            base.updateView();
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
            if (currencyManager.getHasTickets())
            {
                currencyManager.removeToken();
                navigationManager.handleChooseStopEvent();
                canvasController.closeScreen();
            }

        }

        public void usePass()
        {
            if (currencyManager.getHasBusPass())
            {
                navigationManager.handleChooseStopEvent();
                canvasController.closeScreen();
            }

        }

        public void purchaseTokens(int num)
        {
            double amt = num * 2.5;
            if (currencyManager.validateCashPayment(amt))
            {
                currencyManager.subtractFunds(FundsType.Cash, amt);
                currencyManager.addTokens(num);

                string paymentString;
                if (num == 1)
                {
                    paymentString = String.Format(Status.purchaseSingleTicket, num);
                    messageManager.generateStandardSuccessMessage(paymentString, this);
                }
                else
                {
                    paymentString = String.Format(Status.purchaseMultipleTickets, num);
                    messageManager.generateStandardSuccessMessage(paymentString, this);
                }
                
            }
            else
            {
                messageManager.generateStandardErrorMessage("Not enough cash.", this);
            }
        }
    }
}

