using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Manage
{
    //updates player nutrition and shows ui of update
    public class NutritionManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public MessageManager messageManager;
        public Player player;
        public int changeDisplayTime;

        private bool nutritionAdded = false;


        // Update is called once per frame
        void Update()
        {
            if (nutritionAdded && canvasController.popUp == null && canvasController.screenOpen == null)
            {
               // StartCoroutine(displayAddedNutrition());
            }
        }

        public void addNutrition(Food food)
        {
            player.addNutrition(food);
            nutritionAdded = true;

            if (player.getAchievedNutrition())
            {
                messageManager.generateStandardSuccessMessage(Status.achievedNutrition);
            }
        }
        /*
        public IEnumerator displayAddedNutrition()
        {
            nutritionAdded = false;
            plusSignPopUp.SetActive(true);
            yield return new WaitForSeconds(changeDisplayTime);
            plusSignPopUp.SetActive(false);
        }
        */
    }
}

