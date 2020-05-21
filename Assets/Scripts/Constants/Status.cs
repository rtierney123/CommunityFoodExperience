using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public static class Status
{
    //COMMUNITY KITCHEN
    public static string receiveSoup = "'Here is one hot meal. It provides 1 grain and 1 vegetable towards your daily nutrition recommendations.'";
    public static string alreadyReceivedSoup = "'You have already received a hot meal today. Due to the high number of people " +
        "seeking food and the community kitchen receives limited donations, we can only serve one meal per person.'";
    public static string alreadyHaveTransportation = "'It looks like you already have some mode of transportation. We have a limited amount of bus tickets, " +
        "so we must reserve them for someone without any transportation.'";
    public static string receiveTicketCommunityKitchen = "'You look like you could you use a bus ticket. Here you go.'";
    public static string ranOutOfMeals = "'Sorry, we ran out of meals to give out today.'";

    //STORE
    public static string repeatedWIC = "Cannot use voucher on more than one {0}. " +
        "WIC vouchers are redeemable for one WIC food item in each category of fruits, vegetables, grains, protein, and dairy. ";
    public static string wicRedeemed = "WIC voucher has been redeemed.";
    public static string purchaseCompleted = "Purchase completed.";
    public static string snapOnPremade = "Cannot use SNAP funds on premade food.";
    public static string insufficientCash = "Not enough cash.";
    public static string insufficientSnap = "Not enough SNAP fund.";
    public static string totalMismatch = "Total amount does not match.";
}
