using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    // temp player info. will place in better place later
    public string name;
    public string characterDescription = "You are an elderly person (senior citizen) and you live alone on a fixed income of $870 a month from Social Security. Your rent for a small apartment is $600/month and you pay $50/month for electricity, $30 month for a phone and $185/month for two prescriptions that are only partially coverd by Medicare.";
    public string phoneNum = "(404) 888-9360";
    public string socialSecurity = "XXX-XX-6789";
    public string address = "258 W.Real St. Atlanta, GA 30317";
    public int fixedIncome = 870;
    public int expenses = 855;
    public int money = 15;
    int busTokens = 1;
    bool vita = false;
    bool snap = false;
    bool wic = false;
    int progress = 0;
    int carbs = 0;
    int protein = 0;
    int fruits = 0;
    int veggies = 0;


}
