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
    public HashSet<Food> foodInCart;
    private double totalPrice;
    
    private double fruit;
    private double veg;
    private double grain;
    private double fat;
    private double dairy;
    private double protein;
    private double extra;
    private double calories;

    public double getTotalPrice() {
        return totalPrice;
    }
    public double getFruit() {
        return fruit;
    }
    public double getVeg() {
        return veg;
    }
    public double getGrain() {
        return grain;
    }
    public double getFat() {
        return fat;
    }
    public double getDairy() {
        return dairy;
    }
    public double getProtein() {
        return protein;
    }
    public double getExtra() {
        return extra;
    }
    public double getCalories() {
        return calories;
    }
  
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
        fruit += food.fruit;
        veg += food.veg;
        grain += food.grain;
        fat += food.macroFat;
        dairy += food.dairy;
        protein += food.macroProtein;
        extra += food.extra;
        calories += food.calories;
        totalText.text = totalPrice.ToString();

        // updateTotal();

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
        /*
        fruit -= food.fruit;
        veg -= food.veg;
        grain -= food.grain;
        fat -= food.fat;
        dairy -= food.dairy;
        protein -= food.protein;
        extra -= food.extra;
        calories -= food.calories;
        */
        totalText.text = totalPrice.ToString();
        // updateTotal();

        populateGrid.removeItem(icon);
        foodInCart.Remove(food);

        foodInCart.Remove(food);
    }

    // private void updateTotal()
    // {
    //     totalText.text = totalPrice.ToString();
    // }
}
