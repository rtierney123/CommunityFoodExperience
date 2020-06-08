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
        public NutritionManager nutritionManager;
        public MessageManager messageManager;
        public CurrencyManager currencyManager;
        public ClockDisplay clock;
        public GameObject footer;
        public GameObject pauseButton;
        public CommunityKitchenScreen communityKitchen;

        public Form[] forms;

        //TODO set when appropriate
        bool gameStarted = false;

        public float randChildSick = 20;

        public bool openStartScreenOnPlay;

        void Start()
        {
            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
                Debug.Log("START");
                hideIcons();
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

        public void displayIcons()
        {
            clock.gameObject.SetActive(true);
            footer.SetActive(true);
            pauseButton.SetActive(true);
        }

        public void hideIcons()
        {
            clock.gameObject.SetActive(false);
            footer.SetActive(false);
            pauseButton.SetActive(false);
        }

        public void startGame()
        {
            Debug.Log("start game");
            displayIcons();
            clock.startAnimation();
            canvasController.closeScreen();
            currencyManager.update();
            foreach (Form form in forms)
            {
                form.resumeFillingOut();
            }
            navigationManager.reset();
            nutritionManager.reset();
        }

        public void pauseGame()
        {
            pause();
            pauseScreen.SetActive(true);
            foreach(Form form in forms)
            {
                form.pauseFillingOut();
            }
        }

        public void resumeGame()
        {
            resume();
            foreach (Form form in forms)
            {
                form.resumeFillingOut();
            }
        }


        public void openScreenAndPause(GameObject screen)
        {
            pause();
        }

        public void resume()
        {
            clock.resume();
        }

        public void pause()
        {
            clock.pause();
        }

        public void endGame()
        {
            hideIcons();
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
            hideIcons();

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
            messageManager.generateMainScreenOnlyErrorMessage(Status.gettingLate);
            canvasController.playWarning();
            //Debug.Log("warning end");
        }
    }
}

