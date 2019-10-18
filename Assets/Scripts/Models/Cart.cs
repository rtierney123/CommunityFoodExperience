using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;

public class Cart : MonoBehaviour
{
    public RectTransform cartTransform;
    public PopulateGrid populateGrid;
    public Text totalText;
    [HideInInspector]
    public HashSet<FoodCard> foodInCart;

    
    private double totalPrice;
  
    public void Start()
    {
        foodInCart = new HashSet<FoodCard>();
    }
    public void Update()
    {
     
    }

    public void notifyDroppedFood(Vector3 position, FoodCard food)
    {
        if (inCart(position))
        {
            addItem(food);

        }
    }

    public void clearAll()
    {
        foodInCart.Clear();
        populateGrid.clearAll();
        totalPrice = 0;
        updateTotal();
    }


    private bool inCart(Vector3 position)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(cartTransform,
            position))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private void addItem(FoodCard food) {
        totalPrice += food.cost;
        totalPrice = Math.Round(totalPrice * 100) / 100;

         updateTotal();

        GameObject icon = populateGrid.addCartItem(food.cartObject);
        foodInCart.Add(food);

        Transform minusObject = icon.transform.GetChild(1);
        minusObject.gameObject.SetActive(true);
        Button minusButton = minusObject.GetComponent<Button>();

        minusButton.onClick.AddListener(() => removeItemFromCart(food, icon));

        
    }

    private void removeItemFromCart(FoodCard food, GameObject icon)
    {
        totalPrice -= food.cost;
        totalPrice = Math.Round(totalPrice * 100) / 100;

        updateTotal();

        populateGrid.removeItem(icon);
        foodInCart.Remove(food);
        Debug.Log("item removed");
    }

    private void updateTotal()
    {
        totalText.text = totalPrice.ToString();
    }
}
