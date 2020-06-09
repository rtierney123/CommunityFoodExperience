using Model;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    //does logic for navigating the map and showing navigation related pop-ups
    public class NavigationManager : Manager { 
        public Player player;
        

        public CanvasController canvasController;
        public MessageManager messageManager;
        public GameManager gameManager;
        public ClockDisplay clock;
        public Location currentLocation;
        public Location startLocation;
        [HideInInspector]
        public Location possibleDestination;

        public Dictionary<Neighborhood, Location> locationStopDict;
        public List<Neighborhood> locationKeys;
        public List<Location> locationBusStops;

        public NavigationPopUp navigationPopup;
        public GameObject takingBusScreen;
        

        private TravelCalculator travelCalculator;

        private bool routeSelected = false;

        void Start()
        {
            locationStopDict = new Dictionary<Neighborhood, Location>();
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

            travelCalculator = new TravelCalculator();
 
        }

        public override void reset() {
            currentLocation = startLocation;
            dropPlayerOff(startLocation);
            disableBusStopHighlights();
        }


        public void startLocationScreen(Location location)
        {
            if (!player.onBus)
            {
                if(currentLocation == location)
                {

                    location.onImmediateEnter();

                } else
                {
                    possibleDestination = location;

                    if (possibleDestination.neighborhood == currentLocation.neighborhood)
                    {
                        navigationPopup.walkText.text = "Walk (" + formatTime( getPotentialTravelTime(TravelType.Walk)) + ")";
                        navigationPopup.activateWalkButton();
                        navigationPopup.enableWalkButton();
                    }
                    else
                    {
                        navigationPopup.deactivateWalkButton();
                    }

                    if(player.hasTemporaryRide || (player.playerInfo.hasCar && !player.carBrokenDown))
                    {
                        navigationPopup.enableCarButton();
                    }
                    else
                    {
                        navigationPopup.disableCarButton();
                    }

                    navigationPopup.title.text = location.locationTitle;
                    navigationPopup.description.text = location.locationDescription;
                    navigationPopup.carText.text = "Car (" + formatTime( getPotentialTravelTime(TravelType.Car)) + ")";

                    GameObject gameObject = navigationPopup.gameObject;
                    canvasController.openPopup(gameObject);

                }
            }  
            else if (!routeSelected) {
                if (location.busAvailable)
                {
                    canvasController.openScreen(takingBusScreen);
                    possibleDestination = location;
                    routeSelected = true;
                    disableBusStopHighlights();
                }

            }
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

        public void handleCarTravel()
        {
            travelToDestination(TravelType.Car);
        }

        public void handleWalkTravel()
        {
            travelToDestination(TravelType.Walk);
        }

        public void handleBusArrived()
        {
            travelToDestination(TravelType.Bus);
        }

        public void handleChooseStopEvent()
        {
            player.gameObject.SetActive(false);
            player.onBus = true;
            routeSelected = false;
            enableBusStopHighlights();
            canvasController.disableMainPopups();
        }

        public void travelToDestination(TravelType travelType)
        {
            canvasController.disableMainPopups();
            canvasController.closeScreen();
            canvasController.closePopUp();
            double travelTime = getPotentialTravelTime(travelType);
            currentLocation = possibleDestination;

            dropPlayerOff(currentLocation);
            clock.addGameMinutes(travelTime);

            player.setFreeRide(false);

            if (travelType != TravelType.Bus)
            {
                currentLocation.onDelayedEnter();
            } else
            {
                canvasController.enableMainPopups();
                canvasController.dequeueMainScreenPopUpBackLog();
            }

        }

        private void dropPlayerOff(Location location)
        {
            player.onBus = false;
           
            if (location == startLocation)
            {
                player.setIsHome(true);
            } else
            {
                player.setIsHome(false);
            }
            player.gameObject.SetActive(true);
            player.transform.position = new Vector3(location.playerDropoff.position.x, location.playerDropoff.position.y, 0);
        }

        private double getPotentialTravelTime(TravelType travelType)
        {
            return travelCalculator.calculateTravelTime(currentLocation.locationId, possibleDestination.locationId, travelType);
        }


 

        private void disableBusStopHighlights()
        {
            messageManager.hideHintMessage();
            foreach (Location busStop in locationBusStops)
            {
                busStop.endManualHighlight();
            }
        }

        private void enableBusStopHighlights()
        {
            messageManager.displayHintMessage(Status.clickStopInstruction);
            foreach (Location busStop in locationBusStops)
            {
                busStop.startManualHighlight();
            }
        }


        

    }

}
