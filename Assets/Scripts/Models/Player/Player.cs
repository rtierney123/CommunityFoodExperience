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

    public int busTickets = 0;

    public double money = 0;
    public double snapFunds = 0;

    public double ctcAcquired = 0;
    public double eitcAcquired = 0;
    public double snapAcquired = 0;

    public WICVoucher voucher;

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

    public bool hadWic = false;

    public bool onBus = false;

    public bool hasTemporaryRide = false;
    public bool carBrokenDown = false;
    public bool hasKidBeenSick = false;
    private bool isHome = true;


    private void Start()
    {
        resetPlayer();
    }

    public void resetPlayer()
    {
        resetToStarting();

        snapFunds = 0;
        ctcAcquired = 0;
        eitcAcquired = 0;
        snapAcquired = 0;

        voucher = null;

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
        useCommunityKitchen = false;


        hadWic = false;


        hasTemporaryRide = false;
        carBrokenDown = false;
        hasKidBeenSick = false;
        isHome = true;
}

    public void setPlayerInfo(PlayerInfo info)
    {
        playerInfo = info;
        Debug.Log(info.jobIncome);
        resetToStarting();
        playerInfo.setRecievedAssistance(false);
        
    }

    public void resetToStarting()
    {
        money = playerInfo.getStartingCash();
        busTickets = playerInfo.startingBusTokens;
    }

  
    // view player information
    public int[] getNutrition()
    {
        // return { carbs, protein, fruits, veggies};
        return null;
    }


    // update player information
    public void setMoney(double value)
    {
        money = value;
    }
    public void setBusTokens(int value)
    {
        busTickets = value;
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
        snapAcquired += funds;
        playerInfo.setRecievedAssistance(true);
    }

    public void subtractSnap(double funds)
    {
        snapFunds -= funds;
    }

    public void addEITC(double funds)
    {
        eitcAcquired += funds;
    }

    public void subtractEITC(double funds)
    {
        eitcAcquired -= funds;
    }

    public void addCTC(double funds)
    {
        ctcAcquired += funds;
    }

    public void subtractCTC(double funds)
    {
        ctcAcquired -= funds;
    }

    public void addVoucher()
    {
        hadWic = true;
        voucher = new WICVoucher();
        playerInfo.setRecievedAssistance(true);
    }



    //TODO need to alter this logic to deal with what if multiple vouchers
    //do I need to worry about this;
    public void updateVoucher(FoodType foodType)
    {
        if (voucher != null)
        {
            voucher.useVoucher(foodType);
        }
    }

    public bool hasNoModeOfTransportation()
    {
        //TODO: fixe
        if(busTickets == 0 && money <= 2.5 && (!playerInfo.hasCar || carBrokenDown) && !playerInfo.busPass && !onBus)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void setIsHome(bool home)
    {
        isHome = home;
    }
    public bool getIsHome()
    {
        return isHome;
    }

    public bool getAchievedNutrition()
    {
        if(calories >= playerInfo.requiredCalories && grain >= playerInfo.requiredGrain && fat >= playerInfo.requiredFat && protein >= playerInfo.requiredProtein &&
            dairy >= playerInfo.requiredDairy && fruit >= playerInfo.requiredFruit && vegetable >= playerInfo.requiredVegetable && extra >= playerInfo.requiredExtra)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public bool getAchieveCalories()
    {
        return calories >= playerInfo.requiredCalories;
    }

    public String getCaloriesStatus()
    {
        return calories.ToString() + "/" + playerInfo.requiredCalories.ToString(); 
    }
    public bool getAchieveGrain()
    {
        return grain >= playerInfo.requiredGrain;
    }
    public String getGrainStatus()
    {
        return grain.ToString() + "/" + playerInfo.requiredGrain.ToString();
    }
    public bool getAchieveFat()
    {
        return fat >= playerInfo.requiredFat;
    }
    public String getFatStatus()
    {
        return fat.ToString() + "/" + playerInfo.requiredFat.ToString();
    }

    public bool getAchieveProtein()
    {
        return protein >= playerInfo.requiredProtein;
    }
    public String getProteinStatus()
    {
        return protein.ToString() + "/" + playerInfo.requiredProtein.ToString();
    }

    public bool getAchieveDairy()
    {
        return dairy >= playerInfo.requiredDairy;
    }
    public String getDairyStatus()
    {
        return dairy.ToString() + "/" + playerInfo.requiredDairy.ToString();
    }
    public bool getAchieveFruit()
    {
        return fruit >= playerInfo.requiredFruit;
    }
    public String getFruitStatus()
    {
        return fruit.ToString() + "/" + playerInfo.requiredFruit.ToString();
    }
    public bool getAchieveVegetable()
    {
        return vegetable >= playerInfo.requiredVegetable;
    }
    public String getVegetableStatus()
    {
        return vegetable.ToString() + "/" + playerInfo.requiredVegetable.ToString();
    }

    public bool getAchieveExtra()
    {
        return extra >= playerInfo.requiredExtra;
    }
    public String getExtraStatus()
    {
        return extra.ToString() + "/" + playerInfo.requiredExtra.ToString();
    }

    public void setFreeRide(bool freeRide)
    {
        hasTemporaryRide = freeRide;
    }

  


}