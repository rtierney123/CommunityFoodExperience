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
	bool postBusPanning,panRight;
	Vector3 PrevPosition;
	Vector3 PrevRotation;
	int currentSmoothPanIndex;
	const int smoothDepth = 100;

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
			leftButton.SetActive (this.transform.position.x >= .1f && !allowFollowBus);
		}
		if (rightButton != null) {
			rightButton.SetActive (this.transform.position.x <= 9.6f && !allowFollowBus);
		}
		if (postBusPanning) {
			postBusPanning = false;
			var DesiredPosition = panRight ? new Vector3 (9.7f, 10, -10) : new Vector3 (0, 10, -10);
			var DesiredRotation = new Vector3 (45, 0, 0);
			var x = this.transform.position.x - DesiredPosition.x;
			var y = this.transform.position.y - DesiredPosition.y;
			var z = this.transform.position.z - DesiredPosition.z;
			if (x > .1f) {
				x = .1f;
				postBusPanning = true;
			} else if (x < -.1f) {
				x = -.1f;
				postBusPanning = true;
			}
			if (y > .1f) {
				y = .1f;
				postBusPanning = true;
			} else if (y < -.1f) {
				y = -.1f;
				postBusPanning = true;
			}
			if (z > .1f) {
				z = .1f;
				postBusPanning = true;
			} else if (z < -.1f) {
				z = -.1f;
				postBusPanning = true;
			}
			var rx = this.transform.eulerAngles.x - DesiredRotation.x;
			var ry = this.transform.eulerAngles.y - DesiredRotation.y;
			var rz = this.transform.eulerAngles.z - DesiredRotation.z;
			if (rx > 1) {
				rx = 1;
				postBusPanning = true;
			} else if (rx < -1) {
				rx = -1;
				postBusPanning = true;
			}
			if (ry > 1) {
				ry = 1;
				postBusPanning = true;
			} else if (ry < -1) {
				ry = -1;
				postBusPanning = true;
			}
			if (rz > 1) {
				rz = 1;
				postBusPanning = true;
			} else if (rz < -1) {
				rz = -1;
				postBusPanning = true;
			}
			var v = this.transform.position;
			var b = this.transform.eulerAngles;
			this.transform.position = new Vector3(v.x - x, v.y - y, v.z - z);
			this.transform.eulerAngles = new Vector3 (b.x - rx, b.y - ry, b.z - rz);
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
	private void Lerp(Vector3 a, Vector3 b, Vector3 r, Vector3 z, int repeats, int curr) {
		if (curr > repeats)
			return;
		var a2 = new Vector3 (a.x, a.y, a.z);
		var b2 = new Vector3 (b.x, b.y, b.z);
		var r2 = new Vector3 (r.x, r.y, r.z);
		var z2 = new Vector3 (z.x, z.y, z.z);
		float ratio = (float)curr / repeats;
		var inverse = 1.0f / ratio;
		a2.x *= ratio;
		a2.y *= ratio;
		a2.z *= ratio;
		b2.x *= inverse;
		b2.y *= inverse;
		b2.z *= inverse;
		r2.x *= ratio;
		r2.y *= ratio;
		r2.z *= ratio;
		z2.x *= inverse;
		z2.y *= inverse;
		z2.z *= inverse;
		this.transform.position = new Vector3 (a2.x + b2.x, a2.y + b2.y, a2.z + b2.z);
		this.transform.eulerAngles = new Vector3 (r2.x + z2.x, r2.y + z2.y, r2.z + z2.z);
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
