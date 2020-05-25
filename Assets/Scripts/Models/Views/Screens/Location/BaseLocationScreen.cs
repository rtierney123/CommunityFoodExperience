using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class BaseLocationScreen : Screen
    {
        public GameObject greetingLayout;
        public GameObject contentsLayout;

        void OnEnable()
        {
            reset();

        }

        public override void reset()
        {
            base.reset();
            greetingLayout.SetActive(true);
            contentsLayout.SetActive(false);
        }

        public virtual void enter()
        {
            greetingLayout.SetActive(false);
            contentsLayout.SetActive(true);
        }
    }
}

