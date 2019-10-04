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
        public bool fillingOutText;

        private void Start()
        {
            fillingOutText = false;
        }

        public IEnumerator fillOutText(string info)
        {
            fillingOutText = true;
            string currentString = "";
            foreach (char ch in info)
            {
                currentString += ch;
                displayText.text = currentString;
                yield return new WaitForSeconds(fillOutDelayTime);
            }
            fillingOutText = false;
        }

    }
}

