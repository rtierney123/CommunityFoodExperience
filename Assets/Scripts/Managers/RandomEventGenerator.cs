using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility;

namespace Manage
{
    public class RandomEventGenerator : Manager, IClockEventCaller
    {
        public Player player;
        public NavigationManager navigationManager;
        public CurrencyManager currencyManager;
        public MessageManager messageManager;
        public ClockDisplay clock;

        public double chanceFreeRide;
        public double chanceBreakdown;

        private uint hourRepaired = 0;
        private static int carBreakDownDuration = 3;

        public override void reset()
        {
            base.reset();
            hourRepaired = 0;
        }

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
            if (rand < chanceBreakdown && player.playerInfo.hasCar && !player.carBrokenDown)
            {
                messageManager.generateMainScreenOnlyErrorMessage(Status.carBrokeDown);
                hourRepaired =(uint) (clock.getCurrentMilitaryHour() + carBreakDownDuration) % 24;
                player.carBrokenDown = true;
                Debug.Log("car breakdown");
            }
        }

        private void checkCarRepaired()
        {
            uint currentHour = clock.getCurrentMilitaryHour();

            if(player.playerInfo.hasCar && player.carBrokenDown && currentHour >= hourRepaired)
            {
                messageManager.generateMainScreenOnlySuccessMessage(Status.carRepaired);
                hourRepaired = 0;
                player.carBrokenDown = false;
                Debug.Log("car repaired");
            }
        }

        private void checkFreeRide()
            {
                float rand = UnityEngine.Random.Range(0, 100);
                if (rand < chanceFreeRide && player.hasNoModeOfTransportation())
                {
                    player.setFreeRide(true);
                    messageManager.generateMainScreenOnlySuccessMessage(Status.freeRideReceived);
                    Debug.Log("free ride");
                }
            }
        }

}
