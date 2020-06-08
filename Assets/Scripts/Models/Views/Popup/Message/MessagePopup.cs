﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessagePopup : PopUp
    {
        public Text mainText;

        public override void setText(string message)
        {
            mainText.text = message;
        }
    }
}

