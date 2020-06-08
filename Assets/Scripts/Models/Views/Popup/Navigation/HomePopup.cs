using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HomePopup : MessagePopup
    {
        public Player player;

        public override void reset()
        {
            base.reset();
            if (player.getAchievedNutrition())
            {
                setText(Status.homeSufficientFood);
            }
            else
            {
                setText(Status.homeInsufficientFood);
            }
        }

    }

}
