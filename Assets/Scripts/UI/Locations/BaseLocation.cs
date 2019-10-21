using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseLocation : MonoBehaviour
    {

        public Player player;
        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public CanvasController canvasController;
        public ErrorManager errorManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void closeScreen()
        {

            closePopUps();
            canvasController.closeScreen(this.gameObject);
        }

        public void closePopUps()
        {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
        }
    }
}

