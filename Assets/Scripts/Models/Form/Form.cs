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

                StartCoroutine(item.fillOutText(info));
                yield return new WaitUntil(() => item.fillingOutText == false);

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


        protected bool checkValid()
        {
            return true;
        }

        protected void successAction()
        {

        }

        protected void failureAction()
        {

        }
    }
}

