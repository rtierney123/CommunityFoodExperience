using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class View : MonoBehaviour
    {
        [HideInInspector]
        public CanvasController canvasController;
        [HideInInspector]
        public MessageManager messageManager;
        protected virtual void Awake()
        {
            canvasController = GameObject.Find("Canvas").GetComponent<CanvasController>();
            messageManager = GameObject.Find("MessageManager").GetComponent<MessageManager>();
        }

        public virtual void onAttemptDismiss()
        {

        }
        public virtual void onDismiss()
        {

        }

        public virtual void onCancelDismiss()
        {

        }

        public virtual void updateView()
        {

        }

        public virtual void reset()
        {

        }

        public virtual void close()
        {

        }


    }
}

