using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Model
{
    public class HomeLocation : Location
    {
        public GameObject homePopup;

        public override void onDelayedEnter()
        {
            canvasController.delayOpenMainScreenPopup(homePopup);

        }

        public override void onImmediateEnter()
        {
            canvasController.addToMainScreenPopUpBackLog(homePopup);
        }
    }
}

