using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class StartScreen : Screen
    {
        public bool allowChoosePlayer;

        public GameObject choosePlayerScreen;
        public GameObject randomPlayerScreen;

        public PlayerRandomizer playerRandomizer;


        public void startPressed()
        {
            if (allowChoosePlayer)
            {
                nextScreen = choosePlayerScreen;
            }
            else
            {
                playerRandomizer.selectCharacterRandomly();
                nextScreen = randomPlayerScreen;
            }
            openNextScreen();
        }

    }
}

