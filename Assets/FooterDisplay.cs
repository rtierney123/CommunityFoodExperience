using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class FooterDisplay : MonoBehaviour
{
    public Player player;
    public Text mainScreenCashText;
    public Text mainScreenEITCText;
    public Text mainScreenCTCText;
    public Text mainScreenSnapText;

    public void update()
    {
        mainScreenCashText.text = FormatText.formatCost(player.money);
        mainScreenEITCText.text = FormatText.formatCost(player.eitcFunds);
        mainScreenCTCText.text = FormatText.formatCost(player.ctcFunds);
        mainScreenSnapText.text = FormatText.formatCost(player.snapFunds);

    }

}
