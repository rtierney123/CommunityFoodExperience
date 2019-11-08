using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Manage;

public class StartScreen : Screen
{
    public GameObject startScreen;

    void Update() {
        if (canvasController) {
            canvasController.pause();
        }
    }

    public void startButtonClicked() {
        canvasController.resume();
        startScreen.SetActive(false);
    }

}
