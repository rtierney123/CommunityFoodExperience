﻿using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using UnityEngine.UI;
using Utility;

[System.Serializable]
public class Player : MonoBehaviour
{


    // temp player info. will place in better place later

    public string firstName = "Sam";
    public string middleName = "";
    public string lastName = "Smith";
    public string characterDescription = "You are an elderly person (senior citizen) and you live alone on a fixed income of $870 a month from Social Security. Your rent for a small apartment is $600/month and you pay $50/month for electricity, $30 month for a phone and $185/month for two prescriptions that are only partially coverd by Medicare.";
    public string phone = "(404) 888-9360";
    public string ssn = "XXX-XX-6789";
    public string address = "258 W.Real St. Atlanta";
    public string city = "Atlanta";
    public string state = "GA";
    public string zip = "30317";
    public string primaryLanguage = "English";
    public int age = 78;
    public int birthMonth = 12;
    public int birthDay = 1;

    public ChildList children;
    [Serializable]
    public class ChildList
    {
        public FamilyMember[] list;
    }

    public int socialSecurityIncome = 870;
    public double annualIncome = 5000;
    public int numInHouse = 2;
    public int expenses = 855;

    public int busTokens = 1;
    public bool busPass = true;
    public bool hasCar = true;


    public bool pregant;
    public bool single;
    public bool married;
    public bool jointTax;

    public bool inUSSixMonth;
    public bool allIncomeInUS;

    public double money = 15;
    public double snapFunds = 0;
    public double ctcFunds = 0;
    public double eitcFunds = 0;

    //MIGHT NEED TO DELETE THIS
    public bool federalAssistance;
    public double hourlyIncome = 10;

    public double requiredCalories = 5;
    public double requiredGrain = 1;
    public double requiredFat = 1;
    public double requiredProtein = 3;
    public double requiredDairy = 2;
    public double requiredFruit = 1;
    public double requiredVegetable = 2;
    public double requiredExtra = 0;

    public double calories = 0;
    public double grain = 0;
    public double fat = 0;
    public double protein = 0;
    public double dairy = 0;
    public double fruit = 0;
    public double vegetable = 0;
    public double extra = 0;

    public bool usedVita = false;
    public bool usedFoodPantry = false;
    public bool usedWIC = false;
    public bool usedSnap = false;

    public WICVoucher wicVoicher;
    [HideInInspector]
    public bool hasWic = false;

    private void Start()
    {
        addVoucher();
        wicVoicher.gameObject.SetActive(false);
    }

    //string[] foodAcquired = [];

    // view player information
    public int[] getNutrition()
    {
        // return { carbs, protein, fruits, veggies};
        return null;
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
        vegetable += food.nutrition.veg;
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
        return firstName + (String.IsNullOrEmpty(middleName) ? "" : (" " + middleName)) + " " + lastName;
    }

    public string getFullAddress() {
        return address + ", " + city + ", " + state + ", " + zip;
    }

    public string formatFunds(double funds)
    {
        return String.Format("{0:0.##}", funds);
    }

    public string getInfoText(FormQuestionType question)
    {
        switch(question)
        {
            case FormQuestionType.FullName:
                return getFullName();
            case FormQuestionType.Address:
                return address;
            case FormQuestionType.Phone:
                return phone;
            case FormQuestionType.Num_Children:
                return children.list.Length.ToString();
            case FormQuestionType.Monthly_Income:
                return "" + socialSecurityIncome;
            case FormQuestionType.Children_Age:
                return "";
            case FormQuestionType.Aid:
                return (snapFunds + ctcFunds + eitcFunds == 0) ? "No":"Yes";
            case FormQuestionType.Single:
                return FormatText.formatBool(single);
            case FormQuestionType.Married:
                return FormatText.formatBool(married);
            case FormQuestionType.Joint_Tax:
                return FormatText.formatBool(jointTax);
            case FormQuestionType.In_US_More_Than_Six_Month:
                return FormatText.formatBool(inUSSixMonth);
            case FormQuestionType.All_Income_In_US:
                return FormatText.formatBool(allIncomeInUS);
            case FormQuestionType.Income_Less_than_3150:
                double monthlyIncome = getMonthlyIncome();
                bool valid = monthlyIncome <= 3150;
                return FormatText.formatBool(valid);
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
                return FormatText.formatBool(federalAssistance);
            case FormQuestionType.Birth_Day:
                return FormatText.formatInt(birthDay);
            case FormQuestionType.Birth_Month:
                return FormatText.formatInt(birthMonth);
            case FormQuestionType.Birth_Year:
                int y = getBirthYear();
                return FormatText.formatInt(y);
            case FormQuestionType.Hourly_Wage:
                return FormatText.formatDouble(hourlyIncome);
            case FormQuestionType.Num_In_Household:
                return FormatText.formatDouble(numInHouse);
            case FormQuestionType.Primary_Language:
                return primaryLanguage;
            case FormQuestionType.Is_Pregnant:
                return FormatText.formatBool(pregant);
        }
        return "";
            
    }

    public double getMonthlyIncome()
    {
        return annualIncome / 12;
    }

    public string getDOB()
    {
        string day = FormatText.formatInt(birthDay);
        string month = FormatText.formatInt(birthMonth);
        int y = getBirthYear();
        string year = FormatText.formatInt(y);
        return day + "/" + month + "/" + year;
    }

    public int getBirthYear()
    {
        DateTime moment = DateTime.Today;

        int year = moment.Year - age;
        return year;
    }

    public int getNumofChildren()
    {
        return children.list.Length;
    }

}