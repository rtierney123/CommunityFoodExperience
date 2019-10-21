using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextWrapper : MonoBehaviour
    {
        public FormQuestionType questionType;
        public float fillOutDelayTime;
        public Text displayText;

        [HideInInspector]
        public bool doneWithFillingOut;

        private void Start()
        {
            doneWithFillingOut = false;
            displayText.text = "";
        }

        public void startFillOutText(string info)
        {
            StartCoroutine(fillOutText(info));
        }

        public IEnumerator fillOutText(string info)
        {
           
            string currentString = "";
            foreach (char ch in info)
            {
                currentString += ch;
                displayText.text = currentString;
                yield return new WaitForSeconds(fillOutDelayTime);
            }
            Debug.Log("done");
            //set to true to continue coutroutine
            doneWithFillingOut = true;
        }

        public void resetTextWrapper()
        {
            //reset to fill out form again
            doneWithFillingOut = false;
        }

    }
}

