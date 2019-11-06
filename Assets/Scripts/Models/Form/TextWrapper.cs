using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TextWrapper : FormWrapper
    {
        public float fillOutDelayTime;
        public Text displayText;

        private void Awake()
        {
           containsText = true;
        }

        private void Start()
        {
            doneWithFillingOut = false;
            displayText.text = "";
        }

        public override void fillOut()
        {
            StartCoroutine(fillOutText(contents));
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
            //set to true to continue coutroutine
            doneWithFillingOut = true;
        }

    }
}

