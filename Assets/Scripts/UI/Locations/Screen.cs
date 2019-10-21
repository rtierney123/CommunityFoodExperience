using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public CanvasController canvasController;
    public MessageManager messageManager;
    public GameObject nextScreen;
    public float delayTime;

    public IEnumerator delayOpenNextScreen()
    {
        yield return new WaitForSeconds(delayTime);
        openNextScreen();

    }

    public void openNextScreen()
    {
        StartCoroutine(delayCloseScreen());
        canvasController.openScreen(nextScreen);
    }



    protected IEnumerator delayCloseScreen()
    {
        yield return new WaitForSeconds(delayTime);
        closeScreen();
    }


    public void closeScreen()
    {
        closePopUps();
        canvasController.closeScreen(this.gameObject);
    }

    public void closePopUps()
    {
        if (canvasController != null)
        {
            canvasController.closePopUp();
        }
    }


}
