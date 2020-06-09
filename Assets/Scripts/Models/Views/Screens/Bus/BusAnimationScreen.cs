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

        bool busDelayed = false;
        double testDelayedPercent = .1;
        private void OnEnable()
        {
            busDelayed = false;
            testDelayedPercent = .1;
        }

        protected virtual void Update()
        {

            if (progressBar.getDecimalDone() > testDelayedPercent && !progressBar.getComplete())
            {
                if (!busDelayed)
                {
                    float rand = UnityEngine.Random.Range(0, 100);
                    if (rand < chanceBusEvent)
                    {
                        Debug.Log("bus delay");
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
            GameObject message = messageManager.generateStandardErrorMessage(getRandomBusEventStr());
            progressBar.delayLoading(2);
            yield return new WaitUntil(() => message == null);
            Debug.Log("resume bus");
            progressBar.resumeLoading();
        }

        private string getRandomBusEventStr()
        {
            float rand = Random.Range(0, 3);
            Debug.Log("delay bus");
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
    }
}

