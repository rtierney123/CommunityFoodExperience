using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseLocation : Screen
    {

        public Player player;
        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public MessageManager messageManager;
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

    }
}

