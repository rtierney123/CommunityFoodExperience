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
        public List<MapLocations> locationKeys;
        public List<Location> locationValues;
    

        private int timeRemaining;
        private Location possibleDestination;


        // Start is called before the first frame update
        void Start()
        {
            print("Game Start!!!");
            locationLookup = new Dictionary<MapLocations, Location>();
            if (locationKeys.Count == locationValues.Count)
            {
                for (int index = 0; index < locationKeys.Count; index += 2)
                {
                    locationLookup[locationKeys[index]] = locationValues[index];
                }
            }

            Debug.Log(locationKeys.Count);
            Debug.Log(locationValues.Count);
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
            Debug.Log("handleBus");
            if(locationLookup != null && locationLookup.TryGetValue(bus.mapLocation, out busLocation))
            {
                Debug.Log("location found");
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
