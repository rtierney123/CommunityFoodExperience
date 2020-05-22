using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodPantryLocation : AidLocation
{
    void Start()
    {
        reenterAfterReceive = Status.reenterFoodPantryAfterReceived;
        string alreadyEnteredString = String.Format(Status.enterAgain, locationTitle);
        reenterAfterDeny = alreadyEnteredString;
    }

}
