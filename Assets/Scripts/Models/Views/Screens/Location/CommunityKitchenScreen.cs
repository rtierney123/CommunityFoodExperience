using Manage;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utility;

namespace UI
{
    public class CommunityKitchenScreen : BaseLocationScreen, IClockEventCaller
    {

        public GameManager gameManager;
        public int startTickets;
        public int startMeals;

        public int randomMealTakenMax;
        public int randomTicketTakenMax;
        public int lineWaitTimeMax;
        public Text numMealsText;
        public ClockDisplay clock;
        public Player player;
        public NutritionManager nutritionManager;
        public CurrencyManager currencyManager;
        public string jsonLocation;

        private Food soup;

        private int mealRemaining;
        private int ticketsRemaining;

        public override void reset()
        {
            base.reset();
            numMealsText.text = mealRemaining.ToString();
            ticketsRemaining = startTickets;
            mealRemaining = startMeals;
        }

        private void Start()
        {
            reset();

            numMealsText.text = mealRemaining.ToString();

            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                jsonLocation = Application.absoluteURL + "StreamingAssets" + jsonLocation;
                StartCoroutine(GetRequest(jsonLocation));
            }
            else
            {
                string mainPath = Application.dataPath;
                jsonLocation = mainPath + "/StreamingAssets" + jsonLocation;
                bool pathExists = ResourceHandler.testFilePath(jsonLocation);
                if (pathExists)
                {
                    string json = File.ReadAllText(jsonLocation);
                    generateFoodAsset(json);

                }
            }
        }

        public void askForTicket()
        {
            if (ticketsRemaining > 0)
            {
                if (player.busTickets == 0 && !player.playerInfo.busPass && !player.playerInfo.hasCar)
                {
                    currencyManager.addTokens(1);
                    updateTickets(1);
                    messageManager.generateStandardSuccessMessage(Status.receiveTicketCommunityKitchen, this);
                }
                else
                {
                    messageManager.generateStandardErrorMessage(Status.alreadyHaveTransportation, this);

                }
            }
        }

        IEnumerator GetRequest(string uri)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
            {
                // Request and wait for the desired page.
                yield return webRequest.SendWebRequest();

                string[] pages = uri.Split('/');
                int page = pages.Length - 1;

                if (webRequest.isNetworkError)
                {
                    Debug.Log(pages[page] + ": Error: " + webRequest.error);
                }
                else
                {
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    Debug.Log(webRequest.downloadHandler.text);
                    string json = webRequest.downloadHandler.text;

                    generateFoodAsset(json);
                }
            }
        }

        public void generateFoodAsset(string jsonText)
        {
            soup = JsonUtility.FromJson<Food>(jsonText);
        }

        public void hourPassed()
        {
            if (mealRemaining != 0)
            {
                float rand = Random.Range(0, randomMealTakenMax);
                int mealsEaten = (int)rand;
                updateMeals(mealsEaten);
            }

            if (ticketsRemaining != 0)
            {
                float rand = Random.Range(0, randomTicketTakenMax);
                int ticketsTaken = (int)rand;
                updateTickets(ticketsTaken);
            }

        }

        public void minutePassed()
        {
            // throw new System.NotImplementedException();
        }

        public void waitInLine()
        {
            if (!player.useCommunityKitchen)
            {
                if (mealRemaining > 0)
                {
                    updateMeals(1);
                    float rand = Random.Range(0, lineWaitTimeMax);
                    int lossTime = (int)rand;
                    clock.addGameMinutes(lossTime);
                    player.useCommunityKitchen = true;
                    nutritionManager.addNutrition(soup);
                    messageManager.generateStandardSuccessMessage(Status.receiveSoup, this);
                }
                else if (mealRemaining == 0)
                {
                    messageManager.generateStandardErrorMessage(Status.ranOutOfMeals, this);
                }
            }
            else
            {
                messageManager.generateStandardErrorMessage(Status.alreadyReceivedSoup, this);
            }

        }

        private void updateMeals(int mealsEaten)
        {
            mealRemaining -= mealsEaten;
            if (mealRemaining < 0)
            {
                mealRemaining = 0;
            }
            numMealsText.text = mealRemaining.ToString();
        }

        private void updateTickets(int ticketsTaken)
        {
            ticketsRemaining -= ticketsTaken;
            if (ticketsRemaining < 0)
            {
                ticketsRemaining = 0;
            }
        }

        public void hourBeforeEndGame()
        {

        }
    }

}
