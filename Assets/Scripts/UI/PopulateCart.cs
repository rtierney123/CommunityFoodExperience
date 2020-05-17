using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateCart : PopulateGrid
{
    public override FoodCard addItem(Food food)
    {
        FoodCard foodCard = base.addItem(food);
        foodCard.inCart = true;
        return foodCard;
    }
}
