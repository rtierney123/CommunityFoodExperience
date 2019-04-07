using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Movement
{
    public class PathObj : MonoBehaviour
    {
        private bool playerHit;
        // Start is called before the first frame update
        void Start()
        {
            playerHit = false;
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" || coll.tag=="Bus")
            {
                playerHit = true;
            }
           
        }

        public void setHit(bool isHit)
        {
            playerHit = isHit;
        }
        
        public bool getHit()
        {
            return playerHit;
        }
    }
}

