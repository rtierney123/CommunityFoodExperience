using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage;

public class PurchaseButton : MonoBehaviour
{
    public GameObject purchaseOptions;
    public CanvasController canvasController;
    public void PurchaseButtonClicked() {
        if(!purchaseOptions.activeInHierarchy){
            canvasController.openPopup(purchaseOptions);
        } else {
            canvasController.closePopUp();
        }
    }
}
