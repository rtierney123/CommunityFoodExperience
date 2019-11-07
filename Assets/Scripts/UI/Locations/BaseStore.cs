﻿using Manage;
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

