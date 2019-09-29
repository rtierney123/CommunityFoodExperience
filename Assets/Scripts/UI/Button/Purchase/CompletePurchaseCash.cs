using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Manage;
using System;

public class CompletePurchaseCash : MonoBehaviour
{
    public Text cashValue;
    public Text ctcValue;
    public Text eitcValue;
    public Text snapValue;
    public Text errorMessage;

    public CanvasController canvasController;
    public GameObject playerGameObject;
    public GameObject cartGameObject;
    public GameObject errorPopUp;
    Player player;
    Cart cart;

    double cash;
    double ctc;
    double eitc;
    double snap;

    public void Start() {
        cart = cartGameObject.GetComponent<Cart>();
    }

    public void pay() {
        cash = string.IsNullOrEmpty(cashValue.text) ? 0 : Convert.ToDouble(cashValue.text);
        ctc = string.IsNullOrEmpty(ctcValue.text) ? 0 : Convert.ToDouble(ctcValue.text);
        eitc = string.IsNullOrEmpty(eitcValue.text) ? 0 : Convert.ToDouble(eitcValue.text);
        snap = string.IsNullOrEmpty(snapValue.text) ? 0 : Convert.ToDouble(snapValue.text);
        player = playerGameObject.GetComponent<Player>();

        errorMessage.text = validate();
        
        if (!string.IsNullOrEmpty(errorMessage.text)) {
            errorPopUp.SetActive(true);
        } else {
            if (canvasController != null)
            {
                canvasController.closePopUp();
            }
            player.money -= cash;
            player.ctcFunds -= ctc;
            player.eitcFunds -= eitc;
            player.snapFunds -= snap;
            paySucceed();
        }
    }

    private string validate() {
        if (player.money < cash) {
            return "Not enough cash";
        } else if (player.ctcFunds < ctc) {
            return "Not enough CTC fund";
        } else if (player.eitcFunds < eitc) {
            return "Not enough EITC fund";
        } else if (player.snapFunds < snap) {
            return "Not enough SNAP fund";
        } else if (roundTwoDecimal(cash + ctc + eitc + snap) != roundTwoDecimal(Convert.ToDouble(cart.totalText.text))) {
            return "Total amount does not match";
        } else {
            return "";
        }
    }

    private void paySucceed() {
        player.calories += cart.getCalories();
        player.fruit = cart.getFruit();
        player.fat = cart.getFat();
        player.protein = cart.getProtein();
        player.dairy = cart.getDairy();
        player.extra = cart.getExtra();
        player.vegetabele = cart.getVeg();
        player.grain = cart.getGrain();
    }

    public void roundAllCurrencies() {
        player.money = Math.Round(player.money * 100) / 100;
        player.ctcFunds = Math.Round(player.ctcFunds * 100) / 100;
        player.eitcFunds = Math.Round(player.eitcFunds * 100) / 100;
        player.snapFunds = Math.Round(player.snapFunds * 100) / 100;
    }

    private double roundTwoDecimal(double num) {
        return Math.Round(num * 100) / 100;
    }
}
