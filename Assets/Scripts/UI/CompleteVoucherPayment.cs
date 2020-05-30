using Manage;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;

public class CompleteVoucherPayment : PopUp
{

    public Store store;
    public WICVoucherView voucherView;
    public CurrencyManager currencyManager;
    void OnEnable()
    {
        Cart cart = store.cart;
        Dictionary<Food, int> foods = cart.foodInCart;

        WICVoucher voucher = store.currencyManager.getWICVoucher();
        voucherView.setVoucher(voucher);
        voucherView.updateView();

        List<Food> f = foods.Keys.ToList();
        List<FoodType> wicList = currencyManager.getWICArray(f);
        if(wicList != null)
        {
            foreach (FoodType type in wicList)
            {
                voucherView.displayPotentialCheck(type);
            }
        }
        
    }

    void OnDisable()
    {
        Debug.Log("clear");
        voucherView.clearTempChecks();
    }




}
