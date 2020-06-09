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

        private double militaryHourRepaired;

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
            }
        }

        private void checkCarRepaired()
        {
            if(player.playerInfo.hasCar && player.carBrokenDown)
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
