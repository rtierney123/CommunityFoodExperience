using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : GamePiece
{
    public Transform bus;
    public Text busButtonText;

    bool onBus;
    // Start is called before the first frame update
    void Start()
    {
        pieceTransform = this.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void travelPath()
    {
        StartCoroutine(followPath(pieceTransform, path));
    }

    public void takeBus()
    {
        Bus busScript = bus.GetComponent<Bus>();
        if (busScript.getStopBus() && !onBus)
        {
            this.GetComponent<Image>().enabled = false;
            onBus = true;
            busButtonText.text = "Get off bus.";
        }
        else
        {
            this.GetComponent<Image>().enabled = true;
            this.GetComponent<Transform>().position = bus.position;
            onBus = false;
            busButtonText.text = "Take Bus : 1 token";
        }

    }
}
