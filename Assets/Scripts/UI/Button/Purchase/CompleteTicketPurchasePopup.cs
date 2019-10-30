using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class CompleteTicketPurchasePopup : PurchasePopup
    {
        public CurrencyManager currencyManager;
        public MessageManager messageManager;
        public Player player;
        public override void pay()
        {
            base.pay();
          
        }

        public bool validatePayment()
        {
            if (player.money < cash)
            {
                messageManager.generateStandardErrorMessage("Not enough cash.");
                return false;
            }
            else if (player.ctcFunds < ctc)
            {
                messageManager.generateStandardErrorMessage("Not enough CTC fund.");
                return false;

            }
            else if (player.eitcFunds < eitc)
            {
                messageManager.generateStandardErrorMessage("Not enough EITC fund.");
                return false;
            }
            else if (player.snapFunds < snap)
            {
                messageManager.generateStandardErrorMessage("Not enough SNAP fund.");
                return false;
            }
            else if (roundTwoDecimal(cash + ctc + eitc + snap) != roundTwoDecimal(0))
            {
                messageManager.generateStandardErrorMessage("Total amount does not match");
                return false;
            }
            else
            {
                return true;
            }
        }

        private double roundTwoDecimal(double num)
        {
            return Math.Round(num * 100) / 100;
        }
    }
}

