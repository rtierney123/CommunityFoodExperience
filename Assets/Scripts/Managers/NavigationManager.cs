using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    //does logic for navigating the map and showing navigation related pop-ups
    public class NavigationManager : MonoBehaviour { 
        public Player player;
        public GameObject stuckPopup; 

        public CanvasController canvasController;
        public ClockDisplay clock;
        public Location currentLocation;
        public Location startLocation;
        public Bus bus;
        public float locationScreenDelay;


        public Dictionary<MapLocations, Location> locationLookup;
        public List<MapLocations> locationKeys;
        public List<Location> locationValues;
		public bool OnBus;

        private Location possibleDestination;
        Dictionary<Tuple<string, string>, double> distmap;

        // Start is called before the first frame update
        void Start()
        {
            locationLookup = new Dictionary<MapLocations, Location>();
            if (locationKeys.Count == locationValues.Count)
            {
                for (int index = 0; index < locationKeys.Count; index += 1)
                {
                    locationLookup[locationKeys[index]] = locationValues[index];

                }
            }
            distmap = new Dictionary<Tuple<string, string>, double>();
            generateMapEdges();
        }

        public void reset() {
            currentLocation = startLocation;
            dropPlayerOff(startLocation);
        }


        public void startLocationScreen(Location location)
        {
            if (!bus.playerOnBus)
            {
                if(currentLocation == location)
                {
                    currentLocation.onEnter();
                } else
                {
                    possibleDestination = location;

                    NavigiationPopUp popUp = location.getPopUp();
                    GameObject gameObject = popUp.gameObject;
                    popUp.title.text = location.locationTitle;
                    popUp.description.text = location.locationDescription;

                    double inGameHours = 12 + clock.pmEndTime - clock.amStartTime;
                    double multiplier = 60 * 60 * inGameHours / clock.runtimeMiliSeconds * 1000;
                    popUp.carText.text = "Car "+formatTime(calculateTravelTime()* multiplier);
                    if(location.locationType == LocationType.NearbyLocation)
                    {
                        popUp.walkText.text = "Walk "+formatTime(calculateTravelTime()*2.3* multiplier);
                    }
                    canvasController.openPopup(gameObject);
                }
            }     
        }

        private string formatTime(double min)
        {
            min = Math.Floor(min);
            if (min < 60)
            {
                return String.Format("({0}min)", min);
            }
            int h = (int) Math.Floor(min / 60);
            int m = (int) min - h * 60;
            return String.Format("({0}hr {1}min)",h, m);
        }

        public void travelToDestination(TravelType travelType)
        {
            double travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            dropPlayerOff(currentLocation);
            clock.addRunningTime(travelTime);
            currentLocation.onEnter();

            player.setFreeRide(false);
        }

        private void dropPlayerOff(Location location)
        {
            player.transform.position = location.playerDropoff.position;
        }

        public void closePopUp()
        {
            canvasController.closePopUp();
        }

        public double calculateTravelTime()
        {
            //Debug.Log(currentLocation.locationTitle + "->" + possibleDestination.locationTitle);
            //constant 3 minutes right now

            if (distmap.ContainsKey(Tuple.Create(currentLocation.locationTitle, 
                possibleDestination.locationTitle)))
            {
                return distmap[Tuple.Create(currentLocation.locationTitle,
                possibleDestination.locationTitle)];
            }
            if (distmap.ContainsKey(Tuple.Create(possibleDestination.locationTitle,
                currentLocation.locationTitle)))
            {
                return distmap[Tuple.Create(possibleDestination.locationTitle,
                currentLocation.locationTitle)];
            }
            return 0;
        }

        public void handleTakeCar()
        {
            travelToDestination(TravelType.Car);
        }

        public void handleTakeWalk()
        {
            travelToDestination(TravelType.Walk);
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
            player.gameObject.SetActive(false);
            bus.playerOnBus = true;
            Debug.Log("take bus");
			OnBus = true;
        }

        public void handleLeaveBusEvent()
        {
            currentLocation = possibleDestination;
            travelToDestination(TravelType.Bus);
            player.gameObject.SetActive(true);
            bus.playerOnBus = false;
            Debug.Log("leave bus");
			OnBus = false;
        }

        public void displayIfStuck()
        {
            Debug.Log("show stuck");
            Debug.Log(player.hasNoModeOfTransportation());
            Debug.Log(currentLocation.locationType == LocationType.FarLocation);
            Debug.Log(currentLocation);
            if (player.hasNoModeOfTransportation() && currentLocation.locationType == LocationType.FarLocation)
            {
                Debug.Log("showing stuck popup");
                canvasController.forcePopupOpen(stuckPopup);
            }
        }

        private void generateMapEdges()
        {
            double scale = .08f;

            distmap.Add(Tuple.Create("House", "Community Frood Kitchen"), 2 * scale);
            distmap.Add(Tuple.Create("House", "Mo's Corner Store"), 3 * scale);
            distmap.Add(Tuple.Create("House", "Bus stop"), 4 * scale);
            distmap.Add(Tuple.Create("House", "Food Tiger"), 7 * scale);
            distmap.Add(Tuple.Create("House", "Vita Services"), 9 * scale);
            distmap.Add(Tuple.Create("House", "Food Pantry"), 10 * scale);
            distmap.Add(Tuple.Create("House", "WIC Clinic"), 9 * scale);
            distmap.Add(Tuple.Create("House", "Snap Office"), 8 * scale);

            distmap.Add(Tuple.Create("Community Food Kitchen", "Mo's Corner Store"), 3 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Bus stop"), 3 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Food Tiger"), 6 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Vita Services"), 8 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Food Pantry"), 9 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "WIC Clinic"), 8 * scale);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Snap Office"), 7 * scale);

            distmap.Add(Tuple.Create("Mo's Corner Store", "Bus stop"), 3 * scale);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Food Tiger"), 6 * scale);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Vita Services"), 8 * scale);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Food Pantry"), 9 * scale);
            distmap.Add(Tuple.Create("Mo's Corner Store", "WIC Clinic"), 8 * scale);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Snap Office"), 7 * scale);

            distmap.Add(Tuple.Create("Bus stop", "Food Tiger"), 3 * scale);
            distmap.Add(Tuple.Create("Bus stop", "Vita Services"), 5 * scale);
            distmap.Add(Tuple.Create("Bus stop", "Food Pantry"), 6 * scale);
            distmap.Add(Tuple.Create("Bus stop", "WIC Clinic"), 5 * scale);
            distmap.Add(Tuple.Create("Bus stop", "Snap Office"), 4 * scale);

            distmap.Add(Tuple.Create("Food Tiger", "Vita Services"), 2 * scale);
            distmap.Add(Tuple.Create("Food Tiger", "Food Pantry"), 3 * scale);
            distmap.Add(Tuple.Create("Food Tiger", "WIC Clinic"), 2 * scale);
            distmap.Add(Tuple.Create("Food Tiger", "Snap Office"), 1 * scale);

            distmap.Add(Tuple.Create("Vita Services", "Food Pantry"), 2 * scale);
            distmap.Add(Tuple.Create("Vita Services", "WIC Clinic"), 3 * scale);
            distmap.Add(Tuple.Create("Vita Services", "Snap Office"), 1 * scale);

            distmap.Add(Tuple.Create("Food Pantry", "WIC Clinic"), 1 * scale);
            distmap.Add(Tuple.Create("Food Pantry", "Snap Office"), 2.5 * scale);

            distmap.Add(Tuple.Create("WIC Clinic", "Snap Office"), 3 * scale);
        }

    }

}
