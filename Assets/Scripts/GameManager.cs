using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Movement
{
    public class GameManager : MonoBehaviour
    {
        public Transform player;
        public Transform bus;
        public Transform playerPath;
        public Transform busPath;
        public int speed;

        private bool busComplete;
        // Start is called before the first frame update
        void Start()
        {
            
            busComplete = false;
            StartCoroutine(followPath(bus, busPath));
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        public void travelPath()
        {
            StartCoroutine(followPath(player, playerPath));
        }

        IEnumerator takeBus()
        {
            followPath(bus, busPath);
            yield return new WaitForSeconds(1);
        }

        IEnumerator followPath(Transform obj, Transform path)
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();

            foreach (Transform child in path)
            {
                
                Vector2 start = obj.position;
                Vector2 end = child.position;
                PathObj script = child.GetComponent<PathObj>();
           
                float step = speed * Time.deltaTime;
                script.setHit(false);
            
                rb.velocity = step * (end - start);
                //Debug.Log("Waiting for princess to be rescued...");
                yield return new WaitUntil(() => script.getHit());
                //Debug.Log("Princess was rescued!");
                rb.velocity = Vector2.zero;

            }
            
        }
    }

}
