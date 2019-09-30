
using UI;
using UnityEngine;

namespace Manage
{
    public class ErrorManager : MonoBehaviour
    {
        public CanvasController canvasController;
        public ErrorPopUp standardErrorPopup;
        public GameObject standardErrorGameObject;

        public void generateStandardMessage(string message)
        {
            
            standardErrorPopup.setText(message);
            canvasController.forcePopupOpen(standardErrorGameObject);
            Debug.Log("open error");


        }
    }
}

