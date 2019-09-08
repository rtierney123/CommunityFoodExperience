
using Manage;
using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bus : MonoBehaviour
{
    public int stopLength;
    public GameManager manager;
    public GameObject farePopUp;
    public GameObject stopPopUp;
    public MapLocations mapLocation;

    [HideInInspector]
    public bool atStop;
    [HideInInspector]
    public bool playerOnBus;

    private Animator animator;

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
                if (!playerOnBus)
                {
                    manager.handleBusClickedEvent();
                }
              
                Debug.Log(this.name);

            }
        }
    }

    public void startStop(MapLocations stopLocation)
    {
        mapLocation = stopLocation;
        atStop = true;
        if (playerOnBus)
        {
            Debug.Log("blah");
            manager.handleBusStoppedEvent();
        }
        Debug.Log("start stop");
    }

    public void resumeRoute()
    {
        mapLocation = MapLocations.OnRoad;
        atStop = false;
        Debug.Log("resume route");
    }

}