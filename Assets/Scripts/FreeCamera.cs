using UnityEngine;
using System.Collections;

public class FreeCamera : MonoBehaviour {
	
	public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
	public RotationAxes axes = RotationAxes.MouseXAndY;
	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	float rotationY = 0F;
	
	private bool locked;

	// Use this for initialization
	void Start () {
		this.LockLook();
	}
	
	public void LockLook() {
		Screen.lockCursor = true;	
		this.locked = true;
	}
	
	public void UnlockLook() {
		Screen.lockCursor = false;
		this.locked = false;
	}
	
	public void ToggleLook() {
		this.locked = !this.locked;
		Screen.lockCursor = this.locked;	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (locked) {
			
			float speed = (Input.GetKey(KeyCode.LeftShift)) ? 30 : 10;
			speed *= Time.deltaTime;
			
			// keyboard input
			if (Input.GetKey(KeyCode.W)) {
				transform.position += transform.forward.normalized * speed;	
			}
			if (Input.GetKey(KeyCode.A)) {
				transform.position += -transform.right.normalized * speed;	
			}
			if (Input.GetKey(KeyCode.S)) {
				transform.position += -transform.forward.normalized * speed;	
			}
			if (Input.GetKey(KeyCode.D)) {
				transform.position += transform.right.normalized * speed;	
			}
			
			if (axes == RotationAxes.MouseXAndY)
			{
				float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
				
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
			}
			else if (axes == RotationAxes.MouseX)
			{
				transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
			}
			else
			{
				rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
				rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
				
				transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
			}
		}
	}
}
