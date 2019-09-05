using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletButton : MonoBehaviour
{
    public GameObject wallet;

    public void walletButtonClicked() {
        if (wallet.activeSelf) {
            disableWallet();
        } else {
            enableWallet();
        }
    }

    public void disableWallet() {
        wallet.SetActive(false);
    }

    public void enableWallet() {
        wallet.SetActive(true);
    }
}
