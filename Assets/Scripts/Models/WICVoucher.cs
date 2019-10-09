using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WICVoucher : MonoBehaviour
{
    [HideInInspector]
    public bool fruitUsed = false;
    [HideInInspector]
    public bool vegUsed = false;
    [HideInInspector]
    public bool grainUsed = false;
    [HideInInspector]
    public bool proteinUsed = false;
    [HideInInspector]
    public bool dairyUsed = false;

    public GameObject fruitCheck;
    public GameObject vegCheck;
    public GameObject proteinCheck;
    public GameObject grainCheck;
    public GameObject dairyCheck;

    public void useVoucher(Food food)
    {
        FoodType foodType = food.wicType;
        if (checkValid(food))
        {
            switch (foodType)
            {
                case FoodType.Fruit:
                    Debug.Log("fruit used");
                    fruitUsed = true;
                    displayPermCheck(fruitCheck);
                    return;
                case FoodType.Veg:
                    vegUsed = true;
                    displayPermCheck(vegCheck);
                    return;
                case FoodType.Grain:
                    grainUsed = true;
                    displayPermCheck(grainCheck);
                    return;
                case FoodType.Protein:
                    proteinUsed = true;
                    displayPermCheck(proteinCheck);
                    return;
                case FoodType.Dairy:
                    dairyUsed = true;
                    displayPermCheck(dairyCheck);
                    return;
            }
        }
  
    }

    public void displayPotentialCheck(Food food)
    {
        FoodType foodType = food.wicType;

        if (checkValid(food))
        {
            switch (foodType)
            {
                case FoodType.Fruit:
                    displayTempCheck(fruitCheck);
                    return;
                case FoodType.Veg:
                    displayTempCheck(vegCheck);
                    return;
                case FoodType.Grain:
                    displayTempCheck(grainCheck);
                    return;
                case FoodType.Protein:
                    displayTempCheck(proteinCheck);
                    return;
                case FoodType.Dairy:
                    displayPermCheck(dairyCheck);
                    return;
            }
        }
    }

    public void clearTempChecks()
    {
        Debug.Log("clear");
        if (!fruitUsed)
        {
            Debug.Log("fruit");
            fruitCheck.SetActive(false);
        }
        if (!vegUsed)
        {
            vegCheck.SetActive(false);
        }
        if (!grainUsed)
        {
            grainCheck.SetActive(false);
        }
        if (!proteinUsed)
        {
            proteinCheck.SetActive(false);
        }
        if (!dairyUsed)
        {
            dairyCheck.SetActive(false);
        }
    }

    public bool checkValid(Food food)
    {
        FoodType foodType = food.wicType;
        switch (foodType)
        {
            case FoodType.Fruit:
                if (!fruitUsed)
                {
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Veg:
                if (!vegUsed)
                {
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Grain:
                if (!grainUsed)
                {
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Protein:
                if (!proteinUsed)
                {
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Dairy:
                if (!dairyUsed)
                {
                    return true;
                } else
                {
                    return false;
                }
            default:
                return false;
        }    
    }


    public bool voucherUsedUp()
    {
        return fruitUsed && vegUsed && grainUsed && proteinUsed && dairyUsed;
    }

    private void displayPermCheck(GameObject gameObject)
    {
        setColor(gameObject, Color.black);
        gameObject.SetActive(true);
    }

    private void displayTempCheck(GameObject gameObject)
    {
        setColor(gameObject, Color.white);
        gameObject.SetActive(true);
    }

    private void setColor(GameObject gameObject, Color color)
    {
        Image image = gameObject.GetComponent<Image>();
        image.color = color;
    }

}
