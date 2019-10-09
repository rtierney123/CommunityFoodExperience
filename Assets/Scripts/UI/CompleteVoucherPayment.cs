using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class CompleteVoucherPayment : MonoBehaviour
{

    public Store store;
    public WICVoucher voucher;
    
    void OnEnable()
    {
        Debug.Log("enabled");
        Cart cart = store.cart;
        HashSet<Food> foods = cart.foodInCart;

        Debug.Log(foods.Count);
        foreach(Food food in foods)
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
