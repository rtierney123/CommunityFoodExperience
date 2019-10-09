
using UI;
using UnityEngine;

namespace Manage
{
    public class ErrorManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public ErrorPopUp standardErrorPopup;

        public void generateStandardMessage(string message)
        {
            
            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.forcePopupOpen(popup);

        }
    }
}

