using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BusAnimationScreen : Screen
    {
        public ProgressBar progressBar;
        public ClockDisplay clock;
        public int chanceBusEvent;
        [HideInInspector]
        public bool messageDisplayed;
        
        bool busDelayed = false;
        double testDelayedPercent = .2;

        public override void reset()
        {
            progressBar.resetLoading();
            progressBar.resumeLoading();
            messageDisplayed = false;
            busDelayed = false;
            testDelayedPercent = .2;
        }

        protected virtual void Update()
        {

            if (progressBar.getDecimalDone() > testDelayedPercent && !progressBar.getComplete())
            {
                if (!busDelayed)
                {
                    float rand = UnityEngine.Random.Range(0, 100);
                    if (rand <= chanceBusEvent)
                    {
                        busDelayed = true;
                        clock.addGameMinutes(30);
                        StartCoroutine(pauseLoadingForMessage());
                    }
                }
                testDelayedPercent += .1;
            }
        }


        private IEnumerator pauseLoadingForMessage()
        {
            progressBar.pauseLoading();
            messageDisplayed = true;
            GameObject message = messageManager.generateStandardErrorMessage(getRandomBusEventStr());
            progressBar.delayLoading(2);
            yield return new WaitUntil(() => message == null);
            messageDisplayed = false;
            progressBar.resumeLoading();
        }

        private string getRandomBusEventStr()
        {
            float rand = Random.Range(0, 3);
            if (rand < 1)
            {
                return "There is traffic on the road and now the bus is stuck. You lose 30 minutes.";
            }
            else if (rand < 2)
            {
                return "There has been an accident on the road so now traffic is backed up. You lose 30 minutes.";
            }
            else
            {
                return "The bus has broken down and is waiting for the repairman. You lose 30 minutes.";
            }
        }

        public void pauseScreen()
        {
            progressBar.pauseLoading();
        }

        public void resumeScreen()
        {
            if (!messageDisplayed)
            {
                progressBar.resumeLoading();
            }
        }
    }
}

