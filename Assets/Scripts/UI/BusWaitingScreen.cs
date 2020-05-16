using JetBrains.Annotations;
using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusWaitingScreen : Screen
{
    public ProgressBar progressBar;
    public GameObject busPopup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(progressBar.getComplete() && this.gameObject.activeInHierarchy)
        {
            canvasController.addToMainScreenPopUpBackLog(busPopup);
            canvasController.closeScreen();
            Debug.Log("open bus popup");
        }
    }
}
