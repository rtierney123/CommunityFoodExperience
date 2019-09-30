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
    public HashSet<Food> foodInCart;

    
    private double totalPrice;
  
    public void Start()
    {
        foodInCart = new HashSet<Food>();
    }
    public void Update()
    {
     
    }

    public void notifyDroppedFood(Vector3 position, Food food)
    {
        if (inCart(position))
        {
            addItem(food);

        }
    }

    public void clearAll()
    {
        foodInCart.Clear();
        populateGrid.cl
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

    private void addItem(Food food) {
        totalPrice += food.cost;
        totalPrice = Math.Round(totalPrice * 100) / 100;

         updateTotal();

        GameObject icon = populateGrid.addCartItem(food.cartObject);
        foodInCart.Add(food);

        Transform minusObject = icon.transform.GetChild(0);
        Button minusButton = minusObject.GetComponent<Button>();
        minusButton.onClick.AddListener(() => removeItemFromCart(food, icon));

        
    }

    private void removeItemFromCart(Food food, GameObject icon)
    {
        totalPrice -= food.cost;
        totalPrice = Math.Round(totalPrice * 100) / 100;

        updateTotal();

        populateGrid.removeItem(icon);
        foodInCart.Remove(food);
    }

    private void updateTotal()
    {
        totalText.text = totalPrice.ToString();
    }
}
