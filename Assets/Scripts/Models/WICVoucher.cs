using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WICVoucher : MonoBehaviour
{
    public bool fruitUsed = false;
    public bool vegUsed = false;
    public bool grainUsed = false;
    public bool proteinUsed = false;
    public bool dairyUsed = false;

    public void useVoucher(FoodType foodType)
    {
        if (checkValid(foodType))
        {
            switch (foodType)
            {
                case FoodType.Fruit:
                    fruitUsed = true;
                    return;
                case FoodType.Veg:
                    vegUsed = true;
                    return;
                case FoodType.Grain:
                    grainUsed = true;
                    return;
                case FoodType.Protein:
                    proteinUsed = true;
                    return;
                case FoodType.Dairy:
                    dairyUsed = true;
                    return;
            }
        }
  
    }
    public bool checkValid(FoodType foodType)
    {
        switch (foodType)
        {
            case FoodType.Fruit:
                if (!fruitUsed)
                {
                    fruitUsed = true;
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Veg:
                if (!vegUsed)
                {
                    vegUsed = true;
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Grain:
                if (!grainUsed)
                {
                    grainUsed = true;
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Protein:
                if (!proteinUsed)
                {
                    proteinUsed = true;
                    return true;
                } else
                {
                    return false;
                }
            case FoodType.Dairy:
                if (!dairyUsed)
                {
                    dairyUsed = true;
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
}
