using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HomePopup : PopUp
    {
        public Text homeText;
        public Player player;

        public override void reset()
        {
            base.reset();
            if (player.getAchievedNutrition())
            {
                homeText.text = Status.homeSufficientFood;
            }
            else
            {
                homeText.text = Status.homeInsufficientFood;
            }
        }

    }

}
