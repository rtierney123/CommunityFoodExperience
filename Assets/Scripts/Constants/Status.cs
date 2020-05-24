using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Status
{
    //COMMUNITY KITCHEN
    public static string receiveSoup = "'Here is vegetable soup with bread. It provides 1 grain and 1 vegetable towards your daily nutrition recommendations.'";
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
    public static string snapOnPremade = "Cannot use SNAP benefits on premade food.";
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
    public static string justEitc = "Your application has been approved for EITC. " +
        "For a household of {0} and monthly income of {1}, you have received {2} in EITC benefits that can be used to purchase food or bus tokens. " +
        "However, you do not have any children and do not qualify for CTC benefits.";
    public static string neitherEitcCTC = "Your application has been denied. You do not qualify for EITC. " +
        "Income must be wages from a job, so income from social security and unemployment is ineligible. " +
        "You do not have any children and do not qualify for CTC benefits.";

    //Food Pantry
    public static string enterFoodPantry = "This food pantry can only serve individuals living in the 30317 or 30307 zip code due to our limited supply of food. " +
        "Because you live in one of those zip codes you are welcome to the Food Pantry.";
    public static string deniedFoodPantry = "Due to high demand and not enough food, we are currently only serving individuals " +
        "living in the 30317 or 30307 zip code. We cannot provide you food from our food pantry today.";
    public static string leaveFoodPantry = "Thank you for visiting the Food Pantry.";
    public static string reenterFoodPantryAfterReceived = "You have already received food from the food pantry today. Due to the high number of" +
        " people seeking food and limited food, we can only help you one time today.";

    //WIC Clinic
    public static string wicApproved = "For a household of {0} that has either a pregnant woman or children under the age of 5 and monthly income of {1}, " +
        "you have received a WIC voucher. The voucher can be used during one transaction at participating grocery stores for WIC approved foods." +
        " You may find your voucher in your wallet.";
    public static string wicDenied = "Your application has been denied because you are not a pregnant woman and/or you don’t have at least one child under the age of 5 in your household.";

    //Random Event
    public static string freeRideReceived = "A kind stranger has offered to give you a ride to your next location. " +
        "Choose wisely as this will only get you to one place. Redeem by clicking the next location you want to go.";

    //End Game
    public static string gettingLate = "You better get home soon.  It is getting late.";
    public static string notHomeInsufficientFoodEnd = "Unfortunately, the day has ended and you did not make it back home. " +
        "Everything is closed and you did not get enough food for yourself today.";
    public static string notHomeSufficientFoodEnd = "Unfortunately, the day has ended and you did not make it back home. " +
        "Everything is closed, but you were able to get enough food for yourself today.";
    public static string homeInsufficientFoodEnd = "Unfortunately, everything is closed and you did not get enough food for yourself today. " +
        "However, you made it back home.";
    public static string homeSufficientFoodEnd = "Great work! " +
        "You successfully made it through the day and came back home with all the nutritious food you need for today.";

    //General Status
    public static string achievedNutrition = "Great work! You have successfully obtained all the nutritious food you need for today. " +
        "Feel free to head home whenever you want.";
    public static string homeInsufficientFood = "Welcome home! There is still time in the day and you have not obtained all the nutritious food you need. " +
        "Would you like to head back out into the community?";
    public static string homeSufficientFood = "Welcome home! Looks like you have obtained all the nutritious food you need today. " +
        "There is still time in the day. Would you like to head back out into the community?";

    //Bus
    public static string purchaseSingleTicket = "Payment of {0} bus ticket successful.";
    public static string purchaseMultipleTickets = "Payment of {0} bus tickets successful.";

    //Hints
    public static string clickStopInstruction = "Click on a bus stop to have the bus drop you off where you want to go.";

}
