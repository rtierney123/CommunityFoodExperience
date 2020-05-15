using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusWaitingScreen : Screen
{
    public ProgressBar progressBar;
    public NavigationManager manager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(progressBar.getComplete() && this.gameObject.activeInHierarchy)
        {
            canvasController.closeCurrentScreen();
            manager.handleTakeBusEvent();
        }
    }
}
