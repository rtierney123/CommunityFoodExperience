using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UI;
using Manage;

public class StartScreen : MonoBehaviour
{
    public CanvasController canvasController;
    // public ClockDisplay clock;
    public GameObject startScreen;

    void Start() {
        print("WOW");
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
