using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStatusScreen : MonoBehaviour
{
    public Player player;
    public GameObject successBackground;
    public GameObject failureBackground;
    public Text resultsText;

    void OnEnable()
    {
        string statusString = "";
        if(player.getIsHome() && player.getAchievedNutrition())
        {
            statusString = "Congratulations!  You obtained enough food for the day and returned home.";
            activateSuccess();
        } else
        {
            activateFailure();
            statusString = "Oh no.";
            if (!player.getIsHome())
            {
                statusString = statusString + " You did not make it home.";
            }
            if (!player.getAchievedNutrition())
            {
                statusString = statusString + " You did not obtain enough food for the day.";
            }
        }

        resultsText.text = statusString;
    }

    private void activateSuccess()
    {
        successBackground.SetActive(true);
        failureBackground.SetActive(false);
    }

    private void activateFailure()
    {
        successBackground.SetActive(false);
        failureBackground.SetActive(true);
    }
}
