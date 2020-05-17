using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CompleteVoucherPayment : MonoBehaviour
{

    public BaseStore store;
    public WICVoucher voucher;
    
    void OnEnable()
    {
        Cart cart = store.cart;
        Dictionary<Food, int> foods = cart.foodInCart;

        WICVoucher playerVoucher = store.currencyManager.getWICVoucher();
        voucher.copy(playerVoucher);
        foreach(Food food in foods.Keys)
        {
            voucher.displayPotentialCheck(food);
        }
    }

    void OnDisable()
    {
        Debug.Log("clear");
        voucher.clearTempChecks();
    }




}
