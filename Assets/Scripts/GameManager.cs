using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    
    public class GameManager : MonoBehaviour
    {
        public Transform player;
        public CanvasController canvasController;

        private int timeRemaining;
        public Location currentLocation;
        private Location possibleDestination;
        private bool busPopupOpen;
        private bool busAtStop;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {


        }

        public void startLocationPopup(Location location)
        {
            if (!canvasController.popUpOpen)
            {
                possibleDestination = location;

                GameObject popUp = location.getPopUp();
                canvasController.openPopup(popUp);
            }
           
        }

        public void travelToDestination(TravelType travelType)
        {
            int travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            player.localPosition = currentLocation.playerDropoff;
            timeRemaining = timeRemaining - travelTime;
        }

        private void closePopup()
        {
            canvasController.closePopUp();
        }


        private int calculateTravelTime()
        {
            return 0;
        }
        
        public void handleBusStopEvent()
        {
           // Debug.Log("stop");
            busAtStop = true;
        }

        public void handleBusLeavingEvent()
        {
           // Debug.Log("leave stop");
            busAtStop = false;
            if (busPopupOpen)
            {
                canvasController.closePopUp();
                busPopupOpen = false;
            }
        }

        public void handleBusClickedEvent(Bus bus)
        {
            if (currentLocation.location == bus.Location)
            {
                canvasController.openPopup(bus.popUp);
                busPopupOpen = true;
            }
        }



    }

}
