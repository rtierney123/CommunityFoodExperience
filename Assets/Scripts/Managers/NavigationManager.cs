using Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    //does logic for navigating the map and showing navigation related pop-ups
    public class NavigationManager : MonoBehaviour
    {
        public GameObject player;

        public CanvasController canvasController;
        public GameManager gameManager;
        public Location currentLocation;
        public Bus bus;
        public CameraPan cameraPan;
        public float locationScreenDelay;

        public Dictionary<MapLocations, Location> locationLookup;
        public List<MapLocations> locationKeys;
        public List<Location> locationValues;

        private Location possibleDestination;


        // Start is called before the first frame update
        void Start()
        {
            print("Game Start!!!");
            locationLookup = new Dictionary<MapLocations, Location>();
            if (locationKeys.Count == locationValues.Count)
            {
                for (int index = 0; index < locationKeys.Count; index += 1)
                {
                    locationLookup[locationKeys[index]] = locationValues[index];

                }
            }

        
        }

     

        // Update is called once per frame
        void Update()
        {

        }

        public void startLocationPopup(Location location)
        {
            if (!bus.playerOnBus)
            {
                if(currentLocation == location)
                {
                    StartCoroutine(OpenLocationScreen(currentLocation));
                } else
                {
                    possibleDestination = location;

                    NavigiationPopUp popUp = location.getPopUp();
                    GameObject gameObject = popUp.gameObject;
                    popUp.title.text = location.locationTitle;
                    popUp.description.text = location.locationDescription;
                    canvasController.openPopup(gameObject);
                }
        
            }
      
        }


        public void travelToDestination(TravelType travelType)
        {
            int travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            player.transform.localPosition = currentLocation.playerDropoff.position;
            gameManager.subtractTime(travelTime);
            StartCoroutine(OpenLocationScreen(currentLocation));
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
            if(canvasController.popUp == bus.farePopUp || canvasController.popUp == bus.stopPopUp)
            {
                canvasController.closePopUp();
            }

        }

        public void handleBusClickedEvent()
        {
            if (bus.playerOnBus && bus.atStop)
            {
                showLeaveBusDialog();
            }
            else if (currentLocation.mapLocation == bus.mapLocation)
            {
                canvasController.openPopup(bus.farePopUp);
            }
        }

        public void handleBusStoppedEvent()
        {
            if (bus.playerOnBus)
            {
                showLeaveBusDialog();
            }
          
        }

        private void showLeaveBusDialog()
        {
            Location busLocation;

            if (locationLookup.TryGetValue(bus.mapLocation, out busLocation))
            {
                canvasController.openPopup(bus.stopPopUp);
                canvasController.setStopTitle(bus.mapLocation.ToString());
                possibleDestination = busLocation;
            }
        }

        public void handleTakeBusEvent()
        {
            player.SetActive(false);
            bus.playerOnBus = true;
        }

        public void handleLeaveBusEvent()
        {
            currentLocation = possibleDestination;
            travelToDestination(TravelType.Bus);
            if(currentLocation.locationType == LocationType.FarLocation)
            {
                cameraPan.JumpRight();
            } else
            {
                cameraPan.JumpLeft();
            }
            player.SetActive(true);
            bus.playerOnBus = false;

        }


        private IEnumerator OpenLocationScreen(Location locations)
        {
            yield return new WaitForSeconds(locationScreenDelay);
            if (currentLocation.mainScreen != null)
            {
                canvasController.openPopup(currentLocation.mainScreen);
            }
        }



    }

}
