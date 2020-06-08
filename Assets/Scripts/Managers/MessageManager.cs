
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

        public Transform statusHolder;

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
            MessagePopup messagePopup = Instantiate<MessagePopup>(standardErrorPopup, statusHolder);
            messagePopup.setText(message);
            GameObject obj = messagePopup.gameObject;
            canvasController.forcePopupOpen(obj);
            return obj;
        }

        public GameObject generateStandardSuccessMessage(string message, View view)
        {

            GameObject popup = generateStandardSuccessMessage(message);
            view.updateView();
            return popup;
        }

        public GameObject generateStandardSuccessMessage(string message)
        {
            MessagePopup messagePopup = Instantiate<MessagePopup>(standardSuccessPopup, statusHolder);
            messagePopup.setText(message);
            GameObject obj = messagePopup.gameObject;
            canvasController.forcePopupOpen(obj);
            return obj;
        }


        public GameObject generateMainScreenOnlyErrorMessage(string message)
        {
            MessagePopup messagePopup = Instantiate<MessagePopup>(standardErrorPopup, statusHolder);
            messagePopup.setText(message);
            GameObject obj = messagePopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(obj);
            return obj;
        }


        public GameObject generateMainScreenOnlySuccessMessage(string message)
        {
            MessagePopup messagePopup = Instantiate<MessagePopup>(standardSuccessPopup, statusHolder);
            messagePopup.setText(message);
            GameObject obj = messagePopup.gameObject;
            canvasController.addToMainScreenPopUpBackLog(obj);
            return obj;
        }


    }
}

