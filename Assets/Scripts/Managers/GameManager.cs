using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;
using UI;
using Model;

namespace Manage{
    //does logic for determining when the endgame is and when the player has satified all goals
    public class GameManager : MonoBehaviour, IClockEventCaller
    {
        public Player player;
        public static bool isPause = false;

        public GameObject pauseScreen;
        public GameObject startScreen;
        public GameObject endScreen;

        public CanvasController canvasController;
        public NavigationManager navigationManager;
        public NutritionManager nutritionManager;
        public MessageManager messageManager;
        public CurrencyManager currencyManager;
        public ClockDisplay clock;
        public GameObject footer;
        public GameObject pauseButton;

        public BaseLocationScreen[] locationScreens;
        public ControlPanel controlPanel;
        public RandomEventGenerator randomGenerator;

        public Form[] forms;
        public BusAnimationScreen[] busProgressScreen;

        public bool openStartScreenOnPlay;

        [HideInInspector]
        public bool gameStarted = false;

        void Start()
        {
            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
                navigationManager.reset();
                Debug.Log("START");
                hideIcons();
                pause();
            }
            else
            {
                startGame();
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
            foreach(BaseLocationScreen screen in locationScreens)
            {
                screen.reset();
                screen.onStartGame();
            }
            controlPanel.reset();
            navigationManager.reset();
            nutritionManager.reset();
            randomGenerator.reset();
            gameStarted = true;
        }

        public void pauseGame()
        {
            pause();
            foreach(Form form in forms)
            {
                form.pauseFillingOut();
            }
            foreach(BusAnimationScreen busScreen in busProgressScreen)
            {
                busScreen.pauseScreen();
            }
        }

        public void resumeGame()
        {
            resume();
            foreach (Form form in forms)
            {
                form.resumeFillingOut();
            }
            foreach (BusAnimationScreen busScreen in busProgressScreen)
            {
                busScreen.resumeScreen();
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
            canvasController.clearBacklog();
            canvasController.closePopUp();
            canvasController.endGame = true;
            canvasController.openPostGameScreen(endScreen);
            pause();
            gameStarted = false;
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
            gameStarted = false;

        }
        public void resetGameComponents()
        {
            canvasController.reset();
            player.resetPlayer();
            clock.resetAnimation();
            navigationManager.reset();
            foreach (BaseLocationScreen screen in locationScreens)
            {
                screen.reset();
                screen.onStartGame();
            }
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
            Debug.Log("warning end");
        }
    }
}

