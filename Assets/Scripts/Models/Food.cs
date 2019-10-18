﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
<<<<<<< Updated upstream
using UnityEngine.EventSystems;
=======
>>>>>>> Stashed changes

public class Food : MonoBehaviour
{

<<<<<<< Updated upstream
    private string name;
    [HideInInspector]
    public double cost;
    [HideInInspector]
    public double calories;
    private double fat;
    private double satFat;
    private double cholestrol;
    private double sodium;
    private double carbs;
    private double fiber;
    private double sugar;
    private double protein;
    private bool wic;

    public double fruit;
    public double veg;
    public double grain;
    public double dairy;
    public double macroProtein;
    public double macroFat;
    public double extra;

    public FoodType wicType;

    public GameObject cartObject;

    [HideInInspector]
    public Cart cart;

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


    // Start is called before the first frame update
    void Start()
    {
        resetPosition = transform.position;
        startParent = this.transform.parent;
    }

    void Awake()
    {
        foodID = foodID + 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCart(Cart c)
    {
        cart = c;
    }

    public GameObject getGameObject()
    {
        return this.gameObject;
    }

=======
    public string name { get; set; }
    public double cost { get; set; }
    public double calories { get; set; }
    private double fat { get; set; }
    private double satFat { get; set; }
    private double cholestrol { get; set; }
    private double sodium { get; set; }
    private double carbs { get; set; }
    private double fiber { get; set; }
    private double sugar { get; set; }
    private double protein { get; set; }
    private bool wic { get; set; }

    public double fruit { get; set; }
    public double veg { get; set; }
    public double grain { get; set; }
    public double dairy { get; set; }
    public double macroProtein { get; set; }
    public double macroFat { get; set; }
    public double extra { get; set; }


    public FoodType wicType;

>>>>>>> Stashed changes
}
