using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[System.Serializable]
public class Food
{

    public string name;
    public double price;
    public double calories;
    [SerializeField, HideInInspector]
    public MyPlateNutrition nutrition;
    [SerializeField]
    public FoodType[] wicType;
    public bool wic;
    public bool premade;

    private double fat;
    private double saturatedFat;
    private double cholesterol;
    private double sodium;
    private double carbohydrates;
    private double fiber;
    private double sugar;
    private double protein;
   

    [HideInInspector]
    public string imgPath;
    public string imgPath_c;

    /*
    [HideInInspector]
    public string location;
    */

    [System.Serializable]
    public class MyPlateNutrition
    {
        public double fruit;
        public double veg;
        public double grain;
        public double dairy;
        public double protein;
        public double fat;
        public double extra;
    }

}
