using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WICVoucher
{
    public bool fruitUsed = false;
    public bool vegUsed = false;
    public bool grainUsed = false;
    public bool proteinUsed = false;
    public bool dairyUsed = false;

    public bool active;

    public WICVoucher()
    {
        fruitUsed = false;
        vegUsed = false;
        grainUsed = false;
        proteinUsed = false;
        dairyUsed = false;

        active = true;
    }

    public void deactivate()
    {
        active = false;
    }

    public void useVoucher(FoodType foodType)
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

    /*
    public bool checkValid(Food food)
    {
        FoodType foodType = food.wicType;
        switch (foodType)
        {
            case FoodType.Fruit:
                return !fruitUsed;
            case FoodType.Veg:
                return !vegUsed;
            case FoodType.Grain:
                return !grainUsed;
            case FoodType.Protein:
                return !proteinUsed;
            case FoodType.Dairy:
                return !dairyUsed;
            default:
                return false;
        }
    }
    */

    public bool voucherUsedUp()
    {
        return fruitUsed && vegUsed && grainUsed && proteinUsed && dairyUsed;
    }

}
