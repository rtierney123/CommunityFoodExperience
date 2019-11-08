using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;


public class Player : MonoBehaviour
{
   
    public PlayerInfo playerInfo;

    public int busTokens = 0;

    public double money = 15;
    public double snapFunds = 0;
    public double ctcFunds = 0;
    public double eitcFunds = 0;

    public double calories = 0;
    public double grain = 0;
    public double fat = 0;
    public double protein = 0;
    public double dairy = 0;
    public double fruit = 0;
    public double vegetable = 0;
    public double extra = 0;

    public bool usedVita = false;
    public bool usedFoodPantry = false;
    public bool usedWIC = false;
    public bool usedSnap = false;

    public WICVoucher wicVoicher;
    [HideInInspector]

    public bool hasWic = false;


    private void Start()
    {
    }

    public void setPlayerInfo(PlayerInfo info)
    {
        playerInfo = info;
        money = playerInfo.getStartingCash();

    }

    //string[] foodAcquired = [];

    // view player information
    public int[] getNutrition()
    {
        // return { carbs, protein, fruits, veggies};
        return null;
    }

    //.....

    // update player information
    public void setMoney(double value)
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
        grain += food.nutrition.grain;
        fat += food.nutrition.fat;
        protein += food.nutrition.protein;
        dairy += food.nutrition.dairy;
        fruit += food.nutrition.fruit;
        vegetable += food.nutrition.veg;
        extra += food.nutrition.extra;

    }

    public void addCash(double cash)
    {
        money += cash;
    }

    public void subtractCash(double cash)
    {
        money -= cash;
    }

    public void addSnap(double funds)
    {
        snapFunds += funds;
    }

    public void subtractSnap(double funds)
    {
        snapFunds -= funds;
    }

    public void addEITC(double funds)
    {
        eitcFunds += funds;
    }

    public void subtractEITC(double funds)
    {
        eitcFunds -= funds;
    }

    public void addCTC(double funds)
    {
        ctcFunds += funds;
    }

    public void subtractCTC(double funds)
    {
        ctcFunds -= funds;
    }

    public void addVoucher()
    {
        hasWic = true;
        wicVoicher.gameObject.SetActive(true);
    }

    //TODO need to alter this logic to deal with what if multiple vouchers
    //do I need to worry about this;
    public void useVoucher(Food food)
    {
        if (wicVoicher != null)
        {
            if (wicVoicher.checkValid(food))
            {
                wicVoicher.useVoucher(food);
            }

        }


    }

  

}