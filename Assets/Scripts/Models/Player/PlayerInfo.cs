
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
    public int gender;
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
        public FamilyMember[] list = { };
    }

    // temp json workaround
    //public string c_hack;

    public double socialSecurityIncome = 870;
    public double monthlyIncome = 5000;
    public double additionalIncome = 0;
    public double temporaryAssistance = 0;
    public int numInHouse = 2;
    //public double expenses = 855;

    public int startingBusTokens = 1;
    public bool busPass = true;
    public bool hasCar = true;


    public bool pregnant;
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

    public double rentExpense = 0;
    public double utilitiesExpense = 0;
    public double medicalExpense = 0;
    public double transportationExpense = 0;
    public double phoneExpense = 0;
    public double childcareExpense = 0;
    public double taxesExpense = 0;
    public double otherExpense = 0;

    public bool paysTaxes = true;
    public string healthState = "";

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

  
    public CheckmarkType getInfoCheck(FormQuestionType question)
    {
        switch (question)
        {
            case FormQuestionType.Gender:
                {
                    GenderType genderType = (GenderType)gender;
                    
                    if(genderType == GenderType.Male)
                    {
                        return CheckmarkType.Check1;
                    } else if(genderType == GenderType.Female)
                    {
                        return CheckmarkType.Check2;
                    }
                    return CheckmarkType.None;
                }
            case FormQuestionType.Married:
                {
                    if (married)
                    {
                        return CheckmarkType.Check1;
                    } else
                    {
                        return CheckmarkType.Check2;
                    }
                }
            case FormQuestionType.Is_Employed:
                {
                    if(hourlyIncome > 0)
                    {
                        return CheckmarkType.Check1;
                    } else
                    {
                        return CheckmarkType.Check2;
                    }
                }
            case FormQuestionType.In_US_Legally:
                {
                    return CheckmarkType.Check1;
                }
            case FormQuestionType.Is_Pregnant:
                if (pregnant)
                {
                    return CheckmarkType.Check1;
                }
                else
                {
                    return CheckmarkType.Check2;
                }
            case FormQuestionType.Has_Job:
                if (hasJob())
                {
                    return CheckmarkType.Check1; 
                } else
                {
                    return CheckmarkType.Check2;
                }

            case FormQuestionType.WicType_1:
                {
                    bool checkDisplay = checkDisplayWic(1);
                    if (checkDisplay)
                    {
                        return CheckmarkType.Check1;
                    } else
                    {
                        return CheckmarkType.None;
                    }
                }
            case FormQuestionType.WicType_2:
                {
                    bool checkDisplay = checkDisplayWic(2);
                    if (checkDisplay)
                    {
                        return CheckmarkType.Check1;
                    }
                    else
                    {
                        return CheckmarkType.None;
                    }
                }
            case FormQuestionType.WicType_3:
                {
                    bool checkDisplay = checkDisplayWic(3);
                    if (checkDisplay)
                    {
                        return CheckmarkType.Check1;
                    }
                    else
                    {
                        return CheckmarkType.None;
                    }
                }
            case FormQuestionType.WicType_4:
                {
                    bool checkDisplay = checkDisplayWic(4);
                    if (checkDisplay)
                    {
                        return CheckmarkType.Check2;
                    }
                    else
                    {
                        return CheckmarkType.None;
                    }
                }
           


        }
        return CheckmarkType.None;
    }


    public bool checkDisplayWic(int num)
    {
        if(num < 4)
        {
            return checkIfHaveChild(num-1);
        } else
        {
            //add if have pregnant spouse
            return false;
        }

    }



    public int getNumEligableForWic()
    {
        int numEligable = 0;
        if (pregnant)
        {
            numEligable++;
        }

        //TODO check if spouse is pregnant


        int childNum = 0;
        foreach(FamilyMember child in children.list)
        {
            if (checkIfChildEligibleWic(childNum))
            {
                numEligable++;
            }
            childNum++;
        }

        return numEligable;
    }

    private bool checkIfChildEligibleWic(int num)
    {
        FamilyMember[] kids = children.list;
        if (num < kids.Length)
        {
            if (kids[num].age <= 5)
            {
                return true;
            }
        }
        return false;
    }



    private bool checkIfHaveChild(int num) {
            FamilyMember[] kids = children.list;
            if (num < kids.Length)
            {
                return true;
            }
            return false;
    }

    //TODO add variable for wife
    private bool checkIfHaveSpouse()
    {
        return false;
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
            case FormQuestionType.DOB:
                return getDOB();
            case FormQuestionType.Num_Children:
                return getNumofChildren().ToString();
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
                bool valid = false;
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
            case FormQuestionType.Monthly_Income:
                //Debug.Log("get monthly income" + monthlyIncome);
                return FormatText.formatDouble(getFormIncome());
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
                return FormatText.formatBool(pregnant);
            case FormQuestionType.Child_Age_1:
                return getChildAgeStr(0);
            case FormQuestionType.Child_Name_1:
                return getChildNameStr(0);
            case FormQuestionType.Child_Age_2:
                return getChildAgeStr(1);
            case FormQuestionType.Child_Name_2:
                return getChildNameStr(1);
            case FormQuestionType.Child_Age_3:
                return getChildAgeStr(2);
            case FormQuestionType.Child_Name_3:
                return getChildNameStr(2);
            case FormQuestionType.Ages_Of_Children:
                return getChildrenAges();
            case FormQuestionType.Age:
                return age.ToString();
            case FormQuestionType.Description:
                return description;
            case FormQuestionType.Transportation:
                return getStartingTransportationString();
            case FormQuestionType.RentExpense:
                return FormatText.formatDouble(rentExpense);
            case FormQuestionType.UtilitiesExpense:
                return FormatText.formatDouble(utilitiesExpense);
            case FormQuestionType.MedicalExpense:
                return FormatText.formatDouble(medicalExpense);
            case FormQuestionType.TransportationExpense:
                return FormatText.formatDouble(transportationExpense);
            case FormQuestionType.PhoneExpense:
                return FormatText.formatDouble(phoneExpense);
            case FormQuestionType.ChildcareExpense:
                return FormatText.formatDouble(childcareExpense);
            case FormQuestionType.TaxesExpense:
                return FormatText.formatDouble(taxesExpense);
            case FormQuestionType.OtherExpense:
                return FormatText.formatDouble(otherExpense);
            case FormQuestionType.TotalIncome:
                double total = getTotalIncome();
                return FormatText.formatDouble(total);
            case FormQuestionType.TotalLeftAfterExpenses:
                double totalLeft = getTotalIncome() + rentExpense + utilitiesExpense + medicalExpense + transportationExpense +
                    phoneExpense + childcareExpense + taxesExpense + otherExpense;
                return FormatText.formatCost(totalLeft);
        }
        return "";

    }

    private string getChildrenAges()
    {
        FamilyMember[] childList = children.list;
        string ageStr = "";
        bool firstChild = true;
        foreach (FamilyMember child in childList)
        {
            if (firstChild)
            {
                ageStr = FormatText.formatInt(child.age);
                firstChild = false;
            } else
            {
                ageStr = ageStr + "," + FormatText.formatInt(child.age);
            }
        }
        return ageStr;
    }

    private string getChildAgeStr(int childNum)
    {
        FamilyMember[] childList = children.list;
        if(childList.Length > childNum)
        {
            FamilyMember child = childList[childNum];
            double age = child.age;
            return FormatText.formatDouble(age);
        }
        return "";
    }

    private string getChildNameStr(int childNum)
    {
        FamilyMember[] childList = children.list;
        if (childList.Length > childNum)
        {
            FamilyMember child = childList[childNum];
            return child.name;
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
        return month + "/" + day + "/" + year;
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

    public double getStartingCash()
    {
        double monthlyCash = getTotalIncome() + rentExpense + utilitiesExpense + medicalExpense + transportationExpense +
            phoneExpense + childcareExpense + taxesExpense + otherExpense;
        double dailyCash = (monthlyCash / (30 * numInHouse));
        double cashForDay = Math.Round(dailyCash, 2);
        return monthlyCash > 0 ? cashForDay : 0;
    }
    public String getStartingTransportationString()
    {
        string returnStr = "";
        string carStr = "";
        string busPassStr = "";
        string ticketStr = "";
        if(hasCar)
        {
            carStr = "You own a car. You can use this to travel anywhere.";
            returnStr = carStr + " ";
        }
        if (busPass)
        {
            busPassStr = "You paid for a monthly bus pass. You can use this for unlimited bus rides.";
            returnStr = returnStr + busPassStr + " ";
        }
        if(startingBusTokens > 0)
        {
            if(startingBusTokens == 1)
            {
                ticketStr = "You have {0} bus ticket. Each ticket will take you to one bus stop.";
            }
            else
            {
                ticketStr = "You have {0} bus tickets. Each ticket will take you to one bus stop.";
            }
           
            ticketStr = string.Format(ticketStr, startingBusTokens);
            returnStr = returnStr + ticketStr + " ";
        }

        if(!hasCar && !busPass && startingBusTokens == 0)
        {
            returnStr = "You do not have a car, bus pass or bus tokens. You can walk in your local neighborhood or purchase a bus pass or bus tokens from the bus driver.";
        }

        return returnStr;
    }

    public bool hasJob()
    {
        if(hourlyIncome > 0)
        {
            return true;
        } else
        {
            return false;
        }
    }

    public void setRecievedAssistance(bool assistanceReceived)
    {
        federalAssistance = assistanceReceived;
    }

    public double getTotalIncome()
    {
        return monthlyIncome + temporaryAssistance + socialSecurityIncome + additionalIncome;
    }

    public double getFormIncome()
    {
        return monthlyIncome + temporaryAssistance + socialSecurityIncome;
    }
}
    