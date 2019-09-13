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
        public GameObject popUp;
        public GameObject mainScreen;
        public NavigationManager manager;
        public MapLocations mapLocation;
  

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
                    manager.startLocationPopup(this);
                }
            }

        }


        public GameObject getPopUp()
        {
            return popUp;
        }

    }

}
