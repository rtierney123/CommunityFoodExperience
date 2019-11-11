
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utility;

[System.Serializable]
public class PlayerInfo 
{
    public string firstName = "Sam";
    public string middleName = "";
    public string lastName = "Smith";
    public string description = "You are an elderly person (senior citizen) and you live alone on a fixed income of $870 a month from Social Security. Your rent for a small apartment is $600/month and you pay $50/month for electricity, $30 month for a phone and $185/month for two prescriptions that are only partially coverd by Medicare.";
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
        public FamilyMember[] childList = {};
    }

    // temp json workaround
    public string c_hack;

    public double socialSecurityIncome = 870;
    public double monthlyIncome = 5000;
    public int numInHouse = 2;
    public double expenses = 855;

    public int startingBusTokens = 1;
    public bool busPass = true;
    public bool hasCar = true;


    public bool pregant;
    public bool single;
    public bool married;
    public bool jointTax;

    public bool inUSSixMonth;
    public bool allIncomeInUS;

    public double hourlyIncome = 10;

    public double requiredCalories = 5;
    public double requiredGrain = 1;
    public double requiredFat = 1;
    public double requiredProtein = 3;
    public double requiredDairy = 2;
    public double requiredFruit = 1;
    public double requiredVegetable = 2;
    public double requiredExtra = 0;

    //do not put in json
    public bool federalAssistance;

    public string getFullName()
    {
        return firstName + (String.IsNullOrEmpty(middleName) ? "" : (" " + middleName)) + " " + lastName;
    }

    public string getFullAddress()
    {
        return address + ", " + city + ", " + state + ", " + zip;
    }

    public string formatFunds(double funds)
    {
        return String.Format("{0:C}", funds);
    }

    public bool getInfoBool(FormQuestionType quetiion)
    {
        return true;
    }

    public string getInfoText(FormQuestionType question)
    {
        switch (question)
        {
            case FormQuestionType.FullName:
                return getFullName();
            case FormQuestionType.Address:
                return address;
            case FormQuestionType.Phone:
                return phone;
            case FormQuestionType.Num_Children:
                return getNumofChildren().ToString();
            case FormQuestionType.Monthly_Income:
                return "" + socialSecurityIncome;
            case FormQuestionType.Children_Age:
                return c_hack;
            case FormQuestionType.Aid:
                return FormatText.formatBool(federalAssistance);
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
                double annual = monthlyIncome * 12;
                return "" + annual;
            case FormQuestionType.Federal_Assistance:
                return FormatText.formatBool(federalAssistance);
            case FormQuestionType.Birth_Day:
                return FormatText.formatInt(birthDay);
            case FormQuestionType.SNN:
              return ssn;
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
        return monthlyIncome;
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
        return c_hack.Equals("") ? 0 : c_hack.Split(',').Length;
        //return children.childList.Length;
    }

    public double getStartingCash()
    {
        double monthlyCash = monthlyIncome - expenses;
        double cashForDay = Math.Floor(monthlyCash / 30 / numInHouse * 100) / 100;
        return monthlyCash > 0 ? cashForDay : 0;
  }
}
    