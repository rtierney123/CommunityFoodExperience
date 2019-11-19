using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Utility;

public class ChoosePlayerScreen : Screen
{
    public string playerJsonLocation;
    public Dropdown playerDropdown;
    public Player player;
    private PlayerInfo playerInfo;
    [HideInInspector]
    public PlayerList playerChoices;
    [Serializable]
    public class PlayerList
    {
        public PlayerInfo[] list;
    }

    private void Start()
    {
        populatePlayerList();
    
    }

    public void populatePlayerList()
    {
        string jsonLocation;
        if (Application.platform == RuntimePlatform.WebGLPlayer)
        {
            jsonLocation = "/StreamingAssets" + playerJsonLocation;
            StartCoroutine(GetRequest(jsonLocation));
        }
        else
        {
            string mainPath = Application.dataPath;
            jsonLocation = mainPath + "/StreamingAssets" + playerJsonLocation;
            bool pathExists = ResourceHandler.testFilePath(jsonLocation);
            if (pathExists)
            {
                string json = File.ReadAllText(jsonLocation);
                generatePlayerAssets(json);

            }
            fillDropDown();
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

                generatePlayerAssets(json);
                fillDropDown();
            }
        }
    }

    public void fillDropDown()
    {
        List<String> names = retrievePlayerNames();
        playerDropdown.AddOptions(names);
        setPlayerFromDropDown();
    }

    public void generatePlayerAssets(string json)
    {
        playerChoices = JsonUtility.FromJson<PlayerList>(json);
    }

    public List<String> retrievePlayerNames()
    {
        PlayerInfo[] list = playerChoices.list;
        List<string> names = new List<string>();

        foreach (PlayerInfo info in list)
        {
            if(player != null)
            {
                names.Add(info.getFullName());
            } else {
                Debug.Log("Player is null. Problem with json");
            }
       
        }

        return names;
    }

    public void setPlayerFromDropDown()
    {
        player.resetPlayer();
        int index = playerDropdown.value;
        player.setPlayerInfo( playerChoices.list[index]);
    }
}
