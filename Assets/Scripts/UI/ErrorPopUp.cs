using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorPopUp : MonoBehaviour
{
    public GameObject errorPopUp;
    public void confirm() {
        errorPopUp.SetActive(false);
    }
}
