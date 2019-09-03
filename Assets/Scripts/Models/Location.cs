using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Model
{
    public class Location : MonoBehaviour
    {
        public Vector3 playerDropoff;
        public LocationType locationType;
        public GameObject popUp;
        public GameManager manager;

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
                if (Physics.Raycast(ray, out hit))
                {
                    if(Input.GetMouseButtonDown(0)) {

                        if(!popUp.activeSelf)
                        {
                            manager.startLocationPopup(this);
                        }
                        
                    } 
          
                }
            }

        }


        public GameObject getPopUp()
        {
            return popUp;
        }

    }

}
