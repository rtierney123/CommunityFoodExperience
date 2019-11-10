using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Manage;

public class EndScreen : MonoBehaviour
{
    // public ClockDisplay clock;
    public GameObject endScreen;
    public Player player;
    private PlayerInfo playerInfo;
    public GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
    }

    private void OnEnable()
    {
        playerInfo = player.playerInfo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endButtonClicked() {
        endScreen.SetActive(true);
        gameManager.pause();
    }

    public void updateInfo()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "CaloriesValue")
            {
                child.gameObject.GetComponent<Text>().text = player.calories.ToString() + "/" + playerInfo.requiredCalories.ToString();
            }
            else if (child.name == "GrainValue")
            {
                child.gameObject.GetComponent<Text>().text = player.grain.ToString() + "/" + playerInfo.requiredGrain.ToString();
            }
            else if (child.name == "FatValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fat.ToString() + "/" + playerInfo.requiredFat.ToString();
            }
            else if (child.name == "ProteinValue")
            {
                child.gameObject.GetComponent<Text>().text = player.protein.ToString() + "/" + playerInfo.requiredProtein.ToString();
            }
            else if (child.name == "DairyValue")
            {
                child.gameObject.GetComponent<Text>().text = player.dairy.ToString() + "/" + playerInfo.requiredDairy.ToString();
            }
            else if (child.name == "FruitValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fruit.ToString() + "/" + playerInfo.requiredFruit.ToString();
            }
            else if (child.name == "VegetableValue")
            {
                child.gameObject.GetComponent<Text>().text = player.vegetable.ToString() + "/" + playerInfo.requiredVegetable.ToString();
            }
            else if (child.name == "ExtraValue")
            {
                child.gameObject.GetComponent<Text>().text = player.extra.ToString() + "/" + playerInfo.requiredExtra.ToString();
            }
        }
    }
}
