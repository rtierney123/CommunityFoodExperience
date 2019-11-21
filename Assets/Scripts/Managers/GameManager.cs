using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;

namespace Manage{
    //does logic for determining when the endgame is and when the player has satified all goals
    public class GameManager : MonoBehaviour
    {
        public Player player;
        public static bool isPause = false;

        public GameObject pauseScreen;
        public GameObject startScreen;
        public GameObject endScreen;

        public CanvasController canvasController;
        public NavigationManager navigationManager;
        public Bus bus;
        public ClockDisplay clock;
        public Animator busAnimator;

        public bool openStartScreenOnPlay;
        void Start()
        {
            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
                pause();
            }
           
        }

        public void subtractTime(int time)
        {

        }

        public void startGame()
        {
            resume();
            canvasController.closeScreen();
        }

        public void pauseGame()
        {
            Debug.Log("pause game");
            pause();
            canvasController.openScreen(pauseScreen);
        }

        public void resumeGame()
        {
            resume();
            canvasController.closeScreen(pauseScreen);
        }


        public void openScreenAndPause(GameObject screen)
        {
            pause();
        }

        public void resume()
        {
            clock.resume();
            busAnimator.enabled = true;
            Debug.Log("bus enabled");
        }

        public void pause()
        {
            busAnimator.enabled = false;
            clock.pause();
        }

        public void endGame()
        {
            canvasController.openScreen(endScreen);
            pause();
        }

        public void restartGame()
        {
            canvasController.closePopUp();
            canvasController.closeScreen();
            canvasController.reset();
            player.resetPlayer();
            clock.reset();
            bus.reset();
            navigationManager.reset();

            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
                pause();
            } else {
                startGame();
            }
        }
    }
}

