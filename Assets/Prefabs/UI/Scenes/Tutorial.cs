using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Manage;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/document/d/1bC8QtRwKyrnqRTdKrRDrJP01n7SHfHdbrnxgxdeZck4/edit?usp=sharing");
    }
}
