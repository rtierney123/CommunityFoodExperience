
using UI;
using UnityEngine;


namespace Manage
{
    public class MessageManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public MessagePopup standardErrorPopup;
        public MessagePopup standardSuccessPopup;
        public MessagePopup homePopup;
        public HintMessage hintMessage;
        public DismissMessagePopup dismissPopup;

        public void generateHomePopup(string message)
        {
            homePopup.setText(message);
            canvasController.forcePopupOpen(homePopup.gameObject);
        }

        public void generateDismissPopup(string message, View view)
        {

            dismissPopup.setText(message);
            dismissPopup.setDismissed(view);
            canvasController.forcePopupOpen(dismissPopup.gameObject);
        }
        public void displayHintMessage(string message)
        {
            hintMessage.setText(message);
            hintMessage.display();
        }

        public void hideHintMessage()
        {
            hintMessage.hide();
        }

        public GameObject generateStandardErrorMessage(string message, View view)
        {

            GameObject popup = generateStandardErrorMessage(message);
            view.updateView();
            return popup;

        }

        public GameObject generateStandardErrorMessage(string message)
        {

            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.forcePopupOpen(popup);
            return popup;
        }

        public GameObject generateStandardSuccessMessage(string message, View view)
        {

            GameObject popup = generateStandardSuccessMessage(message);
            view.updateView();
            return popup;
        }

        public GameObject generateStandardSuccessMessage(string message)
        {

            standardSuccessPopup.setText(message);
            GameObject popup = standardSuccessPopup.gameObject;
            canvasController.forcePopupOpen(popup);
            return popup;
        }


        public GameObject generateMainScreenOnlyErrorMessage(string message)
        {
            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(popup);
            return popup;
        }


        public GameObject generateMainScreenOnlySuccessMessage(string message)
        {
            standardSuccessPopup.setText(message);
            GameObject popup = standardSuccessPopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(popup);
            return popup;
        }


    }
}

