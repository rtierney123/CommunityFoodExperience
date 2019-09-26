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
    public HashSet<Food> foodInCart;
    private double totalPrice;
    /*
    private double fruit;
    private double veg;
    private double grain;
    private double fat;
    private double dairy;
    private double protein;
    private double extra;
    private double calories;
    */

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
        updateTotal();

        GameObject icon = populateGrid.addCartItem(food.cartObject);
        foodInCart.Add(food);

        Transform minusObject = icon.transform.GetChild(0);
        Button minusButton = minusObject.GetComponent<Button>();
        minusButton.onClick.AddListener(() => removeItemFromCart(food, icon));

        /*
        fruit += food.fruit;
        veg += food.veg;
        grain += food.grain;
        fat += food.fat;
        dairy += food.dairy;
        protein += food.protein;
        extra += food.extra;
        calories += food.calories;
        */
    }

    private void removeItemFromCart(Food food, GameObject icon)
    {
        totalPrice -= food.cost;
        updateTotal();

        populateGrid.removeItem(icon);
        foodInCart.Remove(food);

        foodInCart.Remove(food);

    }

    private void updateTotal()
    {
        totalText.text = totalPrice.ToString();
    }
}
