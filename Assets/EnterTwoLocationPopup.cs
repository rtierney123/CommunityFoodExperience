using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterTwoLocationPopup : EnterLocationPopup
{
    public Button doubleButton;
    protected Location buttonsecondLocation;
    protected void Start()
    {
        doubleButton.onClick.AddListener(enterLocationTwo);
    }

    private void enterLocationTwo()
    {
        buttonsecondLocation.onEnter();
    }
    public void setLocationTwo(Location location)
    {
        buttonsecondLocation = location;
    }
}
