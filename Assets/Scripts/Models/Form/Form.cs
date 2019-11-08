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
        public PlayerInfo playerInfo;
        public List<FormWrapper> textItems;
        public float nextActionTime;
        private bool fillingOutItem;

        private void Start()
        {
            playerInfo = player.playerInfo;
        }
        private void OnEnable()
        {
            StartCoroutine(fillOutForm());
        }

        public virtual bool checkAlreadyEntered()
        {
            return false;
        }

        private IEnumerator fillOutForm()
        {
            //make sure all text items are intialized
            foreach (FormWrapper item in textItems)
            {
                yield return new WaitUntil(() => item.initialized);
            }

            foreach (FormWrapper item in textItems)
            {
                FormQuestionType question = item.questionType;
 
                if (item.containsText)
                {
                    string info = playerInfo.getInfoText(question);
                    item.setText(info);
                } else
                {
                    item.setCheck(CheckmarkType.Check1);
                }

                item.fillOut();
                yield return new WaitForSeconds((float).5);
                yield return new WaitUntil(() => item.doneWithFillingOut);
                item.resetTextWrapper();
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
            StartCoroutine(delayCloseScreen(nextActionTime));
        }

        protected virtual void failureAction()
        {
            StartCoroutine(delayCloseScreen(nextActionTime));
        }

      
    }
}

