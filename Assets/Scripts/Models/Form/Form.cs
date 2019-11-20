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
        public List<FormWrapper> formItems;
        public float nextActionTime;
        public ClockDisplay clock;
        public bool signed = false;
        private bool fillingOutItem;

        //five minute
        private uint lossTime = 5;


        private void Start()
        {
            playerInfo = player.playerInfo;
        }
        private void OnEnable()
        {
            Debug.Log("start filling out form");
            StartCoroutine(fillOutForm());
        }

        public virtual bool checkAlreadyEntered()
        {
            return false;
        }

  
        private IEnumerator fillOutForm()
        {
            //make sure all text items are intialized
            foreach (FormWrapper item in formItems)
            {
                Debug.Log(item.ToString());
                yield return new WaitUntil(() => item.initialized);
            }
            Debug.Log("all initialized");

            foreach (FormWrapper item in formItems)
            {
                FormQuestionType question = item.questionType;
 
                if (item.containsText)
                {
                    string info = playerInfo.getInfoText(question);
                    item.setText(info);
                } else
                {
                    bool type = playerInfo.getInfoBool(question);
                    if (type)
                    {
                        item.setCheck(CheckmarkType.Check1);
                    }
                    else
                    {
                        item.setCheck(CheckmarkType.Check2);
                    }
                   
                }

                item.fillOut();
                yield return new WaitForSeconds((float).5);
                yield return new WaitUntil(() => item.doneWithFillingOut);
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
            clock.addRunningTime(lossTime);
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

        public override void closeScreen()
        {
            base.closeScreen();
            if (!signed)
            {
                foreach (FormWrapper wrapper in formItems)
                {
                    wrapper.resetWrapper();
                }
            }

        }


    }
}

