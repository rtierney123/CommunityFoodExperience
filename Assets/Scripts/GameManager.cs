using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    
    public class GameManager : MonoBehaviour
    {
        public GameObject player;
        public CanvasController canvasController;
        public Location currentLocation;

        private int timeRemaining;
        private Location possibleDestination;
        private bool busPopupOpen;
        private bool busAtStop;

        // Start is called before the first frame update
        void Start()
        {
            print("Game Start!!!");
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void startLocationPopup(Location location)
        {
            possibleDestination = location;

            GameObject popUp = location.getPopUp();
            canvasController.openPopup(popUp);
        }

        public void travelToDestination(TravelType travelType)
        {
            int travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            player.transform.localPosition = currentLocation.playerDropoff.position;
            timeRemaining = timeRemaining - travelTime;
            //add open pop-up for currentLocation
        }

        public void closePopUp()
        {
            canvasController.closePopUp();
        }

        private int calculateTravelTime()
        {
            return 0;
        }

        public void handleBusStopEvent()
        {
            busAtStop = true;
        }

        public void handleBusLeavingEvent()
        {
            busAtStop = false;
            if (busPopupOpen)
            {
                canvasController.closePopUp();
                busPopupOpen = false;
            }
        }

        public void handleBusClickedEvent(Bus bus)
        {
            if (currentLocation.mapLocation == bus.mapLocation)
            {
                canvasController.openPopup(bus.popUp);
                busPopupOpen = true;
            }
        }

        public void handleTakeBusEvent()
        {
            player.SetActive(false);
        }

        public void handleLeaveBusEvent()
        {
            player.transform.localPosition = currentLocation.playerDropoff.position;
            player.SetActive(true);
        }




    }

}
