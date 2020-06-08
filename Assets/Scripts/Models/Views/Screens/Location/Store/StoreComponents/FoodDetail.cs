using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FoodDetail : PopUp
    {
        public Button closeButton;

        protected override void Awake()
        {
            base.Awake();
            closeButton.onClick.AddListener(close);
        }
    }
}

