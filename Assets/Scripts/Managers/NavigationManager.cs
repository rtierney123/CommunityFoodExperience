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

        public float locationScreenDelay;

        public Dictionary<Neighborhood, Location> locationStopDict;
        public List<Neighborhood> locationKeys;
        public List<Location> locationBusStops;

        public NavigationPopUp navigationPopup;
        public GameObject takingBusScreen;
        public GameObject homePopup;

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
        }


        public void startLocationScreen(Location location)
        {
            if (!player.onBus)
            {
                if(currentLocation == location)
                {

                    location.onImmediateEnter();
                    if (currentLocation == startLocation)
                    {
                        Debug.Log("display home when there");
                        canvasController.addToMainScreenPopUpBackLog(homePopup);

                    }
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
                    messageManager.hideHintMessage();
                    
                    foreach (Location busStop in locationBusStops)
                    {
                        busStop.endManualHighlight();
                    }

                    canvasController.enableMainPopups();
                    canvasController.dequeueMainScreenPopUpBackLog();
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
            //canvasController.disableMainPopups();
            travelToDestination(TravelType.Car);
        }

        public void handleWalkTravel()
        {
            //canvasController.disableMainPopups();
            travelToDestination(TravelType.Walk);
        }

        public void travelToDestination(TravelType travelType)
        {
            canvasController.disableMainPopups();
            closePopUp();
            // scaled value from distmap
            double travelTime = getPotentialTravelTime(travelType);
            currentLocation = possibleDestination;

            dropPlayerOff(currentLocation);
            clock.addGameMinutes(travelTime);
            
            if(travelType != TravelType.Bus)
            {
                currentLocation.onDelayedEnter();
            }

            player.setFreeRide(false);

            if(currentLocation == startLocation)
            {
                canvasController.delayOpenPopup(homePopup);

            }

        }

        private void dropPlayerOff(Location location)
        {
            if(location == startLocation)
            {
                player.setIsHome(true);
            } else
            {
                player.setIsHome(false);
            }
            player.transform.position = new Vector3(location.playerDropoff.position.x, location.playerDropoff.position.y, 0);
        }

        private double getPotentialTravelTime(TravelType travelType)
        {
            return travelCalculator.calculateTravelTime(currentLocation.locationId, possibleDestination.locationId, travelType);
        }

        public void closePopUp()
        {
            canvasController.closePopUp();
        }


        public void handleBusContinuingEvent()
        {
            Debug.Log("bus continueing");
        }

        public void handleBusClickedEvent()
        {
            Debug.Log("bus clicked");
        }

        public void handleBusStoppedEvent(Neighborhood currentLocation)
        {
            Debug.Log("bus at stop");
            if(currentLocation == possibleDestination.neighborhood)
            {
                handleLeaveBusEvent();
            }
        }

        public void handleBusArrived()
        {
            player.gameObject.SetActive(true);
            player.onBus = false;
            travelToDestination(TravelType.Bus);
            canvasController.closeScreen();
        }


        public void handleChooseStopEvent()
        {
            messageManager.displayHintMessage(Status.clickStopInstruction);
            player.gameObject.SetActive(false);
            player.onBus = true;
            routeSelected = false;
            foreach(Location busStop in locationBusStops)
            {
                busStop.startManualHighlight();
            }
            canvasController.disableMainPopups();
        }

        public void handleStartBusEvent()
        {
            
        }

        public void handleLeaveBusEvent()
        {
            Debug.Log("leave bus");
            currentLocation = possibleDestination;
            travelToDestination(TravelType.Bus);
            leaveBus();
        }

        private void leaveBus()
        {
            player.gameObject.SetActive(true);
            player.onBus = false;
        }


        public void tempDisableCar(uint inGameMinutes)
        {
            if (player.playerInfo.hasCar)
            {
                player.carBrokenDown = true;
            }

        }




    }

}
