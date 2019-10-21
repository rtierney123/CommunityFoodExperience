using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen : MonoBehaviour
{
    public CanvasController canvasController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void closePopUps()
    {
        if (canvasController != null)
        {
            canvasController.closePopUp();
        }
    }
}
