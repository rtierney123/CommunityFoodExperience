using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public CanvasController canvasController;
    public MessageManager messageManager;
    public GameObject nextScreen;
    public GameObject prevScreen;
    protected float delayTime = (float).5;

    public IEnumerator delayOpenNextScreen()
    {
        yield return new WaitForSeconds(delayTime);
        openNextScreen();

    }
    public IEnumerator delayOpenNextScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        openNextScreen();
    }

    public IEnumerator delayOpenSuccessMessage(float delay, string message)
    {
        Debug.Log("delayOpen");
        yield return new WaitForSeconds(delay);
        displaySuccessMessage(message);
    }

    public void openNextScreen()
    {
        StartCoroutine(delayCloseScreen());
        if(nextScreen != null)
        {
            canvasController.openScreen(nextScreen);
        }
    }

    public void openPrevScreen()
    {
        StartCoroutine(delayCloseScreen());
        if (prevScreen != null)
        {
            canvasController.openScreen(prevScreen);
        } else
        {
            canvasController.closeScreen();
        }
    }


    protected IEnumerator delayCloseScreen()
    {
        yield return new WaitForSeconds(delayTime);
        closeScreen();
    }

    protected IEnumerator delayCloseScreen(float delay)
    {
        yield return new WaitForSeconds(delay);
        closeScreen();
    }


    public virtual void closeScreen()
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

    public void displaySuccessMessage(string message)
    {
        messageManager.generateStandardSuccessMessage(message);
        Debug.Log("display success");
    }
}
