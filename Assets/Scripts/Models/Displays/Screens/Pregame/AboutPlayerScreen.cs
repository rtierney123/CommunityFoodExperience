using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class AboutPlayerScreen : Screen
    {
        public Player player;

        public Text fullNameText;
        public Text startingCashText;
        public Text transportationText;
        public Text descriptionText;

        public override void updateDisplay()
        {
            fullNameText.text = player.playerInfo.getFullName();
            double cash = player.playerInfo.getStartingCash();
            startingCashText.text = FormatText.formatCost(cash);
            transportationText.text = player.playerInfo.getStartingTransportationString();
            descriptionText.text = player.playerInfo.description;

        }
    }

}
