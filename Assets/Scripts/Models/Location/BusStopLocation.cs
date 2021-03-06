﻿using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BusStopLocation : Location
{
    public GameObject waitPopup;
    public Transform busStartLocation;
    public float busAnimationOffset;
    public override void onDelayedEnter()
    {
        entered = true;
        canvasController.delayOpenMainScreenPopup(waitPopup);
    }

    public override void onImmediateEnter()
    {
        entered = true;
        canvasController.openPopup(waitPopup);
    }
}
