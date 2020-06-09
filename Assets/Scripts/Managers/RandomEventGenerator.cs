using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Manage
{
    public class RandomEventGenerator : MonoBehaviour, IClockEventCaller
    {
        public Player player;
        public NavigationManager navigationManager;
        public CurrencyManager currencyManager;
        public MessageManager messageManager;
        public ClockDisplay clock;

        public double chanceFreeRide;
        public double chanceBreakdown;

        private double militaryHourRepaired = -1;
        private double minutesRepaired = -1;

        public void hourBeforeEndGame()
        {
        }

        public void hourPassed()
        {
            checkCarRepaired();
            checkCarBreakDown();
            checkFreeRide();
        }

        public void minutePassed()
        {
        }

        private void checkCarBreakDown()
        {
            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < chanceBreakdown && player.playerInfo.hasCar)
            {
                Debug.Log("car broken down");
                messageManager.generateMainScreenOnlyErrorMessage(Status.carBrokeDown);
                player.carBrokenDown = true;
                militaryHourRepaired = clock.getCurrentMilitaryHour() + 2;
                minutesRepaired = clock.getCurrentMinutes();
            }
        }

        private void checkCarRepaired()
        {
            int currentHour = clock.getCurrentMilitaryHour();
            int currentMin = clock.getCurrentMinutes();
            if(player.playerInfo.hasCar && player.carBrokenDown && currentHour >= militaryHourRepaired + 2 && currentMin >= minutesRepaired)
            {
                Debug.Log("car repaired");
                messageManager.generateMainScreenOnlySuccessMessage(Status.carRepaired);
                player.carBrokenDown = false;
            }
        }

        private void checkFreeRide()
            {
                float rand = UnityEngine.Random.Range(0, 100);
                if (rand < chanceFreeRide && player.hasNoModeOfTransportation() && !player.hasTemporaryRide)
                {
                    player.setFreeRide(true);
                    messageManager.generateMainScreenOnlySuccessMessage(Status.freeRideReceived);
                    Debug.Log("free ride");
                }
            }
        }

}
