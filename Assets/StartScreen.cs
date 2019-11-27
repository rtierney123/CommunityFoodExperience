using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : Screen
{
    public bool allowChoosePlayer;

    public GameObject choosePlayerScreen;
    public GameObject randomPlayerScreen;

    private void Start()
    {
     
    }

    public void startPressed()
    {
        if (allowChoosePlayer)
        {
            nextScreen = choosePlayerScreen;
        }
        else
        {
            nextScreen = randomPlayerScreen;
        }
        openNextScreen();
    }
}
