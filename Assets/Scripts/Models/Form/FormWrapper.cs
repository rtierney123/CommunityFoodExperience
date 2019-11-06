using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FormWrapper : MonoBehaviour
{
    [HideInInspector]
    public bool containsText;
    [HideInInspector]
    public bool doneWithFillingOut;

    public FormQuestionType questionType;
    protected string contents = "";
    protected CheckmarkType checkChoice = CheckmarkType.None;
    public virtual void fillOut()
    {

    }

    public virtual void setText(string info)
    {
        contents = info;
    }

    public virtual void setCheck(CheckmarkType check)
    {
        checkChoice = check;
    }

    public void resetTextWrapper()
    {
        //reset to fill out form again
        doneWithFillingOut = false;
    }
}
