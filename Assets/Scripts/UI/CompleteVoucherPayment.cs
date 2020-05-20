using Manage;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CompleteVoucherPayment : PopUp
{

    public BaseStore store;
    public WICVoucherView voucherView;
    
    void OnEnable()
    {
        Cart cart = store.cart;
        Dictionary<Food, int> foods = cart.foodInCart;

        WICVoucher voucher = store.currencyManager.getWICVoucher();
        voucherView.setVoucher(voucher);
        voucherView.updateView();
        foreach(Food food in foods.Keys)
        {
            voucherView.displayPotentialCheck(food);
        }
    }

    void OnDisable()
    {
        Debug.Log("clear");
        voucherView.clearTempChecks();
    }




}
