using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseStore : Screen
    {

        public Player player;
        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public Cart cart;

        public virtual bool validateCart()
        {
            return true;
        }

        public virtual void completePayment()
        {
            foreach (Food item in cart.foodInCart)
            {
                nutritionManager.addNutrition(item);
            }
            cart.clearAll();
        }

    }
}

