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
    public Text numMealsText;


    private void Start()
    {
        numMealsText.text = mealRemaining.ToString();
    }

    public void hourPassed()
    {
        float rand = Random.Range(0, randomMax);
        int mealsEaten = (int)rand;
        updateMeals(mealsEaten);
        Debug.Log("ck updated");
    }

    public void minutePassed()
    {
       // throw new System.NotImplementedException();
    }


    private void updateMeals(int mealsEaten)
    {
        mealRemaining -= mealsEaten;
        numMealsText.text = mealRemaining.ToString();
    }


}
