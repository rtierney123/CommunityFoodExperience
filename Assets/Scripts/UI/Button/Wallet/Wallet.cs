using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Wallet : MonoBehaviour
{
    public GameObject walletTab;
    public GameObject walletTabBody;
    public GameObject infoTab;
    public GameObject infoTabBody;
    public GameObject transportationTab;
    public GameObject transportationTabBody;

    public Player player;

    void Start() {

        setInactive();
        selectWalletTab();
    }

    public void selectWalletTab() {
        Debug.Log("wallet");
        setInactive();
        walletTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
        walletTabBody.SetActive(true);
        foreach (Transform child in walletTabBody.transform) {
            if (child.name == "CashValue") {
                child.gameObject.GetComponent<Text>().text = player.money.ToString();
            } else if (child.name == "SNAPValue") {
                child.gameObject.GetComponent<Text>().text = player.snap ? "Yes" : "No";
            } else if (child.name == "WICValue") {
                child.gameObject.GetComponent<Text>().text = player.wic ? "Yes" : "No";
            } else if (child.name == "EITCValue") {
                child.gameObject.GetComponent<Text>().text = player.eitc ? "Yes" : "No";
            } else if (child.name == "CTCValue") {
                child.gameObject.GetComponent<Text>().text = player.ctc ? "Yes" : "No";
            } 
        }
    }

    public void selectInfoTab() {
        Debug.Log("info");
        setInactive();
        infoTab.GetComponent<Image>().color = new Color32(171, 117, 0, 255);
        infoTabBody.SetActive(true);
        foreach (Transform child in infoTabBody.transform) {
            if (child.name == "NameValue") {
                child.gameObject.GetComponent<Text>().text = player.characterName;
            } else if (child.name == "SSNValue") {
                child.gameObject.GetComponent<Text>().text = player.socialSecurity;
            } else if (child.name == "DOBValue") {
                child.gameObject.GetComponent<Text>().text = player.DOB;
            } else if (child.name == "TelValue") {
                child.gameObject.GetComponent<Text>().text = player.phoneNum;
            } else if (child.name == "AddressValue") {
                child.gameObject.GetComponent<Text>().text = player.address;
            } 
        }
    }

    public void selectTransportationTab() {
        Debug.Log("transportation");
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
