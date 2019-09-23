using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage;

public class CashButton : MonoBehaviour
{
    public GameObject cashOption;
    public CanvasController canvasController;
    public void CashButtonClicked() {
        if(!cashOption.activeInHierarchy){
            canvasController.openPopup(cashOption);
        } else {
            canvasController.closePopUp();
        }
    }
}
