using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Manage;

namespace UI
{
    public class EndScreen : Screen
    {
        public Player player;
        public NutritionDisplayer nutritionDisplayer;

        public Text makeHomeText;
        public override void reset()
        {
            base.reset();
            Debug.Log("reset end game");
            nutritionDisplayer.updateDisplay(player);
            ColorDisplayer colorDisplayer = new ColorDisplayer();
            if (player.getIsHome())
            {
                makeHomeText.text = "Yes";
                colorDisplayer.setSuccess(makeHomeText);
            }
            else
            {
                makeHomeText.text = "No";
                colorDisplayer.setStandard(makeHomeText);
            }
        }

    }
}

