using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FormQuestionType
{
    FullName = 0,
    Address = 1,
    Phone = 2,
    Num_Children = 3,
    Monthly_Income = 4,
    Children_Age = 5,
    Aid = 6,
    Single = 7,
    Married = 8,
    Joint_Tax = 9,
    City = 13,
    State = 14,
    Zip = 15,
    // string either WIC or SNAP, leave empty string "" if have none
    Federal_Assistance = 16,
    First_Name = 17,
    Last_Name = 18,
    Middle_Name = 19,
    Annual_Income = 20,
    In_US_More_Than_Six_Month = 21,
    All_Income_In_US = 22,
    Income_Less_than_3150 = 23,
    // Leave empty if does not have
    SNN = 24,
    DOB = 25,
    Num_In_Household = 26,
    Signature = 27,

    // Date for signing
    Date = 28,
    Sex = 29,
    Primary_Language = 30,
    Live_In_Public_Housing = 31,
    Is_Pregnant = 32,
    Is_Employed = 33,
    In_US_Legally = 34,
    Hourly_Wage = 35,
    Proof_Of_Transportation = 36,
    Has_Fleeing_Member = 37,
    Has_Mispresent_Identity_Member = 38,
    Has_Guity_Drug_Member = 39,
    Has_Guity_Foodstamp_Member = 40

}
