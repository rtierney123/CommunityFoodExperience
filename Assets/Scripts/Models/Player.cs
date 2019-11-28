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

    public double money = 0;
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
    public bool useCommunityKitchen = false;

    public WICVoucher wicVoicher;
    public bool hasWic = false;

    public bool hasTemporaryRide = false;
    public bool hasKidBeenSick = false;

    private void Start()
    {
    }

    public void resetPlayer()
    {
        resetToStarting();

        snapFunds = 0;
        ctcFunds = 0;
        eitcFunds = 0;


        calories = 0;
        grain = 0;
        fat = 0;
        protein = 0;
        dairy = 0;
        fruit = 0;
        vegetable = 0;
        extra = 0;

        usedVita = false;
        usedFoodPantry = false;
        usedWIC = false;
        usedSnap = false;

        hasWic = false;


        hasTemporaryRide = false;
        hasKidBeenSick = false;
}

    public void setPlayerInfo(PlayerInfo info)
    {
        playerInfo = info;
        resetToStarting();
    }

    public void resetToStarting()
    {
        money = playerInfo.getStartingCash();
        busTokens = playerInfo.startingBusTokens;
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

    public bool hasNoModeOfTransportation()
    {
        if(busTokens == 0 && money + eitcFunds + ctcFunds < 2.5 && !playerInfo.hasCar && !playerInfo.busPass)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void setFreeRide(bool freeRide)
    {
        hasTemporaryRide = freeRide;
    }

  


}