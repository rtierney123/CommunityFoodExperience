using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage;

public class PauseManager : MonoBehaviour
{
    public GameObject PausePopup;
    public CanvasController canvasController;

    public void pauseButtonClick()
    {
        if (!PausePopup.activeInHierarchy)
        {
            canvasController.openScreen(PausePopup);
        }
        else
        {
            canvasController.closeScreen(PausePopup);
        }
    }

    public void resumeButtonClick()
    {
        canvasController.closeScreen(PausePopup);
    }
    public void restartButtonClick()
    {
        canvasController.closeScreen(PausePopup);
    }

    public void quitButtonClick()
    {
        canvasController.closeScreen(PausePopup);
    }
}
