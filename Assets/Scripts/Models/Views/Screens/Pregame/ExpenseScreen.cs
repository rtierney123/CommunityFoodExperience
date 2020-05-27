using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI{
    public class ExpenseScreen : Screen
    {
        public Player player;
        public TextWrapper[] items;
        public Text remainingText;
        public Text householdText;
        public Text equationText;
        public override void reset()
        {
            base.reset();
            PlayerInfo playerInfo = player.playerInfo;
            foreach(TextWrapper item in items)
            {
                string value = player.playerInfo.getInfoText(item.questionType);
                item.setText(value);
            }
            string remainingStr = playerInfo.getInfoText(FormQuestionType.TotalLeftAfterExpenses);
            string householdStr = playerInfo.getInfoText(FormQuestionType.Num_In_Household);
            string equationStr = remainingStr + "/" + householdStr + "/30 = " + FormatText.formatCost(playerInfo.getStartingCash()) +
                " left per person per day";

            remainingText.text =  remainingStr+ " remaining for month";
            householdText.text = householdStr + " in household";
            equationText.text = equationStr;
        }
    }
}

