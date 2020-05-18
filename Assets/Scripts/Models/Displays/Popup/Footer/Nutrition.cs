using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Nutrition : PopUp
    {
        public GameObject myPlateTab;
        public GameObject myPlateTabBody;
        public GameObject healthStatusTab;
        public GameObject healthStatusTabBody;

        public Player player;
        public Color standardColor;
        public Color successColor;

        public Text caloriesText;
        public Text grainText;
        public Text fatText;
        public Text proteinText;
        public Text dairyText;
        public Text fruitText;
        public Text vegText;
        public Text extraText;
        public Text healthStatusText;

        private Color selectColor;
        private Color inactiveColor;

        public GameObject plusSignPopup;
        // Start is called before the first frame update

        protected override void Start()
        {
            base.Start();
            selectColor = new Color32(192, 118, 5, 255);
            inactiveColor = new Color32(255, 150, 0, 255);
            setAllInactive();
            updateInfo();
            selectMyPlate();

        }

        private void OnEnable()
        {
            updateInfo();
            updateHealthStatus();
        }

        private void setAllInactive()
        {
            myPlateTab.GetComponent<Image>().color = inactiveColor;
            healthStatusTab.GetComponent<Image>().color = inactiveColor;
            myPlateTabBody.SetActive(false);
            healthStatusTabBody.SetActive(false);
        }

        public void selectMyPlate()
        {
            setAllInactive();
            myPlateTab.GetComponent<Image>().color = selectColor;
            myPlateTabBody.SetActive(true);
        }

        public void selectHealthStatus()
        {
            setAllInactive();
            healthStatusTab.GetComponent<Image>().color = selectColor;
            healthStatusTabBody.SetActive(true);

        }

        public void updateInfo()
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
        }

        public void updateHealthStatus()
        {
            //TODO: set actual health status once added to JSON and player
            healthStatusText.text = "Your community member does not have a heath condition.";
        }

        private void displaySuccess(Text text, bool success)
        {
            if (success)
            {
                text.color = successColor;
            }
            else
            {
                text.color = standardColor;
            }
        }

    }
}
