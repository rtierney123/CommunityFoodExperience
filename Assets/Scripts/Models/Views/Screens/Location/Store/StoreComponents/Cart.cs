using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System;
using Utility;

public class Cart : MonoBehaviour
{
    public RectTransform cartTransform;
    public PopulateCart populateGrid;
    public Text totalText;
    [HideInInspector]
    public Dictionary<Food, int> foodInCart;

    
    private double totalPrice;
    public double getTotalPrice()
    {
        return totalPrice;
    }
  
    public void Start()
    {
        foodInCart = new Dictionary<Food, int>();
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
        if(foodInCart != null)
        {
            foodInCart.Clear();
        }
        populateGrid.clearAll();
        totalPrice = 0;
        updateTotal();
    }

    public int getCartCount()
    {
        int count = 0;
        foreach (int num in foodInCart.Values)
        {
            count += num;
        }

        return count;
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
        totalPrice += food.price;
        totalPrice = Math.Round(totalPrice * 100) / 100;

         updateTotal();

        FoodCard card = populateGrid.addItem(food);
        if (foodInCart.ContainsKey(food))
        {
            int count;
            foodInCart.TryGetValue(food, out count);

            count++;
            foodInCart[food] = count;
        } else
        {
            foodInCart.Add(food, 1);
        }

        Button minusButton = card.minusButton;
        minusButton.gameObject.SetActive(true);
        minusButton.onClick.AddListener(() => removeItemFromCart(card));

        
    }

    private void removeItemFromCart(FoodCard card)
    {
        Food food = card.food;
        totalPrice -= food.price;
        totalPrice = Math.Round(totalPrice * 100) / 100;

        updateTotal();

        populateGrid.removeItem(card.gameObject);
        foodInCart.Remove(food);
    }

    private void updateTotal()
    {
        if(totalText != null)
        {
            totalText.text = FormatText.formatCost(totalPrice);
        }
       
    }
}
