
using System.ComponentModel;

[System.Serializable]
public enum FoodType
{
    [Description("None")]
    None = 0,
    [Description("grain")]
    Grain = 1,
    [Description("fat")]
    Fat = 2,
    [Description("protein")]
    Protein = 3,
    [Description("dairy")]
    Dairy = 4,
    [Description("fruit")]
    Fruit = 5,
    [Description("vegetable")]
    Veg = 6,
    [Description("extra")]
    Extra = 7


  
}

public static class EnumExtension
{
    public static string toDescriptionString(this FoodType val)
    {
        DescriptionAttribute[] attributes = (DescriptionAttribute[])val
           .GetType()
           .GetField(val.ToString())
           .GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attributes.Length > 0 ? attributes[0].Description : string.Empty;
    }
}
