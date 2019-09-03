using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TravelButton : MonoBehaviour
{
    public TravelType travelType;
    public GameManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void travel()
    {
        manager.travelToDestination(travelType);
    }
}
