using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using static Food;

namespace Manage{
    //does logic for determining when the endgame is and when the player has satified all goals
    public class GameManager : MonoBehaviour
    {

        //public int timeRemaining;

        // Start is called before the first frame update
        void Start()
        {
            Food food1 = new Food();
            food1.calories = 1000;

            Food food2 = new Food();
          
            //FoodList foodList = new FoodList();
            //foodList.list.Add(food1);
            //foodList.list.Add( food2);
            List<Food> list = new List<Food>();
            list.Add(food1);
            list.Add(food2);
            string json = JsonUtility.ToJson(list);
            Debug.Log(json);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void subtractTime(int time)
        {

        }
    }
}

