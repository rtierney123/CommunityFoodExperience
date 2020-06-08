using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class TemporaryMessagePopup : MessagePopup
    {
       
        public Button cancelButton;

        protected override void Awake()
        {
            base.Awake();
            if(cancelButton != null)
            {
                cancelButton.onClick.AddListener(close);
            }
        }

        private void OnDisable()
        {
            Destroy(gameObject);
        }


    }

}
