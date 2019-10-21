using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Form : Screen
    {
        public Player player;
        public List<TextWrapper> textItems;

        private bool fillingOutItem;

        private void OnEnable()
        {
            StartCoroutine(fillOutForm());
        }

        private IEnumerator fillOutForm()
        {
            foreach (TextWrapper item in textItems)
            {
                FormQuestionType question = item.questionType;
                string info = player.getInfo(question);
                Debug.Log(info);

                item.startFillOutText(info);
                yield return new WaitUntil(() => item.doneWithFillingOut);
                item.resetTextWrapper();
                Debug.Log("on to next");
            }
            Debug.Log("done filling out form");
            onFormFilled();

        }

        private void onFormFilled()
        {
            if (checkValid())
            {
                successAction();
            }
            else
            {
                failureAction();
            }
        }


        protected virtual bool checkValid()
        {
            return true;
        }

        protected virtual void successAction()
        {

        }

        protected virtual void failureAction()
        {

        }


        private void displayNotAllowedMessage()
        {
            string message = notAllowedMessage();
            messageManager.generateStandardErrorMessage(message);
            StartCoroutine(delayCloseScreen());
        }

        protected virtual bool checkAllowed()
        {
            return false;
        }

        protected virtual string notAllowedMessage()
        {
            return "not allowed";
        }

      
    }
}

