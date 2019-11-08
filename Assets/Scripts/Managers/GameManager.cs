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

        public GameObject startScreen;
        public GameObject pauseScreen;

        public CanvasController canvasController;
        public ClockDisplay clock;
        public Animator busAnimator;

        void Start()
        {
            pause();
        }

        public void subtractTime(int time)
        {

        }

        public void startGame()
        {
            resume();
            canvasController.closeScreen(startScreen);
        }

        public void pauseGame()
        {
            pause();
            canvasController.openScreen(pauseScreen);
        }


        public void openScreenAndPause(GameObject screen)
        {
            pause();
        }

        public void resume()
        {
            clock.resume();
            busAnimator.enabled = true;
        }

        public void pause()
        {
            busAnimator.enabled = false;
            clock.pause();
        }

    }
}

