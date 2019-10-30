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
  

        private void updateTicket()
        {
            numTicketText.text = FormatText.formatInt(numTickets);
            double totalCost = numTickets * ticketCost;
            costTotalText.text = FormatText.formatCost(totalCost);
        }

    }

}
