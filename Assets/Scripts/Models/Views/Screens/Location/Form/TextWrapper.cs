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
        public bool resize;
        [HideInInspector]
        private RectTransform rectText;

        protected override void Awake()
        {
           containsText = true;
            base.Awake();
            rectText = this.gameObject.GetComponent<RectTransform>();
        }

        private void OnEnable()
        {
            if (resize)
            {
                rectText.sizeDelta = new Vector2(rectText.rect.width, displayText.preferredHeight);
            }
            
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

        public override void resetWrapper()
        {
            base.resetWrapper();
            displayText.text = "";
        }

        public void setText(string info)
        {
            displayText.text = info;
        }

    }
}

