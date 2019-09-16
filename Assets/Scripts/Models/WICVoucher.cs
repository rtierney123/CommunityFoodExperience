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
        switch (foodType)
        {
            case FoodType.Fruit:
                fruitUsed = true;
                break;
            case FoodType.Veg:
                vegUsed = true;
                break;
            case FoodType.Grain:
                grainUsed = true;
                break;
            case FoodType.Protein:
                proteinUsed = true;
                break;
            case FoodType.Dairy:
                dairyUsed = true;
                break;
        }    
    }

    public bool voucherUsedUp()
    {
        return fruitUsed && vegUsed && grainUsed && proteinUsed && dairyUsed;
    }
}
