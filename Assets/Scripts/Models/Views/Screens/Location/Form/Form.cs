using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Form : BaseLocationScreen
    {
        public Player player;
        [HideInInspector]
        public PlayerInfo playerInfo;
        public List<FormWrapper> formItems;
        public ClockDisplay clock;
        public DisableableButton signButton;

        [HideInInspector]
        public string cannotEnterStr = "";


        protected float nextActionTime = 2;

        [HideInInspector]
        public bool pauseFilling = false;


        public override void reset()
        {
            base.reset();
            signButton.gameObject.SetActive(false);
            foreach(FormWrapper item in formItems)
            {
                item.resetWrapper();
            }
        }

        public void startFillingOutForm()
        {
            playerInfo = player.playerInfo;
            StartCoroutine(fillOutForm());
            signButton.gameObject.SetActive(true);
            signButton.disable();
        }

        public virtual bool checkAlreadyEntered()
        {
            return false;
        }

        public virtual bool checkCanEnter()
        {
            return true;
        }

        public void pauseFillingOut()
        {
            pauseFilling = true;
        }

        public void resumeFillingOut()
        {
            pauseFilling = false;
        }

        protected virtual uint getProcessTimeInMinutes()
        {
            return 0;
        }

        private IEnumerator fillOutForm()
        {
            //make sure all text items are intialized
            foreach (FormWrapper item in formItems)
            {
                yield return new WaitUntil(() => item.initialized);
            }

            foreach (FormWrapper item in formItems)
            {
                FormQuestionType question = item.questionType;
                bool skip = false;
                if (item.containsText)
                {
                    string info = playerInfo.getInfoText(question);
                    skip = String.IsNullOrEmpty(info);
                    item.setInfo(info);
                } else
                {
                    CheckmarkType checktype = playerInfo.getInfoCheck(question);
                    item.setCheck(checktype);  
                }

                
                if (!skip)
                {
                    item.fillOut();
                    yield return new WaitForSeconds((float).5);
                    yield return new WaitUntil(() => item.doneWithFillingOut);
                }


                while (pauseFilling)
                {
                    yield return new WaitForSeconds((float).5);
                }
                
            }
            onFormFilled();

        }

        private void onFormFilled()
        {
            activateSignButton();
        }

        private void activateSignButton()
        {
            signButton.enable();
        }

        public void checkAcceptance()
        {
            if (checkValid())
            {
                successAction();
            }
            else
            {
                failureAction();
            }
            uint processTime = getProcessTimeInMinutes();
            clock.addGameMinutes(processTime);
            signButton.disable();
        }
        public virtual bool checkValid()
        {
            return true;
        }

        protected virtual void successAction()
        {

        }

        protected virtual void failureAction()
        {

        }

        public override void closeScreen()
        {
            base.closeScreen();
            foreach (FormWrapper wrapper in formItems)
            {
                wrapper.resetWrapper();
            }

        }


    }
}

