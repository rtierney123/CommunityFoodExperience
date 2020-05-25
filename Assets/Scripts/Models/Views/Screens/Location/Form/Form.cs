using Manage;
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
        

        protected float nextActionTime = 2;

        //five minute
        private uint lossTime = 5;

        [HideInInspector]
        public bool pauseFilling = false;


        public override void reset()
        {
            base.reset();
            signButton.gameObject.SetActive(false);
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

        public void pauseFillingOut()
        {
            pauseFilling = true;
        }

        public void resumeFillingOut()
        {
            pauseFilling = false;
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
 
                if (item.containsText)
                {
                    string info = playerInfo.getInfoText(question);
                    item.setText(info);
                } else
                {
                    CheckmarkType checktype = playerInfo.getInfoCheck(question);
                    item.setCheck(checktype);
                    item.fillOut();
                   
                }

                item.fillOut();
                yield return new WaitForSeconds((float).5);
                while (pauseFilling)
                {
                    Debug.Log("paused filling out");
                    yield return new WaitForSeconds((float).5);
                }
                yield return new WaitUntil(() => item.doneWithFillingOut);
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
            clock.addGameMinutes(lossTime);
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

