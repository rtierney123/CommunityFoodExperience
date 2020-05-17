using Manage;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class FooterDisplay : MonoBehaviour
{
    public CurrencyManager currencyManager;
    public Text mainScreenCashText;
    public Text mainScreenEITCText;
    public Text mainScreenCTCText;
    public Text mainScreenSnapText;

    public void update()
    {
        double cash = currencyManager.getCashAvailable();
        mainScreenCashText.text = FormatText.formatCost(cash);
        //mainScreenEITCText.text = FormatText.formatCost(player.eitcFunds);
        //mainScreenCTCText.text = FormatText.formatCost(player.ctcFunds);
        double snap = currencyManager.getSnapAvailable();
        mainScreenSnapText.text = FormatText.formatCost(snap);

    }

}
