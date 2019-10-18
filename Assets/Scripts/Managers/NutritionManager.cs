using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage
{
    //updates player nutrition and shows ui of update
    public class NutritionManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public Nutrition nutritionDisplay;
        public Player player;
        public int changeDisplayTime;

        private bool nutritionAdded = false;
        private GameObject plusSignPopUp;

        // Start is called before the first frame update
        void Start()
        {
            plusSignPopUp = nutritionDisplay.plusSignPopup;
        }

        // Update is called once per frame
        void Update()
        {
            if (nutritionAdded && canvasController.popUp == null && !canvasController.screenDisplayed)
            {
                StartCoroutine(displayAddedNutrition());
            }
        }

        public void addNutrition(FoodCard food)
        {
            player.addNutrition(food);
            nutritionAdded = true;
            nutritionDisplay.updateInfo();
        }

        public IEnumerator displayAddedNutrition()
        {
            nutritionAdded = false;
            plusSignPopUp.SetActive(true);
            yield return new WaitForSeconds(changeDisplayTime);
            plusSignPopUp.SetActive(false);
        }
    }
}

