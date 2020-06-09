using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TakingBusScreen : BusAnimationScreen
    {
        public NavigationManager navigationManager;

        protected override void Update()
        {
            base.Update();
            if (progressBar.getComplete() && this.gameObject.activeInHierarchy)
            {

                progressBar.resetLoading();
                navigationManager.handleBusArrived();
            }
        }
    }
}
