using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNutrition : MonoBehaviour
{
    public GameObject gameObject;
    public CanvasController canvasController;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void showPopup()
    {
        this.gameObject.SetActive(false);
        canvasController.openPopup(gameObject);
    }
}
