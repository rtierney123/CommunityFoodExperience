using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class NavigationPopUp : PopUp
{
    public NavigationManager manager;

    public Text title;
    public Text description;
    public Text carText;
    public Text walkText;
    public DisableableButton walkButton;
    public DisableableButton carButton;

    [HideInInspector]
    public GameObject popUp;
    // Start is called before the first frame update
    void Start()
    {
        popUp = this.gameObject;
    }

    private void OnEnable()
    {   
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void enableCarButton()
    {
        carButton.enable();
    }

    public void disableCarButton()
    {
        carButton.disable();
    }


    public void enableWalkButton()
    {
        walkButton.enable();
    }

    public void disableWalkButton()
    {
        walkButton.disable();
    }

    public void deactivateWalkButton()
    {
        walkButton.gameObject.SetActive(false);
    }

    public void activateWalkButton()
    {
        walkButton.gameObject.SetActive(true);
    }

}
