using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI{
    public class ExpenseScreen : Screen
    {
        public Player player;
        public TextWrapper[] items;
        public override void reset()
        {
            base.reset();
            foreach(TextWrapper item in items)
            {
                string value = player.playerInfo.getInfoText(item.questionType);
                item.setText(value);
            }

        }
    }
}

