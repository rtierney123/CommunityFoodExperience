using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [System.Serializable]
    public class NutritionDisplayer
    {
        public Text caloriesText;
        public Text grainText;
        public Text fatText;
        public Text proteinText;
        public Text dairyText;
        public Text fruitText;
        public Text vegText;
        public Text extraText;
        public Text healthStatusText;


        public NutritionDisplayer(Text calories, Text grain, Text fat, Text protein, Text dairy, Text fruit, Text veg, Text extra, Text healthStatus)
        {
            caloriesText = calories;
            grainText = grain;
            fatText = fat;
            proteinText = protein;
            dairyText = dairy;
            fruitText = fruit;
            vegText = veg;
            extraText = extra;
            healthStatusText = healthStatus;
        }

        public void updateDisplay(Player player)
        {
            caloriesText.text = player.getCaloriesStatus();
            grainText.text = player.getGrainStatus();
            fatText.text = player.getFatStatus();
            proteinText.text = player.getProteinStatus();
            dairyText.text = player.getDairyStatus();
            fruitText.text = player.getFruitStatus();
            vegText.text = player.getVegetableStatus();
            extraText.text = player.getExtraStatus();

            displaySuccess(caloriesText, player.getAchieveCalories());
            displaySuccess(grainText, player.getAchieveGrain());
            displaySuccess(fatText, player.getAchieveFat());
            displaySuccess(proteinText, player.getAchieveProtein());
            displaySuccess(dairyText, player.getAchieveDairy());
            displaySuccess(fruitText, player.getAchieveFruit());
            displaySuccess(vegText, player.getAchieveVegetable());
            displaySuccess(extraText, player.getAchieveExtra());

            if(healthStatusText != null)
            {
                healthStatusText.text = player.playerInfo.healthState;
            }
           
        }

        private void displaySuccess(Text text, bool success)
        {
            ColorDisplayer colorDisplayer = new ColorDisplayer();
            if (success)
            {
                colorDisplayer.setSuccess(text);
            }
            else
            {
                colorDisplayer.setStandard(text);
            }
        }


    }

}
