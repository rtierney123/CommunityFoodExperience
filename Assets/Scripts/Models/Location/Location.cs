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
        public NavigiationPopUp popUp;
        public GameObject mainScreen;
        public NavigationManager navigationManager;
        public CanvasController canvasController;
        public MapLocations mapLocation;

        public string locationTitle;
        public string locationDescription;
        public float delayTime;
        public bool entered = false;

        public GameObject map;
        public Material[] mats; // size 9: 2 elements each

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
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
            {
                
                over = true;
                var elements = map.GetComponent<Renderer>().materials;
                elements[0] = Resources.Load<Material>("Materials/map " + locationTitle);
                //elements[1] = Resources.Load<Material>("Materials/map " + locationTitle);
                
                // comment out line below to disable building highlight
                map.GetComponent<Renderer>().materials = elements;
                // Debug.Log("this: " + locationTitle);

                // enter buildings
                if (Input.GetMouseButtonDown(0))
                {
                    navigationManager.startLocationScreen(this);
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
            StartCoroutine(OpenLocationScreen());
            if (locationType == LocationType.FarLocation)
            {
                StartCoroutine(MonitorScreen());
            }
        }


        public NavigiationPopUp getPopUp()
        {
            return popUp;
        }

        public IEnumerator OpenLocationScreen()
        {
            yield return new WaitForSeconds(delayTime);
            if (mainScreen != null)
            {
                canvasController.closePopUp ();
                canvasController.openScreen(mainScreen);
            }
        }

        public IEnumerator MonitorScreen()
        {
            yield return new WaitUntil(() => mainScreen.activeSelf);
            yield return new WaitUntil(() => !mainScreen.activeSelf);
            navigationManager.displayIfStuck();
        }

    }

}
