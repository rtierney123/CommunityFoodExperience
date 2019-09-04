using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalletTabClicked : MonoBehaviour
{
    public GameObject walletTab;
    // public GameObject infoTab;
    // public GameObject transportationTab;
    // Start is called before the first frame update
    void Start()
    {
        walletTab = GameObject.Find("WalletTab");
        if (walletTab == null) {
            print("Null");
        } else {
            print("Found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void walletTabSelected() {
        // print(walletTab);
        // Renderer wallet = walletTab.GetComponentInChildren<Renderer>();
        if (walletTab == null) {
            print("Null object");
        } else {
            walletTab.GetComponentInChildren<Renderer>().material.shader = Shader.Find("_Color");
            walletTab.GetComponentInChildren<Renderer>().material.SetColor("_Color", Color.green);
        }
    }
}
