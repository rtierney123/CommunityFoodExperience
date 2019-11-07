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

        Player player = store.player;
        WICVoucher playerVoucher = player.wicVoicher;
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
