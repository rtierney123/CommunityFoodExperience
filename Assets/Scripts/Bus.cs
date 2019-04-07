using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bus : MonoBehaviour
{
    private bool stopBus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "Station" )
        {
            Debug.Log("Stop bus");
            setStopBus(true);
        }

    }

    public void setStopBus(bool stop)
    {
        stopBus = stop;
        Image image = GetComponent<Image>();
        Color newColor = image.color;
        if (stop)
        {
            image.color = Color.yellow;
            
        } else
        {
            image.color = Color.white;
        }
    }

    public bool getStopBus()
    {
        return stopBus;
    }

}
