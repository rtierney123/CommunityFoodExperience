using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nutrition : MonoBehaviour
{
    public Player player;
    public Color standardColor;
    public Color successColor;
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
        playerInfo = player.playerInfo;
        foreach (Transform child in this.transform)
        {
            if (child.name == "CaloriesValue")
            {
                child.gameObject.GetComponent<Text>().text = player.calories.ToString() + "/" + playerInfo.requiredCalories.ToString();
                if(player.calories >= playerInfo.requiredCalories)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "GrainValue")
            {
                child.gameObject.GetComponent<Text>().text = player.grain.ToString() + "/" + playerInfo.requiredGrain.ToString();
                if (player.grain >= playerInfo.requiredGrain)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "FatValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fat.ToString() + "/" + playerInfo.requiredFat.ToString();
                if (player.fat >= playerInfo.requiredFat)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "ProteinValue")
            {
                child.gameObject.GetComponent<Text>().text = player.protein.ToString() + "/" + playerInfo.requiredProtein.ToString();
                if (player.protein >= playerInfo.requiredProtein)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "DairyValue")
            {
                child.gameObject.GetComponent<Text>().text = player.dairy.ToString() + "/" + playerInfo.requiredDairy.ToString();
                if (player.dairy >= playerInfo.requiredDairy)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "FruitValue")
            {
                child.gameObject.GetComponent<Text>().text = player.fruit.ToString() + "/" + playerInfo.requiredFruit.ToString();
                if (player.fruit >= playerInfo.requiredFruit)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "VegetableValue")
            {
                child.gameObject.GetComponent<Text>().text = player.vegetable.ToString() + "/" + playerInfo.requiredVegetable.ToString();
                if (player.vegetable >= playerInfo.requiredVegetable)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
            else if (child.name == "ExtraValue")
            {
                child.gameObject.GetComponent<Text>().text = player.extra.ToString() + "/" + playerInfo.requiredExtra.ToString();
                if (player.extra >= playerInfo.requiredExtra)
                {
                    child.gameObject.GetComponent<Text>().color = successColor;
                }
                else
                {
                    child.gameObject.GetComponent<Text>().color = standardColor;
                }
            }
        }
    }

}
