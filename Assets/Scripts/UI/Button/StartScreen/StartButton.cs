using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public GameObject startScreen;
    public CanvasController canvasController;

    public void startButtonClicked() {
            canvasController.resume();
    }

}
