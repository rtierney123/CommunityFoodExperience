
using UnityEngine;

namespace UI
{
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
		bool panCamera, panRight;
		Vector3 PrevPosition;
		Vector3 PrevRotation;
		int currentSmoothPanIndex;
		const int smoothDepth = 100;

		Vector3 desiredPosition;
		float desiredScale;


		private bool allowFollowBus = false;
		// Start is called before the first frame update
		void Start()
		{
			currentSmoothPanIndex = 0;
		}
		// Update is called once per frame
		void Update()
		{
			if (panCamera)
			{
				Debug.Log("pan camera");
				panCamera = false;

				Camera camera = this.GetComponent<Camera>();
				//var DesiredRotation = new Vector3 (45, 0, 0);
				var x = this.transform.position.x - desiredPosition.x;
				var y = this.transform.position.y - desiredPosition.y;
				var z = this.transform.position.z - desiredPosition.z;
				float scale = camera.orthographicSize - desiredScale;
				if (x > .1f)
				{
					x = .1f;
					panCamera = true;
				}
				else if (x < -.1f)
				{
					x = -.1f;
					panCamera = true;
				}
				if (y > .1f)
				{
					y = .1f;
					panCamera = true;
				}
				else if (y < -.1f)
				{
					y = -.1f;
					panCamera = true;
				}
				if (z > .1f)
				{
					z = .1f;
					panCamera = true;
				}
				else if (z < -.1f)
				{
					z = -.1f;
					panCamera = true;
				}
				if (scale > .1f)
				{
					scale = .1f;
					panCamera = true;
				}
				else if (scale < -.1f)
				{
					scale = -.1f;
					panCamera = true;
				}
				/*
				var rx = this.transform.eulerAngles.x - DesiredRotation.x;
				var ry = this.transform.eulerAngles.y - DesiredRotation.y;
				var rz = this.transform.eulerAngles.z - DesiredRotation.z;
				if (rx > 1) {
					rx = 1;
					panCamera = true;
				} else if (rx < -1) {
					rx = -1;
					panCamera = true;
				}
				if (ry > 1) {
					ry = 1;
					panCamera = true;
				} else if (ry < -1) {
					ry = -1;
					panCamera = true;
				}
				if (rz > 1) {
					rz = 1;
					panCamera = true;
				} else if (rz < -1) {
					rz = -1;
					panCamera = true;
				}
				*/
				var v = this.transform.position;
				var b = this.transform.eulerAngles;
				this.transform.position = new Vector3(v.x - x, v.y - y, v.z - z);
				camera.orthographicSize = camera.orthographicSize - scale;
				//this.transform.eulerAngles = new Vector3 (b.x - rx, b.y - ry, b.z - rz);
			}
		}

		public void setDesiredPosition(Transform gameObject)
		{
			desiredPosition = gameObject.position;
		}

		public void setCameraScale(float scale)
		{
			desiredScale = scale;
		}

		public void startPan()
		{
			panCamera = true;
			Debug.Log("start pan");
		}

		/*
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
			panCamera = true;
			panRight = false;
		}

		public void JumpRight()
		{
			PrevPosition = this.transform.position;
			PrevRotation = this.transform.eulerAngles;
			currentSmoothPanIndex = 0;
			panCamera = panRight = true;
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
		*/
	}

}
