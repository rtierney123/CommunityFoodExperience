using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using UnityEngine.Rendering;
using UI;

namespace Manage
{
    public class CanvasController : Manager
    {

        public float allowWaitTime;
        public Animator warning;

        // Normal raycasts do not work on UI elements, they require a special kind
        GraphicRaycaster raycaster;
        [HideInInspector]
        public GameObject popUp;
        [HideInInspector]
        public GameObject screenOpen;
        private Queue<GameObject> popUpBackLog = new Queue<GameObject>();
        private Queue<GameObject> mainScreenOnlyBackLog = new Queue<GameObject>();

        bool allowClose;
        bool allowOpen;

        public bool endGame = false;

        private bool allowMainScreenPopups = true;
       

        void Awake()
        {
            // Get both of the components we need to do this
            this.raycaster = GetComponent<GraphicRaycaster>();
            allowClose = true;
            allowOpen = true;
            popUp = null;
            screenOpen = null;
            //popUpBackLog = new Queue<GameObject>();
            //mainScreenOnlyBackLog = new Queue<GameObject>();
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
        
        public void disableMainPopups()
        {
            allowMainScreenPopups = false;
        }

        public void enableMainPopups()
        {
            allowMainScreenPopups = true;
        }


        public void playWarning()
        {
            warning.SetBool("warning", true);
        }

        public void addToPopUpBackLog(GameObject gameObject)
        {
            if (popUp == null)
            {
                Debug.Log("open general queue screen popup");
                openPopup(gameObject);
            }
            else
            {
                Debug.Log("enqueue general popup");
                popUpBackLog.Enqueue(gameObject);
            }
            
        }

        public void addToMainScreenPopUpBackLog(GameObject gameObject)
        {
            if(popUp == null && screenOpen == null && allowMainScreenPopups)
            {
                Debug.Log("open main screen popup");
                openPopup(gameObject);
            } else
            {
                Debug.Log("add to main screen queue");
                mainScreenOnlyBackLog.Enqueue(gameObject);
            }
        }

        public void dequeueMainScreenPopUpBackLog()
        {
            Debug.Log("called dequeue main screen");
            if(popUp == null && screenOpen == null && mainScreenOnlyBackLog.Count > 0 && allowMainScreenPopups)
            {
                Debug.Log("display main screen only");
                popUp = mainScreenOnlyBackLog.Dequeue();
                setPopUp(true);
            }
        }

        public void openPopup(GameObject gameObject)
        {
            if (popUp == null && allowOpen && !endGame)
            {
                popUp = gameObject;
                View view = popUp.GetComponent<UI.PopUp>();
                if(view != null)
                {
                    view.reset();
                }
                
                setPopUp(true);
                StartCoroutine(WaitAllowClose(allowWaitTime));
            }
            else
            {
                addToPopUpBackLog(gameObject);
            }

        }

        public void forcePopupOpen(GameObject gameObject)
        {
              Debug.Log("open popup");
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
            if (popUpBackLog != null && popUpBackLog.Count > 0)
            {
                Debug.Log("dequeue standard popup backlog");
                popUp = popUpBackLog.Dequeue();
                setPopUp(true);
            }
            else if (screenOpen == null && mainScreenOnlyBackLog.Count > 0)
            {
                Debug.Log("dequeue from close popup");
                dequeueMainScreenPopUpBackLog();
            }
            else
            {
                popUp = null;
            }
        }

        public void openScreen(GameObject screen)
        {
            if (!endGame && screen != null)
            {
                closeCurrentScreen();
                screenOpen = screen;
                View view = screen.GetComponent<UI.Screen>();
                if (view != null)
                {
                    view.reset();
                }
                screen.SetActive(true);
            }
        }

        public void openPostGameScreen(GameObject screen)
        {
            closeCurrentScreen();
            screenOpen = screen;
            View view = screen.GetComponent<UI.Screen>();
            if (view != null)
            {
                view.reset();
            }
            screen.SetActive(true);
        }

        public void closeOnlyScreen(GameObject screen)
        {
            
            if (screen  == screenOpen)
            {
                screenOpen.SetActive(false);
                screenOpen = null;
            }
        }


        public void closeScreen()
        {
            closeCurrentScreen();
            checkForMainPopupBackLog();
        }


       
        public void closeCurrentScreen()
        {
            if (screenOpen != null)
            {
                screenOpen.SetActive(false);
                screenOpen = null;
                closePopUp();
               
            }
        }

        private void checkForMainPopupBackLog()
        {
            if (mainScreenOnlyBackLog.Count > 0)
            {
                dequeueMainScreenPopUpBackLog();
            }

        }

        public override void reset()
        {
            mainScreenOnlyBackLog = new Queue<GameObject>();
            popUpBackLog = new Queue<GameObject>();
            closePopUp();
            closeScreen();
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


