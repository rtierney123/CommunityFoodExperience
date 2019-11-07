using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UI;
using Manage;

public class EndScreen : MonoBehaviour
{
    public CanvasController canvasController;
    // public ClockDisplay clock;
    public GameObject endScreen;
    public Player player;

    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvasController) {
            canvasController.pause();
        }
    }

    public void endButtonClicked() {
        canvasController.pause();
        endScreen.SetActive(true);
    }

    public void updateInfo()
    {
        foreach (Transform child in this.transform)
        {
            if (child.name == "CaloriesValue")
            {
                child.gameObject.GetComponent<Text>().text = player.calories.ToString() + "/" + player.requiredCalories.ToString();
            }
            else if (child.name == "GrainValue")
            {
                child.gameObject.GetComponent<Text>().text = player.grain.ToString() + "/" + player.requiredGrain.ToString();
            }
            else if (child.name == "FatValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fat.ToString() + "/" + player.requiredFat.ToString();
            }
            else if (child.name == "ProteinValue")
            {
                child.gameObject.GetComponent<Text>().text = player.protein.ToString() + "/" + player.requiredProtein.ToString();
            }
            else if (child.name == "DairyValue")
            {
                child.gameObject.GetComponent<Text>().text = player.dairy.ToString() + "/" + player.requiredDairy.ToString();
            }
            else if (child.name == "FruitValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fruit.ToString() + "/" + player.requiredFruit.ToString();
            }
            else if (child.name == "VegetableValue")
            {
                child.gameObject.GetComponent<Text>().text = player.vegetable.ToString() + "/" + player.requiredVegetable.ToString();
            }
            else if (child.name == "ExtraValue")
            {
                child.gameObject.GetComponent<Text>().text = player.extra.ToString() + "/" + player.requiredExtra.ToString();
            }
        }
    }
}
