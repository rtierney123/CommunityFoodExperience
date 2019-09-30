using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nutrition : MonoBehaviour
{
    public Player player;

    public GameObject plusSignPopup;
    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in this.transform) {
            if (child.name == "CaloriesValue") {
                child.gameObject.GetComponent<Text>().text = player.calories.ToString() + "/" + player.requiredCalories.ToString();
            } else if (child.name == "GrainValue") {
                child.gameObject.GetComponent<Text>().text = player.grain.ToString() + "/" + player.requiredGrain.ToString();
            } else if (child.name == "FatValue") {
                child.gameObject.GetComponent<Text>().text = player.fat.ToString() + "/" + player.requiredFat.ToString();
            } else if (child.name == "ProteinValue") {
                child.gameObject.GetComponent<Text>().text = player.protein.ToString() + "/" + player.requiredProtein.ToString();
            } else if (child.name == "DairyValue") {
                child.gameObject.GetComponent<Text>().text = player.dairy.ToString() + "/" + player.requiredDairy.ToString();
            } else if (child.name == "FruitValue") {
                child.gameObject.GetComponent<Text>().text = player.fruit.ToString() + "/" + player.requiredFruit.ToString();
            } else if (child.name == "VegetableValue") {
                child.gameObject.GetComponent<Text>().text = player.vegetabele.ToString() + "/" + player.requiredVegetable.ToString();
            } else if (child.name == "ExtraValue") {
                child.gameObject.GetComponent<Text>().text = player.extra.ToString() + "/" + player.requiredExtra.ToString();
            } 
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
