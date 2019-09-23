using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public class DeleteButton : MonoBehaviour
{
    public GameObject purchase;

    double price = 1.99;

    void Start() {
        foreach (Transform child in purchase.transform) {
            if (child.name == "ValueText") {
                Cart.totalPrice = Convert.ToDouble(child.gameObject.GetComponent<Text>().text);
            } 
        }
    }

    public void buttonClicked() {
        Cart.totalPrice -= price;
        foreach (Transform child in purchase.transform) {
            if (child.name == "ValueText") {
                child.gameObject.GetComponent<Text>().text = Cart.totalPrice.ToString();
            } 
        }
    }
}
