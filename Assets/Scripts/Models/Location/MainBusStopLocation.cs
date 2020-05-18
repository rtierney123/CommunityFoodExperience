using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MainBusStopLocation : Location
{
    public GameObject waitPopup;
    public Transform busStartLocation;
    public float busAnimationOffset;
    public override void onEnter()
    {
        entered = true;
        canvasController.openPopup(waitPopup);
    }
}
