﻿using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationPopUp : MonoBehaviour
{
    public Text title;
    public Text description;
    public Text carText;
    public Text walkText;

    public Player player;
    public NavigationManager manager;
    public DisableableButton walkButton;
    public DisableableButton carButton;

    [HideInInspector]
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {
        popUp = this.gameObject;
    }

    private void OnEnable()
    {   if(carButton != null)
        {
            if (player.playerInfo.hasCar || player.hasTemporaryRide)
            {
                carButton.enable();
            }
            else
            {
                carButton.disable();
            }
        }
      
        if(walkButton != null)
        {
            if (manager.currentLocation.locationType == LocationType.NearbyLocation || manager.currentLocation.mapLocation == manager.possibleDestination.mapLocation)
            {
                walkButton.enable();
            }
            else
            {
                walkButton.disable();
            }
        }
       
    }

    // Update is called once per frame
    void Update()
    {

    }
}