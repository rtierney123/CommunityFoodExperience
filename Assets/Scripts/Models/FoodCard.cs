﻿using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utility;

public class FoodCard : MonoBehaviour, UnityEngine.EventSystems.IDragHandler, IEndDragHandler
{

    public Text nameText;
    public Text caloriesText;
    public Text fatText;
    public Text proteinText;
    public Text grainText;
    public Text fruitText;
    public Text vegText;
    public Text extraText;

    [HideInInspector]
    public Food food { get; set; }
    [HideInInspector]
    public Cart cart { get; set; }
    [HideInInspector]
    public CanvasController canvasController { get; set; }

    public GameObject backCard;

   
    private Vector3 resetPosition;
    private Transform startParent;
   
    private static int foodID { get; set; }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        OnBeginDrag(eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Transform canvas = GameObject.FindGameObjectWithTag("Canvas").transform;
        transform.SetParent(canvas);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(cart != null)
        {
            cart.notifyDroppedFood(transform.position, this);
            transform.SetParent(startParent);
            transform.position = resetPosition;
        } else
        {
            Debug.Log("No cart");
        }
        
    }

    public void flipCardBack()
    {
        if(canvasController != null)
        {
            canvasController.openPopup(backCard);
        } else
        {
            Debug.Log("CanvasController not set");
        }
    }

    public void flipCardForward()
    {
        if (canvasController != null)
        {
            canvasController.closePopUp(backCard);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
        startParent = this.transform.parent;

        
        display();
   

    }

    void Awake()
    {
        foodID = foodID + 1;
    }


    public void setCart(Cart c)
    {
        cart = c;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

    public void setFood(Food foodItem)
    {
        food = foodItem;
        display();
    }

    private void display()
    {
        Debug.Log("update display");
        
        if (food != null)
        {
            nameText.text = food.name;
            caloriesText.text = FormatText.formatDouble(food.calories);
            fatText.text = FormatText.formatDouble(food.nutrition.fat);
            proteinText.text = FormatText.formatDouble(food.nutrition.protein);
            grainText.text = FormatText.formatDouble(food.nutrition.grain);
            fruitText.text = FormatText.formatDouble(food.nutrition.fruit);
            vegText.text = FormatText.formatDouble(food.nutrition.veg);
            extraText.text = FormatText.formatDouble(food.nutrition.extra);

        }
    }

   
}