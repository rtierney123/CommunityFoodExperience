using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
 

    // temp player info. will place in better place later
    public string characterName = "John Smith";
    public string characterDescription = "You are an elderly person (senior citizen) and you live alone on a fixed income of $870 a month from Social Security. Your rent for a small apartment is $600/month and you pay $50/month for electricity, $30 month for a phone and $185/month for two prescriptions that are only partially coverd by Medicare.";
    public string phoneNum = "(404) 888-9360";
    public string socialSecurity = "XXX-XX-6789";
    public string address = "258 W.Real St. Atlanta, GA 30317";
    public string DOB = "01/01/2010";
    public int fixedIncome = 870;
    public int expenses = 855;
    public int money = 15;
    public int busTokens = 1;
    public bool vita = false;
    public bool snap = false;
    public bool wic = false;
    public bool ctc = false;
    public bool eitc = false;

    public int progress = 0;
    
    public int calories = 0;
    public int grain = 0;
    public int fat = 0;
    public int protein = 0;
    public int dairy = 0;
    public int fruit = 0;
    public int vegetabele = 0;
    public int extra = 0;

    public int requiredCalories = 5;
    public int requiredGrain = 1;
    public int requiredFat = 1;
    public int requiredProtein = 3;
    public int requiredDairy = 2;
    public int requiredFruit = 1;
    public int requiredVegetable = 2;
    public int requiredExtra = 0;

    public Player() {}

    //string[] foodAcquired = [];

    // view player information
    public int[] getNutrition()
    {
    // return { carbs, protein, fruits, veggies};
    return null;
    }
    public int getProgress()
    {
        return progress;
    }
    
    //.....

    // update player information
    public void setMoney(int value)
    {
        money = value;
    }
    public void setBusTokens(int value)
    {
        busTokens = value;
    }


    //.....

    // update player nutrition
    public void addNutrition(Food food)
    {
        calories += food.calories;
        grain += food.grain;
        fat += food.fat;
        protein += food.protein;
        dairy += food.dairy;
        fruit += food.fruit;
        vegetabele += food.veg;
        extra = food.extra;

    }
}