using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Manage
{
    public class PathObj : MonoBehaviour
    {
        private bool pieceHit;
        // Start is called before the first frame update
        void Start()
        {
            pieceHit = false;
        }

        // Update is called once per frame
        void Update()
        {

        }


        private void OnTriggerEnter(Collider coll)
        {
            if (coll.tag == "Player" || coll.tag=="Bus")
            {
                pieceHit = true;
            }
           
        }

        public void setHit(bool isHit)
        {
            pieceHit = isHit;
        }
        
        public bool getHit()
        {
            return pieceHit;
        }
    }
}

