using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
	public GameObject leftButton, rightButton;

    private bool allowFollowBus = false; 
    // Start is called before the first frame update
    void Start()
    {
		currentSmoothPanIndex = 0;
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
		if (leftButton != null) {
			leftButton.SetActive (this.transform.position.x >= .1f);
		}
		if (rightButton != null) {
			rightButton.SetActive (this.transform.position.x <= 9.6f);
		}
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
	public void SetRight(GameObject g) {
		ConstantMoving = RightLeft = true;
		if (rightButton == null)
			rightButton = g;
		if (leftButton != null)
			leftButton.SetActive (true);
	}
	public void SetLeft(GameObject g) {
		ConstantMoving = true;
		RightLeft = false;
		if (leftButton == null)
			leftButton = g;
		if (rightButton != null)
			rightButton.SetActive (true);
	}
    public void JumpLeft()
    {
		PrevPosition = this.transform.position;
		PrevRotation = this.transform.eulerAngles;
		currentSmoothPanIndex = 0;
		postBusPanning = true;
		panRight = false;
    }

    public void JumpRight()
    {
		PrevPosition = this.transform.position;
		PrevRotation = this.transform.eulerAngles;
		currentSmoothPanIndex = 0;
		postBusPanning = panRight = true;
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
