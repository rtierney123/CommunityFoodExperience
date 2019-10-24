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

    public PopulateGrid grid;
    public string jsonLocation;


    private FoodList foods { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            Debug.Log("webgl");
            jsonLocation = Path.Combine(Application.streamingAssetsPath, jsonLocation);
            StartCoroutine(GetRequest(jsonLocation));
        } else
        {
            jsonLocation = Application.dataPath + "/StreamingAssets" + jsonLocation;
            bool valid = ResourceHandler.testFilePath(jsonLocation);
            Debug.Log(valid);
            Debug.Log(jsonLocation);
            generateAssets(jsonLocation);
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
                string json = webRequest.downloadHandler.text;

                generateAssets(json);
            }
        }
    }

    public void generateAssets(string jsonText)
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
