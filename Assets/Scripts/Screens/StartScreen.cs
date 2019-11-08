using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Manage;

public class StartScreen : Screen
{
    public GameObject startScreen;

    void Start() {
        canvasController.pause();
    }

    // void Update() {
    //     if (canvasController) {
    //         print("WOW");
    //         canvasController.pause();
    //     }
    // }

    public void startButtonClicked() {
        canvasController.resume();
        startScreen.SetActive(false);
    }

}
