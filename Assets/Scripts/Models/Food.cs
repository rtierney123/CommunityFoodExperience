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
    public FoodType wicType;

    private double fat;
    private double saturatedFat;
    private double cholesterol;
    private double sodium;
    private double carbohydrates;
    private double fiber;
    private double sugar;
    private double protein;
    private bool wic;

    [HideInInspector]
    public string imgPath;

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
