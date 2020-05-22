
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

        public void generateStandardErrorMessage(string message, View view)
        {

            generateStandardErrorMessage(message);
            view.updateView();

        }

        public void generateStandardErrorMessage(string message)
        {

            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.forcePopupOpen(popup);

        }

        public void generateStandardSuccessMessage(string message, View view)
        {

            generateStandardSuccessMessage(message);
            view.updateView();

        }

        public void generateStandardSuccessMessage(string message)
        {

            standardSuccessPopup.setText(message);
            GameObject popup = standardSuccessPopup.gameObject;
            canvasController.forcePopupOpen(popup);

        }


        public void generateMainScreenOnlyErrorMessage(string message)
        {
            standardErrorPopup.setText(message);
            GameObject popup = standardErrorPopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(popup);
        }


        public void generateMainScreenOnlySuccessMessage(string message)
        {
            standardSuccessPopup.setText(message);
            GameObject popup = standardSuccessPopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(popup);
        }


    }
}

