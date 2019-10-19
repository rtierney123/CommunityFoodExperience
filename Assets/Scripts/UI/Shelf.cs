using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
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
        string mainPath = Application.dataPath;
        jsonLocation = mainPath + jsonLocation;
        bool pathExists = ResourceHandler.testFilePath(jsonLocation);
        if (pathExists)
        {
            string json = File.ReadAllText(jsonLocation);
            foods = JsonUtility.FromJson<FoodList>(json);
            

            foreach (Food food in foods.list)
            {
                grid.addItem(food);
            }

        }


    }


    [Serializable]
    public class FoodList
    {
        public Food[] list;
    }
}
