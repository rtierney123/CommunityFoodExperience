using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPan : MonoBehaviour
{

    // The target we are following
    public Transform target;
    // The distance in the x-z plane to the target
    public float distance = 10.0f;
    // the height we want the camera to be above the target
    public float height = 5.0f;
    // How much we 
    public float heightDamping = 2.0f;
    public float rotationDamping = 3.0f;


    private bool allowFollowBus = false; 
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

    // Place the script in the Camera-Control group in the component menu
    [AddComponentMenu("Camera-Control/Smooth Follow")]

    void LateUpdate()
    {
        if (allowFollowBus)
        {

            // Set the height of the camera
            transform.position = new Vector3(target.position.x, this.transform.position.y, this.transform.position.z);

            // Always look at the target
            transform.LookAt(target);
        }
      
    }

    public void startFollowingBus()
    {
        allowFollowBus = true;
    }

    public void stopFollowingBus()
    {
        allowFollowBus = false;
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
    public void JumpLeft()
    {
        this.transform.position = new Vector3(0, 10, -10);
        this.transform.eulerAngles = new Vector3(45, 0, 0);
    }

    public void JumpRight()
    {
        this.transform.position = new Vector3(10, 10, -10);
        this.transform.eulerAngles = new Vector3(45, 0, 0);
    }
	void MoveRight() {
		if (transform.position.x > 0f) {
			var temp = transform.position;
			temp.x -= .1f;
			transform.position = temp;
        }
        else
        {
            JumpLeft();
        }
	}
	void MoveLeft() {
		if (transform.position.x < 10f) {
			var temp = this.transform.position;
			temp.x += .1f;
			this.transform.position = temp;
		} else
        {
            JumpRight();
        }
	}
	void MoveFar() {
		if (ConstantMoving) {
			if (!RightLeft) {
				MoveRight ();
				if (this.transform.position.x > 10f) {
					ConstantMoving = false;
				}
			} else {
				MoveLeft ();
				if (this.transform.position.x < 0f) {
					ConstantMoving = false;
				}
			}
		}
	}
}
