using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;
using UI;

namespace Manage{
    //does logic for determining when the endgame is and when the player has satified all goals
    public class GameManager : MonoBehaviour, IClockEventCaller
    {
        public Player player;
        public static bool isPause = false;

        public GameObject pauseScreen;
        public GameObject startScreen;
        public GameObject endScreen;
        public GameObject credits;

        public CanvasController canvasController;
        public NavigationManager navigationManager;
        public MessageManager messageManager;
        public CurrencyManager currencyManager;
        public Bus bus;
        public ClockDisplay clock;
        public CommunityKitchenScreen communityKitchen;


        public float randChildSick = 20;

        public bool openStartScreenOnPlay;

        void Start()
        {
            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
                Debug.Log("START");
                pause();
            }
            else
            {
                startGame();
            }

        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.P) == true)
            {
                pauseGame();
            }
        }

        public void startGame()
        {
            clock.startAnimation();
            //bus.startAnimation();
            navigationManager.reset();
            canvasController.closeScreen();
            currencyManager.update();
        }

        public void pauseGame()
        {
            pause();
            pauseScreen.SetActive(true);
        }

        public void resumeGame()
        {
            resume();
        }


        public void openScreenAndPause(GameObject screen)
        {
            pause();
        }

        public void resume()
        {
            clock.resume();
            //bus.resumeAnimation();
        }

        public void pause()
        {
            bus.pauseAnimation();
            clock.pause();
        }

        public void endGame()
        {
            canvasController.closePopUp();
            canvasController.endGame = true;
            canvasController.openPostGameScreen(endScreen);
            pause();
        }

        public void displayCredits()
        {
            canvasController.openScreen(credits);
        }

        public void restartGame()
        {
            resetGameComponents();
            startGame();
        }

        public void quitGame()
        {
            canvasController.endGame = false;
            resetGameComponents();
            canvasController.closePopUp();
            canvasController.closeScreen();
            canvasController.openScreen(startScreen);
            pause();

        }
        public void resetGameComponents()
        {
            canvasController.reset();
            player.resetPlayer();
            clock.resetAnimation();
            //bus.resetAnimation();
            navigationManager.reset();
            communityKitchen.reset();
        }

        public void hourPassed()
        {
           
        }

        public void minutePassed()
        {
           
        }

        public void hourBeforeEndGame()
        {
            messageManager.generateMainScreenOnlyErrorMessage("You better get home soon.  It is getting late.");
            canvasController.playWarning();
            //Debug.Log("warning end");
        }
    }
}

