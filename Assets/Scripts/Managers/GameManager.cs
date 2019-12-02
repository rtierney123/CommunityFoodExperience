using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;

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
        public Bus bus;
        public ClockDisplay clock;
        public FooterDisplay footerDisplay;


        public float randChildSick = 20;

        public bool openStartScreenOnPlay;

        void Start()
        {
            if (openStartScreenOnPlay)
            {
                canvasController.openScreen(startScreen);
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

        public void subtractTime(int time)
        {

        }

        public void startGame()
        {
            clock.startAnimation();
            bus.startAnimation();
            navigationManager.reset();
            footerDisplay.update();
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
            bus.resumeAnimation();
        }

        public void pause()
        {
            bus.pauseAnimation();
            clock.pause();
        }

        public void endGame()
        {
            canvasController.openScreen(endScreen);
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
            resetGameComponents();
            canvasController.closePopUp();
            canvasController.closeScreen();
            canvasController.openScreen(startScreen);
            pause();

        }
        public void resetGameComponents()
        {
            canvasController.closePopUp();
            canvasController.closeScreen();
            player.resetPlayer();
            clock.resetAnimation();
            bus.resetAnimation();
            navigationManager.reset();
        }

        public void hourPassed()
        {

        }

        public void minutePassed()
        {
            PlayerInfo info = player.playerInfo;
            FamilyMember[] children = info.children.list;

            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < randChildSick && children.Length > 0 && !player.hasKidBeenSick)
            {
                float scale = (float) navigationManager.scale*100;
                double timeLost = UnityEngine.Random.Range((float) (3*scale), (float) (6*scale))/100;
                double gametimeLost = navigationManager.realToGameTime(timeLost);
                string timeString = navigationManager.formatTime(gametimeLost);

                messageManager.generateMainScreenOnlyErrorMessage(String.Format("Oh no! Your child is sick at school.  You have to pick them up. (You lose {0}.)", timeString));
                clock.addRunningTime(timeLost);
                player.hasKidBeenSick = true;
            }
        }

        public void hourBeforeEndGame()
        {
            messageManager.generateMainScreenOnlyErrorMessage("You better get home soon.  It is getting late.");
            canvasController.playWarning();
        }
    }
}

