using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nutrition : MonoBehaviour
{
    public Player player;
    private PlayerInfo playerInfo;

    public GameObject plusSignPopup;
    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
       
    }

    private void OnEnable()
    {
        playerInfo = player.playerInfo;
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
