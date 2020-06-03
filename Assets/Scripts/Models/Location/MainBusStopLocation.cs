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
        //canvasController.addToPopUpBackLog(waitPopup);
        //canvasController.openPopup(waitPopup);
        StartCoroutine(OpenBusPopup());
        Debug.Log("on enter");
    }

    public IEnumerator OpenBusPopup()
    {
        yield return new WaitForSeconds(delayTime);
        canvasController.forcePopupOpen(waitPopup);

    }
}
