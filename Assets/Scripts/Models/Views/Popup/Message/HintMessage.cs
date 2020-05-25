using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HintMessage : MonoBehaviour
{
    public Text message;

    public void setText(string str)
    {
        message.text = str;
    }

    public void display()
    {
        this.gameObject.SetActive(true);
    }

    public void hide()
    {
        this.gameObject.SetActive(false);
    }
}
