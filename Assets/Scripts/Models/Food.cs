using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Food : MonoBehaviour
{

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

}
