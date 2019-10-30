using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;
namespace UI
{
    public class TicketPurchasePopup : PopUp
    {
        public Text numTicketText;
        public Text costTotalText;
        public double ticketCost;
        public GameObject paymentPopup;
        public GameObject farePopup;
        public CanvasController canvasController;
        private int numTickets;
        
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

        public void openPaymentPopup()
        {
            canvasController.forcePopupOpen(paymentPopup);
        }

        public void cancel()
        {
            canvasController.forcePopupOpen(farePopup);
        }

        private void updateTicket()
        {
            numTicketText.text = FormatText.formatInt(numTickets);
            double totalCost = numTickets * ticketCost;
            costTotalText.text = FormatText.formatCost(totalCost);
        }


    }

}
