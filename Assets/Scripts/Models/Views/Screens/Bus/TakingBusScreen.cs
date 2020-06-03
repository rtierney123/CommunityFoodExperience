using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class TakingBusScreen : Screen
    {
        public ProgressBar progressBar;
        public NavigationManager navigationManager;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
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
