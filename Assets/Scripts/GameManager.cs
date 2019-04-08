using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Manage
{
    public class GameManager : MonoBehaviour
    {
        public Transform player;
        public Transform bus;
        public Transform playerPath;
        public Transform busPath;
        public Bus busScript;
        public Player playerScript;
        public int speed;
        private bool routeComplete;
        private bool onBus;
        // Start is called before the first frame update
        void Start()
        {

            routeComplete = true;
            onBus = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (routeComplete)
            {
                StartCoroutine(followPath(bus, busPath));
                routeComplete = false;
            } 

            if (busScript.isMoving)
            {

            } 

            if (playerScript.isMoving)
            {

            }
            
        }

        public void travelPath()
        {
            StartCoroutine(followPath(player, playerPath));
        }
        
        public void takeBus()
        {
            if(busScript.getStopBus() && !onBus)
            {
                player.GetComponent<Image>().enabled = false ;
                onBus = true;
            }
            else
            {
                player.GetComponent<Image>().enabled = true ;
                player.position = bus.position;
                onBus = false;
            }
           
        }

        IEnumerator followPath(Transform obj, Transform path)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            foreach (Transform child in path)
            {
                PathObj script = child.GetComponent<PathObj>();
                script.setHit(false);

                /*
                Vector2 start = obj.position;
                Vector2 end = child.position;


                float step = speed * Time.deltaTime;


                rb.velocity = step * (end - start);
                obj.rotation = child.rotation;
                */
                StartCoroutine(goToPathObj(rb, obj, child, script));
                if (obj.tag == "Bus" && busScript.getStopBus())
                {
                    rb.velocity = Vector2.zero;
                    yield return new WaitForSeconds(1);
                    script.setHit(true);
                    busScript.setStopBus(false);
                }
                //Corroutine---
               

                //Debug.Log("Waiting for princess to be rescued...");
                yield return new WaitUntil(() => script.getHit());
                //Debug.Log("Princess was rescued!");


            }

            if (obj.tag == "Bus")
            {
                routeComplete = true;
            }
          
        }
        
        IEnumerator goToPathObj(Rigidbody rb, Transform obj, Transform pathObj, PathObj script)
        {
            Vector2 start = obj.position;
            Vector2 end = pathObj.position;

            Vector3 startPosition = obj.position;
            float step = speed * Time.deltaTime;
            while (!script.getHit())
            {
                //rb.velocity = step * (end - start);
                obj.position = Vector3.Lerp(obj.position, pathObj.position, speed*Time.deltaTime);
                obj.rotation = pathObj.rotation;
                yield return new WaitForSeconds(.1f);
            }

          
        }
        
    }

}
