using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormWrapper : MonoBehaviour
{
    [HideInInspector]
    public bool containsText;
    [HideInInspector]
    public bool doneWithFillingOut;
    [HideInInspector]
    public bool initialized = false;
    [HideInInspector]
    public bool pauseFillingOut = false;

    public FormQuestionType questionType;
    protected string contents = "";
    protected CheckmarkType checkChoice = CheckmarkType.None;


    protected virtual void Awake()
    {
        initialized = true;
    }

    public virtual void fillOut()
    {

    }

    public virtual void setInfo(string info)
    {
        contents = info;
    }

    public virtual void setCheck(CheckmarkType check)
    {
        checkChoice = check;
    }

    public virtual void resetWrapper()
    {
        //reset to fill out form again
        doneWithFillingOut = false;
    }


}
