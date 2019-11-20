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
        Debug.Log("Deactivate button");
    }

    public void enable()
    {
        button.interactable = true;
        Debug.Log("Activate button");
    }
}
