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
            addCost(food.cost);
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

    private void addCost(int cost)
    {
        totalPrice += cost;
        totalText.text = totalPrice.ToString();

    }


}
