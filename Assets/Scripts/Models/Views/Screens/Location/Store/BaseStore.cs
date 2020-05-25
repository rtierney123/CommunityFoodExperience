using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseStore : BaseLocationScreen
    {

        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public Cart cart;

        private void OnDisable()
        {
            cart.clearAll();
        }

        public virtual bool validateCart()
        {
            return true;
        }

        public virtual void completePayment()
        {
            foreach (KeyValuePair<Food, int> item in cart.foodInCart)
            {
                for(int i = 0; i < item.Value; i++)
                {
                    nutritionManager.addNutrition(item.Key);
                }
      
            }
            cart.clearAll();
        }

    }
}

