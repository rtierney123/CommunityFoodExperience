using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ErrorPopUp : PopUp
    {
        public Text mainText;


        public void setText(string message)
        {
            mainText.text = message;
        }
    }

}
