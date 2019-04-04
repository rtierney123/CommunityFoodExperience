using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopUpButton : MonoBehaviour
{
    public Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canvas.enabled)
            {
                canvas.enabled = false;
            }
        }
          
    }

    public void showPopUp()
    {
        canvas.enabled = true;
    }
}
