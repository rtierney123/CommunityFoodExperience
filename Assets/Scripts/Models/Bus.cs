﻿
using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public int stopLength;
    public GameManager manager;
    public GameObject popUp;

    private Animator animator;

    private MapLocations busLocation;
    public MapLocations mapLocation
    {

        get { return busLocation; }

        set { busLocation = value; }

    }

    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
            {
                manager.handleBusClickedEvent(this);
                Debug.Log(this.name);

            }
        }
    }

    public void startStop(MapLocations stopLocation)
    {
        busLocation = stopLocation;
        manager.handleBusStopEvent();
    }

    public void resumeRoute()
    {
        busLocation = MapLocations.OnRoad;
    }

}