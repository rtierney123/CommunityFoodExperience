using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

namespace Manage
{
    public class CanvasController : MonoBehaviour
    {

        public float allowWaitTime;
        public Text busStopTitle;
        public Animator warning;

        // Normal raycasts do not work on UI elements, they require a special kind
        GraphicRaycaster raycaster;
        [HideInInspector]
        public GameObject popUp;
        private Queue<GameObject> popUpBackLog;
        private Queue<GameObject> mainScreenOnlyBackLog;

        bool allowClose;
        bool allowOpen;

        public bool endGame = false;

        private GameObject screenOpen;
        [HideInInspector]
        public bool screenDisplayed;
        void Awake()
        {
            // Get both of the components we need to do this
            this.raycaster = GetComponent<GraphicRaycaster>();
            allowClose = true;
            allowOpen = true;
            popUp = null;
            screenDisplayed = false;
            popUpBackLog = new Queue<GameObject>();
            mainScreenOnlyBackLog = new Queue<GameObject>();
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
                    if (result.gameObject.tag == "Popup")
                    {
                        justBackgroundClicked = false;
                    }
                }

                if (justBackgroundClicked)
                {
                    if (popUp != null)
                    {
                        if (allowClose)
                        {
                            closePopUp();
                        }

                    }
                }
            }
        }

        public void playWarning()
        {
            warning.Play("Warning", -1, 0f);
        }

        public void addToPopUpBackLog(GameObject gameObject)
        {
            if (popUp == null)
            {
                openPopup(gameObject);
            }
            else
            {
                popUpBackLog.Enqueue(gameObject);
            }
            
        }

        public void addToMainScreenPopUpBackLog(GameObject gameObject)
        {
            if(popUp == null && !screenDisplayed)
            {
                forcePopupOpen(gameObject);
                Debug.Log("main screen popup");
            } else
            {
                mainScreenOnlyBackLog.Enqueue(gameObject);
                Debug.Log("enqueue");
            }
        }

        public void openPopup(GameObject gameObject)
        {
            if (popUp == null && allowOpen && !endGame)
            {
                popUp = gameObject;
                setPopUp(true);
                StartCoroutine(WaitAllowClose(allowWaitTime));
            } 


        }

        public void forcePopupOpen(GameObject gameObject)
        {
            if (!endGame)
            {
                setPopUp(false);
                popUp = gameObject;
                setPopUp(true);
                StartCoroutine(WaitAllowClose(allowWaitTime));
            }
            
        }

        public void closePopUp(GameObject gameObject)
        {
            if(popUp == gameObject)
            {
                closePopUp();
            }
        }

        public void closePopUp()
        {
            setPopUp(false);
            StartCoroutine(WaitAllowOpen(allowWaitTime));
            if(popUpBackLog!= null && popUpBackLog.Count > 0)
            {
                popUp = popUpBackLog.Dequeue();
                setPopUp(true);
            } else
            {
                if(!screenDisplayed && mainScreenOnlyBackLog.Count > 0)
                {
                    popUp = mainScreenOnlyBackLog.Dequeue();
                    setPopUp(true);
                } else
                {
                    popUp = null;
                }
            }
        }

        public void openScreen(GameObject screen)
        {
            if (!endGame)
            {
                screenDisplayed = true;
                closeWithoutBool();
                screenOpen = screen;
                screen.SetActive(true);
            }
        }

        public void openPostGameScreen(GameObject screen)
        {
            Debug.Log("open post");
            screenDisplayed = true;
            closeWithoutBool();
            screenOpen = screen;
            screen.SetActive(true);
        }

        private void closeWithoutBool()
        {
            if (screenOpen != null)
            {
                screenOpen.SetActive(false);
                if (popUp != null)
                {
                    addToMainScreenPopUpBackLog(popUp);
                }
                closePopUp();
                if (mainScreenOnlyBackLog.Count > 0)
                {
                    popUp = mainScreenOnlyBackLog.Dequeue();
                    setPopUp(true);
                }
            }
        }

        public void closeScreen()
        {
            if(screenOpen != null)
            {
                screenOpen.SetActive(false);
                screenDisplayed = false;
                closePopUp();
                if (mainScreenOnlyBackLog.Count > 0)
                {
                    popUp = mainScreenOnlyBackLog.Dequeue();
                    setPopUp(true);
                }
            } 
          
        }

        public void closeScreen(GameObject screen)
        {
            screen.SetActive(false);
            screenOpen = null;
            screenDisplayed = false;
            closePopUp();
            if (mainScreenOnlyBackLog.Count > 0)
            {
                popUp = mainScreenOnlyBackLog.Dequeue();
                setPopUp(true);
            }
        }

        public void setStopTitle(string title)
        {
            busStopTitle.text = title;
        }


        void setPopUp(bool active)
        { 
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

        private IEnumerator WaitAllowOpen(float waitTime)
        {
            allowOpen = false;
            yield return new WaitForSeconds(waitTime);
            allowOpen = true;
        }
       

    }
}


