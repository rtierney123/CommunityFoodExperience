﻿using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Manage
{
    public class CanvasController : MonoBehaviour
    {

        public float allowWaitTime;
        public Text busStopTitle;

        // Normal raycasts do not work on UI elements, they require a special kind
        GraphicRaycaster raycaster;
        [HideInInspector]
        public GameObject popUp;
        Vector3 playerStopLocation;
        bool allowClose;
        bool allowOpen;
        bool clickedUsed;
        void Awake()
        {
            // Get both of the components we need to do this
            this.raycaster = GetComponent<GraphicRaycaster>();
            allowClose = true;
            allowOpen = true;
            popUp = null;
        }

        void Update()
        {
            //Check if the left Mouse button is clicked
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                PointerEventData pointerData = new PointerEventData(EventSystem.current);
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                pointerData.position = Input.mousePosition;
                this.raycaster.Raycast(pointerData, results);

                bool justBackgroundClicked = true;
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    //Debug.Log("Hit tag " + result.gameObject.tag);
                    if (result.gameObject.tag == "Popup")
                    {
                        justBackgroundClicked = false;
                    }
                }

                if (justBackgroundClicked)
                {
                    if (popUp != null)
                    {
                        if (allowClose)
                        {
                            setPopUp(false);
                        }

                    }
                }
            }
        }

        public void openPopup(GameObject gameObject)
        {
            if (popUp == null && allowOpen)
            {
                popUp = gameObject;
                setPopUp(true);
                StartCoroutine(WaitAllowClose(allowWaitTime));
            } 


        }

        public void closePopup(GameObject gameObject)
        {
            if(popUp == gameObject)
            {
                setPopUp(false);
                StartCoroutine(WaitAllowOpen(allowWaitTime));
            }
        }

        public void closePopUp()
        {
            setPopUp(false);
            StartCoroutine(WaitAllowOpen(allowWaitTime));
            popUp = null;
        }

        public void setStopTitle(string title)
        {
            busStopTitle.text = title;
        }

        void setPopUp(bool active)
        { 
            if (popUp != null)
            {
                popUp.SetActive(active);
            }

            if (!active)
            {
                popUp = null;
            }
        }

        private IEnumerator WaitAllowClose(float waitTime)
        {
            allowClose = false;
            yield return new WaitForSeconds(waitTime);
            allowClose = true;
        }

        private IEnumerator WaitAllowOpen(float waitTime)
        {
            allowOpen = false;
            yield return new WaitForSeconds(waitTime);
            allowOpen = true;
        }

    }
}

