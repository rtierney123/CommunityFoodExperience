using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckWrapper : FormWrapper
{
    public GameObject check1;
    public GameObject check2;

    private void Awake()
    {
        containsText = false;
    }

    private void Start()
    {
        doneWithFillingOut = false;
        check1.SetActive(false);
        check2.SetActive(false);
    }

    public override void fillOut()
    {
        
        if (checkChoice == CheckmarkType.Check1)
        {
            check1.SetActive(true);
            check2.SetActive(false);
        } else if (checkChoice == CheckmarkType.Check2)
        {
            check1.SetActive(false);
            check2.SetActive(true);
        } else if (checkChoice == CheckmarkType.None)
        {
            check1.SetActive(false);
            check2.SetActive(false);
        }

        doneWithFillingOut = true;
    }


}
