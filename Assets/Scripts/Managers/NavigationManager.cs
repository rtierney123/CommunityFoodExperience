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
        public Dictionary<MapLocations, string> locationNameDict;
        public List<MapLocations> locationKeys;
        public List<string> locationTitles;
        public List<Location> locationValues;
		public bool OnBus;

        private Location possibleDestination;
        Dictionary<Tuple<string, string>, double> distmap;
        public double scale = .18; // 7min@.18
        double carWalkRatio = 2.3;

        // Start is called before the first frame update
        void Start()
        {
            locationLookup = new Dictionary<MapLocations, Location>();
            locationNameDict = new Dictionary<MapLocations, string>();
            if (locationKeys.Count == locationValues.Count)
            {
                for (int index = 0; index < locationKeys.Count; index += 1)
                {
                    locationLookup[locationKeys[index]] = locationValues[index];
                    locationNameDict[locationKeys[index]] = locationTitles[index];
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

                    
                    popUp.carText.text = "Car ("+formatTime(realToGameTime(calculateTravelTime())) + ")";
                    if(location.locationType == LocationType.NearbyLocation)
                    {
                        popUp.walkText.text = "Walk ("+formatTime(realToGameTime(calculateTravelTime()) * carWalkRatio)+ ")";
                    }
                    canvasController.openPopup(gameObject);
                }
            }     
        }

        public double realToGameTime(double realTime)
        {
            double inGameHours = 12 + clock.pmEndTime - clock.amStartTime;
            double multiplier = 60 * 60 * inGameHours / clock.runtimeMiliSeconds * 1000;
            return realTime * multiplier;
        }

        public string formatTime(double min)
        {
            min = Math.Floor(min);
            if (min < 60)
            {
                return String.Format("{0}min", min);
            }
            int h = (int) Math.Floor(min / 60);
            int m = (int) min - h * 60;
            return String.Format("{0}hr {1}min",h, m);
        }

        public void travelToDestination(TravelType travelType)
        {
            // scaled value from distmap
            double travelTime = calculateTravelTime();
            currentLocation = possibleDestination;

            dropPlayerOff(currentLocation);
            if (travelType == TravelType.Car)
            {
                clock.addRunningTime(travelTime/scale);
            } else if (travelType == TravelType.Walk)
            {
                clock.addRunningTime(travelTime / scale * carWalkRatio);
            }
            currentLocation.onEnter();

            player.setFreeRide(false);
        }

        private void dropPlayerOff(Location location)
        {
            if(location.mapLocation == MapLocations.House)
            {
                player.setIsHome(true);
            } else
            {
                player.setIsHome(false);
            }
            player.transform.position = location.playerDropoff.position;
        }

        public void closePopUp()
        {
            canvasController.closePopUp();
        }

        public double calculateTravelTime()
        {

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
            return -1;
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
                canvasController.setStopTitle(locationNameDict[bus.mapLocation]);
                possibleDestination = busLocation;
            }
        }

        public void handleTakeBusEvent()
        {
            player.gameObject.SetActive(false);
            bus.playerOnBus = true;
			OnBus = true;
        }

        public void handleLeaveBusEvent()
        {
            currentLocation = possibleDestination;
            travelToDestination(TravelType.Bus);
            player.gameObject.SetActive(true);
            bus.playerOnBus = false;
			OnBus = false;
        }

        public void displayIfStuck()
        {
            if (player.hasNoModeOfTransportation() && currentLocation.locationType == LocationType.FarLocation)
            {
                canvasController.forcePopupOpen(stuckPopup);
            }
        }

        private void generateMapEdges()
        {
            distmap.Add(Tuple.Create("House", "Community Food Kitchen"), 2 * scale);
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
