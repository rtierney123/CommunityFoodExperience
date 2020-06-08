using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class Screen : View
    {
        public GameObject nextScreen;
        public GameObject prevScreen;
        protected float delayTime = (float).5;
        internal static int height;
        internal static float width;


        public IEnumerator delayOpenNextScreen()
        {
            yield return new WaitForSeconds(delayTime);
            openNextScreen();

        }
        public IEnumerator delayOpenNextScreen(float delay)
        {
            yield return new WaitForSeconds(delay);
            openNextScreen();
        }

        public IEnumerator delayOpenSuccessMessage(float delay, string message)
        {
            yield return new WaitForSeconds(delay);
            displaySuccessMessage(message);
        }

        public void openNextScreen()
        {
            if (nextScreen != null)
            {
                canvasController.openScreen(nextScreen);
            }
        }

        public void openPrevScreen()
        {
            if (prevScreen != null)
            {
                canvasController.openScreen(prevScreen);
            }
            else
            {
                canvasController.closeScreen();
            }
        }


        protected IEnumerator delayCloseScreen()
        {
            yield return new WaitForSeconds(delayTime);
            closeScreen();
        }

        protected IEnumerator delayCloseScreen(float delay)
        {
            yield return new WaitForSeconds(delay);
            closeScreen();
        }


        public virtual void closeScreen()
        {
            closePopUps();
            canvasController.closeScreen();
        }

        public void closePopUps()
        {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
        }

        public void displaySuccessMessage(string message)
        {
            messageManager.generateStandardSuccessMessage(message);
        }

        public virtual void updateDisplay()
        {

        }
    }

}
