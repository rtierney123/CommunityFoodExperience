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
        public LocationType locationType;
        public GameObject mainScreen;
        public NavigationManager navigationManager;
        public CanvasController canvasController;
        public Neighborhood neighborhood;
        public Material highlight;

        public string locationTitle;
        public string locationDescription;
        public float delayTime;
        [HideInInspector]
        public bool entered = false;
        public bool busAvailable = false;

        public GameObject map;
       
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
                var elements = map.GetComponent<Renderer>().materials;
                elements[0] = highlight;
                //elements[1] = Resources.Load<Material>("Materials/map " + locationTitle);
                
                // comment out line below to disable building highlight
                map.GetComponent<Renderer>().materials = elements;
                // Debug.Log("this: " + locationTitle);

                // enter buildings
                if (Input.GetMouseButtonDown(0))
                {
                    navigationManager.startLocationScreen(this);
                    Debug.Log("location clicked");
                }
            }
            else
            {
                // reset map
                if (over)
                {
                    var elements = map.GetComponent<Renderer>().materials;
                    elements[0] = Resources.Load<Material>("Materials/map");
                    // elements[1] = Resources.Load<Material>("Materials/map");
                    map.GetComponent<Renderer>().materials = elements;
                    
                    over = false;
                }
                
            }
        }

        public virtual void onEnter()
        {
            entered = true;
            //StartCoroutine(OpenLocationScreen());
            canvasController.openScreen(mainScreen);
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

            if (locationType == LocationType.FarLocation)
            {
                navigationManager.displayIfStuck();
            }
          
        }

    }

}
