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

        public double chanceSickChild;
        public double chanceFreeRide;
        public double chanceBreakdown;

        public void hourBeforeEndGame()
        {
        }

        public void hourPassed()
        {
            checkForSickChildEvent();
            checkFreeRide();
            checkCarBreakDown();
        }

        public void minutePassed()
        {
        }

        private void checkForSickChildEvent()
        {
            PlayerInfo info = player.playerInfo;
            FamilyMember[] children = info.children.list;

            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < chanceSickChild && children.Length > 0 && !player.hasKidBeenSick)
            {
                int timeLost = UnityEngine.Random.Range(30, 60);
                string timeString = FormatText.formatInt(timeLost);

                Debug.Log("child sick");
                messageManager.generateMainScreenOnlyErrorMessage(String.Format("Oh no! Your child is sick at school.  You have to pick them up. (You lose {0} minutes.)", timeString));
                clock.addGameMinutes(timeLost);
                player.hasKidBeenSick = true;
            }
        }

        private void checkCarBreakDown()
        {
            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < chanceBreakdown && player.playerInfo.hasCar)
            {
                Debug.Log("car broken down");
                currencyManager.tempDisableCar(60);
            }

           
        }

        private void checkFreeRide()
        {
            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < chanceFreeRide && player.hasNoModeOfTransportation() && !player.hasTemporaryRide)
            {
                player.setFreeRide(true);
                Debug.Log("free ride");
                messageManager.generateMainScreenOnlySuccessMessage("A kind stranger has offered to give you a ride to your next location. Choose wisely as this will only get you to one place. Redeem by clicking the next location you want to go.");
            }
        }

    }

}
