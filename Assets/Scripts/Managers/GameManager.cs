using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage{
    //does logic for determining when the endgame is and when the player has satified all goals
    public class GameManager : MonoBehaviour
    {

        public int timeRemaining;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void subtractTime(int time)
        {
            timeRemaining -= time;
        }
    }
}

