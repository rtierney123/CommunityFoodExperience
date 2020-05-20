using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

public class FooterDisplay : View
{
    public CurrencyManager currencyManager;
    public Text mainScreenCashText;
    public Text mainScreenSnapText;
    public Text mainScreenVoucherText;
    public override void updateView()
    {
        double cash = currencyManager.getCashAvailable();
        mainScreenCashText.text = FormatText.formatCash(cash);
        double snap = currencyManager.getSnapAvailable();
        mainScreenSnapText.text = FormatText.formatSNAP(snap);

    }

}
