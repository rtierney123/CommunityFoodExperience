using Manage;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Model
{
    public class Location : MonoBehaviour
    {
        public Transform playerDropoff;
        public GameObject mainScreen;
        public NavigationManager navigationManager;
        public CanvasController canvasController;
        public Neighborhood neighborhood;
        public LocationID locationId;

        public string locationTitle;
        public string locationDescription;

        [HideInInspector]
        public bool entered = false;
        public bool busAvailable = false;

        public GameObject highlightSprite;
        public bool manualHighlight = false;
        public Camera mainCamera;

        Ray ray;
        RaycastHit hit;

        bool over = false;


        // Update is called once per frame
        void Update()
        {   
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject() )
            {
                over = true;
                if (highlightSprite != null && !manualHighlight)
                {
                    highlightSprite.SetActive(true);
                }

                if (Input.GetMouseButtonDown(0))
                {
                    navigationManager.startLocationScreen(this);
                }
            }
            else if(over)
            {
                if(highlightSprite != null && !manualHighlight)
                {
                    highlightSprite.SetActive(false);
                }

                over = false;
            }
           
        }

        public void startManualHighlight()
        {
            manualHighlight = true;
            if (highlightSprite != null)
            {
                highlightSprite.SetActive(true);
            }
        }

        public void endManualHighlight()
        {
            manualHighlight = false;
            if (highlightSprite != null)
            {
                highlightSprite.SetActive(false);
            }
        }

        public virtual void onDelayedEnter()
        {
            entered = true;
            canvasController.delayOpenScreen(mainScreen);
            
        }

        public virtual void onImmediateEnter()
        {
            entered = true;
            canvasController.openScreen(mainScreen);
        }

    }

}
