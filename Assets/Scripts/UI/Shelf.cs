using Manage;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Shelf : MonoBehaviour
{

    public PopulateGrid grid;
    public string jsonLocation;


    private FoodList foods { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        string mainPath =  Application.dataPath;
        jsonLocation = mainPath + jsonLocation;
        Debug.Log(jsonLocation);
        if (System.IO.File.Exists(jsonLocation))
        {
            Debug.Log("file exists");
        }
        else
        {
            Debug.Log("file does not exist");
        }
        string json = File.ReadAllText(jsonLocation);
        foods = JsonUtility.FromJson<FoodList>(json);
        Debug.Log(foods.list[0].name);

       List<FoodCard> cards = new List<FoodCard>();

       foreach (Food food in foods.list)
       {
            grid.addItemToShelf(food);
        }


    }

    [Serializable]
    public class FoodList
    {
        public Food[] list;
    }
}
