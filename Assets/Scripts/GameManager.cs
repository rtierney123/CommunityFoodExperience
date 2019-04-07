using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Movement
{
    public class GameManager : MonoBehaviour
    {
        public Transform player;
        private Rigidbody playerRb;
        public Transform path;
       
        public int speed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            playerRb = player.GetComponent<Rigidbody>();
        }

        public void travelPath()
        {
            StartCoroutine(movePlayer());
        }

        IEnumerator movePlayer()
        {
            
            foreach (Transform child in path)
            {
                
                Vector2 start = player.position;
                Vector2 end = child.position;
                PathObj script = child.GetComponent<PathObj>();
           
                float step = speed * Time.deltaTime;
                playerRb.velocity = step * (end - start);
                script.setHit(false);
                Debug.Log("Waiting for princess to be rescued...");
                yield return new WaitUntil(() => script.getHit());
                Debug.Log("Princess was rescued!");
                playerRb.velocity = Vector2.zero;

            }
            
        }
    }

}
