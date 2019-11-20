using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateShelf : PopulateGrid
{
    public GameObject foodCardHolder;

    public override FoodCard addItem(Food food)
    {
        GameObject holder = Instantiate<GameObject>(foodCardHolder, transform);

        FoodCard foodCard = Instantiate<FoodCard>(cardPrefab, holder.transform);
        foodCard.setFood(food);
        foodCard.setCart(cart);
        foodCard.canvasController = canvasController;


        return foodCard;
    }
}
