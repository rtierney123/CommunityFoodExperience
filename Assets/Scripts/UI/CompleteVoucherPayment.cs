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
        Cart cart = store.cart;
        HashSet<Food> foods = cart.foodInCart;

        Debug.Log("voucher enabled");

        foreach(Food food in foods)
        {
            voucher.displayPotentialCheck(food);
        }
    }




}
