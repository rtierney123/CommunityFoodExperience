using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TakingBusScreen : BusAnimationScreen
    {
        public NavigationManager navigationManager;

        void Update()
        {
            if (progressBar.getComplete() && this.gameObject.activeInHierarchy)
            {

                canvasController.closeScreen();
                progressBar.resetLoading();
                navigationManager.handleBusArrived();
            }
        }
    }
}
