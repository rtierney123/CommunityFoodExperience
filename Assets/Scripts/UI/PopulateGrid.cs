using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

public class PopulateGrid : MonoBehaviour
{
    public CanvasController canvasController;
    public Cart cart;

    public FoodCard cardPrefab;

    public virtual FoodCard addItem(Food food)
    {
        FoodCard foodCard = Instantiate<FoodCard>(cardPrefab, transform);
        Debug.Log(cardPrefab.transform.localScale.x);
        Debug.Log(foodCard.transform.localScale.x);
        foodCard.setFood(food);
        foodCard.setCart(cart);
        foodCard.canvasController = canvasController;
        return foodCard;
    }


    public void removeItem(GameObject obj)
    {
        Destroy(obj);
    }

    public void clearAll()
    {
        foreach (Transform child in this.transform)
        {
            removeItem(child.gameObject);
        }
    }

}
