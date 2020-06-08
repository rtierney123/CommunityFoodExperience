using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.NetworkSystem;
using UnityEngine.UI;

namespace UI
{
    
    public class EndGameStatus : View
    {
        public Text text;
        public void setText(string message)
        {
            text.text = message;
        }
    }
}

