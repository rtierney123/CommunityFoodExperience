using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigiationPopUp : MonoBehaviour
{
    public Text title;
    public Text description;

    [HideInInspector]
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {
        popUp = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
