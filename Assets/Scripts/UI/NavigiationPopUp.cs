using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigiationPopUp : MonoBehaviour
{
    public Text title;
    public Text description;
    public Text carText;
    public Text walkText;

    public Player player;
    public NavigationManager manager;
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
        if (player.playerInfo.hasCar)
        {
            carButton.enable();
        } else
        {
            carButton.disable();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
