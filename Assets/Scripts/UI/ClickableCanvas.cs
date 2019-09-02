using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ClickableCanvas : MonoBehaviour
{
    
    public float allowWaitTime;

    // Normal raycasts do not work on UI elements, they require a special kind
    GraphicRaycaster raycaster;
    GameObject popUp;
    bool allowClose;

    void Awake()
    {
        // Get both of the components we need to do this
        this.raycaster = GetComponent<GraphicRaycaster>();
        allowClose = true;
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
                if (result.gameObject.tag != "Background")
                {
                    justBackgroundClicked = false;
                }
            }

            if (justBackgroundClicked)
            {
                if(popUp != null)
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
        popUp = gameObject;
        setPopUp(true);
        StartCoroutine(WaitAllowClose(allowWaitTime));
       
    }

    void setPopUp(bool active)
    {
        /*
        if (active)
        {
            Debug.Log("show pupup");
        } else
        {
            Debug.Log("close pupup");

        }
        */

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
}

