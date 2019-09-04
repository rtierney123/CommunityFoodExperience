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
        private Location currentLocation;
        private Location possibleDestination;

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
            possibleDestination = location;

            GameObject popUp = location.getPopUp();
            canvasController.openPopup(popUp);
        }

        public void travelToDestination(TravelType travelType)
        {
            int travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            player.localPosition = currentLocation.playerDropoff;
            timeRemaining = timeRemaining - travelTime;
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
            Debug.Log("stop");
        }


    }

}
