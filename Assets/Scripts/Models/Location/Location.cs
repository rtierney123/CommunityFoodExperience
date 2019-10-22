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
                if (Physics.Raycast(ray, out hit) && hit.collider.gameObject == this.gameObject && !EventSystem.current.IsPointerOverGameObject())
                {
                    navigationManager.startLocationScreen(this);
                }
            }

        }

        public virtual void onEnter()
        {
            StartCoroutine(OpenLocationScreen());
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
                canvasController.closePopUp();
                canvasController.openScreen(mainScreen);
            }
        }
    }

}
