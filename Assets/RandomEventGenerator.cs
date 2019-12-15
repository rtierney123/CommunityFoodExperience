using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
                //float scale = (float)navigationManager.scale * 100;
                //double timeLost = UnityEngine.Random.Range((float)(3 * scale), (float)(6 * scale)) / 100;
                double timeLost = 30 / .18;
                double gametimeLost = navigationManager.realToGameTime(timeLost);
                string timeString = navigationManager.formatTime(gametimeLost);

                messageManager.generateMainScreenOnlyErrorMessage(String.Format("Oh no! Your child is sick at school.  You have to pick them up. (You lose {0}.)", timeString));
                clock.addGameMinutes(timeLost);
                player.hasKidBeenSick = true;
            }
        }

        private void checkCarBreakDown()
        {
            float rand = UnityEngine.Random.Range(0, 100);
            if (rand < chanceBreakdown && player.playerInfo.hasCar)
            {
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
                messageManager.generateMainScreenOnlySuccessMessage("'Hey you look like you could use a ride.' (You can take a ride to one location. You lose this ride if you move from this location)");
            }
        }

    }

}
