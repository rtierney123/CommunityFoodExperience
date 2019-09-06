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
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
			MoveLeft();
			ConstantMoving = false;
		} else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
			MoveRight();
			ConstantMoving = false;
		}
		MoveFar();
    }
	bool ConstantMoving;
	bool RightLeft;
	public void SetRight() {
		ConstantMoving = RightLeft = true;
	}
	public void SetLeft() {
		ConstantMoving = true;
		RightLeft = false;
	}
	void MoveRight() {
		if (transform.position.x >= 0.1f) {
			var temp = transform.position;
			temp.x -= .1f;
			transform.position = temp;
		}
	}
	void MoveLeft() {
		if (transform.position.x <= 9.6f) {
			var temp = this.transform.position;
			temp.x += .1f;
			this.transform.position = temp;
		}
	}
	void MoveFar() {
		if (ConstantMoving) {
			if (!RightLeft) {
				MoveRight ();
				if (this.transform.position.x > 9.6f) {
					ConstantMoving = false;
				}
			} else {
				MoveLeft ();
				if (this.transform.position.x < 0.1f) {
					ConstantMoving = false;
				}
			}
		}
	}
}
