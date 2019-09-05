using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeTab : MonoBehaviour
{
    public GameObject walletTab;
    public GameObject walletTabBody;
    public GameObject infoTab;
    public GameObject infoTabBody;
    public GameObject transportationTab;
    public GameObject transportationTabBody;

    public void selectWalletTab() {
        setInactive();
        walletTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
        walletTabBody.SetActive(true);
    }

    public void selectInfoTab() {
        setInactive();
        infoTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
        infoTabBody.SetActive(true);
    }

    public void selectTransportationTab() {
        setInactive();
        transportationTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
        transportationTabBody.SetActive(true);
    }

    private void setInactive() {
        walletTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
        infoTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
        transportationTab.GetComponent<Image>().color = new Color32(231, 201, 88, 255);
        walletTabBody.SetActive(false);
        infoTabBody.SetActive(false);
        transportationTabBody.SetActive(false);
    }

}
