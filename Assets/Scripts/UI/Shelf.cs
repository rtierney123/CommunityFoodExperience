using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utility;

public class Shelf : MonoBehaviour
{

    public PopulateShelf grid;
    public string jsonLocation;
    public Image foodImage_c;
    public GameObject detailPopup;

    private FoodList foods { get; set; }

    // Start is called before the first frame update
    void Start()
    {

        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            jsonLocation = "/StreamingAssets" + jsonLocation;
            Debug.Log(jsonLocation);
            StartCoroutine(GetRequest(jsonLocation));
        }
        else
        {
            string mainPath = Application.dataPath;
            jsonLocation = mainPath + "/StreamingAssets" + jsonLocation;
            Debug.Log(jsonLocation);
            bool pathExists = ResourceHandler.testFilePath(jsonLocation);
            if (pathExists)
            {
                string json = File.ReadAllText(jsonLocation);
                generateFoodAssets(json);

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

                generateFoodAssets(json);
            }
        }
    }

    public void generateFoodAssets(string jsonText)
    {
        foods = JsonUtility.FromJson<FoodList>(jsonText);


        foreach (Food food in foods.list)
        {
            grid.addItem(food);
        }
    }

    [Serializable]
    public class FoodList
    {
        public Food[] list;
    }
}
