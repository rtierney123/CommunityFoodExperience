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
            food1.wicType = FoodType.Fruit;
            string json = JsonUtility.ToJson(food1);
            //Debug.Log(json);
            
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

