﻿using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    public class Location : MonoBehaviour
    {
        public Transform playerDropoff;
        public LocationType locationType;
        public NavigiationPopUp popUp;
        public GameObject mainScreen;
        public NavigationManager manager;
        public MapLocations mapLocation;

        public string locationTitle;
        public string locationDescription;

        Ray ray;
        RaycastHit hit;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {

            if (Input.GetMouseButtonDown(0))
            {
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject)
                {
                    manager.startLocationPopup(this);
                    Debug.Log(this.name);
          
                }
            }

        }


        public NavigiationPopUp getPopUp()
        {
            return popUp;
        }

    }

}
