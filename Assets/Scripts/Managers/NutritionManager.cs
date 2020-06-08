using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Manage
{
    //updates player nutrition and shows ui of update
    public class NutritionManager : Manager
    {
        public CanvasController canvasController;
        public MessageManager messageManager;
        public Player player;

        private bool achievedNutritionDisplayed = false;

        public override void reset()
        {
            achievedNutritionDisplayed = false;
        }

        public void addNutrition(Food food)
        {
            player.addNutrition(food);

            if (player.getAchievedNutrition() && !achievedNutritionDisplayed)
            {
                messageManager.generateMainScreenOnlySuccessMessage(Status.achievedNutrition);
                achievedNutritionDisplayed = true;
                Debug.Log("nutrition complete displayed");
            }
        }
    }
}

