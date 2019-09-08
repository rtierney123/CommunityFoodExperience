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
        public Bus bus;
        public CameraPan cameraPan;

        public Dictionary<MapLocations, Location> locationLookup;
        [SerializeField] List<MapLocations> locationKeys;
        [SerializeField] List<Location> gameobjectValues;
    

        private int timeRemaining;
        private Location possibleDestination;
        private bool gameStarted = false;

        // Start is called before the first frame update
        void Start()
        {
            print("Game Start!!!");


        }

        private void Awake()
        {
            gameStarted = true;
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


        public void handleBusLeavingEvent()
        {
            canvasController.closePopUp();
         
        }

        public void handleBusClickedEvent()
        {
            if (currentLocation.mapLocation == bus.mapLocation)
            {
                canvasController.openPopup(bus.farePopUp);
            }
        }

        public void handleBusStoppedEvent()
        {
            Location busLocation;
            if(locationLookup != null && locationLookup.TryGetValue(bus.mapLocation, out busLocation))
            {
                canvasController.openPopup(bus.stopPopUp);
                canvasController.setStopTitle(currentLocation.name);
            }
        }

        public void handleTakeBusEvent()
        {
            player.SetActive(false);
            bus.playerOnBus = true;
        }

        public void handleLeaveBusEvent()
        {
            player.transform.localPosition = currentLocation.playerDropoff.position;
            player.SetActive(true);
            bus.playerOnBus = false;

        }




    }

}
