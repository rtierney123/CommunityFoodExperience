using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Cart : MonoBehaviour
{
    public RectTransform cartTransform;
    public PopulateGrid populateGrid;
    public Text totalText;

    private double fruit;
    private double veg;
    private double grain;
    private double fat;
    private double dairy;
    private double protein;
    private double extra;
    private double calories;
    private double totalPrice;

    public void Start()
    {
        
    }
    public void Update()
    {
     
    }

    public void notifyDroppedFood(Vector3 position, Food food)
    {
        if (inCart(position))
        {
            populateGrid.addItem(food.cartObject);
            addItem(food);
            // Debug.Log(totalPrice);
            // Debug.Log(calories);
            // Debug.Log(totalText.text);
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

    // private void addCost(int cost)
    // {
    //     totalPrice += cost;
    //     totalText.text = totalPrice.ToString();
    // }

    private void addItem(Food food) {
        totalPrice += food.cost;
        totalText.text = totalPrice.ToString();
        fruit += food.fruit;
        veg += food.veg;
        grain += food.grain;
        fat += food.fat;
        dairy += food.dairy;
        protein += food.protein;
        extra += food.extra;
        calories += food.calories;
    }


}
