using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommunityKitchenScreen : Screen, IClockEventCaller
{

    public GameManager gameManager;
    public int mealRemaining;
    public int randomMax;
    public int lineWaitTimeMax;
    public Text numMealsText;
    public ClockDisplay clock;


    private void Start()
    {
        numMealsText.text = mealRemaining.ToString();
    }

    public void hourPassed()
    {
        if(mealRemaining != 0)
        {
            float rand = Random.Range(0, randomMax);
            int mealsEaten = (int)rand;
            updateMeals(mealsEaten);
        }
        
    }

    public void minutePassed()
    {
       // throw new System.NotImplementedException();
    }

    public void waitInLine()
    {
        if (mealRemaining > 0)
        {
            updateMeals(1);
            float rand = Random.Range(0, lineWaitTimeMax);
            int lossTime = (int)rand;
            clock.addRunningTime(lossTime);
            messageManager.generateStandardSuccessMessage("'Here is some vegatable soup.'");
        }
        else if (mealRemaining == 0)
        {
            messageManager.generateStandardErrorMessage("'Sorry, we ran out of meals to give out today.'");
        }
    }

    private void updateMeals(int mealsEaten)
    {
        mealRemaining -= mealsEaten;
        if(mealRemaining < 0)
        {
            mealRemaining = 0;
        }
        numMealsText.text = mealRemaining.ToString();
    }

}
