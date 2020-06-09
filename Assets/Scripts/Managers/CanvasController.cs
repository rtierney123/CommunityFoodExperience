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
        public float delayOpenTime;
        public Animator warning;

        [HideInInspector]
        public GameObject popUp;
        [HideInInspector]
        public GameObject screenOpen;
        [HideInInspector]
        public bool endGame = false;

        private GraphicRaycaster raycaster;
        private Queue<GameObject> popUpBackLog = new Queue<GameObject>();
        private Queue<GameObject> mainScreenOnlyBackLog = new Queue<GameObject>();
        private bool allowClose;
        private bool allowOpen;
        private bool allowMainScreenPopups = true;
       

        void Awake()
        {
            // Get both of the components we need to do this
            this.raycaster = GetComponent<GraphicRaycaster>();
            allowClose = true;
            allowOpen = true;
            popUp = null;
            screenOpen = null;
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

                /*
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
                */
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
                openPopup(gameObject);
            }
            else
            {
                View view = gameObject.GetComponent<UI.PopUp>();
                if (view != null)
                {
                    view.reset();
                }
                popUpBackLog.Enqueue(gameObject);
            }
            
        }

        public void addToMainScreenPopUpBackLog(GameObject gameObject)
        {
            if(popUp == null && screenOpen == null && allowMainScreenPopups)
            {
                openPopup(gameObject);
            } else
            {
                View view = gameObject.GetComponent<UI.PopUp>();
                if (view != null)
                {
                    view.reset();
                }
                mainScreenOnlyBackLog.Enqueue(gameObject);
            }
        }

        public void dequeueMainScreenPopUpBackLog()
        {

            if (popUp == null && screenOpen == null && mainScreenOnlyBackLog.Count > 0 && allowMainScreenPopups)
            {
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


        }

        public void delayOpenMainScreenPopup(GameObject gameObject)
        {
            disableMainPopups();
            StartCoroutine(delayOpenMainScreenPopupRoutine(gameObject));
        }

        private IEnumerator delayOpenMainScreenPopupRoutine(GameObject gameObject)
        {
            yield return new WaitForSeconds(delayOpenTime);
            enableMainPopups();
            if (gameObject != null)
            {
                addToMainScreenPopUpBackLog(gameObject);
            } else
            {
                dequeueMainScreenPopUpBackLog();
            }
            
        }

        public void delayOpenScreen(GameObject gameObject)
        {
            disableMainPopups();
            StartCoroutine(delayOpenScreenRoutine(gameObject));
        }

        private IEnumerator delayOpenScreenRoutine(GameObject gameObject)
        {
            yield return new WaitForSeconds(delayOpenTime);
            if (gameObject != null)
            {
                closePopUp();
                openScreen(gameObject);
                enableMainPopups();
            }
            else
            {
                enableMainPopups();
                dequeueMainScreenPopUpBackLog();
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
            if (popUpBackLog != null && popUpBackLog.Count > 0)
            {
                popUp = popUpBackLog.Dequeue();
                setPopUp(true);
            }
            else if (screenOpen == null && mainScreenOnlyBackLog.Count > 0)
            {
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
                disableMainPopups();
                closeCurrentScreen();
                View view = screen.GetComponent<UI.Screen>();
                if (view != null)
                {
                    view.reset();
                }
                screenOpen = screen;
                screen.SetActive(true);
                enableMainPopups();
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
            //dequeueMainScreenPopUpBackLog();
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


