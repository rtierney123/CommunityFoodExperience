using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class AboutPlayerScreen : Screen
    {
        public Player player;

        public TextWrapper[] info;

        public override void reset()
        {
            PlayerInfo playerInfo = player.playerInfo;
            foreach (TextWrapper item in info)
            {
                item.setText(playerInfo.getInfoText(item.questionType));
            }

        }
    }

}
