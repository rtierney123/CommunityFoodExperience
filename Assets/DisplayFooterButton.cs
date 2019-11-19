using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayFooterButton : MonoBehaviour
{
    public Sprite collapseImage;
    public Sprite expandImage;
    public Image buttonImage;

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
    }

    private void closeFooter()
    {
        buttonImage.sprite = expandImage;
    }
}
