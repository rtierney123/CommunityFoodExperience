using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Manage;

public class CommunityKitchenScreen : Screen
{
    public int mealRemaining;
    public Text numMealsText;

    private void Start()
    {
        numMealsText.text = mealRemaining.ToString();
    }


}
