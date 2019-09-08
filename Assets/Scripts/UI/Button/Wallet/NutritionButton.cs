using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manage;

public class NutritionButton : MonoBehaviour
{
    public GameObject nutrition;
    public CanvasController canvasController;
    
    public void nutritionButtonClicked() {
        if(!nutrition.activeInHierarchy){
            canvasController.openPopup(nutrition);
        } else {
            canvasController.closePopUp();
        }
    }


}
