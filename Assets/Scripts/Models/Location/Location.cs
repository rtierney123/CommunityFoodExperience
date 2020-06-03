using Manage;
using System.Collections;
using System.Collections.Generic;
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
        public Material highlight;

        public string locationTitle;
        public string locationDescription;
        public float delayTime;
        [HideInInspector]
        public bool entered = false;
        public bool busAvailable = false;

        //public GameObject map;
        public GameObject highlightSprite;
        public Camera mainCamera;

        Ray ray;
        RaycastHit hit;

        bool over = false;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {   
            ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                over = true;
                if (highlightSprite != null)
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
                if(highlightSprite != null)
                {
                    highlightSprite.SetActive(false);
                }

                over = false;
            }
           
        }

        public virtual void onEnter()
        {
            entered = true;
            StartCoroutine(OpenLocationScreen());
            //canvasController.openScreen(mainScreen);
        }

        public IEnumerator OpenLocationScreen()
        {
            yield return new WaitForSeconds(delayTime);
            if (mainScreen != null)
            {
                canvasController.closePopUp();
                canvasController.openScreen(mainScreen);
            } else
            {
                canvasController.dequeueMainScreenPopUpBackLog();
            }

            /*
            if (locationType == LocationType.FarLocation)
            {
                navigationManager.displayIfStuck();
            }
            */
          
        }

    }

}
