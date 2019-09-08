using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletButton : MonoBehaviour
{
    public GameObject wallet;
    public CanvasController canvasController;

    public void walletButtonClicked() {
        if(!wallet.activeInHierarchy) {
            canvasController.openPopup(wallet);
        } else {
            canvasController.closePopUp();
        }

    }

}
