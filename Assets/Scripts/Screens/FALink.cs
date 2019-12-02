using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Manage;

public class FALink : MonoBehaviour
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
          Debug.Log("fa");
          Application.OpenURL("https://www.feedingamerica.org");
      }
}
