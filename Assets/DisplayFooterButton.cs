using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFooterButton : MonoBehaviour
{
    public Sprite collapseImage;
    public Sprite expandImage;
    public Image buttonImage;

    public Animator walletAnimator;
    public Animator nutritionAnimator;
    public Animator pauseAnimator;

    private bool expanded = true;


    public void newState()
    {
        if (expanded)
        {
            closeFooter();
            expanded = false;
        } else
        {
            openFooter();
            expanded = true;
        }
    }

    private void openFooter()
    {
        buttonImage.sprite = collapseImage;
        pauseAnimator.Play("pauseExpand");
        walletAnimator.Play("walletExpand");
        nutritionAnimator.Play("nutritionExpand");
        Debug.Log("asasdfasddf");
    }

    private void closeFooter()
    {
        buttonImage.sprite = expandImage;
        walletAnimator.Play("walletAnimation");
        nutritionAnimator.Play("nutritionAnimation");
        pauseAnimator.Play("pauseAnimation");
        Debug.Log("asdf");
    }
}
