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

    //SNAP
    public static string snapApproved = "Your application has been approved. For a household of {0} and monthly income of {1}, " +
        "you have received {2} in SNAP benefits that can be used to purchase any food that is not prepared and ready to eat.";
    public static string snapDenied = "Your application has been denied. For a household of {0} and monthly income of {1}, you make too much money to receive SNAP benefits.";

    //FORM
    public static string enterAgain = "You have already visited the {0} today.";

    //VITA
    public static string bothEitcCTC = "Your application has been approved. " +
        "For a household of {0} and monthly income of {1}, you have received {2} in EITC benefits that can be used to purchase food or bus tickets. " +
        "You have also received $2.74 in CTC benefits and can be used to purchase food.";
    public static string justCtc = "Your application has been approved for CTC. " +
        "For a household of {0} and monthly income of {1}, you have received $2.74 in CTC benefits and can be used to purchase food. " +
        "However, you do not qualify for EITC. Income must be wages from a job, so income from social security and unemployment is ineligible.";
    public static string justEitc = "Your application has been approved for EITC." +
        "For a household of {0} and monthly income of {1}, you have received {2} in EITC benefits that can be used to purchase food or bus tokens. " +
        "However, you do not have any children and do not qualify for CTC benefits.";
    public static string neitherEitcCTC = "Your application has been denied. You do not qualify for EITC. " +
        "Income must be wages from a job, so income from social security and unemployment is ineligible. " +
        "You do not have any children and do not qualify for CTC benefits.";
}
