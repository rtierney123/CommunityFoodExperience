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
        public NavigationPopUp navigationPopup;

        public CanvasController canvasController;
        public MessageManager messageManager;
        public GameManager gameManager;
        public ClockDisplay clock;
        public Location currentLocation;
        public Location startLocation;
        [HideInInspector]
        public Location possibleDestination;

        public Bus bus;
        public float locationScreenDelay;

        public Dictionary<MapLocations, Location> locationStopDict;
        public List<MapLocations> locationKeys;
        public List<Location> locationBusStops;

        public double walkScale;
        public double carScale;

        
        Dictionary<Tuple<string, string>, double> distmap;

       

        // Start is called before the first frame update
        void Start()
        {
            locationStopDict = new Dictionary<MapLocations, Location>();
            if (locationKeys.Count == locationBusStops.Count)
            {
                for (int index = 0; index < locationKeys.Count; index += 1)
                {
                    locationStopDict[locationKeys[index]] = locationBusStops[index];
                }
            }
            else
            {
                Debug.Log("Number of keys must match the number of bus stop locations");
            }
            distmap = new Dictionary<Tuple<string, string>, double>();
            generateMapEdges();
        }

        public void reset() {
            currentLocation = startLocation;
            dropPlayerOff(startLocation);
        }

        public bool getHasCar()
        {
            return player.playerInfo.hasCar || player.hasTemporaryRide;
        }


        public void startLocationScreen(Location location)
        {
            if (!bus.playerOnBus)
            {
                if(currentLocation == location)
                {
                    location.onEnter();
                    if (currentLocation == startLocation)
                    {
                        displayHomePopup();

                    }
                } else
                {
                    possibleDestination = location;
                    //NavigationPopUp popUp;
                    if ((possibleDestination.mapLocation == currentLocation.mapLocation ) || (possibleDestination.locationType == LocationType.NearbyLocation && currentLocation.locationType== LocationType.NearbyLocation))
                    {
                        navigationPopup.walkText.text = "Walk (" + formatTime(calculateTravelTime(TravelType.Walk)) + ")";
                        navigationPopup.activateWalkButton();
                        navigationPopup.enableWalkButton();
                    }
                    else
                    {
                        navigationPopup.deactivateWalkButton();
                    }

                    if(player.hasTemporaryRide || player.playerInfo.hasCar)
                    {
                        navigationPopup.enableCarButton();
                    }
                    else
                    {
                        navigationPopup.disableCarButton();
                    }

                    navigationPopup.title.text = location.locationTitle;
                    navigationPopup.description.text = location.locationDescription;
                    navigationPopup.carText.text = "Car (" + formatTime(calculateTravelTime(TravelType.Car)) + ")";

                    GameObject gameObject = navigationPopup.gameObject;
                    canvasController.openPopup(gameObject);

                }
            }  
            else if (!bus.stopSelected) {
                if (location.busAvailable)
                {
                    possibleDestination = location;
                    bus.stopSelected = true;
                    messageManager.hideHintMessage();
                    handleStartBusEvent();
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

            closePopUp();
            // scaled value from distmap
            double travelTime = calculateTravelTime(travelType);
            currentLocation = possibleDestination;

            dropPlayerOff(currentLocation);

            //canvasController.screenDisplayed = true;
            clock.addGameMinutes(travelTime);
            
            //currentLocation.onEnter();

            player.setFreeRide(false);

            if(currentLocation == startLocation)
            {
                displayHomePopup();
                
            }

        }

        private void displayHomePopup()
        {
            if (player.getAchievedNutrition())
            {
                messageManager.generateHomePopup(Status.homeSufficientFood);
            }
            else
            {
                messageManager.generateHomePopup(Status.homeInsufficientFood);
            }
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

        public double calculateTravelTime(TravelType travelType)
        {
            double travelTime = 0;
            if (distmap.ContainsKey(Tuple.Create(currentLocation.locationTitle,
                possibleDestination.locationTitle)))
            {
                travelTime = distmap[Tuple.Create(currentLocation.locationTitle,
                possibleDestination.locationTitle)];
            }
            else if (distmap.ContainsKey(Tuple.Create(possibleDestination.locationTitle,
                currentLocation.locationTitle)))
            {
                travelTime = distmap[Tuple.Create(possibleDestination.locationTitle,
                currentLocation.locationTitle)];
            }

            if (travelType == TravelType.Walk)
            {
                return travelTime * walkScale;
            } else if (travelType == TravelType.Car)
            {
                return travelTime * carScale;
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

        public void handleBusContinuingEvent()
        {
            Debug.Log("bus continueing");
        }

        public void handleBusClickedEvent()
        {
            Debug.Log("bus clicked");
        }

        public void handleBusStoppedEvent(MapLocations currentLocation)
        {
            Debug.Log("bus at stop");
            if(currentLocation == possibleDestination.mapLocation)
            {
                handleLeaveBusEvent();
            }
        }

        public void handleBusArrived()
        {
            bus.pauseAnimation();
            MainBusStopLocation busStop = (MainBusStopLocation)currentLocation;
            bus.setLocation(busStop.busStartLocation);
        }


        public void handleChooseStopEvent()
        {
            messageManager.displayHintMessage("Click on a stop to have the bus drop you off there.");
            bus.stopSelected = false;
            player.gameObject.SetActive(false);
            bus.playerOnBus = true;
            player.onBus = true;
        }

        public void handleStartBusEvent()
        {
            MainBusStopLocation busStop = (MainBusStopLocation)currentLocation;
            bus.playAnimation(busStop.busAnimationOffset);
            
        }

        public void handleLeaveBusEvent()
        {
            Debug.Log("leave bus");
            currentLocation = possibleDestination;
            travelToDestination(TravelType.Bus);
            player.gameObject.SetActive(true);
            bus.playerOnBus = false;
            player.onBus = false;
        }


        public void setPossibleLocation(Location nextLocation)
        {
            //Debug.Log("set new destinations");
            possibleDestination = nextLocation;
        }

        public void displayIfStuck()
        {
            if (player.hasNoModeOfTransportation() && currentLocation.locationType == LocationType.FarLocation)
            {
                canvasController.addToMainScreenPopUpBackLog(stuckPopup);
            }
        }


        private void generateMapEdges()
        {
            distmap.Add(Tuple.Create("House", "Community Food Kitchen"), 2 );
            distmap.Add(Tuple.Create("House", "Mo's Corner Store"), 3 );
            distmap.Add(Tuple.Create("House", "Bus stop"), 4 );
            distmap.Add(Tuple.Create("House", "Food Tiger"), 7 );
            distmap.Add(Tuple.Create("House", "Vita Services"), 9);
            distmap.Add(Tuple.Create("House", "Food Pantry"), 10 );
            distmap.Add(Tuple.Create("House", "WIC Clinic"), 9);
            distmap.Add(Tuple.Create("House", "Snap Office"), 8);

            distmap.Add(Tuple.Create("Community Food Kitchen", "Mo's Corner Store"), 3 );
            distmap.Add(Tuple.Create("Community Food Kitchen", "Bus stop"), 3);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Food Tiger"), 6);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Vita Services"), 8);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Food Pantry"), 9);
            distmap.Add(Tuple.Create("Community Food Kitchen", "WIC Clinic"), 8);
            distmap.Add(Tuple.Create("Community Food Kitchen", "Snap Office"), 7);

            distmap.Add(Tuple.Create("Mo's Corner Store", "Bus stop"), 3);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Food Tiger"), 6);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Vita Services"), 8);
            distmap.Add(Tuple.Create("Mo's Corner Store", "Food Pantry"), 9);
            distmap.Add(Tuple.Create("Mo's Corner Store", "WIC Clinic"), 8 );
            distmap.Add(Tuple.Create("Mo's Corner Store", "Snap Office"), 7);

            distmap.Add(Tuple.Create("Bus stop", "Food Tiger"), 3);
            distmap.Add(Tuple.Create("Bus stop", "Vita Services"), 5);
            distmap.Add(Tuple.Create("Bus stop", "Food Pantry"), 6);
            distmap.Add(Tuple.Create("Bus stop", "WIC Clinic"), 5);
            distmap.Add(Tuple.Create("Bus stop", "Snap Office"), 4 );

            distmap.Add(Tuple.Create("Food Tiger", "Vita Services"), 2);
            distmap.Add(Tuple.Create("Food Tiger", "Food Pantry"), 3);
            distmap.Add(Tuple.Create("Food Tiger", "WIC Clinic"), 2);
            distmap.Add(Tuple.Create("Food Tiger", "Snap Office"), 1);

            distmap.Add(Tuple.Create("Vita Services", "Food Pantry"), 2 );
            distmap.Add(Tuple.Create("Vita Services", "WIC Clinic"), 3);
            distmap.Add(Tuple.Create("Vita Services", "Snap Office"), 0);

            distmap.Add(Tuple.Create("Food Pantry", "WIC Clinic"), 0);
            distmap.Add(Tuple.Create("Food Pantry", "Snap Office"), 2.5);

            distmap.Add(Tuple.Create("WIC Clinic", "Snap Office"), 3);
        }

    }

}
