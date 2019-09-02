using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ClickableLocation : MonoBehaviour
    {
        Ray ray;
        RaycastHit hit;
        public GameObject pop_up;
        public ClickableCanvas canvasScript;

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
                        canvasScript.openPopup(pop_up);
                    } 
          
                }
            }

        }

    }

}
