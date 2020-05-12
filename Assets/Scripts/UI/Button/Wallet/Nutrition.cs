using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nutrition : MonoBehaviour
{
    public Player player;
    public Color standardColor;
    public Color successColor;

    public Text caloriesText;
    public Text grainText;
    public Text fatText;
    public Text proteinText;
    public Text dairyText;
    public Text fruitText;
    public Text vegText;
    public Text extraText;

    public GameObject plusSignPopup;
    // Start is called before the first frame update
    void Start()
    {
        updateInfo();
    }

    private void OnEnable()
    {
        updateInfo();
    }

    public void updateInfo()
    {
        caloriesText.text = player.getCaloriesStatus();
        grainText.text = player.getGrainStatus();
        fatText.text = player.getFatStatus();
        proteinText.text = player.getProteinStatus();
        dairyText.text = player.getDairyStatus();
        fruitText.text = player.getFruitStatus();
        vegText.text = player.getVegetableStatus();
        extraText.text = player.getExtraStatus();

        displaySuccess(caloriesText, player.getAchieveCalories());
        displaySuccess(grainText, player.getAchieveGrain());
        displaySuccess(fatText, player.getAchieveFat());
        displaySuccess(proteinText, player.getAchieveProtein());
        displaySuccess(dairyText, player.getAchieveDairy());
        displaySuccess(fruitText, player.getAchieveFruit());
        displaySuccess(vegText, player.getAchieveVegetable());
        displaySuccess(extraText, player.getAchieveExtra());
    }

    private void displaySuccess(Text text, bool success)
    {
        if (success)
        {
            text.color = successColor;
        }
        else
        {
            text.color = standardColor;
        }
    }

}
