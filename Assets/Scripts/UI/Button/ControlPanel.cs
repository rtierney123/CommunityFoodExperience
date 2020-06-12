using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class ControlPanel : View
    {
        public GameManager gameManager;
        public GameObject pauseMenuScreen;
        public GameObject notepadScreen;
        [HideInInspector]
        public bool pauseScreenOpen = false;

        public override void reset()
        {
            pauseScreenOpen = false;
        }

        void Update()
        {
            
            if (Input.GetKeyDown(KeyCode.P) == true && gameManager.gameStarted)
            {
                if (!pauseScreenOpen)
                {
                    if (!pauseMenuScreen.activeInHierarchy)
                    {
                        openPauseScreen();
                    }
                    
                }
               
            }
        }

        public void openPauseScreen()
        {
            gameManager.pauseGame();
            pauseMenuScreen.SetActive(true);
            pauseScreenOpen = true;
        }

        public void closePauseScreen()
        {
            gameManager.resumeGame();
            pauseMenuScreen.SetActive(false);
            pauseScreenOpen = false;
        }


        public void openNotepadScreen()
        {
            gameManager.pauseGame();
            notepadScreen.SetActive(true);
            pauseScreenOpen = true;
            Debug.Log("open notepad");
        }

        public void closeNotepadScreen()
        {
            gameManager.resumeGame();
            notepadScreen.SetActive(false);
            pauseScreenOpen = false;
        }
    }
}

