using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisableableButton : MonoBehaviour
{
    public Button button;

    public void disable()
    {
        button.interactable = false;
    }

    public void enable()
    {
        button.interactable = true;
    }
}
