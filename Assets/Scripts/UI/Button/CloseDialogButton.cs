using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseDialogButton : MonoBehaviour
{
    public GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void closePopup()
    {
        popup.SetActive(false);
    }
}
