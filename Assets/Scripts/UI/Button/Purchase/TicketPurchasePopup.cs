using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
namespace UI
{
    public class TicketPurchasePopup : PurchasePopup
    {
        public Text numTicketText;
        public Text costTotalText1;
        public Text costTotalText2;
        public double ticketCost;
        public GameObject farePopup;
        public GameObject chooseTicketDisplay;
        public GameObject paymentDisplay;
        public CanvasController canvasController;
        private int numTickets;

        public CurrencyManager currencyManager;
        public MessageManager messageManager;

        private void OnEnable()
        {
            openChooseTickeDisplay();
        }

        public override void pay()
        {
            canvasController.addToPopUpBackLog(this.gameObject);
            base.pay();
            if (currencyManager.validatePayment(cash, ctc, eitc, 0, ticketCost*numTickets))
            {
                currencyManager.subtractFunds(FundsType.Cash, cash);
                currencyManager.subtractFunds(FundsType.CTC, ctc);
                currencyManager.subtractFunds(FundsType.EITC, eitc);
                messageManager.generateStandardSuccessMessage("Payment successful");
            } 
        }

        public void addTicket()
        {
            if(numTickets < 100)
            {
                numTickets++;
                updateTicket();
            }
        }

        public void removeTicket()
        {
            if(numTickets > 0)
            {
                numTickets--;
                updateTicket();
            }
        }

        public void openPaymentDisplay()
        {
            chooseTicketDisplay.SetActive(false);
            paymentDisplay.SetActive(true);
        }

        public void openChooseTickeDisplay()
        {
            chooseTicketDisplay.SetActive(true);
            paymentDisplay.SetActive(false);
        }

        public void cancel()
        {
            canvasController.forcePopupOpen(farePopup);
        }

        private void updateTicket()
        {
            numTicketText.text = FormatText.formatInt(numTickets);
            double totalCost = numTickets * ticketCost;
            costTotalText1.text = FormatText.formatCost(totalCost);
            costTotalText2.text = FormatText.formatCost(totalCost);
        }

    }

}
