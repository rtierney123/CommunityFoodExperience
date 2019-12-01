using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterLocationPopup : MonoBehaviour
{
    public Button singleButton;
    public Text popUpTitle;
    protected Location buttonsingleLocation;
    protected void Start()
    {
        singleButton.onClick.AddListener(enterLocationOne);
    }

    private void enterLocationOne()
    {
        buttonsingleLocation.onEnter();
    }
    public void setTitle(string title)
    {
        popUpTitle.text = title;
    }
    public void setLocationOne(Location location)
    {
        buttonsingleLocation = location;
    }

}
