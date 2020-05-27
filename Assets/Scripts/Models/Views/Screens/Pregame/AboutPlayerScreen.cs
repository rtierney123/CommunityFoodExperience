using System;
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

        public Text descriptionText;

        public override void reset()
        {
            PlayerInfo playerinfo = player.playerInfo;
            string name = String.Format("You are {0}.", playerinfo.getFullName());
            string description = playerinfo.description;
            string transportation = playerinfo.getStartingTransportationString();
            string total = String.Format("  {0} {1} \n\n  {2}", name, description, transportation);
            descriptionText.text = total;
        }
    }

}
