using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
    public GameObject walletTab;
    public GameObject infoTab;
    public GameObject transportationTab;

    public void selectWalletTab() {
        setInactive();
        walletTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
    }

    public void selectInfoTab() {
        setInactive();
        infoTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
    }

    public void selectTransportationTab() {
        setInactive();
        transportationTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
    }

    private void setInactive() {
        walletTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
        infoTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
        transportationTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
    }

}
