using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class DisplayPlayerInfoScreen : Screen
{
    public Player player;

    public Text playerName;
    public Text playerCash;
    public Text playerTransportation;
    public Text playerDescript;


    private void OnEnable()
    {
        playerName.text = player.playerInfo.getFullName();
        playerDescript.text = player.playerInfo.description;
        playerCash.text = FormatText.formatCost(player.playerInfo.getStartingCash());
        playerTransportation.text = player.playerInfo.getStartingTransportationString();
    }
}
