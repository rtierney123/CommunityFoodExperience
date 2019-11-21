
using UI;
using UnityEngine;

namespace Manage
{
    public class MessageManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public MessagePopup standardErrorPopup;
        public MessagePopup standardSuccessPopup;
        public void generateStandardErrorMessage(string message)
        {

            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.forcePopupOpen(popup);

        }

        public void generateStandardSuccessMessage(string message)
        {

            standardSuccessPopup.setText(message);
            GameObject popup = standardSuccessPopup.gameObject;
            canvasController.forcePopupOpen(popup);
            Debug.Log("display popup");

        }

    }
}

