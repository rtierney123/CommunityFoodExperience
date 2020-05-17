using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FamilyMember 
{
    public int age;
    public string name;
    public string ssn;

    public bool pregnant = false;
    
    public FamilyMember() {
        age = 0;
        name = "";
        ssn = "xxx-xx-xxxx";
    }
    public FamilyMember(int age, string name, string ssn) {
        this.age = age;
        this.name = name;
        this.ssn = ssn;
    }

    public void setPregnant(bool state)
    {
        pregnant = state;
    }

}
