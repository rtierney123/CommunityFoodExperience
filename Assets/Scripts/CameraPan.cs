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
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D) || Input.mousePosition.x > Screen.width * 0.9f) {
			MoveLeft();
		} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A) || Input.mousePosition.x < Screen.width * .1f) {
			MoveRight();
		}
    }

	void MoveRight() {
		if (transform.position.x >= 0.1) {
			var temp = transform.position;
			temp.x -= .1f;
			transform.position = temp;
		}
	}
	void MoveLeft() {
		if (transform.position.x <= 9.6) {
			var temp = this.transform.position;
			temp.x += .1f;
			this.transform.position = temp;
		}
	}
}
