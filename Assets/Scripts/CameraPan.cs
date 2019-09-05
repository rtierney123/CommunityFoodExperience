using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		if (Input.GetKey(KeyCode.RightArrow)) {
			if (transform.position.x <= 9.6) {
				var temp = transform.position;
				transform.position.Set(temp.x + .1f, temp.y, temp.z);
			}
		} else if (Input.GetKey(KeyCode.LeftArrow)) {
			if (transform.position.x >= 0.1) {
				var temp = transform.position;
				transform.position.Set(temp.x + .1f, temp.y, temp.z);
			}
		}
    }
}
