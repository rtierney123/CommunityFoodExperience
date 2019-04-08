using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class PopUpButton : MonoBehaviour
    {
        public Transform popUp;
        private Canvas canvas;

        // Start is called before the first frame update
        void Start()
        {
            canvas = popUp.GetComponent<Canvas>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (canvas.enabled)
                {
                    //canvas.enabled = false;
                }
            }

        }

        public void showPopUp()
        {
            canvas.enabled = true;
            // popUp.rotation = Quaternion.Euler(0, 0, 0);
            Debug.Log("Pop up shown");
        }
    }

}
