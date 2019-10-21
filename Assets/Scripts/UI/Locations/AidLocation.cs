
using System.Collections;
using UnityEngine;

namespace UI
{

    public class AidLocation : BaseLocation
    {
        public Form form;
        public int closeTime;
        private void OnEnable()
        {
            if (checkAllowed())
            {
                canvasController.openPopup(form.gameObject);
            } else
            {
                displayNotAllowedMessage();
            }

        }

        private void displayNotAllowedMessage()
        {
            string message = notAllowedMessage();
            errorManager.generateStandardMessage(message);
            StartCoroutine(waitToClose(closeTime));
        }

        protected virtual bool checkAllowed()
        {
            return false;
        }

        protected virtual string notAllowedMessage()
        {
            return "not allowed";
        }

        IEnumerator waitToClose(float waitTime)
        {
            yield return new WaitForSeconds(waitTime);
            closeScreen();
        }
    }
}
