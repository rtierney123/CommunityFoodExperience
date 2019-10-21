using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace UI
{
    public class FoodPantry : AidLocation
    {
        protected override bool checkAllowed()
        {
            if (!player.usedFoodPantry)
            {
                return true;
            } else
            {
                return false;
            }
        }

        protected override string notAllowedMessage()
        {
            return "Cannot enter Food Pantry twice in one day.";
        }
    }
}

