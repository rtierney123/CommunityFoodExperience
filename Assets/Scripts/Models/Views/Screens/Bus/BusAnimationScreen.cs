using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BusAnimationScreen : Screen, IClockEventCaller
    {
        public ProgressBar progressBar;
        public ClockDisplay clock;
        public int chanceBusEvent;
        bool busDelayed = false;

        private void OnEnable()
        {
            canvasController.disableMainPopups();
            busDelayed = false;
        }

        public void hourBeforeEndGame()
        {
            //throw new System.NotImplementedException();
        }

        public void hourPassed()
        {
            //throw new System.NotImplementedException();
        }

        public void minutePassed()
        {
            if (this.gameObject.activeSelf && !busDelayed)
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
        }

        private IEnumerator pauseLoadingForMessage()
        {
            progressBar.pauseLoading();
            GameObject message = messageManager.generateStandardErrorMessage(getRandomBusEventStr());
            progressBar.delayLoading(2);
            yield return new WaitUntil(() => !message.activeSelf);
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

