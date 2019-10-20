using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{


    // temp player info. will place in better place later
    public string characterName = "Sam Smith";
    public string firstName = "Sam";
    public string middleName = "";
    public string lastName = "Smith";
    public string characterDescription = "You are an elderly person (senior citizen) and you live alone on a fixed income of $870 a month from Social Security. Your rent for a small apartment is $600/month and you pay $50/month for electricity, $30 month for a phone and $185/month for two prescriptions that are only partially coverd by Medicare.";
    public string phoneNum = "(404) 888-9360";
    public string socialSecurity = "XXX-XX-6789";
    public string address = "258 W.Real St. Atlanta, GA 30317";
    public string city = "Atlanta";
    public string state = "GA";
    public string zip = "30317";
    public string DOB = "01/01/2010";
    public string federalAssistance = "Yes";
    public string inUSSixMonth = "Yes";
    public string allIncomeInUS = "No";
    public string incomeLessThan3150 = "Yes";
    public string numOfChildren = "3";
    public string childrenAges = "2, 3, 4";
    public int fixedIncome = 870;
    public double annualIncome = 5000;
    public int expenses = 855;
    public int busTokens = 1;


    //Need to delete this and refactor wallet
    // public bool vita = false;
    // public bool snap = false;
    // public bool wic = false;
    // public bool ctc = false;
    // public bool eitc = false;

    public double money = 15;
    public double snapFunds = 0;
    public double ctcFunds = 0;
    public double eitcFunds = 0;

    public int progress = 0;

    public double calories = 0;
    public double grain = 0;
    public double fat = 0;
    public double protein = 0;
    public double dairy = 0;
    public double fruit = 0;
    public double vegetabele = 0;
    public double extra = 0;

    public double requiredCalories = 5;
    public double requiredGrain = 1;
    public double requiredFat = 1;
    public double requiredProtein = 3;
    public double requiredDairy = 2;
    public double requiredFruit = 1;
    public double requiredVegetable = 2;
    public double requiredExtra = 0;

    public WICVoucher wicVoicher;
    private bool hasWic = false;

    private void Start()
    {
        addVoucher();
    }

    //string[] foodAcquired = [];

    // view player information
    public int[] getNutrition()
    {
        // return { carbs, protein, fruits, veggies};
        return null;
    }
    public int getProgress()
    {
        return progress;
    }

    //.....

    // update player information
    public void setMoney(double value)
    {
        money = value;
    }
    public void setBusTokens(int value)
    {
        busTokens = value;
    }


    //.....

    // update player nutrition
    public void addNutrition(Food food)
    {
        calories += food.calories;
        grain += food.nutrition.grain;
        fat += food.nutrition.fat;
        protein += food.nutrition.protein;
        dairy += food.nutrition.dairy;
        fruit += food.nutrition.fruit;
        vegetabele += food.nutrition.veg;
        extra += food.nutrition.extra;

    }

    public void addCash(double cash)
    {
        money += cash;
    }

    public void subtractCash(double cash)
    {
        money -= cash;
    }

    public void addSnap(double funds)
    {
        snapFunds += funds;
    }

    public void subtractSnap(double funds)
    {
        snapFunds -= funds;
    }

    public void addEITC(double funds)
    {
        eitcFunds += funds;
    }

    public void subtractEITC(double funds)
    {
        eitcFunds -= funds;
    }

    public void addCTC(double funds)
    {
        ctcFunds += funds;
    }

    public void subtractCTC(double funds)
    {
        ctcFunds -= funds;
    }

    public void addVoucher()
    {
        hasWic = true;
        wicVoicher.gameObject.SetActive(true);
    }

    //TODO need to alter this logic to deal with what if multiple vouchers
    //do I need to worry about this;
    public void useVoucher(Food food)
    {
        if (wicVoicher != null)
        {
            if (wicVoicher.checkValid(food))
            {
                wicVoicher.useVoucher(food);
            }

        }


    }

    public string getFullName() {
        print(firstName);
        return firstName + (String.IsNullOrEmpty(middleName) ? "" : (" " + middleName)) + " " + lastName;
    }

    public string getFullAddress() {
        return address + ", " + city + ", " + state + ", " + zip;
    }

    public string formatFunds(double funds)
    {
        return String.Format("{0:0.##}", funds);
    }

    public string getInfo(FormQuestionType question)
    {
        switch(question)
        {
            case FormQuestionType.FullName:
                return getFullName();
            case FormQuestionType.Address:
                return address;
            case FormQuestionType.Phone:
                return phoneNum;
            case FormQuestionType.Num_Children:
                return numOfChildren;
            case FormQuestionType.Monthly_Income:
                return "" + fixedIncome;
            case FormQuestionType.Children_Age:
                return childrenAges;
            case FormQuestionType.Aid:
                return (snapFunds + ctcFunds + eitcFunds == 0) ? "No":"Yes";
            case FormQuestionType.Single:
                return "Yes";
            case FormQuestionType.Married:
                return "No";
            case FormQuestionType.Joint_Tax:
                return "No";
            case FormQuestionType.In_US_More_Than_Six_Month:
                return "Yes";
            case FormQuestionType.All_Income_In_US:
                return "Yes";
            case FormQuestionType.Income_Less_than_3150:
                return "Yes";
            case FormQuestionType.First_Name:
                return firstName;
            case FormQuestionType.Last_Name:
                return lastName;
            case FormQuestionType.Middle_Name:
                return middleName;
            case FormQuestionType.City:
                return city;
            case FormQuestionType.State:
                return state;
            case FormQuestionType.Zip:
                return zip;
            case FormQuestionType.Annual_Income:
                return "" + annualIncome;
            case FormQuestionType.Federal_Assistance:
                return federalAssistance;
        }
        return "";
            
    }

}