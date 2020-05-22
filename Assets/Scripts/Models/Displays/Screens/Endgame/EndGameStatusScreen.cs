using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class EndGameStatusScreen : MonoBehaviour
{
    public Player player;
    public MessagePopup successPopup;
    public MessagePopup failurePopup;

    void OnEnable()
    {
        string statusString = "";
        if(player.getIsHome() && player.getAchievedNutrition())
        {
            statusString = Status.homeSufficientFoodEnd;
            successPopup.setText(statusString);
            activateSuccess();
        } else
        {
            
            if (!player.getIsHome() && player.getAchievedNutrition())
            {
                statusString = Status.notHomeSufficientFoodEnd;
            }
            else if (player.getIsHome() && !player.getAchievedNutrition())
            {
                statusString = Status.homeInsufficientFoodEnd;
            }
            else
            {
                statusString = Status.notHomeInsufficientFoodEnd;
            }
            failurePopup.setText(statusString);
            activateFailure();
        }
    }

    private void activateSuccess()
    {
        successPopup.gameObject.SetActive(true);
        failurePopup.gameObject.SetActive(false);
    }

    private void activateFailure()
    {
        successPopup.gameObject.SetActive(false);
        failurePopup.gameObject.SetActive(true);
    }
}
