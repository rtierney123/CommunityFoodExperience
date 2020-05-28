﻿using Model;
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

        public Dictionary<Neighborhood, Location> locationStopDict;
        public List<Neighborhood> locationKeys;
        public List<Location> locationBusStops;

        private TravelCalculator travelCalculator;

       

        // Start is called before the first frame update
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

        public void reset() {
            currentLocation = startLocation;
            if (bus.playerOnBus)
            {
                leaveBus();
            }
            bus.setOffScreen();
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
                    if (possibleDestination.neighborhood == currentLocation.neighborhood)
                    {
                        Debug.Log("potential time " + getPotentialTravelTime(TravelType.Walk));
                        navigationPopup.walkText.text = "Walk (" + formatTime( getPotentialTravelTime(TravelType.Walk)) + ")";
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
                    navigationPopup.carText.text = "Car (" + formatTime( getPotentialTravelTime(TravelType.Car)) + ")";

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
            double travelTime = getPotentialTravelTime(travelType);
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
            if(location == startLocation)
            {
                player.setIsHome(true);
            } else
            {
                player.setIsHome(false);
            }
            player.transform.position = location.playerDropoff.position;
        }

        private double getPotentialTravelTime(TravelType travelType)
        {
            return travelCalculator.calculateTravelTime(currentLocation.locationId, possibleDestination.locationId, travelType);
        }

        public void closePopUp()
        {
            canvasController.closePopUp();
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
            bus.pauseAnimation();
            MainBusStopLocation busStop = (MainBusStopLocation)currentLocation;
            bus.setLocation(busStop.busStartLocation);
        }


        public void handleChooseStopEvent()
        {
            messageManager.displayHintMessage(Status.clickStopInstruction);
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
            leaveBus();
        }

        private void leaveBus()
        {
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
            /*
            if (player.hasNoModeOfTransportation() && currentLocation.locationType == LocationType.FarLocation)
            {
                canvasController.addToMainScreenPopUpBackLog(stuckPopup);
            }
            */
        }

       

    }

}
