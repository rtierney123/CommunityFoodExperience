using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class MessagePopup : PopUp
    {
        public Text mainText;
        public Button cancelButton;

        protected override void Awake()
        {
            base.Awake();
            cancelButton.onClick.AddListener(close);
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }

        public void setText(string message)
        {
            mainText.text = message;
        }
    }

}
