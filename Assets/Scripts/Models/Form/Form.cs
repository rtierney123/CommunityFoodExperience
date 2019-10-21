using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Form : MonoBehaviour
    {
        public List<TextWrapper> textItems;
        public Player player;

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
    }
}

