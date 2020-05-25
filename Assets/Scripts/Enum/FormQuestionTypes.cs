using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormQuestionType
{
    // Name
    FullName = 0,
    First_Name = 17,
    Last_Name = 18,
    Middle_Name = 19,

    //Gender
    Gender = 64,

    // Address
    Address = 1,
    City = 13,
    State = 14,
    Zip = 15,

    // General Information
    Phone = 2,
    Num_Children = 3,
    Single = 7,
    Married = 8,
    // Leave empty if does not have
    SNN = 24,
    DOB = 25,
    Sex = 29,
    Primary_Language = 30,

    // string either WIC or SNAP, leave empty string "" if have none
    Federal_Assistance = 16,
    Is_Employed = 33,
    Hourly_Wage = 35,
    Monthly_Income = 4,
    Annual_Income = 20,
    All_Income_In_US = 22,
    In_US_More_Than_Six_Month = 21,
    In_US_Legally = 34,
    Income_Less_than_3150 = 23,

    Num_In_Household = 26,
    Signature = 27,

    // Date when sign the paper
    Date = 28,
    Live_In_Public_Housing = 31,
    Is_Pregnant = 32,
    Proof_Of_Transportation = 36,
    Has_Fleeing_Member = 37,
    Has_Mispresent_Identity_Member = 38,
    Has_Guity_Drug_Member = 39,
    Has_Guity_Foodstamp_Member = 40,
    Birth_Month = 41,
    Birth_Day = 42,
    Birth_Year = 43,
    Joint_Tax = 9,
    Child_Age_1 = 50,
    Child_Name_1 = 51,
    Child_Age_2 = 52,
    Child_Name_2 = 53,
    Child_Age_3 = 54,
    Child_Name_3 = 55,
    Aid = 6,

    //wic validations
    WicType_1 = 59,
    WicType_2 = 60,
    WicType_3 = 61,
    WicType_4 = 62,

    Ages_Of_Children = 63,

    Has_Job = 70,
    Age = 71,
    Description = 72
}
